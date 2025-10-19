using JetBrains.Annotations;
using KSY.Manager;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataSO", menuName = "Scriptable Objects/MapDataSO")]
public class MapDataSO : ScriptableObject
{
    [field: SerializeField]
    public int SizeX { get; private set; }
    [field: SerializeField]
    public int SizeY { get; private set; }

    [HideInInspector]   
    public TileMatrix tiles = new TileMatrix();
    [HideInInspector]
    public DestinationMatrix destinations = new DestinationMatrix();

    public void OnEnable()
    {
        tiles = new TileMatrix(SizeX,SizeY);
        destinations = new DestinationMatrix(SizeX, SizeY);
    }
}

