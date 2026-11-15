using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip bgm;
    [SerializeField] private Slider bgmSlider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != this) return;
       
        if(bgm != null)
        {
          playBGM(false, bgm);  
        }
        
        if(bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(delegate{setVolume(bgmSlider.value);});
        }
    }

    public static void setVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }

    public void playBGM(bool resetMusic, AudioClip audioClip = null)
    {
        if(audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        
        if (audioSource.clip != null)
        {
            if(resetMusic)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }

    public void pauseBGM()
    {
        audioSource.Pause();
    }
}
