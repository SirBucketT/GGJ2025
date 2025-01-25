using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundSetter : MonoBehaviour
{
    
    [Header("Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;
    
    [Header("UI Sliders")]
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider effectsVolumeSlider;
    
    private const string MusicVolumeParam = "MusicVolume";
    private const string EffectsVolumeParam = "EffectsVolumeParam";
    
    void Start()
    {
        float defaultVolume = 0f;  
        float musicVol  = PlayerPrefs.GetFloat(MusicVolumeParam, defaultVolume);
        float sfxVol    = PlayerPrefs.GetFloat(EffectsVolumeParam, defaultVolume);
        
        musicVolumeSlider.value = musicVol;
        effectsVolumeSlider.value   = sfxVol;
        
        audioMixer.SetFloat(MusicVolumeParam,  musicVol);
        audioMixer.SetFloat(EffectsVolumeParam,    sfxVol);
        
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        
        void SetMusicVolume(float value)
        {
            audioMixer.SetFloat(MusicVolumeParam, value);
            PlayerPrefs.SetFloat(MusicVolumeParam, value);
        }
        
        void SetSfxVolume(float value)
        {
            audioMixer.SetFloat(EffectsVolumeParam, value);
            PlayerPrefs.SetFloat(EffectsVolumeParam, value);
        }
    }
    
    void Update()
    {
        
    }
}
