using UnityEngine;

public static class UserData
{
    //-------------------General Keys---------------------------

    public static string userName = "userName"; //string
    private static string isMuted = "isMuted"; //boolean
    public static string finishedTutorial = "finishedTutorial"; //boolean
    public static string currentLevelNum = "currentLevelNum"; //int


    //-------------------User statistics Keys-------------------

    public static string numOfgamesPlayed = "numOfgamesPlayed"; //int
    public static string numOfLevelsFinished = "numOfLevelsFinished"; //int
    public static string numOfCubeRolled = "numOfCubeRolled"; //int
    public static string numOfTotalScore = "numOfTotalScore"; //int
    public static string numOfAbilitiesCollected = "numOfAbilitiesCollected"; //int
    public static string numOfAbilitiesUsed = "numOfAbilitiesUsed"; //int
    public static string numOfDeaths = "numOfDeaths"; //int
    public static string totalTimePlayed = "totalTimePlayed"; //float


    //-------------------HighScores of levels keys--------------

    public static string level1HighScore = "level1HighScore"; //int
    public static string level2HighScore = "level2HighScore"; //int
    public static string level3HighScore = "level3HighScore"; //int
    public static string level4HighScore = "level4HighScore"; //int


    //-------------------------Getters--------------------------

    public static int GetInt(string key)   //Retrieving Integer value from local storage.
    {                                      //Usage: UserData.GetInt(UserData.numOfDeaths);
        return PlayerPrefs.GetInt(key);
    }

    public static bool GetBool(string key)  //Retrieving Boolean value from local storage.
    {                                       //Usage: if (UserData.GetBool(UserData.finishedTutorial)) {....};
        return PlayerPrefs.GetInt(key) == 1;
    }

    public static string GetString(string key)  //Retrieving String value from local storage.
    {                                           //Usage: UserData.GetString(UserData.userName);
        return PlayerPrefs.GetString(key);
    }

    public static float GetFloat(string key)    //Retrieving Float value from local storage.
    {                                           //Usage: UserData.GetFloat(UserData.totalTimePlayed);
        return PlayerPrefs.GetFloat(key);
    }


    //-------------------------Setters--------------------------

    public static void SetInt(string key, int value)    //Setting Integer value in the local storage.
    {                                                   //Usage: UserData.SetInt(UserData.level1HighScore, 1000);
        PlayerPrefs.SetInt(key, value);
    }

    public static void SetBool(string key, bool value)  //Setting Boolean value in the local storage. 
    {                                                   //Usage: UserData.SetBool(UserData.finishedTutorial, true);
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
        PlayerPrefs.SetString(key, value);
    }

    public static void SetFloat(string key, float value)    //Setting Float value in the local storage.
    {                                                       //Usage: UserData.SetFloat(UserData.totalTimesPlayed, 932283.22);
        PlayerPrefs.SetFloat(key, value);
    }


    //------------------Increment and Decrement-------------------

    public static void IncrementInt(string key)     //Increment Integer value from local storage by one.
    {                                               //Usage: UserData.IncrementInt(UserData.numOfGamesPlayed);
        int newValue = PlayerPrefs.GetInt(key) + 1;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void IncrementInt(string key, int value)  //Increment Integer value from local storage by given value.
    {                                                       //Usage: UserData.IncrementInt(UserData.numOfTotalScore, 1200);
        int newValue = PlayerPrefs.GetInt(key) + value;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void DecrementInt(string key)         //Decrement Integer value from local storage by one.
    {                                                   //Usage: UserData.DecrementInt(UserData.numOfGamesPlayed);
        int newValue = PlayerPrefs.GetInt(key) - 1;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void DecrementInt(string key, int value)  //Decrement Integer value from local storage by given value.
    {                                                       //Usage: UserData.DecrementInt(UserData.numOfTotalScore, 100);
        int newValue = PlayerPrefs.GetInt(key) - value;
        PlayerPrefs.SetInt(key, newValue);
    }

    public static void IncrementFloat(string key)     //Increment Float value from local storage by 0.1
    {                                               //Usage: UserData.IncrementFloat(UserData.totalTimePlayed);
        float newValue = PlayerPrefs.GetFloat(key) + 0.1f;
        PlayerPrefs.SetFloat(key, newValue);
    }

    public static void IncrementFloat(string key, float value)  //Increment Float value from local storage by given value.
    {                                                           //Usage: UserData.IncrementFloat(UserData.totalTimePlayed, 86.8);
        float newValue = PlayerPrefs.GetFloat(key) + value;
        PlayerPrefs.SetFloat(key, newValue);
    }

    public static void DecrementFloat(string key)         //Decrement Float value from local storage by 0.1
    {                                                     //Usage: UserData.DecrementFloat(UserData.totalTimePlayed);
        float newValue = PlayerPrefs.GetFloat(key) - 0.1f;
        PlayerPrefs.SetFloat(key, newValue);
    }

    public static void DecrementFloat(string key, float value)  //Decrement float value from local storage by given value.
    {                                                           //Usage: UserData.DecrementFloat(UserData.totalTimePlayed, 77.3);
        float newValue = PlayerPrefs.GetFloat(key) - value;
        PlayerPrefs.SetFloat(key, newValue);
    }

}

