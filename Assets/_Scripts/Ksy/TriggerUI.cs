using KSY.UI;
using System;

public class TriggerUI : UI
{
    public event Action OnShowing;
    public event Action OnHide;
    private void Awake()
    {
        //hide on start
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        OnShowing?.Invoke();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        OnHide?.Invoke();
    }   
}
