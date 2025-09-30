using Unity.XR.OpenVR;

namespace KSY_Tile
{
    public class Diamagnetic : Tile
    {
        public override bool Magnetization(sbyte xDir, sbyte yDir, Tile presser)
        {
            return base.Magnetization(xDir, yDir, presser);
        }
    }
}

