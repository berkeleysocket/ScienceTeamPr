using System;
using System.Collections.Generic;
using KSY.Pattern;
using KSY.Tile;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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

        public UnityEngine.Tilemaps.Tile HardTile_Diamagnetic;
        public UnityEngine.Tilemaps.Tile HardTile_Ferromagnetic;
        public UnityEngine.Tilemaps.Tile HardTile_Paramagnetic;

        [field: SerializeField]
        public List<MapDataSO> Maps { get; private set;  } = new List<MapDataSO>();
        public MainMenuReturn mainMenuReturn;

        //오디오 소스
        public AudioSource src;

        public static bool IsHardMode = false;
        public void OnHardMode()
        {
            GameObject hardModeBtn = GameObject.Find("Canvas/TitleMenu/Btn_AllLevel");

            TextMeshProUGUI text = hardModeBtn.GetComponentInChildren<TextMeshProUGUI>();

            if (!IsHardMode)
            {
                text.text = "Hard";
                hardModeBtn.GetComponent<Image>().color = Color.red;
                text.color = Color.white;
            }
            else if (IsHardMode)
            {
                text.text = "Normal";
                hardModeBtn.GetComponent<Image>().color = Color.white;
                text.color = Color.black;
            }

            IsHardMode = !IsHardMode;
            Debug.Log($"IsHardMode : {IsHardMode}");
        }

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
            Init_Element();
        }
        private void Start()
        {
            //게임이 시작했다면 이벤트 시작
            GameStarted?.Invoke();
        }
        private void Init_Element()
        {
            if (SceneManager == null)
            {
                SceneManager = new SceneManager();
            }

            if (TileManager == null)
            {
                TileManager = new TileManager();
            }

            if (InputManager == null)
                InputManager = gameObject.AddComponent<InputManager>();

            if (MapManager == null)
            {
                MapManager = gameObject.AddComponent<MapManager>();
                MapManager.TileMapCompo = gameObject.GetComponentInChildren<Tilemap>();

                MapManager.Tile_Diamagnetic = Tile_Diamagnetic;
                MapManager.Tile_Ferromagnetic = Tile_Ferromagnetic;
                MapManager.Tile_MirrorPlayer = Tile_MirrorPlayer;
                MapManager.Tile_Paramagnetic = Tile_Paramagnetic;
                MapManager.Tile_Player = Tile_Player;
                MapManager.Tile_Void = Tile_Void;

                MapManager.HardTile_Diamagnetic = HardTile_Diamagnetic;
                MapManager.HardTile_Ferromagnetic = HardTile_Ferromagnetic;
                MapManager.HardTile_Paramagnetic = HardTile_Paramagnetic;
            }

            if (src == null)
            {
                src = gameObject.AddComponent<AudioSource>();
            }
        }
        public void StartMap(int index)
        {
            if (Instance == null || SceneManager == null || Maps == null || Maps[index] == null) return;
            
            //인 게임 씬이 전부 로드되었다면 맵이 생성되게 이벤트 등록.
            SceneManager.AddEvent_Loaded(SceneManager.SceneType.InGame, () => MapManager.Create(index));
            //인 게임 씬이 끝났다면 타일맵 지우게 이벤트 등록
            SceneManager.AddEvent_Exit(SceneManager.SceneType.InGame, () => MapManager.TileMapCompo.ClearAllTiles());

            //인 게임 씬 로드.
            SceneManager.LoadScene(SceneManager.SceneType.InGame);
        }
    }
}