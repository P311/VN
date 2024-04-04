using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING {
    public class TestDialogueFiles : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private TextAsset fileName;

        void Start()
        {
            StartConversation();
        }

        // Update is called once per frame
        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(fileName);
            // Debug.Log(lines.Count);

            foreach(string line in lines){
                // Debug.Log($"Segmenting line '{line}'");
                DIALOGUE_LINE dline = DialogueParser.Parse(line);
                // if (dline.hasSpeaker)
                //     Debug.Log($"{dline.speaker.name} - {dline.speaker.castName} at {dline.speaker.castPosition} with {string.Join(',', dline.speaker.CastExpressions)}");

                // int i = 0;
                // foreach(DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment in dline.dialogue.segments){
                //     Debug.Log($"Segment [{i++}] = '{segment.dialogue}' [signal={segment.startSignal.ToString()} {(segment.signalDelay > 0 ? $"{segment.signalDelay}" : $"")}]");
                // }
            }
            
            DialogueSystem.instance.Say(lines);
        }
    }
}
