using System;
using System.Collections.Generic;
using KSY.Pattern;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KSY.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public UnityEngine.Tilemaps.Tile Tile_Diamagnetic;
        public UnityEngine.Tilemaps.Tile Tile_Ferromagnetic;
        public UnityEngine.Tilemaps.Tile Tile_MirrorPlayer;
        public UnityEngine.Tilemaps.Tile Tile_Paramagnetic;
        public UnityEngine.Tilemaps.Tile Tile_Player;
        public UnityEngine.Tilemaps.Tile Tile_Void;

        [field: SerializeField]
        public List<MapDataSO> Maps { get; private set;  } = new List<MapDataSO>();
        public MainMenuReturn mainMenuReturn;

        //게임 시작 이벤트
        public event Action GameStarted;

        //게임의 타일들을 관리하는 타일 매니저
        public TileManager TileManager { get; private set; }
        public InputManager InputManager { get; private set; }
        public SceneManager SceneManager { get; private set; }
        public MapManager MapManager { get; private set; }

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

            if (MapManager == null)
            {
                MapManager = gameObject.AddComponent<MapManager>();
                MapManager.TileMapCompo = gameObject.GetComponentInChildren<Tilemap>();
                .
                MapManager.Tile_Diamagnetic = Tile_Diamagnetic;
                MapManager.Tile_Ferromagnetic = Tile_Ferromagnetic;
                MapManager.Tile_MirrorPlayer = Tile_MirrorPlayer;
                MapManager.Tile_Paramagnetic = Tile_Paramagnetic;
                MapManager.Tile_Player = Tile_Player;
                MapManager.Tile_Void = Tile_Void;
            }
        }
        public void StartMap(int index)
        {
            if (Instance == null || SceneManager == null || Maps == null || Maps[index] == null) return;
            
            //인 게임 씬이 전부 로드되었다면 맵이 생성되게 이벤트 등록.
            SceneManager.AddEvent_Loaded(SceneManager.SceneType.InGame, () => MapManager.Create(index));

            //인 게임 씬 로드.
            SceneManager.LoadScene(SceneManager.SceneType.InGame);
        }
    }
}