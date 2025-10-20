using KSY.Manager;
using KSY.UI;

public class Btn_Retry : UI
{
    public void Reetry()
    {
        GameManager.Instance.StartMap(MapManager.CurrentMapIndex);
    }
}
