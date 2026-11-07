using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    private static SFXManager Instance;
    private static AudioSource audioSource;
    private static SFXLibrary sfxLibrary;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            sfxLibrary = GetComponent<SFXLibrary>();
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName)
    {
        AudioClip audioClip = sfxLibrary.GetRandomClip(soundName);
        if(audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }


    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate{OnValueChanged();});
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }
}
