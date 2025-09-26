using System;
using KSY_Pattern;
using KSY_Tile;
using UnityEngine;

namespace KSY_Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [Header("Create Tiles")]
        [SerializeField] private Tile[] TilePrefabs;

        //게임 시작 이벤트
        public event Action GameStarted;

        //게임의 타일들을 관리하는 타일 매니저
        private TileManager _tileManager;

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
            _tileManager = new TileManager();
        }
        private void InitGame()
        {
            foreach (Tile element in TilePrefabs)
            {
                Tile tile = Instantiate(element);

                sbyte x = tile.InitX;
                sbyte y = tile.InitY;

                _tileManager.SetObject(x, y, tile);
            }
        }
    }
}