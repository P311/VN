using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CommandDatabase
{
    private Dictionary<string, Delegate> database = new Dictionary<string, Delegate>();

    public bool HasCommand(string commandName) => database.ContainsKey(commandName);

    public void AddCommand(string commandName, Delegate command){
        if(!database.ContainsKey(commandName)){
            database.Add(commandName, command);
        } else {
            Debug.LogError($"command already exists '{commandName}'");
        }
    }

    public Delegate GetCommand(string commandName){
        if(!database.ContainsKey(commandName)){
            Debug.LogError($"command not exist '{commandName}'");
            return null;
        } else {
            return database[commandName];
        }        
    }
}
