using UnityEngine;
using UnityEngine.UI;

public class UiMuteButton : MonoBehaviour
{
    public Button yourButton;
    [SerializeField] SoundsController music;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        music.Mute();
    }
}
