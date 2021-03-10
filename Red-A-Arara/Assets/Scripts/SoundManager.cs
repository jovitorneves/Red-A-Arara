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
    }

    public void PlayFxBuritiCollector (AudioClip clip)
    {
        fxBuritiCollector.clip = clip;
        fxBuritiCollector.Play();
    }

    public void PlayFxCobraAttack (AudioClip clip)
    {
        fxCobraAttack.clip = clip;
        fxCobraAttack.Play();
    }

    public void PlayFxCobraDie (AudioClip clip)
    {
        fxCobraDie.clip = clip;
        fxCobraDie.Play();
    }
    
    public void PlayFxCobraChefeDamageTaken (AudioClip clip)
    {
        fxCobraChefeDamageTaken.clip = clip;
        fxCobraChefeDamageTaken.Play();
    }
}
