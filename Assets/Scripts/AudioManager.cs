using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string DIALOGUE_KEY = "dialogueVolume";
    
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    public const string MIXER_DIALOGUE = "DialogueVolume";

    public AudioSource mainMusic;
    public AudioSource receptionMusic;
    public AudioSource administrationMusic;
    public AudioSource laboratoriesMusic;
    public AudioSource officeMusic;
    
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        /*
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        */

        if (scene.name == "MainMenu")
        {
            mainMusic.Play();
            receptionMusic.Stop();
            administrationMusic.Stop();
            laboratoriesMusic.Stop();
            officeMusic.Stop();
        }

        if (scene.name == "Cutscene Room")
        {
            mainMusic.Play();
            receptionMusic.Stop();
            administrationMusic.Stop();
            laboratoriesMusic.Stop();
            officeMusic.Stop();
        }

        if(scene.name == "Reception")
        {
            mainMusic.Stop();
            receptionMusic.Play();
            administrationMusic.Stop();
            laboratoriesMusic.Stop();
            officeMusic.Stop(); 
        }

        if(scene.name == "Administration")
        {
            mainMusic.Stop();
            receptionMusic.Stop();
            administrationMusic.Play();
            laboratoriesMusic.Stop();
            officeMusic.Stop(); 
        }

        if(scene.name == "Laboratory")
        {
            mainMusic.Stop();
            receptionMusic.Stop();
            administrationMusic.Stop();
            laboratoriesMusic.Play();
            officeMusic.Stop(); 
        }

        if(scene.name == "Office")
        {
            mainMusic.Stop();
            receptionMusic.Stop();
            administrationMusic.Stop();
            laboratoriesMusic.Stop();
            officeMusic.Play(); 
        }

        LoadVolume();
    }

    void Update()
    {
    }

    void LoadVolume() //Volume saved in VolumeSettings script
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY,1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY,1f);
        float dialogueVolume = PlayerPrefs.GetFloat(DIALOGUE_KEY,1f);

        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_DIALOGUE, Mathf.Log10(dialogueVolume) * 20);
    }
}
