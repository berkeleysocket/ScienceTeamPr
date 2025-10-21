namespace KSY.Tile
{
    public class Diamagnetic : TileObject
    {
        public override void Magnetization(int xDir, int yDir, TileObject presser)
        {
            base.Magnetization(xDir, yDir, presser);
            Move(xDir, yDir);
        }
    }
}

