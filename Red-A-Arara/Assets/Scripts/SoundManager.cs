using UnityEngine;

public class SoundManager : GenericSingletonClass<SoundManager>
{

    public AudioSource fxPlayer;
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
                buriti = 0.051f,
                cobraAttack = 0.555f,
                cobraChefeDamageTaken = 0.318f,
                cobraDie = 0.348f,
                player = 0.055f
            };

            loadedData = model;
        }
        this.loadedData = loadedData;
    }

    //Action
    public void PlayFxPlayer (AudioClip clip)
    {
        fxPlayer.clip = clip;
        fxPlayer.Play();
        fxPlayer.volume = loadedData.player;
    }

    public void PlayFxBuritiCollector (AudioClip clip)
    {
        fxBuritiCollector.clip = clip;
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
