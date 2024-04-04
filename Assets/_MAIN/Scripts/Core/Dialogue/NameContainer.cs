using UnityEngine;
using TMPro;


namespace DIALOGUE{
    [System.Serializable]
    public class NameContainer : MonoBehaviour
    {
        public GameObject root;
        public TextMeshProUGUI nameText;


        public void Show(string nameToShow = ""){
            root.SetActive(true);
            if (nameToShow != string.Empty)
                nameText.text = nameToShow;
        }

        public void Hide(){
            root.SetActive(false);
        }
    }
    }
