using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RecycleGame
{
    public class EventManager : MonoBehaviour

    {
        public event Action<string> SpeechBubbleEvent;
        public event Action<Color> ColorBubbleEvent;
        public event Action<int, int> PuanArtt�r;


        public void OnSpeechBubble(string speechBubble)
        {
            SpeechBubbleEvent.Invoke(speechBubble);
        }
        public void OnColorBubble(Color Renk)
        {
            ColorBubbleEvent.Invoke(Renk);
        }
        /*[SerializeField] TextMeshProUGUI speechText;
         * private void KarakterKonustur(string prompter)
        {
            speechText.text = prompter;
        }
        private void Start()
        {
            speechText.text = "Banttan gelen at�klar� kaybolmadan do�ru kutulara at�n�z!!";
            Instance.SpeechBubbleEvent += KarakterKonustur;
        }*/
        public void PuanArtt�rma(int puan, int bonus)
        {
            PuanArtt�r.Invoke(puan, bonus);
        }


        public static EventManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

        }
    }

    
        
}


