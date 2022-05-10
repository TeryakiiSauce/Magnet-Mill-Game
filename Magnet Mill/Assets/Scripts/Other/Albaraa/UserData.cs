using UnityEngine;

public static class UserData
{
    //-------------------General Keys---------------------------

    public const string userName = "userName"; //string
    public const string isMuted = "isMuted"; //boolean
    public const string finishedTutorial = "finishedTutorial"; //boolean
    public const string finishedLevel1 = "finishedLevel1";    //boolean
    public const string finishedLevel2 = "finishedLevel2";    //boolean
    public const string finishedLevel3 = "finishedLevel3";    //boolean
    public const string finishedLevel4 = "finishedLevel4";    //boolean
    public const string jumpCollected = "jumpCollected"; //boolean
    public const string speedCollected = "speedCollected"; //boolean
    public const string freezeCollected = "freezeCollected"; //boolean
    public const string currentLevel = "currentLevel"; //string


    //-------------------User statistics Keys-------------------

    public const string numOfgamesPlayed = "numOfgamesPlayed"; //int
    public const string numOfLevelsFinished = "numOfLevelsFinished"; //int
    public const string numOfCubeRolled = "numOfCubeRolled"; //int
    public const string numOfTotalScore = "numOfTotalScore"; //int
    public const string numOfAbilitiesUsed = "numOfAbilitiesUsed"; //int
    public const string numOfDeaths = "numOfDeaths"; //int
    public const string leastDeaths = "leastDeaths";   //int
    public const string leastCubeMoves = "leastCubeMoves";    //int
    public const string fastestTime = "fastestTime";    //float
    public const string totalTimePlayed = "totalTimePlayed"; //float


    //-------------------HighScores of levels keys--------------

    public const string level1HighScore = "level1HighScore"; //int
    public const string level2HighScore = "level2HighScore"; //int
    public const string level3HighScore = "level3HighScore"; //int
    public const string level4HighScore = "level4HighScore"; //int


    //-------------------------Getters--------------------------

    public static int GetInt(string key)   //Retrieving Integer value from local storage.
    {                                      //Usage: UserData.GetInt(UserData.numOfDeaths);
        if(!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " +key);
            return 0;
        }
        return PlayerPrefs.GetInt(key);
    }

    public static bool GetBool(string key)  //Retrieving Boolean value from local storage.
    {                                       //Usage: if (UserData.GetBool(UserData.finishedTutorial)) {....};
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return false;
        }
        return PlayerPrefs.GetInt(key) == 1;
    }

    public static string GetString(string key)  //Retrieving String value from local storage.
    {                                           //Usage: UserData.GetString(UserData.userName);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return null;
        }
        return PlayerPrefs.GetString(key);
    }

    public static float GetFloat(string key)    //Retrieving Float value from local storage.
    {                                           //Usage: UserData.GetFloat(UserData.totalTimePlayed);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return 0;
        }
        return PlayerPrefs.GetFloat(key);
    }


    //-------------------------Setters--------------------------

    public static void SetInt(string key, int value)    //Setting Integer value in the local storage.
    {                                                   //Usage: UserData.SetInt(UserData.level1HighScore, 1000);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        PlayerPrefs.SetInt(key, value);
    }

    public static void SetBool(string key, bool value)  //Setting Boolean value in the local storage. 
    {                                                   //Usage: UserData.SetBool(UserData.finishedTutorial, true);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        if (value)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    public static void SwitchBool (string key)      //Switching Boolean value in the local storage.
    {                                               //Usage: UserData.SwitchBool(UserData.finishedTutorial);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        if (PlayerPrefs.GetInt(key) == 1)
        {
            PlayerPrefs.SetInt(key, 0);
        }
        else
        {
            PlayerPrefs.SetInt(key, 1);
        }
    }
    public static void SetString(string key, string value)  //Setting String value in the local storage.
    {                                                       //Usage: UserData.SetString(UserData.userName, "Albaraa");
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        PlayerPrefs.SetString(key, value);
    }

    public static void SetFloat(string key, float value)    //Setting Float value in the local storage.
    {                                                       //Usage: UserData.SetFloat(UserData.totalTimesPlayed, 932283.22);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        PlayerPrefs.SetFloat(key, value);
    }


    //------------------Increment and Decrement-------------------

    public static void IncrementInt(string key)     //Increment Integer value from local storage by one.
    {                                               //Usage: UserData.IncrementInt(UserData.numOfGamesPlayed);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        int newValue = PlayerPrefs.GetInt(key) + 1;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void IncrementInt(string key, int value)  //Increment Integer value from local storage by given value.
    {                                                       //Usage: UserData.IncrementInt(UserData.numOfTotalScore, 1200);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        int newValue = PlayerPrefs.GetInt(key) + value;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void DecrementInt(string key)         //Decrement Integer value from local storage by one.
    {                                                   //Usage: UserData.DecrementInt(UserData.numOfGamesPlayed);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        int newValue = PlayerPrefs.GetInt(key) - 1;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void DecrementInt(string key, int value)  //Decrement Integer value from local storage by given value.
    {                                                       //Usage: UserData.DecrementInt(UserData.numOfTotalScore, 100);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        int newValue = PlayerPrefs.GetInt(key) - value;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void IncrementFloat(string key)     //Increment Float value from local storage by 0.1
    {                                               //Usage: UserData.IncrementFloat(UserData.totalTimePlayed);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        float newValue = PlayerPrefs.GetFloat(key) + 0.1f;
        PlayerPrefs.SetFloat(key, newValue);
    }

    public static void IncrementFloat(string key, float value)  //Increment Float value from local storage by given value.
    {                                                           //Usage: UserData.IncrementFloat(UserData.totalTimePlayed, 86.8);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        float newValue = PlayerPrefs.GetFloat(key) + value;
        PlayerPrefs.SetFloat(key, newValue);
    }

    public static void DecrementFloat(string key)         //Decrement Float value from local storage by 0.1
    {                                                     //Usage: UserData.DecrementFloat(UserData.totalTimePlayed);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        float newValue = PlayerPrefs.GetFloat(key) - 0.1f;
        PlayerPrefs.SetFloat(key, newValue);
    }

    public static void DecrementFloat(string key, float value)  //Decrement float value from local storage by given value.
    {                                                           //Usage: UserData.DecrementFloat(UserData.totalTimePlayed, 77.3);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        float newValue = PlayerPrefs.GetFloat(key) - value;
        PlayerPrefs.SetFloat(key, newValue);
    }


    //----------------------Other Functions-----------------------

    public static void ResetValue(string key)       //Reset value from local storage.
    {                                               //Usage: UserData.ResetValue(UserData.finishedTutorial);
        if (!IsValidKey(key))
        {
            Debug.LogWarning("You are passing invalid key to the UserData class, the passed key: " + key);
            return;
        }
        PlayerPrefs.DeleteKey(key);
    }

    public static void ResetStatistics()           //Reset all statistics values.
    {                                              //Usage: UserData.ResetStatistics();
        PlayerPrefs.DeleteKey(numOfgamesPlayed);
        PlayerPrefs.DeleteKey(numOfLevelsFinished);
        PlayerPrefs.DeleteKey(numOfCubeRolled);
        PlayerPrefs.DeleteKey(numOfTotalScore);
        PlayerPrefs.DeleteKey(numOfAbilitiesUsed);
        PlayerPrefs.DeleteKey(numOfDeaths);
        PlayerPrefs.DeleteKey(leastDeaths);
        PlayerPrefs.DeleteKey(leastCubeMoves);
        PlayerPrefs.DeleteKey(fastestTime);
        PlayerPrefs.DeleteKey(totalTimePlayed);
    }

    public static void ResetAllData()   //Reset all data
    {                                   //Usage: UserData.ResetAllData();
        PlayerPrefs.DeleteAll();
    }

    private static bool IsValidKey(string key)      //To check whether the passed key is valid or not
    {
        System.Type t = typeof(UserData);
        if (t.GetField(key) != null)
        {
            return true;
        }
        return false;
    }
}

