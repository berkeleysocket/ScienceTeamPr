using UnityEngine;
using DG.Tweening;
using KSY.Manager;

public class MainMenuReturn : MonoBehaviour
{
    public GameObject mainMenuUI;
    public void Cleared()
    {
        mainMenuUI.GetComponent<RectTransform>().DOAnchorPosY(0, 0.4f).SetEase(Ease.OutExpo);
    }
    public void ReturnToMainMenu()
    {
        GameManager.Instance.SceneManager.LoadScene(SceneManager.SceneType.MainMenu);
        mainMenuUI.GetComponent<RectTransform>().DOAnchorPosY(2000, 0.4f).SetEase(Ease.OutExpo);
    }
}
