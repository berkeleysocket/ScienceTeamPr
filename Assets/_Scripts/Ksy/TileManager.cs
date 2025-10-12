using KSY.Tile;
using UnityEngine;

namespace KSY.Manager
{
    public class TileManager
    {
        #region Member
        public TileObject[,] Map = new TileObject[5,5];
        #endregion

        #region public
        public void StartSet(TileObject[,] gameMap)
        {
            //���� ó��
            if (gameMap == null || gameMap.Length <= 0) return;

            foreach (TileObject tile in gameMap)
            {
                if (tile == null) return;

                sbyte rowIndex = tile.InitX;
                sbyte columnIndex = tile.InitY;

                if (Map[rowIndex,columnIndex] != null)
                {
                    Debug.LogError("������ ��ġ�� �ִ� Ÿ���� �ֽ��ϴ�.");
                    return;
                }

                SetObject(rowIndex, columnIndex, tile);
            }
        }
        public void SetObject(sbyte rowIndex, sbyte columnIndex, TileObject? obj)
        {
            if (InMap(rowIndex,columnIndex))
            {
                if (obj != null)
                {
                    sbyte lastRowIndex = obj.CurrentX;
                    sbyte lastColumIndex = obj.CurrentY;

                    Map[lastRowIndex, lastColumIndex] = null;
                }

                if (Map[rowIndex, columnIndex] == null && obj != null)
                {
                    obj.CurrentX = rowIndex;
                    obj.CurrentY = columnIndex;
                    Map[rowIndex, columnIndex] = obj;
                    UpdatePos(rowIndex, columnIndex, obj);
                }
                else if(obj == null)
                {
                    Map[rowIndex, columnIndex] = obj;
                }
                else
                {
                    Debug.LogError("�Ҵ� ���� : �ش� ��ǥ�� �ٸ� ������Ʈ�� �����մϴ�.");
                    return;
                }
            }
            else
            {
                Debug.LogError("�� ��踦 ��� ��ǥ���� ������Ʈ�� �Ҵ��� �� �����ϴ�.");
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
        //�ش� ��ǥ�� �� ��(�迭)�ȿ� �ִ� ��ǥ���� ����ϴ� �Լ�
        public static bool InMap(sbyte rowIndex, sbyte columnIndex)
        {
            return rowIndex >= 0 && rowIndex < GameManager.Instance.MapSizeX && 
                columnIndex >= 0 && columnIndex < GameManager.Instance.MapSizeY;
        }
        #endregion
    }
}

