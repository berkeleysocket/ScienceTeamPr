using KSY.Manager;
using System;

namespace KSY.Tile
{
    public class MirrorPlayer : TileObject
    {
        public Action<sbyte, sbyte> HandleMoveKeyPressed;
        public override void Magnetization(int xDir, int yDir, TileObject presser)
        {
            throw new System.NotImplementedException();
        }
        private void OnDisable()
        {
            GameManager.Instance.InputManager.MoveKeyPressed -= HandleMoveKeyPressed;
        }
        private void Start()
        {
            HandleMoveKeyPressed = (x, y) => Move(-x, -y);
            GameManager.Instance.InputManager.MoveKeyPressed += HandleMoveKeyPressed;
        }
    }
}

