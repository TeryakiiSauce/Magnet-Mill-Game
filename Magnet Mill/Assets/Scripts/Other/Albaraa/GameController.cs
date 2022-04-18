using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool gamePaused;
    public enum PowerAbilities {None ,JumpAbility, FreezeAbility, BoostAbility};
    [HideInInspector] public PowerAbilities currentAbility;
    [HideInInspector] public int score;
    [HideInInspector] public int rollsCount;
    [HideInInspector] public float levelTimer;
    [HideInInspector] public int deathCount;
    [HideInInspector] public int abilitesUsedCount;
    public string currentLevel;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        UserData.SetString(UserData.currentLevel, currentLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
