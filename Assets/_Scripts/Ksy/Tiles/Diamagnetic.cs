namespace KSY.Tile
{
    public class Diamagnetic : TileObject
    {
        public override void Magnetization(sbyte xDir, sbyte yDir, TileObject presser)
        {
            UnityEngine.Debug.Log("Diamagnetic Move");
            Move(xDir, yDir);
        }
    }
}

