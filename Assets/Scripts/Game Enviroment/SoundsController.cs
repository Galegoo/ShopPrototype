using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundsController : MonoBehaviour
{
    public AudioSource trilha;
    public bool controlFlag;
    public GameObject sounds;
    public GameObject soundsButton;
    public AudioSource baloonAudio;
    int counter;

    private static SoundsController instance = null;


    public static SoundsController Instance
    {
        get { return instance; }
    }

    void Start()
    {
        controlFlag = true; 
    }
    void Update()
    {
        if (controlFlag == false)
        {
            trilha.mute = true;
        }
        else
        {
            trilha.mute = false;
        }
    }
    void Awake()
    {
        trilha = GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            if (instance.trilha.clip != trilha.clip)
            {
                instance.trilha.clip = trilha.clip;
                instance.trilha.volume = trilha.volume;
                instance.trilha.Play();
            }

            Destroy(this.gameObject);
            return;
        }
        instance = this;

        trilha.Play();

        DontDestroyOnLoad(this.gameObject);
    }

    public void Mute()
    {
        if (counter < 1)
        {
            controlFlag = false;
            sounds.SetActive(false);
            baloonAudio.enabled = false;
            soundsButton.GetComponent<Image>().enabled = false;
            soundsButton.transform.GetChild(0).gameObject.SetActive(true);
            counter++;
        }
        else
            UnMute();

    }
    public void UnMute()
    {
        controlFlag = true;
        sounds.SetActive(true);
        baloonAudio.enabled = true;
        soundsButton.GetComponent<Image>().enabled = true;
        soundsButton.transform.GetChild(0).gameObject.SetActive(false);
        counter = 0;
    }
}
