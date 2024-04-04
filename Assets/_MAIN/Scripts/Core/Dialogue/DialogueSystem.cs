using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DIALOGUE{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] private DialogueSystemConfigurationSO _config;
        public DialogueSystemConfigurationSO config => _config;

        public DialogueContainer dialogueContainer;
        public NameContainer nameContainer;
        private ConversationManager conversationManager;
        
        private TextArchitect architect;

        public static DialogueSystem instance { get; private set; }

        public bool isRunningConversation;

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;

        public void Start(){
            isRunningConversation = conversationManager.isRunning;
        }

        public void OnUserPrompt_Next(){
            onUserPrompt_Next?.Invoke();    
        }

        private void Awake(){
            if (instance == null){
                instance = this;
                Initialize();
            } else {
                DestroyImmediate(gameObject);
            }
        }

        bool _initialized = false;
        private void Initialize(){
            if (_initialized)
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }
    

        public void ShowSpeakerName(string speakerName = "") => nameContainer.Show(speakerName);
        public void HideSpeakerName(string speakerName = "") => nameContainer.Hide();

        public void Say(string speaker, string dialogue ){
            List<string> conversation = new List<string>() {$"{speaker} \"{dialogue}\""};
            Say(conversation);
        }

        public void Say(List<string> conversation){
            conversationManager.StartConversation(conversation);
        }
    }
}
