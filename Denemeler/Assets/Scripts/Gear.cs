using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecycleGame
{
    public class Gear : MonoBehaviour  
    {
        private float m_rotationSpeed;
        public float RotationSpeed { get { return m_rotationSpeed; } set { m_rotationSpeed = value; } }
        private bool m_clockWiseRotation;
        public bool ClockWiseRotation { get { return m_clockWiseRotation; } set { m_clockWiseRotation = value; } }

        public Gear(float _rotationSpeed, bool _clockWiseRotation)
        {
            
            RotationSpeed = _rotationSpeed;
            ClockWiseRotation = _clockWiseRotation;
            
        }
    }



}


