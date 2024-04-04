using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING {
    public class TestParsing : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            string line = "Speaker \"Dialogue goes in here\" Commands(arg1, arg2)";
            DIALOGUE.DIALOGUE_LINE l1 = DialogueParser.Parse(line);
            Debug.Log($"Speaker: {l1.speaker}, Dialogue: {l1.dialogue}");

            string line2 = "Commands(arg3)";
            DIALOGUE.DIALOGUE_LINE l2 = DialogueParser.Parse(line2);
            Debug.Log($"Speaker: {l2.speaker}, Dialogue: {l2.dialogue}");

        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
