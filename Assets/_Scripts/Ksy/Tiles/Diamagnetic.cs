namespace KSY.Tile
{
    public class Diamagnetic : TileObject
    {
        public override void Magnetization(sbyte xDir, sbyte yDir, TileObject presser)
        {
            Move(xDir, yDir);
        }
    }
}

