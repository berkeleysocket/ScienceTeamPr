using KSY.Manager;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataSO", menuName = "Scriptable Objects/MapDataSO")]
public class MapDataSO : ScriptableObject
{
    [HideInInspector]
    public sbyte MapSizeX = 7;
    [HideInInspector]
    public sbyte MapSizeY = 7;
    [HideInInspector]
    public MapData Map = new MapData(7, 7);
    [HideInInspector]
    public TargetsData Targets = new TargetsData(7,7);
}
