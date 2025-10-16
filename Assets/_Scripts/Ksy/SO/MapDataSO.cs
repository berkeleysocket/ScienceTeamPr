using KSY.Manager;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataSO", menuName = "Scriptable Objects/MapDataSO")]
public class MapDataSO : ScriptableObject
{
    public sbyte MapSizeX = 7;
    public sbyte MapSizeY = 7;
    public MapData Map = new MapData(7, 7);
    public TargetsData Targets = new TargetsData(7,7);
}
