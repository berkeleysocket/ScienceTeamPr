namespace KSY_Tile
{
    public class Paramagnetic : Tile
    {
        public override bool Magnetization(sbyte xDir, sbyte yDir, Tile presser)
        {
            return base.Magnetization(xDir, yDir, this);
        }
    }
}