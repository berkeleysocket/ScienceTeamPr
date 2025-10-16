using System;
using System.Collections.Generic;
using KSY.Pattern;
using KSY.Tile;
using UnityEngine;

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

        //게임 시작 이벤트
        public event Action GameStarted;

        //게임의 타일들을 관리하는 타일 매니저
        public TileManager TileManager { get; private set; }
        public InputManager InputManager { get; private set; }
        public SceneManager SceneManager { get; private set; }


        private void Awake()
        {
            base.Awake();
            //GameManager Initialization
            Init();
        }
        private void Start()
        {
            //게임이 시작했다면 이벤트 시작
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

                            Debug.Log($"{tile.name} : {initX}, {initY}");

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

                        //타겟 타일 배열의 타일이 None이 아닐 경우, 목적지 타일로 기록.
                        if (tileT != TileType.None)
                        {
                            sbyte y = (sbyte)(MapSizeY - 1 - g);
                            sbyte x = (sbyte)h;

                            Debug.Log($"<color=green>Result Point : ({x},{y}) T : {tileT}</color>");
                            TileManager.ResultPoints.Add(new ResultPoint(x, y, tileT));
                        }
                    }
                }


                TileManager.InitMap(_tileMatrix);
                TileManager.InitTilePos();
            }
        }
    }
}