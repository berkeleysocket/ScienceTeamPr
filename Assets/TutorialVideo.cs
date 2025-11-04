using UnityEngine;
using UnityEngine.Video;

public class TutorialVideo : MonoBehaviour
{
    [SerializeField] private VideoClip[] _tutorialVideo;

    private VideoPlayer _vp;

    private void Awake()
    {
        _vp = GetComponent<VideoPlayer>();
    }
    private void Start()
    {
        Debug.Log($"<color=blue>CurrentMapIndex : {MapManager.CurrentMapIndex}</color>");
        if (MapManager.CurrentMapIndex < 2)
        {
            _vp.clip = _tutorialVideo[MapManager.CurrentMapIndex];
            GameObject.Find("Canvas/Tutorial").SetActive(true);
        }
        else
        {
            GameObject.Find("Canvas/Tutorial").SetActive(false);
        }
    }
}
