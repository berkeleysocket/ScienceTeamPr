using System;
using System.Collections.Generic;
using KSY.Pattern;
using KSY.Tile;
using UnityEngine;

namespace KSY.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public int TestNum = 1;

        public GameObject[,] initMatrix;
        private TileObject[,] _tileMatrix;

        public sbyte MapSizeX { get; private set; }
        public sbyte MapSizeY { get; private set; }

        //게임 시작 이벤트
        public event Action GameStarted;

        //게임의 타일들을 관리하는 타일 매니저
        public TileManager TileManager { get; private set; }
        public InputManager InputManager { get; private set; }

        private void Awake()
        { 
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
            TileManager = new TileManager();
            InputManager = gameObject.AddComponent<InputManager>();

            //Init TileMatrix
            //인스펙터에서 맵 제작 안했으면 return
            if (initMatrix == null || initMatrix.Length == 0) return;
            foreach(GameObject go in initMatrix)
            {
                //null이 아니고 TileObject가 붙어있다면 tileMatrix에 맞는 위치에 할당
                if (go != null && go.TryGetComponent(out TileObject tileSc))
                {
                    sbyte x = tileSc.InitX;
                    sbyte y = tileSc.InitY;
                    _tileMatrix[x,y] = tileSc;
                }
            }

            //맵 가로 세로열 길이 저장
            MapSizeX = (sbyte)initMatrix.GetLength(0);
            MapSizeY = (sbyte)initMatrix.GetLength(1);
        }
        private void InitGame()
        {
            TileManager.StartSet(_tileMatrix);
        }
    }
}