using System;
using System.Collections.Generic;
using System.Linq;
using KSY.Pattern;
using KSY.Tile;
using Unity.VisualScripting;
using UnityEngine;

namespace KSY.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [HideInInspector] public List<MapData> Maps = new List<MapData>();
        [HideInInspector] public List<TargetsData> Targets = new List<TargetsData>();
        public int CurrentMapIndex { get; private set; } = 0;

        public GameObject[,] initMatrix;
        private TileObject[,] _tileMatrix;

        [field: SerializeField] public sbyte MapSizeX { get; private set; } = 0;
        [field: SerializeField] public sbyte MapSizeY { get; private set; } = 0;

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

            //Game Initialization
            InitGame();
        }
        private void Start()
        {
            //게임이 시작했다면 이벤트 시작
            GameStarted?.Invoke();
        }
        private void Init()
        {
            initMatrix = new GameObject[MapSizeX, MapSizeY];
            _tileMatrix = new TileObject[MapSizeX, MapSizeY];

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
        private void InitGame()
        {
            if(CurrentMapIndex > -1)
            {
                if (Maps == null || Maps[CurrentMapIndex] == null) return;

                TileObject.TileCount = 0;

                //init initMatrix
                MapData selectMap = Maps[CurrentMapIndex];

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
                TileManager.SetMap(_tileMatrix);
                TileManager.InitTilePos();
            }
        }
    }
}