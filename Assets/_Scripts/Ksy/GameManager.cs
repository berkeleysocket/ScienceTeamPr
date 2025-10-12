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

        //���� ���� �̺�Ʈ
        public event Action GameStarted;

        //������ Ÿ�ϵ��� �����ϴ� Ÿ�� �Ŵ���
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
            //������ �����ߴٸ� �̺�Ʈ ����
            GameStarted?.Invoke();
        }
        private void Init()
        {
            TileManager = new TileManager();
            InputManager = gameObject.AddComponent<InputManager>();

            //Init TileMatrix
            //�ν����Ϳ��� �� ���� �������� return
            if (initMatrix == null || initMatrix.Length == 0) return;
            foreach(GameObject go in initMatrix)
            {
                //null�� �ƴϰ� TileObject�� �پ��ִٸ� tileMatrix�� �´� ��ġ�� �Ҵ�
                if (go != null && go.TryGetComponent(out TileObject tileSc))
                {
                    sbyte x = tileSc.InitX;
                    sbyte y = tileSc.InitY;
                    _tileMatrix[x,y] = tileSc;
                }
            }

            //�� ���� ���ο� ���� ����
            MapSizeX = (sbyte)initMatrix.GetLength(0);
            MapSizeY = (sbyte)initMatrix.GetLength(1);
        }
        private void InitGame()
        {
            TileManager.StartSet(_tileMatrix);
        }
    }
}