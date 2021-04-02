using UnityEngine;

public class SoundManager : GenericSingletonClass<SoundManager>
{
    public AudioSource fxAmbiente;
    public AudioSource fxPlayer;
    public AudioSource fxArremessoCoco;
    public AudioSource fxAtordoado;
    public AudioSource fxDash;
    public AudioSource fxPorta;
    public AudioSource fxCoco;
    public AudioSource fxJumpEnemy;
    public AudioSource fxTronco;
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
    public void PlayFxPlayer(AudioClip clip)
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
        fxAtordoado.volume = loadedData.cobraDie;
    }

    public void PlayFxDash()
    {
        fxDash.Play();
        fxDash.volume = loadedData.player;
    }

    public void PlayFxPorta()
    {
        fxPorta.pitch = 3.5f;
        fxPorta.Play();
        fxPorta.volume = loadedData.ambiente;
    }

    public void PlayFxCoco()
    {
        fxCoco.Play();
        fxCoco.volume = loadedData.buriti;
    }

    public void PlayFxJumpEnemy()
    {
        fxJumpEnemy.Play();
        fxJumpEnemy.volume = loadedData.cobraAttack;
    }

    public void PlayFxTronco()
    {
        fxTronco.Play();
        fxTronco.volume = loadedData.ambiente;
    }

    public void PlayFxBuritiCollector()
    {
        fxBuritiCollector.Play();
        fxBuritiCollector.volume = loadedData.buriti;
    }

    public void PlayFxCobraAttack()
    {
        fxCobraAttack.Play();
        fxCobraAttack.volume = loadedData.cobraAttack;
    }

    public void PlayFxCobraDie()
    {
        fxCobraAttack.Play();
        fxCobraAttack.volume = loadedData.cobraDie;
    }
    
    public void PlayFxCobraChefeDamageTaken()
    {
        fxCobraChefeDamageTaken.Play();
        fxCobraChefeDamageTaken.volume = loadedData.cobraChefeDamageTaken;
    }
}
