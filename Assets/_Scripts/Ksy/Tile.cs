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
        //private void OnValidate()
        //{
        //    transform.position = new Vector2(InitX, InitY);
        //}
        #endregion
        public virtual void Magnetization()
        {
            Debug.Log("��ȭ ����");
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

