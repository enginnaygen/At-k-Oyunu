using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeCountDown : MonoBehaviour
{
    float _time;
    [SerializeField] TextMeshProUGUI TimeText,ScoreTexti;
    [SerializeField] GameObject Panel;
    void Start()
    {
        _time = 30;
    }

    void Update()
    {
        
        _time -= Time.deltaTime;
        TimeText.text = "" + Mathf.RoundToInt(_time) ;
        TimeControl();
    }
    public void TimeControl()
    {
        if (_time <= 0) 
        {
            Panel.SetActive(true);
            Time.timeScale = 0;
            ScoreTexti.text = "Oyun Bitti Skorunuz:" + DropDrag.Score;
        }
    }

    public void Quit() 
    {
        Application.Quit();
    }
    public void ReStart()
    {
        _time = 30;
        Time.timeScale = 1;
        DropDrag.Score = 0;
        DropDrag._bonusCount = 0;
        SceneManager.LoadScene(0);
       
    }
}
