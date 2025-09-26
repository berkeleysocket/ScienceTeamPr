using UnityEngine;

namespace KSY_Tile
{
    public abstract class Tile : MonoBehaviour
    {
        [field: SerializeField] public sbyte InitX { get; private set; } = 0;
        [field: SerializeField] public sbyte InitY { get; private set; } = 0;
        [SerializeField] TileType type;
    }

    #region enum
    public enum TileType
    {
        None = 0,
        MagneticMaterial
    }
    #endregion
}

