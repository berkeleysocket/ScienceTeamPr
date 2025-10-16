using KSY.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManagerDataSO", menuName = "Scriptable Objects/GameManagerDataSO")]
public class GameManagerDataSO : ScriptableObject
{
    [HideInInspector]
    public GameManager instance = null;

    private void OnEnable()
    {
        instance = GameManager.Instance;
    }
    public void SettingMap(int selectMapIndex)
    {
        instance.SettingMap(selectMapIndex);
    }
    public void ResetMap()
    {
        instance.StartInGame(instance.CurrentMapIndex);
    }
}
