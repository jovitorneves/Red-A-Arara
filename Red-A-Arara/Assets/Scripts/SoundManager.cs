using UnityEngine;

public class SoundManager : GenericSingletonClass<SoundManager>
{

    public AudioSource fxPlayer;
    public AudioSource fxBuritiCollector;
    public AudioSource fxCobraAttack;
    public AudioSource fxCobraDie;
    public AudioSource fxCobraChefeDamageTaken;

    public void PlayFxPlayer (AudioClip clip)
    {
        fxPlayer.clip = clip;
        fxPlayer.Play();
        fxPlayer.volume = 0.055f;
    }

    public void PlayFxBuritiCollector (AudioClip clip)
    {
        fxBuritiCollector.clip = clip;
        fxBuritiCollector.Play();
        fxBuritiCollector.volume = 0.051f;
    }

    public void PlayFxCobraAttack (AudioClip clip)
    {
        fxCobraAttack.clip = clip;
        fxCobraAttack.Play();
        fxCobraAttack.volume = 0.555f;
    }

    public void PlayFxCobraDie (AudioClip clip)
    {
        fxCobraDie.clip = clip;
        fxCobraAttack.Play();
        fxCobraAttack.volume = 0.348f;
    }
    
    public void PlayFxCobraChefeDamageTaken (AudioClip clip)
    {
        fxCobraChefeDamageTaken.clip = clip;
        fxCobraChefeDamageTaken.Play();
        fxCobraChefeDamageTaken.volume = 0.318f;
    }
}
