using System.Collections.Generic;
using KSY_Manager;

namespace KSY_Tile
{
    public class Ferromagnetic : Tile
    {
        public override bool Magnetization(sbyte xDir, sbyte yDir, Tile presser)
        {
            //Ž�� ���� ���
            sbyte exploreDirX = (sbyte)-xDir;
            sbyte exploreDirY = (sbyte)-yDir;

            List<Tile> tilesToMove = new List<Tile>();

            //Ž���� ��ǥ ����
            sbyte checkX = CurrentX;
            sbyte checkY = CurrentY;

            while (true)
            {
                Tile? t = GameManager.Instance.TileManager.GetObject(checkX, checkY);
                if (t != null) tilesToMove.Add(t);

                //Ž���� ������ Ž���ϴ� ��ǥ �������� �� ����
                checkX += exploreDirX;
                checkY += exploreDirY;

                //���� ��� ���̶�� return;
                if (!TileManager.InMap(checkX, checkY))
                {
                    return false;
                }

                //�� ĭ�� �߰��ϸ� Ž���� ����
                if (GameManager.Instance.TileManager.GetObject(checkX, checkY) == null)
                {
                    break;
                }
            }

            //�ڿ������� �ݴ� �������� �� ĭ�� �̵�
            for (int i = tilesToMove.Count - 1; i >= 0; i--)
            {
                Tile tile = tilesToMove[i];
                sbyte rowIndex = (sbyte)(tile.CurrentX + exploreDirX);
                sbyte columnIndex = (sbyte)(tile.CurrentY + exploreDirY);

                GameManager.Instance.TileManager.SetObject(rowIndex, columnIndex, tile);
            }
            return true;
        }
    }
}