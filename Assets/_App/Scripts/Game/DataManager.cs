using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Data _gameData;
    public Data GameData {
        get
        {
            if (_gameData == null)
            {
                LoadData();
            }
            return _gameData;
        }
    }

    public void SaveLives(int lives)
    {
        _gameData.PlayerLives = lives;
    }

    public void SaveLevel(int level)
    {
        _gameData.CurrentLevel = level;
    }
    
    public void SaveGold(int gold)
    {
        _gameData.Gold = gold;
    }

    public void SaveData()
    {
        var data = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString("GameData", data);
    }
    
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            var data = PlayerPrefs.GetString("GameData");
            _gameData = JsonUtility.FromJson<Data>(data);
        }
        else
        {
            _gameData = new Data();
        }
    }

    public void ResetGameData()
    {
        _gameData = new Data();
    }
}

public class Data
{
    public int PlayerLives { get; set; }
    public int CurrentLevel { get; set; }
    public int Gold { get; set; }

    public Data()
    {
        PlayerLives = 3;
        CurrentLevel = 1;
        Gold = 0;
    }
}