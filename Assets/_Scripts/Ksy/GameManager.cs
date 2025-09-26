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

        //���� ���� �̺�Ʈ
        public event Action GameStarted;

        //������ Ÿ�ϵ��� �����ϴ� Ÿ�� �Ŵ���
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