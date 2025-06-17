using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public Slider volumeSlider;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        audioSource.volume = volumeSlider.value;

        
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }
}
