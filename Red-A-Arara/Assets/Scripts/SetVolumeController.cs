using UnityEngine;
using UnityEngine.UI;

public class SetVolumeController : MonoBehaviour
{

    public Slider sliderPlayer;
    public Slider sliderBuriti;
    public Slider sliderCobraAttack;
    public Slider sliderCobraDie;
    public Slider sliderCobraChefeDamageTaken;

    // Start is called before the first frame update
    void Start()
    {
        LoadedData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Carrega os dados do volume
    private void LoadedData()
    {
        SoundDB loadedData = DataBase.loadData<SoundDB>("soundDB");
        if (loadedData == null) { return; }

        sliderPlayer.value = loadedData.player;
        sliderBuriti.value = loadedData.buriti;
        sliderCobraAttack.value = loadedData.cobraAttack;
        sliderCobraDie.value = loadedData.cobraDie;
        sliderCobraChefeDamageTaken.value = loadedData.cobraChefeDamageTaken;
    }

    //Actions
    public void SetVolumePlayer(float volume)
    {
        var model = new SoundDB
        {
            player = volume,
            buriti = sliderBuriti.value,
            cobraAttack = sliderCobraAttack.value,
            cobraDie = sliderCobraDie.value,
            cobraChefeDamageTaken = sliderCobraChefeDamageTaken.value
        };

        //Save data from PlayerInfo to a file named players
        DataBase.saveData(model, "soundDB");
    }

    public void SetVolumeBuriti(float volume)
    {
        var model = new SoundDB
        {
            buriti = volume,
            cobraAttack = sliderCobraAttack.value,
            cobraChefeDamageTaken = sliderCobraChefeDamageTaken.value,
            cobraDie = sliderCobraDie.value,
            player = sliderPlayer.value
        };

        //Save data from PlayerInfo to a file named players
        DataBase.saveData(model, "soundDB");
    }

    public void SetVolumeCobraAttack(float volume)
    {
        var model = new SoundDB
        {
            buriti = sliderBuriti.value,
            cobraAttack = volume,
            cobraChefeDamageTaken = sliderCobraChefeDamageTaken.value,
            cobraDie = sliderCobraDie.value,
            player = sliderPlayer.value
        };

        //Save data from PlayerInfo to a file named players
        DataBase.saveData(model, "soundDB");
    }

    public void SetVolumeCobraDie(float volume)
    {
        var model = new SoundDB
        {
            buriti = sliderBuriti.value,
            cobraAttack = sliderCobraAttack.value,
            cobraChefeDamageTaken = sliderCobraChefeDamageTaken.value,
            cobraDie = volume,
            player = sliderPlayer.value
        };

        //Save data from PlayerInfo to a file named players
        DataBase.saveData(model, "soundDB");
    }

    public void SetVolumeCobraChefeDamageTaken(float volume)
    {
        var model = new SoundDB
        {
            buriti = sliderBuriti.value,
            cobraAttack = sliderCobraAttack.value,
            cobraChefeDamageTaken = volume,
            cobraDie = sliderCobraDie.value,
            player = sliderPlayer.value
        };

        //Save data from PlayerInfo to a file named players
        DataBase.saveData(model, "soundDB");
    }
}
