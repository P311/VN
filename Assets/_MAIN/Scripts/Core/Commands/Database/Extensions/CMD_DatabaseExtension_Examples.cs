using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CMD_DatabaseExtension_Examples : CMD_DatabaseExtension
{
    new public static void Extend(CommandDatabase database){
        // add action with no param
        database.AddCommand("print", new Action(PrintDefaultMessage));
        database.AddCommand("print_1p", new Action<string>(PrintUserMessage));
        database.AddCommand("print_mp", new Action<string[]>(PrintLines));

        //add lambda with no param
        database.AddCommand("lambda", new Action(() => {Debug.Log("Lambda msg");}));
        database.AddCommand("lambda_1p", new Action<string>((arg) => {Debug.Log($"Lambda '{arg}'");}));
        database.AddCommand("lambda_mp", new Action<string[]>((args) => {Debug.Log($"Lambda msg {string.Join(',', args)}");}));

        //add coroutine with no params
        database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
        database.AddCommand("process_1p", new Func<string, IEnumerator>(LineProcess));
        database.AddCommand("process_mp", new Func<string[], IEnumerator>(LinesProcess));
    }

    private static void PrintDefaultMessage(){
        Debug.Log("Printing a default message to test cmd db");
    }

    private static void PrintUserMessage(string message){
        Debug.Log($"User msg: '{message}'");
    }

    private static void PrintLines(string[] lines){
        int i = 1;
        foreach(string line in lines){
            Debug.Log($"{i}: {line}");
        }
    }

    private static IEnumerator SimpleProcess(){
        for(int i = 1; i <= 5; i++){
            Debug.Log($"Process Running... [{i}]");
            yield return new WaitForSeconds(1);
        }
    }

    private static IEnumerator LineProcess(string line){
        for(int i = 1; i <= 5; i++){
            Debug.Log($"Process Running... [{i}] - {line}");
            yield return new WaitForSeconds(1);
        }
    }

    private static IEnumerator LinesProcess(string[] lines){
        for(int i = 1; i <= lines.Length; i++){
            Debug.Log($"Process Running... [{i}] - {lines[i-1]}");
            yield return new WaitForSeconds(1);
        }
    }
}
