using System;
using UnityEngine;

namespace KSY_Manager
{
    public class InputManager : MonoBehaviour
    {
        public event Action<sbyte,sbyte> MoveKeyPressed;
        public sbyte MoveDirX { get; private set; }
        public sbyte MoveDirY { get; private set; }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                MoveDirY = 1;
                MoveDirX = 0;
                MoveKeyPressed?.Invoke(MoveDirX,MoveDirY);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveDirY = -1;
                MoveDirX = 0;
                MoveKeyPressed?.Invoke(MoveDirX, MoveDirY);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveDirX = 1;
                MoveDirY = 0;
                MoveKeyPressed?.Invoke(MoveDirX, MoveDirY);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MoveDirX = -1;
                MoveDirY = 0;
                MoveKeyPressed?.Invoke(MoveDirX, MoveDirY);
            }
        }
    }
}

