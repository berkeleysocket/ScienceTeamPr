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
            //���� ó��
            if (Map == null || Map.Length <= 0) return;

            foreach (TileObject tile in Map)
            {
                if (tile == null)
                {
                    Debug.Log("return : Ÿ���� null�Դϴ�.");
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
                Debug.LogError("�Ҵ� ���� : �� ��踦 ��� ��ǥ���� ������Ʈ�� �Ҵ��� �� �����ϴ�.");
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
                    Debug.LogError("�Ҵ� ���� : �ش� ��ü�� Null�Դϴ�.");
                    return;
                }
                else
                {
                    Debug.LogError("�Ҵ� ���� : �ش� ��ǥ�� �ٸ� ������Ʈ�� �����մϴ�.");
                    return;
                }
            }
            else
            {
                Debug.LogError("�Ҵ� ���� : �� ��踦 ��� ��ǥ���� ������Ʈ�� �Ҵ��� �� �����ϴ�.");
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
        //�ش� ��ǥ�� �� ��(�迭)�ȿ� �ִ� ��ǥ���� ����ϴ� �Լ�
        public static bool InMap(sbyte rowIndex, sbyte columnIndex)
        {
            return rowIndex >= 0 && rowIndex < GameManager.Instance.MapSizeX && 
                columnIndex >= 0 && columnIndex < GameManager.Instance.MapSizeY;
        }
        #endregion
    }
}

