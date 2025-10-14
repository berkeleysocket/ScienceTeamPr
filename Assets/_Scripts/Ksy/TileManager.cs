using KSY.Tile;
using UnityEngine;
namespace KSY.Manager
{
    public class TileManager
    {
        #region Member
        public TileObject[,] Map;
        public void SetMap(TileObject[,] gameMap)
        {
            if (gameMap == null || gameMap.Length <= 0) return;

            //Map = (TileObject[,])gameMap.Clone();
            Map = gameMap;
        }
        public void InitTilePos()
        {
            //예외 처리
            if (Map == null || Map.Length <= 0) return;

            foreach (TileObject tile in Map)
            {
                if (tile == null)
                {
                    Debug.Log("return : 타일이 null입니다.");
                    continue;
                }

                sbyte rowIndex = tile.InitX;
                sbyte columnIndex = tile.InitY;

                UpdatePos(rowIndex, columnIndex, tile);
            }
        }
        public void SetNull(sbyte rowIndex, sbyte columIndex)
        {
            if (InMap(rowIndex, columIndex))
            {
                Map[rowIndex, columIndex] = null;
            }
            else
            {
                Debug.LogError("할당 실패 : 맵 경계를 벗어난 좌표에는 오브젝트를 할당할 수 없습니다.");
                return;
            }
        }
        public void SetObject(sbyte rowIndex, sbyte columnIndex, TileObject obj)
        {
            if (InMap(rowIndex,columnIndex))
            {
                if (Map[rowIndex, columnIndex] == null && obj != null)
                {
                    sbyte lastRowIndex = obj.CurrentX;
                    sbyte lastColumIndex = obj.CurrentY;

                    if(Map[lastRowIndex, lastColumIndex] != null && Map[lastRowIndex, lastColumIndex].TileID == obj.TileID)
                    {
                        Map[lastRowIndex, lastColumIndex] = null;
                    }

                    obj.CurrentX = rowIndex;
                    obj.CurrentY = columnIndex;
                    Map[rowIndex, columnIndex] = obj;

                    UpdatePos(rowIndex, columnIndex, obj);

                    Debug.Log($"SetObject {obj.name} : ({rowIndex},{columnIndex})");
                }
                else if(obj == null)
                {
                    Debug.LogError("할당 실패 : 해당 객체가 Null입니다.");
                    return;
                }
                else
                {
                    Debug.LogError("할당 실패 : 해당 좌표에 다른 오브젝트가 존재합니다.");
                    return;
                }
            }
            else
            {
                Debug.LogError("할당 실패 : 맵 경계를 벗어난 좌표에는 오브젝트를 할당할 수 없습니다.");
                return;
            }
        }
        public TileObject? GetObject(sbyte rowIndex, sbyte columnIndex)
        {
            TileObject? obj = Map[rowIndex, columnIndex];
            return obj;
        }
        #endregion

        #region private
        private void UpdatePos(sbyte x, sbyte y, TileObject obj)
        {
            obj.transform.position = new Vector2((float)x + 0.5f, (float)y + 0.5f);
            Debug.Log($"<color=yellow>UpdatePos {obj.name} : ({obj.transform.position.x},{obj.transform.position.y})</color>");
        }
        #endregion

        #region static
        //해당 좌표가 맵 안(배열)안에 있는 좌표인지 계산하는 함수
        public static bool InMap(sbyte rowIndex, sbyte columnIndex)
        {
            return rowIndex >= 0 && rowIndex < GameManager.Instance.MapSizeX && 
                columnIndex >= 0 && columnIndex < GameManager.Instance.MapSizeY;
        }
        #endregion
    }
}

