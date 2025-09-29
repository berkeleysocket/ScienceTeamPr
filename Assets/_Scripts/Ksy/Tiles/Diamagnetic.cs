using Unity.XR.OpenVR;

namespace KSY_Tile
{
    public class Diamagnetic : Tile
    {
        public override bool Magnetization(sbyte xDir, sbyte yDir, Tile presser)
        {
            UnityEngine.Debug.Log($"<color=white>{presser}</color>");
            return base.Magnetization(xDir, yDir, presser);
        }
    }
}

