using System.Collections.Generic;
using KSY_Tile;
using UnityEngine;

namespace KSY_Manager
{
    public class TileManager
    {
        public List<List<Tile>> Map = new List<List<Tile>>();
        public void SetObject(sbyte rowIndex, sbyte columnIndex, Tile obj)
        {
            Map[rowIndex][columnIndex] = obj;
            UpdatePos(rowIndex, columnIndex, obj);
        }
        public Tile GetObject(sbyte rowIndex, sbyte columnIndex)
        {
            Tile obj = Map[rowIndex][columnIndex];
            return obj;
        }
        private void UpdatePos(sbyte x, sbyte y, Tile obj)
        {
            obj.transform.position = new Vector2(x, y);
        }
    }
}

