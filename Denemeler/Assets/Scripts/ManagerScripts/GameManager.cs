using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RecycleGame
{
    public class GameManager : MonoBehaviour
    {
        private bool _isGameFast;
        public bool IsGameFast { get { return _isGameFast; } set { _isGameFast = value; } }

        private bool _isGameOver;
        public bool IsGameOver { get { return _isGameOver; } set { _isGameOver = value; } }


        public static GameManager Instance { get; private set; }

        private bool _isSpawnTimeEnd;
        public bool IsSpawnTimeEnd { get { return _isSpawnTimeEnd; }  set {_isSpawnTimeEnd= value; } }

        private void Awake()
        {
            Tekil();

            /*if(Instance != null && Instance!=this) 
            { Destroy(this);
              return;
            }
            Instance = this;*/
            
        }
        private void Tekil()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject); 
            }

            else
                Destroy(this.gameObject);
        }
    }

}
