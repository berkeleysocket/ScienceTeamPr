using KSY.Manager;

namespace KSY.Tile
{
    public class Paramagnetic : TileObject
    {
        public override void Magnetization(sbyte xDir, sbyte yDir, TileObject presser)
        {
            sbyte myTX = CurrentX;
            sbyte myTY = CurrentY;
            sbyte otherTX = presser.CurrentX;
            sbyte otherTY = presser.CurrentY;

            TileObject other = GameManager.Instance.TileManager.GetObject(otherTX, otherTY);
            TileObject my = GameManager.Instance.TileManager.GetObject(myTX, myTY);

            CurrentX = otherTX;
            CurrentY = otherTY;
            presser.CurrentX = myTX;
            presser.CurrentY = myTY;////////////

            GameManager.Instance.TileManager.SetObject(myTX, myTY, null);
            GameManager.Instance.TileManager.SetObject(otherTX, otherTY, null);
            GameManager.Instance.TileManager.SetObject(myTX, myTY, other);
            GameManager.Instance.TileManager.SetObject(otherTX, otherTY, my);
        }
    }
}