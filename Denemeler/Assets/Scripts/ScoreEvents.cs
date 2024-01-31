using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

namespace RecycleGame {
    public class ScoreEvents : MonoBehaviour
    {


        [SerializeField] TextMeshProUGUI ScoreTexti, BonusTexti;
        //GameObject _gamePanel;
         static int _score,_comboCount;
        //[SerializeField] TMP_Text ScoreText, BonusText;

       /* private void Awake()
        {
            _gamePanel = GameObject.FindWithTag("GamePanel");
        }*/

        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag(this.tag))
            {
                _score += 50;
                _comboCount++;
                ScoreText.text = _score.ToString();
                BonusText.text = "x"+_comboCount.ToString();
                if (_comboCount >=5)
                {
                    _score += 200;
                    Time.timeScale = 2;
                }
                if (_comboCount >= 10)
                {
                    _score += 500;
                    Time.timeScale = 3;
                }
            }
            if(!collision.gameObject.CompareTag(this.tag))
            {
                _score -= 50;
                ScoreText.text = _score.ToString();
                _comboCount = 0;
                BonusText.text ="x"+ _comboCount.ToString();
                Time.timeScale = 1;
            }
        }*/

        private void Start()
        {
            EventManager.Instance.PuanArttýr += PuanYükselt;
        }

        public void PuanYükselt(int Puan,int bonus) 
        {
            
            ScoreTexti.text = Puan.ToString();
            BonusTexti.text = "x"+bonus.ToString();
        }
    }
}
