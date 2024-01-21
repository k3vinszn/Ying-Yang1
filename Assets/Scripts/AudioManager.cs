
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;



    [Header("Audio Clips")]

    [Header("Enviroment")]
    public AudioClip Checkpoint;
    public AudioClip DoorOpen;
    public AudioClip KeyCollect;
    public AudioClip Portal;
    public AudioClip ButtonSelect;
    public AudioClip Cloud;
    public AudioClip Iddleportal;


    [Header("Enemies")]
    public AudioClip Frogattack;
    public AudioClip Froghit;
    public AudioClip Frogtrigger;
    public AudioClip Spike;
    public AudioClip Shuriken;


    [Header("Players")]

    public AudioClip ShieldTrigger;
    public AudioClip ShieldHit;
    public AudioClip Sword;
    public AudioClip Run;
    public AudioClip Respawn;
    public AudioClip Jump;
    public AudioClip TakingDamage;


    [Header("UI/Interface")]
    public AudioClip PauseMenu;
    public AudioClip ExitPauseMenu;
    public AudioClip ButtonHighlight;
    public AudioClip ButtonPressDown;
    public AudioClip TVInterference;

    private void Start()
    {

    }
    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
