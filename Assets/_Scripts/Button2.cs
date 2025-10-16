using KSY.Manager;
using UnityEngine;

public class Button2 : MonoBehaviour
{
    public int mapIndex;

    public void OnClick()
    {
        GameManager.Instance.StartInGame(mapIndex);
    }
}
