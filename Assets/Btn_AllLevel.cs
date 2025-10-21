using KSY.Manager;
using UnityEngine;
using UnityEngine.UI;

public class Btn_AllLevel : MonoBehaviour
{
    private void Awake()
    {

    }

    private void Start()
    {
        Button my = gameObject.GetComponent<Button>();

        my.onClick.AddListener(GameManager.Instance.OnHardMode);
    }
}
