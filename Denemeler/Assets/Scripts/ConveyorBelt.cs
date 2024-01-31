using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace RecycleGame
{

    
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] GameObject[] gearObjects;
        Gear _gear;
        private float _gearRotZ;
        private AreaEffector2D _effector2D;

        private void Awake()
        {
            _effector2D = GetComponent<AreaEffector2D>();
        }

        private void Start()
        {
            GameManager.Instance.IsGameFast = false;
        }

        private void Update()
        {
            ChangeBeltSpped();
            RotateGear();
        }


        private void ChangeBeltSpped()
        {
            if(GameManager.Instance.IsGameFast)
            {
                _effector2D.forceMagnitude = 25;
            }
            else
                _effector2D.forceMagnitude = 15;
        }

        private void RotateGear()
        {
            Gear _gear = new Gear(50f, true);
            //Gear _gear = gameObject.AddComponent<Gear>();
            

            if (GameManager.Instance.IsGameFast )
            {
                _gear.RotationSpeed = 150f;
                Debug.Log(_gear.RotationSpeed);
            }
            else if(!GameManager.Instance.IsGameFast)
            {
                _gear.RotationSpeed = 50f;
            }
            else if (GameManager.Instance.IsGameOver)
            {
                _gear.RotationSpeed = 0f;
            }
            
            if(_gear.ClockWiseRotation == false)
            {
                _gearRotZ += Time.deltaTime * _gear.RotationSpeed;
                
            }
            else
            {
                _gearRotZ -= Time.deltaTime * _gear.RotationSpeed;
                
            }

            foreach ( GameObject gearItem in gearObjects )
            {
                 gearItem.transform.rotation = Quaternion.Euler(0, 0, _gearRotZ);
            }
        }

    }

}
