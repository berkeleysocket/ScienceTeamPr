using KSY.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;
namespace KSY.Manager
{
    //public struct Destination
    //{
    //    //와야하는 위치
    //    public int X;
    //    public int Y;

    //    //와야하는 타일 오브젝트 타입
    //    public TileObjectType Type;
    //    public Destination(int x, int y, TileObjectType t)
    //    {
    //        this.X = x;
    //        this.Y = y;
    //        this.Type = t;
    //    }
    //}

    //인게임내의 타일 오브젝트들을 관리하는 매니저
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
                Debug.LogError("할당 실패 : 맵 경계를 벗어난 좌표에는 오브젝트를 할당할 수 없습니다.");
                return;
            }
        }
        public void SetObject(int rowIndex, int columnIndex, TileObject obj)
        {
            //놓으려는 위치가 맵 안인지 확인함.
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
                    //None일 경우 무시
                    if (checkTileType == TileObjectType.None || checkTileType == TileObjectType.Wall) continue;

                    //목표 카운트 up.
                    successCount++;
                    //if (TileMatrix[x, y] != null)
                    //    Debug.Log($"있어야 하는 거 {checkTileType} : 실제로 있는 거 {TileMatrix[x, y].Type}");

                    //실제로 객체가 존재하면서 타일 타입이 겹친다면 카운트 up.
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

