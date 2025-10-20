using KSY.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;
namespace KSY.Manager
{
    //public struct Destination
    //{
    //    //�;��ϴ� ��ġ
    //    public int X;
    //    public int Y;

    //    //�;��ϴ� Ÿ�� ������Ʈ Ÿ��
    //    public TileObjectType Type;
    //    public Destination(int x, int y, TileObjectType t)
    //    {
    //        this.X = x;
    //        this.Y = y;
    //        this.Type = t;
    //    }
    //}

    //�ΰ��ӳ��� Ÿ�� ������Ʈ���� �����ϴ� �Ŵ���
    public class TileManager
    {
        public TileObject[,] TileMatrix;
        public TileObjectType[,] DestinationMatrix;
        public void GetMapInfo(TileObject[,] tileMatrix, TileObjectType[,] destinationMatrix)
        {
            TileMatrix = tileMatrix;
            DestinationMatrix = destinationMatrix;
        }
        public void SetNull(int rowIndex, int columIndex)
        {
            if (MapManager.InMap(rowIndex, columIndex))
            {
                TileMatrix[rowIndex, columIndex] = null;
            }
            else
            {
                Debug.LogError("�Ҵ� ���� : �� ��踦 ��� ��ǥ���� ������Ʈ�� �Ҵ��� �� �����ϴ�.");
                return;
            }
        }
        public void SetObject(int rowIndex, int columnIndex, TileObject obj)
        {
            //�������� ��ġ�� �� ������ Ȯ����.
            if (MapManager.InMap(rowIndex,columnIndex))
            {
                if (TileMatrix[rowIndex, columnIndex] == null && obj != null)
                {
                    int lastRowIndex = obj.CurrentX;
                    int lastColumIndex = obj.CurrentY;

                    obj.CurrentX = rowIndex;
                    obj.CurrentY = columnIndex;

                    TileMatrix[rowIndex, columnIndex] = obj;

                    UpdatePos(rowIndex, columnIndex, obj);

                    if (TileMatrix[lastRowIndex, lastColumIndex] != null && TileMatrix[lastRowIndex, lastColumIndex].Id == obj.Id)
                    {
                        TileMatrix[lastRowIndex, lastColumIndex] = null;
                    }

                    Check_AllTileArrivedDestination();
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
        public TileObject GetObject(int rowIndex, int columnIndex)
        {
            TileObject obj = TileMatrix[rowIndex, columnIndex];
            return obj;
        }

        #region private
        private void UpdatePos(int x, int y, TileObject obj)
        {
            obj.transform.position = new Vector2(x + 0.5f, y + 0.5f);
            //Debug.Log($"<color=yellow>UpdatePos {obj.name} : ({obj.transform.position.x},{obj.transform.position.y})</color>");
        }

        private void Check_AllTileArrivedDestination()
        {
            byte arrivedCount = 0;
            byte successCount = 0;

            for (int g = 0; g < MapManager.MapSizeX; g++)
            {
                for (int h = 0; h < MapManager.MapSizeY; h++)
                {
                    int x = g;
                    int y = h;
                    TileObjectType checkTileType = DestinationMatrix[x, y];

                    Debug.Log(checkTileType);
                    //None�� ��� ����
                    if (checkTileType == TileObjectType.None || checkTileType == TileObjectType.Wall) continue;

                    //��ǥ ī��Ʈ up.
                    successCount++;
                    //if (TileMatrix[x, y] != null)
                    //    Debug.Log($"�־�� �ϴ� �� {checkTileType} : ������ �ִ� �� {TileMatrix[x, y].Type}");

                    //������ ��ü�� �����ϸ鼭 Ÿ�� Ÿ���� ��ģ�ٸ� ī��Ʈ up.
                    if (TileMatrix[x, y] != null && TileMatrix[x, y].Type == checkTileType)
                    {
                        arrivedCount++;
                        Debug.Log($"{arrivedCount}");
                    }
                }
            }

            if (arrivedCount == successCount)
            {
                Debug.Log("<color=red>Clear!</color>");
                GameManager.Instance.mainMenuReturn.Cleared();
            }
        }
        #endregion
    }
}

