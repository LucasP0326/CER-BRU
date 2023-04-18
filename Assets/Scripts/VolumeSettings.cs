using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider dialogueSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    public const string MIXER_DIALOGUE = "DialogueVolume";

    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        dialogueSlider.onValueChanged.AddListener(SetDialogueVolume);
    }
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
        dialogueSlider.value = PlayerPrefs.GetFloat(AudioManager.DIALOGUE_KEY, 1f);
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MIXER_MUSIC, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MIXER_SFX, sfxSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MIXER_DIALOGUE, dialogueSlider.value);
    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

        void SetDialogueVolume(float value)
    {
        mixer.SetFloat(MIXER_DIALOGUE, Mathf.Log10(value) * 20);
    }
}
