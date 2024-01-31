using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RecycleGame
{
    public class ColorEvent : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        private void Start()
        {
            EventManager.Instance.ColorBubbleEvent += RenkVer;
        }
        private void RenkVer(Color Renk)
        {
            textMeshProUGUI.color = Renk;

        }
    }

}
