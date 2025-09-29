using KSY_Manager;
using UnityEngine;

namespace KSY_Tile
{
    public class Player : Tile
    {
        private void Start()
        {
            GameManager.Instance.InputManager.MoveKeyPressed += (x,y)=> Move(x,y);
        }
    }
}

