using System;
using System.Collections.Generic;
using KSY.Pattern;
using KSY.Tile;
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

        public sbyte MapSizeX { get; private set; } = 7;
        public sbyte MapSizeY { get; private set; } = 7;

        //���� ���� �̺�Ʈ
        public event Action GameStarted;

        //������ Ÿ�ϵ��� �����ϴ� Ÿ�� �Ŵ���
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
        }
        public void StartInGame(int MapIndex)
        {
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

                //init initMatrix
                initMatrix = new GameObject[MapSizeX, MapSizeY];
                _tileMatrix = new TileObject[MapSizeX, MapSizeY];

                MapData selectMap = Maps[selectMapIndex];

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
                        TileType tileT = Targets[selectMapIndex].rows[g].colums[h];

                        //Ÿ�� Ÿ�� �迭�� Ÿ���� None�� �ƴ� ���, ������ Ÿ�Ϸ� ���.
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