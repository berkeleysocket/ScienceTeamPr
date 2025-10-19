using System.ComponentModel;
using KSY.Manager;
using UnityEngine;

namespace KSY.Tile
{
    public abstract class TileObject : MonoBehaviour
    {
        public static int TileCount = 0;
        [field: SerializeField] public int Id { get; private set; } = -1;

        public int InitX
        {
            get => _initX;
            set
            {
                if (value < 0) _initX = 0;
                else if (value >= MapManager.MapSizeX) _initX = MapManager.MapSizeX - 1;
                else _initX = value;
            }
        }
        private int _initX;

        public int InitY
        {
            get => _initY;
            set
            {
                if (value < 0) _initY = 0;
                else if (value >= MapManager.MapSizeY) _initY = MapManager.MapSizeY - 1;
                else _initY = value;
            }
        }
        private int _initY;

        public int CurrentX
        {
            get => _currentX;
            set
            {
                if (value < 0) _currentX = 0;
                else if (value >= MapManager.MapSizeX) _currentX = MapManager.MapSizeX - 1;
                else
                {
                    _currentX = value;
                }
            }
        }
        private int _currentX;

        public int CurrentY
        {
            get => _currentY;
            set
            {
                if (value < 0) _currentY = 0;
                else if (value >= MapManager.MapSizeY) _currentY = MapManager.MapSizeY - 1;
                else _currentY = value;
            }
        }
        private int _currentY;

        [field: SerializeField] public TileObjectType Type { get; private set; } = TileObjectType.None;
        public void InitPos()
        {
            Id = TileCount++;

            CurrentX = InitX;
            CurrentY = InitY;
        }
        public abstract void Magnetization(int xDir, int yDir, TileObject presser);
        protected virtual bool Move(int xDir, int yDir)
        {
            int x = CurrentX + xDir;
            int y = CurrentY + yDir;

            //���� ��踦 ����ٸ� return�ϱ�.
            if (!MapManager.InMap(x, y)) { return false; }

            //�ش� ��ǥ�� �ٸ� Ÿ���� �ִ��� Ȯ���ϱ�.
            TileObject? destinationTile = GameManager.Instance.TileManager.GetObject(x, y);

            //���� Ÿ���� ���ٸ� �ش� ��ǥ�� �̵���Ű��.
            if (destinationTile == null)
            {
                GameManager.Instance.TileManager.SetObject(x, y, this);
            }
            //���� Ÿ���� �ִµ� �� Ÿ���� �ƴ϶�� ��ȭ ������ �ߵ���Ű��.
            else if(destinationTile.Type != TileObjectType.Wall)
            {
                destinationTile.Magnetization(xDir, yDir, this);
            }

            return true;
        }
    }
    #region enum
    public enum TileObjectType
    {
        None = 0,
        Player,//�÷��̾�
        MirrorPlayer,//�ſ� �÷��̾�
        Ferromagnetic,//���ڼ�ü
        Paramagnetic,//���ڼ�ü
        Diamagnetic,//���ڼ�ü
        Wall//��
    }
    #endregion
}

