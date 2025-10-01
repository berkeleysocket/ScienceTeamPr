using KSY.Tile;
using UnityEngine;

namespace KSY.Manager
{
    public class TileManager
    {
        #region Member
        public TileObject[,] Map = new TileObject[5,5];
        const int MAP_SIZE = 5;
        #endregion

        #region public
        public void StartSet(TileObject[] tiles)
        {
            //예외 처리
            if (tiles == null || tiles.Length <= 0) return;

            foreach (TileObject tile in tiles)
            {
                sbyte rowIndex = tile.InitX;
                sbyte columnIndex = tile.InitY;

                if (Map[rowIndex,columnIndex] != null)
                {
                    Debug.LogError("동일한 위치에 있는 타일이 있습니다.");
                    return;
                }

                SetObject(rowIndex, columnIndex, tile);
            }
        }
        public void SetObject(sbyte rowIndex, sbyte columnIndex, TileObject obj)
        {
            if (InMap(rowIndex,columnIndex))
            {
                sbyte lastRowIndex = obj.CurrentX;
                sbyte lastColumIndex = obj.CurrentY;

                Map[lastRowIndex, lastColumIndex] = null;
                if(Map[rowIndex, columnIndex] == null)
                {
                    Map[rowIndex, columnIndex] = obj;
                    UpdatePos(rowIndex, columnIndex, obj);
                }
                else
                {
                    Debug.LogError("할당 실패 : 해당 좌표에 다른 오브젝트가 존재합니다.");
                    return;
                }
            }
            else
            {
                Debug.LogError("맵 경계를 벗어난 좌표에는 오브젝트를 할당할 수 없습니다.");
            }
        }
        public KSY.Tile.TileObject? GetObject(sbyte rowIndex, sbyte columnIndex)
        {
            TileObject? obj = Map[rowIndex, columnIndex];
            return obj;
        }
        #endregion

        #region private
        private void UpdatePos(sbyte x, sbyte y, TileObject obj)
        {
            obj.transform.position = new Vector2(x + 0.5f, y + 0.5f);
        }
        #endregion

        #region static
        //해당 좌표가 맵 안(배열)안에 있는 좌표인지 계산하는 함수
        public static bool InMap(sbyte rowIndex, sbyte columnIndex)
        {
            return rowIndex >= 0 && rowIndex < MAP_SIZE && columnIndex >= 0 && columnIndex < MAP_SIZE;
        }
        #endregion
    }
}

