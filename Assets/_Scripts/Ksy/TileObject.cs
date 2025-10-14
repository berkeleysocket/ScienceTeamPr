using System.ComponentModel;
using KSY.Manager;
using UnityEngine;

namespace KSY.Tile
{
    public class Target : MonoBehaviour
    {

    }
    public abstract class TileObject : MonoBehaviour
    {
        public sbyte InitX
        {
            get => _initX;
            set
            {
                if (value < 0) _initX = 0;
                else if (value >= GameManager.Instance.MapSizeX) _initX = (sbyte)(GameManager.Instance.MapSizeX - 1);
                else _initX = value;
            }
        }
        private sbyte _initX;

        public sbyte InitY
        {
            get => _initY;
            set
            {
                if (value < 0) _initY = 0;
                else if (value >= GameManager.Instance.MapSizeY) _initY = (sbyte)(GameManager.Instance.MapSizeY - 1);
                else _initY = value;
            }
        }
        private sbyte _initY;

        public sbyte CurrentX
        {
            get => _currentX;
            set
            {
                if (value < 0) _currentX = 0;
                else if (value >= GameManager.Instance.MapSizeX) _currentX = (sbyte)(GameManager.Instance.MapSizeX - 1);
                else
                {
                    Debug.Log($"CurrentX : {value}");
                    _currentX = value;
                }
            }
        }
        private sbyte _currentX;

        [field: SerializeField]
        public sbyte CurrentY
        {
            get => _currentY;
            set
            {
                if (value < 0) _currentY = 0;
                else if (value >= GameManager.Instance.MapSizeY) _currentY = (sbyte)(GameManager.Instance.MapSizeY - 1);
                else _currentY = value;
            }
        }
        private sbyte _currentY;

        [field: SerializeField] public TileType Type { get; private set; } = TileType.None;
        public void InitPos()
        {
            CurrentX = InitX;
            CurrentY = InitY;
        }
        public abstract void Magnetization(sbyte xDir, sbyte yDir, TileObject presser);
        protected virtual bool Move(sbyte xDir, sbyte yDir)
        {
            int _x = CurrentX + xDir;
            int _y = CurrentY + yDir;
            sbyte x = (sbyte)_x;
            sbyte y = (sbyte)_y;


            //맵의 경계를 벗어났다면 return하기.
            if (!TileManager.InMap(x, y)) { return false; }

            //해당 좌표에 다른 타일이 있는지 확인하기.
            TileObject? destinationTile = GameManager.Instance.TileManager.GetObject(x, y);

            //만약 타일이 없다면 해당 좌표로 이동시키기.
            if (destinationTile == null)
            {
                GameManager.Instance.TileManager.SetObject(x, y, this);
            }
            //만약 타일이 있다면 자화 현상을 발동시키기.
            else
            {
                destinationTile.Magnetization(xDir, yDir, this);
            }

            return true;
        }
    }
    #region enum
    public enum TileType
    {
        None = 0,
        Player,
        Ferromagnetic,//강자성체
        Paramagnetic,//상자성체
        Diamagnetic//반자성체
    }
    #endregion
}

