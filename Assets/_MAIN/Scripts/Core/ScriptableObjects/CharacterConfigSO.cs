using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS{

    [CreateAssetMenu(fileName = "Character Configuration Asset", menuName = "Dialogue System/Character Configuration Asset")]
    public class CharacterConfigSO: ScriptableObject {
        // data for every character
        public CharacterConfigData[] characters;

        public CharacterConfigData GetConfig(string characterName){

            characterName = characterName.ToLower();

            for(int i = 0; i < characters.Length; i++){
                CharacterConfigData data = characters[i];

                if(string.Equals(characterName, data.name.ToLower()) || 
                    string.Equals(characterName, data.alias.ToLower())){
                        Debug.Log($"character {characterName} exists in config");
                        return data.Copy();
                }
            }
            
            CharacterConfigData result = CharacterConfigData.Default;
            return result;
        }
    }
}
