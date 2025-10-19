using KSY.Manager;

namespace KSY.Tile
{
    public class Paramagnetic : TileObject
    {
        public override void Magnetization(int xDir, int yDir, TileObject presser)
        {
            int myTX = CurrentX;
            int myTY = CurrentY;
            int otherTX = presser.CurrentX;
            int otherTY = presser.CurrentY;

            TileObject other = GameManager.Instance.TileManager.GetObject(otherTX, otherTY);
            TileObject my = GameManager.Instance.TileManager.GetObject(myTX, myTY);

            GameManager.Instance.TileManager.SetNull(myTX, myTY);
            GameManager.Instance.TileManager.SetNull(otherTX, otherTY);
            GameManager.Instance.TileManager.SetObject(myTX, myTY, other);
            GameManager.Instance.TileManager.SetObject(otherTX, otherTY, my);
        }
    }
}