using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING{
    public class CommandTesting : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Running());
        }

        IEnumerator Running(){
            yield return CommandManager.instance.Execute("print");
            yield return CommandManager.instance.Execute("print_1p", "Hello World");
            yield return CommandManager.instance.Execute("print_mp", "line1", "line2", "line3");
            yield return CommandManager.instance.Execute("lambda");
            yield return CommandManager.instance.Execute("lambda_1p", "Hello World");
            yield return CommandManager.instance.Execute("lambda_mp", "line1", "line2", "line3");
            yield return CommandManager.instance.Execute("process");
            yield return CommandManager.instance.Execute("process_1p", "test");
            yield return CommandManager.instance.Execute("process_mp", "test1", "test2", "test3");
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}