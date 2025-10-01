using System;
using KSY.Tile;
using KSY.Pattern;
using UnityEngine;

namespace KSY.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [Header("Create Tiles")]
        [SerializeField] private TileObject[] Tiles = new TileObject[25];

        //���� ���� �̺�Ʈ
        public event Action GameStarted;

        //������ Ÿ�ϵ��� �����ϴ� Ÿ�� �Ŵ���
        public TileManager TileManager { get; private set; }
        public InputManager InputManager { get; private set; }

        private void Awake()
        {
            Init();
            InitGame();
        }
        private void Start()
        {
            GameStarted?.Invoke();
        }
        private void Init()
        {
            TileManager = new TileManager();
            InputManager = gameObject.AddComponent<InputManager>();
        }
        private void InitGame()
        {
            TileManager.StartSet(Tiles);
        }
    }
}