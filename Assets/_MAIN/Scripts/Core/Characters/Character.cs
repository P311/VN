using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS {

    public abstract class Character{
        public string name = "";
        public RectTransform root = null;

        public enum CharacterType{
            Text, // just for dialogue
            Sprite,  // texture
            SpriteSheet, // multiple sprite per texture
            Live2D,
            Model3D
        }

        public Character(string characterName){
            name = characterName;
        }

    }
}
