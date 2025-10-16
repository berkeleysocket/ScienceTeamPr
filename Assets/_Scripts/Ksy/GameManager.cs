using System;
using System.Collections.Generic;
using KSY.Pattern;
using KSY.Tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KSY.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public List<MapDataSO> Maps = new List<MapDataSO>();
        public int CurrentMapIndex { get; private set; } = 0;

        public GameObject[,] initMatrix;
        private TileObject[,] _tileMatrix;
        public MainMenuReturn mainMenuReturn;
        public sbyte MapSizeX { get; private set; } = 7;
        public sbyte MapSizeY { get; private set; } = 7;

        //���� ���� �̺�Ʈ
        public event Action GameStarted;

        //������ Ÿ�ϵ��� �����ϴ� Ÿ�� �Ŵ���
        public TileManager TileManager { get; private set; }
        public InputManager InputManager { get; private set; }
        public SceneManager SceneManager { get; private set; }
        
        public UnityEngine.Tilemaps.Tile Tile_Diamagnetic;
        public UnityEngine.Tilemaps.Tile Tile_Ferromagnetic;
        public UnityEngine.Tilemaps.Tile Tile_MirrorPlayer;
        public UnityEngine.Tilemaps.Tile Tile_Paramagnetic;
        public UnityEngine.Tilemaps.Tile Tile_Player;
        public UnityEngine.Tilemaps.Tile Tile_Void;

        public Tilemap _tilemap;


        private void Awake()
        {
            base.Awake();
            //GameManager Initialization
            Init();
        }
        private void Start()
        {
            //������ �����ߴٸ� �̺�Ʈ ����
            GameStarted?.Invoke();
        }
        private void Init()
        {
            if (TileManager == null)
            {
                TileManager = new TileManager();
            }

            if(SceneManager == null)
            {
                SceneManager = new SceneManager();
            }

            if (InputManager == null)
                InputManager = gameObject.AddComponent<InputManager>();

            if (_tilemap == null)
                _tilemap = gameObject.GetComponentInChildren<Tilemap>();
        }
        public void StartInGame(int MapIndex)
        {
            if (Instance == null || SceneManager == null) return;
            SceneManager.AddEvent_Loaded(SceneManager.SceneType.Ksy_InGame, () => SettingMap(MapIndex));
            SceneManager.LoadScene(SceneManager.SceneType.Ksy_InGame);
        }
        public void SettingMap(int selectMapIndex)
        {
            this.CurrentMapIndex = selectMapIndex;

            if (selectMapIndex > -1)
            {
                if (Maps == null || Maps[selectMapIndex] == null) return;

                TileObject.TileCount = 0;
                MapDataSO currentMap = Maps[selectMapIndex];
                sbyte sizeX = currentMap.MapSizeX;
                sbyte sizeY = currentMap.MapSizeY;

                //init initMatrix
                initMatrix = new GameObject[sizeX, sizeY];
                _tileMatrix = new TileObject[sizeX, sizeY];

                MapData selectMap = currentMap.Map;

                for(int g = MapSizeY - 1; 0 <= g ; g--)
                {
                    for(int h = MapSizeX - 1; 0 <= h; h--)
                    {
                        GameObject tile = selectMap.rows[g].colums[h];

                        if(tile != null)
                        {
                            GameObject tileBody = Instantiate(tile);

                            sbyte initY = (sbyte)(MapSizeY - 1 - g);
                            sbyte initX = (sbyte)h;

                            if (!tileBody.TryGetComponent(out TileObject tileSc)) return;

                            tileSc.InitY = initY;
                            tileSc.InitX = initX;

                            initMatrix[initX, initY] = tileBody;
                            _tileMatrix[initX, initY] = tileSc;

                            tileSc.InitPos();
                        }
                    }
                }

                TileManager.ResultPoints.Clear();
                for (int g = MapSizeY - 1; 0 <= g; g--)
                {
                    for (int h = MapSizeX - 1; 0 <= h; h--)
                    {
                        TileType tileT = currentMap.Targets.rows[g].colums[h];
                        sbyte y = (sbyte)(MapSizeY - 1 - g);
                        sbyte x = (sbyte)h;

                        //Ÿ�� Ÿ�� �迭�� Ÿ���� None�� �ƴ� ���, ������ Ÿ�Ϸ� ���.
                        if (tileT != TileType.None)
                        {
                            Debug.Log($"<color=green>Result Point : ({x},{y}) T : {tileT}</color>");
                            TileManager.ResultPoints.Add(new ResultPoint(x, y, tileT));
                        }

                        switch (tileT)
                        {
                            case TileType.Diamagnetic:
                                {
                                    _tilemap.SetTile(new Vector3Int(x, y), Tile_Diamagnetic);
                                    break;
                                }
                            case TileType.Ferromagnetic:
                                {
                                    _tilemap.SetTile(new Vector3Int(x, y), Tile_Ferromagnetic);
                                    break;
                                }
                            case TileType.MirrorPlayer:
                                {
                                    _tilemap.SetTile(new Vector3Int(x, y), Tile_MirrorPlayer);
                                    break;
                                }
                            case TileType.Paramagnetic:
                                {
                                    _tilemap.SetTile(new Vector3Int(x, y), Tile_Paramagnetic);
                                    break;
                                }
                            case TileType.Player:
                                {
                                    _tilemap.SetTile(new Vector3Int(x, y), Tile_Player);
                                    break;
                                }
                            default:
                                {
                                    _tilemap.SetTile(new Vector3Int(x, y), Tile_Void);
                                    break;
                                }

                        }
                    }
                }


                TileManager.InitMap(_tileMatrix);
                TileManager.InitTilePos();
            }
        }
    }
}