using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;

namespace TESTING {
    public class TestCharacters : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Character character = CharacterManager.instance.CreateCharacter("Mako");
            Character character2 = CharacterManager.instance.CreateCharacter("Mako");
            Character character3 = CharacterManager.instance.CreateCharacter("Nagisa");
            Character character4 = CharacterManager.instance.CreateCharacter("Alice");
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}