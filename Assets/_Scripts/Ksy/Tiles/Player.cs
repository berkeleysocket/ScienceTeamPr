using KSY.Manager;

namespace KSY.Tile
{
    public class Player : TileObject
    {
        public override void Magnetization(sbyte xDir, sbyte yDir, TileObject presser)
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            GameManager.Instance.InputManager.MoveKeyPressed += (x,y)=> Move(x,y);
        }
    }
}

