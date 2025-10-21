using KSY.Manager;
using KSY.Tile;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static int CurrentMapIndex { get; private set; } = 0;
    public static int MapSizeX { get; private set; }
    public static int MapSizeY { get; private set; }

    //타일맵 타일 종류
    public UnityEngine.Tilemaps.Tile Tile_Diamagnetic;
    public UnityEngine.Tilemaps.Tile Tile_Ferromagnetic;
    public UnityEngine.Tilemaps.Tile Tile_MirrorPlayer;
    public UnityEngine.Tilemaps.Tile Tile_Paramagnetic;
    public UnityEngine.Tilemaps.Tile Tile_Player;
    public UnityEngine.Tilemaps.Tile Tile_Void;

    public UnityEngine.Tilemaps.Tile HardTile_Diamagnetic;
    public UnityEngine.Tilemaps.Tile HardTile_Ferromagnetic;
    public UnityEngine.Tilemaps.Tile HardTile_Paramagnetic;

    public Tilemap TileMapCompo;
    public void Create(int mapIndex)
    {
        //지정된 인덱스의 맵을 불러옴
        MapDataSO data = GameManager.Instance.Maps[mapIndex];

        //맵이 불러와지지 않았다면 return
        Debug.Log($"<color=red>map data is null : {data == null}</color>");
        if (data == null) return;

        //맵의 사이즈를 가져옴
        int sizeX = data.SizeX;
        int sizeY = data.SizeY;

        MapSizeX = sizeX;
        MapSizeY = sizeY;

        //맵의 타일 배치도를 가져옴
        TileMatrix ref_tiles = data.tiles;

        //맵의 클리어 타일 배치도를 가져옴(완성본)
        DestinationMatrix ref_destinations = data.destinations;

        //TileManager에 넘겨줄 맵 데이터를 따로 만듬
        TileObject[,] tilesData = new TileObject[sizeX, sizeY];
        TileObjectType[,] destinationData = new TileObjectType[sizeX,sizeY];

        for (int g = sizeY - 1; 0 <= g; g--)
        {
            for (int h = sizeX - 1; 0 <= h; h--)
            {
                GameObject tile = ref_tiles.rows[g].colums[h];

                //각 행과 열을 모두 검사하다가 만약 타일이 null이 아니라면 타일을 생성함.
                if (tile != null)
                {
                    GameObject tileBody = Instantiate(tile);

                    //타일의 초기화 위치를 계산함
                    int initY = sizeY - 1 - g;
                    int initX = h;

                    if (!tileBody.TryGetComponent(out TileObject tileSc)) return;

                    //타일의 초기화 위치
                    Debug.Log($"{tileBody.name} : {initX},{initY}");
                    tileSc.InitY = initY;
                    tileSc.InitX = initX;
                    tileSc.Init();

                    //데이터 기록
                    tilesData[initX,initY] = tileSc;
                }
            }
        }

        for (int g = sizeY - 1; 0 <= g; g--)
        {
            for (int h = sizeX - 1; 0 <= h; h--)
            {
                //각 행과 열의 타일 타입을 검사함.
                TileObjectType tileT = ref_destinations.rows[g].colums[h];

                //타일의 위치를 계산함.
                sbyte x = (sbyte)h;
                sbyte y = (sbyte)(sizeY - 1 - g);

                //타겟 타일 배열의 타일이 None이 아닐 경우, 목적지 타일로 기록.
                if (tileT != TileObjectType.None)
                {
                    Debug.Log($"<color=green>Result Point : ({x},{y}) T : {tileT}</color>");
                    destinationData[x, y] = tileT;
                }

                switch (tileT)
                {
                    case TileObjectType.Diamagnetic:
                        {
                            if(GameManager.IsHardMode)
                            {
                                TileMapCompo.SetTile(new Vector3Int(x, y), HardTile_Diamagnetic);
                                break;
                            }
                            TileMapCompo.SetTile(new Vector3Int(x, y), Tile_Diamagnetic);
                            break;
                        }
                    case TileObjectType.Ferromagnetic:
                        {
                            if (GameManager.IsHardMode)
                            {
                                TileMapCompo.SetTile(new Vector3Int(x, y), HardTile_Ferromagnetic);
                                break;
                            }
                            TileMapCompo.SetTile(new Vector3Int(x, y), Tile_Ferromagnetic);
                            break;
                        }
                    case TileObjectType.MirrorPlayer:
                        {
                            TileMapCompo.SetTile(new Vector3Int(x, y), Tile_MirrorPlayer);
                            break;
                        }
                    case TileObjectType.Paramagnetic:
                        {
                            if (GameManager.IsHardMode)
                            {
                                TileMapCompo.SetTile(new Vector3Int(x, y), HardTile_Paramagnetic);
                                break;
                            }
                            TileMapCompo.SetTile(new Vector3Int(x, y), Tile_Paramagnetic);
                            break;
                        }
                    case TileObjectType.Player:
                        {
                            TileMapCompo.SetTile(new Vector3Int(x, y), Tile_Player);
                            break;
                        }
                    default:
                        {
                            TileMapCompo.SetTile(new Vector3Int(x, y), Tile_Void);
                            break;
                        }

                }
            }
        }

        CurrentMapIndex = mapIndex;
        GameManager.Instance.TileManager.GetMapInfo(tilesData,destinationData);
    }

 #region static
    //해당 좌표가 맵 안(배열)안에 있는 좌표인지 계산하는 함수
    public static bool InMap(int rowIndex, int columnIndex)
    {
        return rowIndex >= 0 && rowIndex < MapSizeX &&
        columnIndex >= 0 && columnIndex < MapSizeY;
    }

#endregion
}
