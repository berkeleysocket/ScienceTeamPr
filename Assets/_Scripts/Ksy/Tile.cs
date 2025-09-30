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
            Debug.Log("��ȭ ����");
            Debug.Log($"{gameObject.name} : {xDir} , {yDir}");
            return Move(xDir, yDir);
        }
        protected virtual bool Move(sbyte xDir, sbyte yDir)
        {
            int _x = CurrentX + xDir;
            int _y = CurrentY + yDir;
            sbyte x = (sbyte)_x;
            sbyte y = (sbyte)_y;


            //���� ��踦 ����ٸ� return�ϱ�.
            if (!TileManager.InMap(x, y)) { return false; }

            //�ش� ��ǥ�� �ٸ� Ÿ���� �ִ��� Ȯ���ϱ�.
            Tile? destinationTile = GameManager.Instance.TileManager.GetObject(x, y);

            //���� Ÿ���� ���ٸ� �ش� ��ǥ�� �̵���Ű��.
            if (destinationTile == null)
            {
                GameManager.Instance.TileManager.SetObject(x, y, this);
            }
            //���� Ÿ���� �ִٸ� ��ȭ ������ �ߵ���Ű��.
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
        Ferromagnetic,//���ڼ�ü
        Paramagnetic,//���ڼ�ü
        Diamagnetic//���ڼ�ü
    }
    #endregion
}

