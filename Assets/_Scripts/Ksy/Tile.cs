using KSY_Manager;
using UnityEngine;

namespace KSY_Tile
{
    public abstract class Tile : MonoBehaviour
    {
        [field: SerializeField] public sbyte InitX { get; private set; } = 0;
        [field: SerializeField] public sbyte InitY { get; private set; } = 0;
        public sbyte CurrentX => (sbyte)transform.position.x;
        public sbyte CurrentY => (sbyte)transform.position.y;
        [field: SerializeField] public TileType Type { get; private set; } = TileType.None;

        #region UnityEngine
        private void OnValidate()
        {
            transform.position = new Vector2((float)InitX + 0.5f, (float)InitY + 0.5f);
        }
        #endregion
        public virtual bool Magnetization(sbyte xDir, sbyte yDir, Tile presser)
        {
            Debug.Log("자화 현상");
            Debug.Log($"{gameObject.name} : {xDir} , {yDir}");
            return Move(xDir, yDir);
        }
        protected virtual bool Move(sbyte xDir, sbyte yDir)
        {
            int _x = CurrentX + xDir;
            int _y = CurrentY + yDir;
            sbyte x = (sbyte)_x;
            sbyte y = (sbyte)_y;


            //맵의 경계를 벗어났다면 return하기.
            if (!TileManager.InMap(x, y)) { return false; }

            //해당 좌표에 다른 타일이 있는지 확인하기.
            Tile? destinationTile = GameManager.Instance.TileManager.GetObject(x, y);

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

