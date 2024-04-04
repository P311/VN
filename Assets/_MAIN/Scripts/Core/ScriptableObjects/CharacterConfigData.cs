using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DIALOGUE;

namespace CHARACTERS{

    [System.Serializable]
    public class CharacterConfigData{
        public string name;
        public string alias;
        public Character.CharacterType characterType;

        public Color nameColor;
        public Color dialogueColor;

        public TMP_FontAsset nameFont;
        public TMP_FontAsset dialogueFont;

        public CharacterConfigData Copy(){
            CharacterConfigData result = new CharacterConfigData();

            result.name = name;
            result.alias = alias;
            result.characterType = characterType;

            result.nameColor = new Color(nameColor.r, nameColor.g, nameColor.b, nameColor.a);
            result.dialogueColor = new Color(dialogueColor.r, dialogueColor.g, dialogueColor.b, dialogueColor.a);

            result.nameFont = nameFont;
            result.dialogueFont = dialogueFont;

            return result;
        }

        private static Color defaultColor => DialogueSystem.instance.config.defaultColor;

        private static TMP_FontAsset defaultFont => DialogueSystem.instance.config.defaultFont;

        public static CharacterConfigData Default {
            get {
                CharacterConfigData result = new CharacterConfigData();
                result.name = "";
                result.alias = "";
                result.characterType = Character.CharacterType.Text;
                result.nameColor = defaultColor;
                result.dialogueColor = defaultColor;
                result.nameFont = defaultFont;
                result.dialogueFont = defaultFont;

                return result;
            }
        }
    }
}