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

            //���� ��踦 ����ٸ� return�ϱ�.
            if (!TileManager.InMap(x, y)) { return; }

            //�ش� ��ǥ�� �ٸ� Ÿ���� �ִ��� Ȯ���ϱ�.
            Tile? destinationTile = GameManager.Instance.TileManager.GetObject(x, y);

            //���� Ÿ���� ���ٸ� �ش� ��ǥ�� �÷��̾� �̵���Ű��.
            if (destinationTile == null)
            {
                GameManager.Instance.TileManager.SetObject(x, y, this);
            }
            //���� Ÿ���� �ִٸ�
            else
            {
                destinationTile.Magnetization();
            }
        }
    }
}

