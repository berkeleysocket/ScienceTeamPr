using KSY.Manager;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerDataSO", menuName = "Scriptable Objects/GameManagerDataSO")]
public class GameManagerDataSO : ScriptableObject
{
    [HideInInspector]
    public GameManager instance = null;

    private void OnEnable()
    {
        instance = GameManager.Instance;
    }
    public void ResetMap()
    {
        instance.StartMap(MapManager.CurrentMapIndex);
    }
}
