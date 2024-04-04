using System.Collections;
using UnityEngine;
using TMPro;

public class TextArchitect
{
    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

    public string currentText => tmpro_ui.text;
    // targetText: text needs to be shwon
    public string targetText { get; private set; } = "";
    // preText: text already shown
    public string preText { get; private set; } = "";
    private int preTextLength = 0;

    public string fullTargetText => preText + targetText;
    // how to show text  
    public enum BuildMethod { instant, typewriter, fade }
    public BuildMethod buildMethod = BuildMethod.fade;
    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; }}

    public float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    private const float baseSpeed = 0.5f;
    private float speedMultiplier = 1;

    public int charactersPerCycle { get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 1 : characterMultiplier * 2; } }
    private int characterMultiplier = 1;
    public bool hurryUp = false;

    public TextArchitect(TextMeshProUGUI tmpro_ui){
        this.tmpro_ui = tmpro_ui;
    }

    public TextArchitect(TextMeshPro tmpro_world){
        this.tmpro_world = tmpro_world;
    }

    public Coroutine Build(string text){
        preText = "";
        targetText = text;

        Stop();
        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    public Coroutine Append(string text){
        preText = tmpro.text;
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;

    public void Stop(){
        if (!isBuilding)
            return;

        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }

    IEnumerator Building(){
        Prepare();

        switch(buildMethod){
            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }

        OnComplete();
    }


    private void OnComplete(){
        buildProcess = null;
        hurryUp = false;
    }

    public void ForceComplete(){
        switch(buildMethod){
            case BuildMethod.typewriter:
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case BuildMethod.fade:
                tmpro.ForceMeshUpdate();
                break;
        }
        
        Stop();
        OnComplete();
    }

    private void Prepare(){
        switch(buildMethod){
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }

    private void Prepare_Instant(){
        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        // update any change we made
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }

    private void Prepare_Typewriter(){
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if (preText != ""){
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }

    private void Prepare_Fade(){
        tmpro.text = preText;
        if (preText != "")
        {
            tmpro.ForceMeshUpdate();
            preTextLength = preText.Length;
        } else {
            preTextLength = 0;
        }

        tmpro.text += targetText;
        tmpro.maxVisibleCharacters = int.MaxValue;
        tmpro.ForceMeshUpdate();

        TMP_TextInfo textInfo = tmpro.textInfo;

        Color colorVisible = new Color(textColor.r, textColor.g, textColor.b, 1);
        Color colorHidden = new Color(textColor.r, textColor.g, textColor.b, 0);

        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

        for(int i = 0; i < textInfo.characterCount; i++){
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible){
                continue;
            }

            if (i < preTextLength){
                for (int v = 0; v < 4; v++){
                    vertexColors[charInfo.vertexIndex + v] = colorVisible;
                }
            } else {
                for (int v = 0; v < 4; v++){
                    vertexColors[charInfo.vertexIndex + v] = colorHidden;
                }
            }
            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }
    }


    private IEnumerator Build_Typewriter(){
        while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount){
            tmpro.maxVisibleCharacters += hurryUp ? 5 * charactersPerCycle : charactersPerCycle;
            tmpro.maxVisibleCharacters = tmpro.maxVisibleCharacters > tmpro.textInfo.characterCount ?
                tmpro.textInfo.characterCount : tmpro.maxVisibleCharacters;

            yield return new WaitForSeconds(0.015f / speed);
        }
    }

    private IEnumerator Build_Fade(){
        int minRange = preTextLength;
        int maxRange = minRange + 1;

        byte alphaThreshold = 30;

        TMP_TextInfo textInfo = tmpro.textInfo;


        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

        float[] alphas = new float[textInfo.characterCount];

        while(alphas[textInfo.characterCount - 1] < 255){

            float fadeSpeed = ((hurryUp ? 5 * charactersPerCycle : charactersPerCycle) * speed) * 4f;
            for(int i = minRange; i < maxRange; i++){

                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            
                alphas[i] = Mathf.MoveTowards(alphas[i], 255, fadeSpeed);

                if (alphas[i] >= 255)
                    minRange = i+1;
                if (maxRange < textInfo.characterCount && alphas[maxRange - 1] >= alphaThreshold)
                    maxRange++;


                // if you have space or any invisible character, the index would actually look back to character 0
                if(!charInfo.isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                for (int v = 0; v < 4; v++)
                    vertexColors[charInfo.vertexIndex + v].a = (byte)alphas[i];


                tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
