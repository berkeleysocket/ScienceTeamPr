using KSY.Manager;
using KSY.Tile;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static int CurrentMapIndex { get; private set; } = 0;
    public static int MapSizeX { get; private set; }
    public static int MapSizeY { get; private set; }

    //Ÿ�ϸ� Ÿ�� ����
    public UnityEngine.Tilemaps.Tile Tile_Diamagnetic;
    public UnityEngine.Tilemaps.Tile Tile_Ferromagnetic;
    public UnityEngine.Tilemaps.Tile Tile_MirrorPlayer;
    public UnityEngine.Tilemaps.Tile Tile_Paramagnetic;
    public UnityEngine.Tilemaps.Tile Tile_Player;
    public UnityEngine.Tilemaps.Tile Tile_Void;

    public Tilemap TileMapCompo;

    public void Create(int mapIndex)
    {
        //������ �ε����� ���� �ҷ���
        MapDataSO data = GameManager.Instance.Maps[mapIndex];

        //���� �ҷ������� �ʾҴٸ� return
        if (data == null) return;

        //���� ����� ������
        int sizeX = data.SizeX;
        int sizeY = data.SizeY;

        //���� Ÿ�� ��ġ���� ������
        TileMatrix ref_tiles = data.tiles;

        //���� Ŭ���� Ÿ�� ��ġ���� ������(�ϼ���)
        DestinationMatrix ref_destinations = data.destinations;

        //TileManager�� �Ѱ��� �� �����͸� ���� ����
        TileObject[,] tilesData = new TileObject[sizeX, sizeY];
        TileObjectType[,] destinationData = new TileObjectType[sizeX,sizeY];

        for (int g = sizeY - 1; 0 <= g; g--)
        {
            for (int h = sizeX - 1; 0 <= h; h--)
            {
                GameObject tile = ref_tiles.rows[g].colums[h];

                //�� ��� ���� ��� �˻��ϴٰ� ���� Ÿ���� null�� �ƴ϶�� Ÿ���� ������.
                if (tile != null)
                {
                    GameObject tileBody = Instantiate(tile);

                    //Ÿ���� �ʱ�ȭ ��ġ�� �����
                    int initY = sizeY - 1 - g;
                    int initX = h;

                    if (!tileBody.TryGetComponent(out TileObject tileSc)) return;

                    //Ÿ���� �ʱ�ȭ ��ġ
                    tileSc.InitY = initY;
                    tileSc.InitX = initX;
                    tileSc.InitPos();

                    //������ ���
                    tilesData[initX,initY] = tileSc;
                }
            }
        }

        for (int g = sizeY - 1; 0 <= g; g--)
        {
            for (int h = sizeX - 1; 0 <= h; h--)
            {
                //�� ��� ���� Ÿ�� Ÿ���� �˻���.
                TileObjectType tileT = ref_destinations.rows[g].colums[h];

                //Ÿ���� ��ġ�� �����.
                sbyte x = (sbyte)h;
                sbyte y = (sbyte)(sizeY - 1 - g);

                //Ÿ�� Ÿ�� �迭�� Ÿ���� None�� �ƴ� ���, ������ Ÿ�Ϸ� ���.
                if (tileT != TileObjectType.None)
                {
                    Debug.Log($"<color=green>Result Point : ({x},{y}) T : {tileT}</color>");
                    destinationData[x, y] = tileT;
                }

                switch (tileT)
                {
                    case TileObjectType.Diamagnetic:
                        {
                            TileMapCompo.SetTile(new Vector3Int(x, y), Tile_Diamagnetic);
                            break;
                        }
                    case TileObjectType.Ferromagnetic:
                        {
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

        TileManager.GetMapInfo(tilesData,destinationData);
    }

 #region static
    //�ش� ��ǥ�� �� ��(�迭)�ȿ� �ִ� ��ǥ���� ����ϴ� �Լ�
    public static bool InMap(int rowIndex, int columnIndex)
    {
        return rowIndex >= 0 && rowIndex < MapSizeX &&
        columnIndex >= 0 && columnIndex < MapSizeY;
    }

#endregion
}
