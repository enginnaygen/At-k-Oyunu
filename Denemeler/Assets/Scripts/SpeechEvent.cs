using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RecycleGame
{
    public class SpeechEvent : MonoBehaviour
    {
        public TextMeshProUGUI speechText;


        private void Start()
        {
            speechText.text = "Banttan gelen atýklarý kaybolmadan doðru kutulara atýnýz!!";
            EventManager.Instance.SpeechBubbleEvent += KarakterKonustur;
        }

        private void KarakterKonustur(string prompter)
        {
            speechText.text = prompter;
            
        }
       
    }

}
