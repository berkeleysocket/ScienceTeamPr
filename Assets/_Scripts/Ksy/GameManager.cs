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

        //����� �ҽ�
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

        //���� ���� �̺�Ʈ
        public event Action GameStarted;

        //������ Ÿ�ϵ��� �����ϴ� Ÿ�� �Ŵ���
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
            //������ �����ߴٸ� �̺�Ʈ ����
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
            
            //�� ���� ���� ���� �ε�Ǿ��ٸ� ���� �����ǰ� �̺�Ʈ ���.
            SceneManager.AddEvent_Loaded(SceneManager.SceneType.InGame, () => MapManager.Create(index));
            //�� ���� ���� �����ٸ� Ÿ�ϸ� ����� �̺�Ʈ ���
            SceneManager.AddEvent_Exit(SceneManager.SceneType.InGame, () => MapManager.TileMapCompo.ClearAllTiles());

            //�� ���� �� �ε�.
            SceneManager.LoadScene(SceneManager.SceneType.InGame);
        }
    }
}