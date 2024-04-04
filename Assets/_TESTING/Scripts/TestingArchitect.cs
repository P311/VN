using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING {
    public class TestingArchitect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[5]{
            "This is line 1 for testing",
            "This is extended line 2 for testing",
            "This is very complicated line 3 for testing ?12t6*`Aajla31^!#^%*",
            "THIS IS A NORMAL LINE 4 FOR TESTING",
            "This is the fifth loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo"+
            "oooooooooooooooooong line for testingThis is the fifth looooooooooooooooooooooooooooo"
        };

        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = bm;
        }

        // Update is called once per frame
        void Update()
        {
            if (bm != architect.buildMethod){
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
                architect.Stop();

            if(Input.GetKeyDown(KeyCode.Space)){
                if (architect.isBuilding){
                    if (!architect.hurryUp){
                        architect.hurryUp = true;
                    }
                    else {
                        architect.ForceComplete();
                    }
                } else {
                    architect.Build(lines[Random.Range(0, lines.Length)]);
                }
            } else if (Input.GetKeyDown(KeyCode.A)){
                architect.Append(lines[Random.Range(0, lines.Length)]);
            }
        }
    }
}
