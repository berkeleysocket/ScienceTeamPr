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
            
            //�� ���� ���� ���� �ε�Ǿ��ٸ� ���� �����ǰ� �̺�Ʈ ���.
            SceneManager.AddEvent_Loaded(SceneManager.SceneType.InGame, () => MapManager.Create(index));

            //�� ���� �� �ε�.
            SceneManager.LoadScene(SceneManager.SceneType.InGame);
        }
    }
}