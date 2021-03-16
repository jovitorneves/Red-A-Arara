using UnityEngine;

public class SoundManager : GenericSingletonClass<SoundManager>
{
    public AudioSource fxAmbiente;
    public AudioSource fxPlayer;
    public AudioSource fxArremessoCoco;
    public AudioSource fxAtordoado;
    public AudioSource fxJumpEnemy;
    public AudioSource fxBuritiCollector;
    public AudioSource fxCobraAttack;
    public AudioSource fxCobraDie;
    public AudioSource fxCobraChefeDamageTaken;

    private SoundDB loadedData;

    // Start is called before the first frame update
    void Start()
    {
        LoadSom();
    }

    private void FixedUpdate()
    {
        LoadSom();
    }

    //Som
    private void LoadSom()
    {
        SoundDB loadedData = DataBase.loadData<SoundDB>("soundDB");
        if (loadedData == null)
        {
            var model = new SoundDB
            {
                ambiente = 0.055f,
                buriti = 0.051f,
                cobraAttack = 0.555f,
                cobraChefeDamageTaken = 0.318f,
                cobraDie = 0.348f,
                player = 0.055f
            };

            loadedData = model;
        }
        this.loadedData = loadedData;
        fxAmbiente.volume = loadedData.ambiente;
    }

    //Action
    public void PlayFxPlayer (AudioClip clip)
    {
        fxPlayer.clip = clip;
        fxPlayer.Play();
        fxPlayer.volume = loadedData.player;
    }

    public void PlayFxArremessoCoco()
    {
        fxArremessoCoco.Play();
        fxArremessoCoco.volume = loadedData.player;
    }

    public void PlayFxAtordoado()
    {
        fxAtordoado.Play();
        fxAtordoado.volume = loadedData.cobraAttack;
    }

    public void PlayFxJumpEnemy()
    {
        fxJumpEnemy.Play();
        fxJumpEnemy.volume = loadedData.cobraAttack;
    }

    public void PlayFxBuritiCollector()
    {
        fxBuritiCollector.Play();
        fxBuritiCollector.volume = loadedData.buriti;
    }

    public void PlayFxCobraAttack (AudioClip clip)
    {
        fxCobraAttack.clip = clip;
        fxCobraAttack.Play();
        fxCobraAttack.volume = loadedData.cobraAttack;
    }

    public void PlayFxCobraDie (AudioClip clip)
    {
        fxCobraDie.clip = clip;
        fxCobraAttack.Play();
        fxCobraAttack.volume = loadedData.cobraDie;
    }
    
    public void PlayFxCobraChefeDamageTaken (AudioClip clip)
    {
        fxCobraChefeDamageTaken.clip = clip;
        fxCobraChefeDamageTaken.Play();
        fxCobraChefeDamageTaken.volume = loadedData.cobraChefeDamageTaken;
    }
}
