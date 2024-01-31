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
        public event Action<int, int> PuanArttýr;


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
            speechText.text = "Banttan gelen atýklarý kaybolmadan doðru kutulara atýnýz!!";
            Instance.SpeechBubbleEvent += KarakterKonustur;
        }*/
        public void PuanArttýrma(int puan, int bonus)
        {
            PuanArttýr.Invoke(puan, bonus);
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


