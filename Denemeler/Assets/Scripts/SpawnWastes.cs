using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RecycleGame
{
    public class SpawnWastes : MonoBehaviour
    {
        public float waitTime;
        public GameObject[] allWastes;
        public GameObject spawnPoint;
        [SerializeField] GameObject panel;
        /*bool _isTwoSecondPass = true;
        [SerializeField] float _currentTime;


        private void Update()
       {
           _currentTime += Time.deltaTime;
           if (_currentTime>3)
           {
               Wastes();
               //StartCoroutine("Wastes");

           }

       }

       public void Wastes()
       {
           //_currentTime += Time.deltaTime;
               //_isTwoSecondPass = true;
               int i = Random.Range(0, allWastes.Length);
               Instantiate(allWastes[i], spawnPoint.transform.position, Quaternion.identity, panel.transform);
               _currentTime = 0;
               //_isTwoSecondPass = false;
       }*/


        private void Start()
        {
            StartCoroutine("Wastess");
        }

       


        public IEnumerator Wastess()
        {
            while (!GameManager.Instance.IsSpawnTimeEnd)
            {
                GameObject allWaste = Instantiate(GetRandomShape(), spawnPoint.transform.position, Quaternion.identity, panel.transform) as GameObject;
                yield return new WaitForSeconds(waitTime);
                //yield return null;
            }
            
        }

        public GameObject GetRandomShape()
        {
            int i = Random.Range(0, allWastes.Length);

            if (allWastes[i])
            { 
                return allWastes[i];
            }
            else
            {
                Debug.Log("Dikkat Geçersiz Atýk");
                return null;
            }
        }

    }
}



