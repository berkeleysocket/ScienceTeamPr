using KSY_Manager;
using UnityEngine;

namespace KSY_Tile
{
    public class Player : Tile
    {
        private void Start()
        {
            GameManager.Instance.InputManager.MoveKeyPressed += Move;
        }
        private void Move(sbyte xDir, sbyte yDir)
        {
            int _x = CurrentX + xDir;
            int _y = CurrentY + yDir;
            sbyte x = (sbyte)_x;
            sbyte y = (sbyte)_y;

            //맵의 경계를 벗어났다면 return하기.
            if (!TileManager.InMap(x, y)) { return; }

            //해당 좌표에 다른 타일이 있는지 확인하기.
            Tile? destinationTile = GameManager.Instance.TileManager.GetObject(x, y);

            //만약 타일이 없다면 해당 좌표로 플레이어 이동시키기.
            if (destinationTile == null)
            {
                GameManager.Instance.TileManager.SetObject(x, y, this);
            }
            //만약 타일이 있다면
            else
            {
                destinationTile.Magnetization();
            }
        }
    }
}

