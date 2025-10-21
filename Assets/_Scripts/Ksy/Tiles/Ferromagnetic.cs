using System.Collections.Generic;
using KSY.Manager;

namespace KSY.Tile
{
    public class Ferromagnetic : TileObject
    {
        public override void Magnetization(int xDir, int yDir, TileObject presser)
        {
            base.Magnetization(xDir, yDir, presser);

            //Ž�� ���� ���
            int exploreDirX = -xDir;
            int exploreDirY = -yDir;

            List<TileObject> tilesToMove = new List<TileObject>();

            //Ž���� ��ǥ ����
            int checkX = CurrentX;
            int checkY = CurrentY;

            while (true)
            {
                TileObject? t = GameManager.Instance.TileManager.GetObject(checkX, checkY);
                if (t != null) tilesToMove.Add(t);

                //Ž���� ������ Ž���ϴ� ��ǥ �������� �� ����
                checkX += exploreDirX;
                checkY += exploreDirY;

                //���� ��� ���̶�� return;
                if (!MapManager.InMap(checkX, checkY)) return;

                //�� ĭ�� �߰��ϸ� Ž���� ����
                if (GameManager.Instance.TileManager.GetObject(checkX, checkY) == null)
                {
                    break;
                }
            }

            //�ڿ������� �ݴ� �������� �� ĭ�� �̵�
            for (int i = tilesToMove.Count - 1; i >= 0; i--)
            {
                TileObject tile = tilesToMove[i];

                sbyte rowIndex = (sbyte)(tile.CurrentX + exploreDirX);
                sbyte columnIndex = (sbyte)(tile.CurrentY + exploreDirY);

                if(tile.Type != TileObjectType.Wall)
                    GameManager.Instance.TileManager.SetObject(rowIndex, columnIndex, tile);
            }
        }
    }
}