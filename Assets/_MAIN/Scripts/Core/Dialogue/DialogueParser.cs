using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE{
    
    public class DialogueParser{

        private const string commandRegexPattern = @"[\w\[\]]*[^\s]\(";

        public static DIALOGUE_LINE Parse(string rawLine){
            
            // return new DIALOGUE_LINE(rawLine.Split(','));
            (string speaker, string dialogue, string commands) = RipContent(rawLine);
            return new DIALOGUE_LINE(speaker, dialogue, commands);
        }

        private static (string, string, string) RipContent(string rawLine){
            string speaker = "", dialogue = "", commands = "";

            int dialogueStart = -1;
            int dialogueEnd = -1;
            bool isEscaped = false;

            for(int i = 0; i < rawLine.Length; i++){
                char current = rawLine[i];
                if (current == '\\'){
                    isEscaped = !isEscaped;
                } else if (current == '"' && !isEscaped){
                    if (dialogueStart == -1)
                        dialogueStart = i;
                    else if(dialogueEnd == -1)
                        dialogueEnd = i;
                } else 
                    isEscaped = false;
            }
            
            Regex commandRegex = new Regex(commandRegexPattern);
            Match match = commandRegex.Match(rawLine);
            int commandStart = -1;
            if(match.Success){
                commandStart = match.Index;
                if (dialogueStart == -1 && dialogueEnd == -1){
                    return ("", "", rawLine.Trim());
                }
            }
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd)){
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"", "\"");
                if (commandStart != -1){
                    commands = rawLine.Substring(commandStart).Trim();
                }
            } else if (commandStart != -1 && dialogueStart > commandStart){
                commands = rawLine;
            } else{
                speaker = rawLine; 
            }
            return (speaker, dialogue, commands);
        }

        //     string[] res = rawLine.Split(',');

        //     for(int i = 0; i < res.Length; i++){
        //         res[i] = res[i].Trim();
        //     }
        //     if (res.Length == 3){
        //         return (res[0], res[1], res[2]);
        //     } else if (res.Length == 1){
        //         return ("", "", res[0]);
        //     }

        //     string speaker = res[2];
        //     string dialogue = res[3];

        //     return (speaker, dialogue, "");
        // }
    }
}
