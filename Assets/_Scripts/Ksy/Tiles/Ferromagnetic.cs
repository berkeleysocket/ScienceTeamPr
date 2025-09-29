using System.Collections.Generic;
using KSY_Manager;

namespace KSY_Tile
{
    public class Ferromagnetic : Tile
    {
        public override bool Magnetization(sbyte xDir, sbyte yDir, Tile presser)
        {
            //탐색 방향 계산
            sbyte exploreDirX = (sbyte)-xDir;
            sbyte exploreDirY = (sbyte)-yDir;

            List<Tile> tilesToMove = new List<Tile>();

            //탐색할 좌표 저장
            sbyte checkX = CurrentX;
            sbyte checkY = CurrentY;

            while (true)
            {
                Tile? t = GameManager.Instance.TileManager.GetObject(checkX, checkY);
                if (t != null) tilesToMove.Add(t);

                //탐색할 때마다 탐색하는 좌표 방향으로 값 증가
                checkX += exploreDirX;
                checkY += exploreDirY;

                //만약 경계 밖이라면 return;
                if (!TileManager.InMap(checkX, checkY))
                {
                    return false;
                }

                //빈 칸을 발견하면 탐색을 종료
                if (GameManager.Instance.TileManager.GetObject(checkX, checkY) == null)
                {
                    break;
                }
            }

            //뒤에서부터 반대 방향으로 한 칸씩 이동
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