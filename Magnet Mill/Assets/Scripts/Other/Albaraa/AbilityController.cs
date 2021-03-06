using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public static AbilityController instance;
    public enum Ability { None, Speed, Jump, Freeze};

    private bool isSpeedCollected;
    private bool isJumpCollected;
    private bool isFreezeCollected;
    private Ability currentActiveAbility = Ability.None;
    private bool isSpeedAvailable;
    private bool isJumpAvailable;
    private bool isFreezeAvailable;
    private bool isSpeedCoolingDown;
    private bool isJumpCoolingDown;
    private bool isFreezeCoolingDown;
    private float activeAbilitytimer;
    private float currentAbilityTimeLimit;
    private float speedCoolDownTimer;
    private float jumpCoolDownTimer;
    private float freezeCoolDownTimer;
    private float terrainDefaultWindSpeed;
    private float terrianDefaultBending;
    private Terrain levelTerrain;
    private ParticleSystem[] particles;
    private float[] particlesSpeed;

    const float speedTimeLimit = 4f;
    const float speedCoolDownTimeLimit = 12f;
    const float jumpTimeLimit = 3f;
    const float jumpCoolDownTimeLimit = 8f;
    const float freezeTimeLimit = 5f;
    const float freezeCoolDownTimeLimit = 18f;


    void Awake()
    {
        if (instance == null)    //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
    }

    void Start()
    {
        levelTerrain = FindObjectOfType<Terrain>();         //get terrian component in order to freeze moving grass,trees etc.. during freeze ability
        particles = FindObjectsOfType<ParticleSystem>();    //get all particles of the level to freeze them during the freeze ability
        if(particles != null)
        {
            particlesSpeed = new float[particles.Length];
            for (int i = 0; i < particles.Length; i++)  //getting all defalut simulation speed of all particels (will be used when freeze ability finished)
            {
                particlesSpeed[i] = particles[i].main.simulationSpeed;
            }
        }
        
        if (levelTerrain != null)   
        {
            terrainDefaultWindSpeed = levelTerrain.terrainData.wavingGrassSpeed;
            terrianDefaultBending = levelTerrain.terrainData.wavingGrassStrength;
        }
        if (UserData.GetBool(UserData.speedCollected)) isSpeedCollected = true; //checking from userdata if already collected the abilites
        if (UserData.GetBool(UserData.jumpCollected)) isJumpCollected = true;
        if (UserData.GetBool(UserData.freezeCollected)) isFreezeCollected = true;

        if(isSpeedCollected)    //make ability available since the user already collected it
        {
            HUDController.instance.SetBoostAbilityAvailable();
            isSpeedAvailable = true;
        }
        else
        {
            HUDController.instance.SetBoostAbilityCooldown();
            HUDController.instance.SetBoostAbilityNotCollected();
        }

        if(isJumpCollected)
        {
            HUDController.instance.SetJumpAbilityAvailable();
            isJumpAvailable = true;
        }
        else
        {
            HUDController.instance.SetJumpAbilityCooldown();
            HUDController.instance.SetJumpAbilityNotCollected();
        }

        if(isFreezeCollected)
        {
            HUDController.instance.SetFreezeAbilityAvailable();
            isFreezeAvailable = true;
        }
        else
        {
            HUDController.instance.SetFreezeAbilityCooldown();
            HUDController.instance.SetFreezeAbilityNotCollected();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CoolingDownAbilites();
        if(currentActiveAbility != Ability.None)    //means that there is an ability active currently
        {
            if(activeAbilitytimer < currentAbilityTimeLimit)    //check if ability time finished
            {
                activeAbilitytimer += Time.deltaTime;
            }
            else
            {
                ResetAbility();
            }
            return;
        }

        if(isSpeedCollected)    //is speed ability already unlocked?
        {
            if(isSpeedAvailable) //is ability is cooleddown?
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    SetSpeed();
                }
            }
        }

        if(isJumpCollected)
        {
            if(isJumpAvailable)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    SetJump();
                }

            }
        }

        if(isFreezeCollected)
        {
            if(isFreezeAvailable)
            {
                if(Input.GetKey(KeyCode.Q))
                {
                    SetFreeze();
                }
            }
        }
    }

    public void SpeedCollected()    //will be called when the user first time collect the ability
    {
        isSpeedCollected = true;
        isSpeedAvailable = true;
        UserData.SetBool(UserData.speedCollected, true);
        HUDController.instance.SetBoostAbilityCollected();
        HUDController.instance.SetBoostAbilityAvailable();
    }

    public void JumpCollected()
    {
        isJumpCollected = true;
        isJumpAvailable = true;
        UserData.SetBool(UserData.jumpCollected, true);
        HUDController.instance.SetJumpAbilityCollected();
        HUDController.instance.SetJumpAbilityAvailable();
    }

    public void FreezeCollected()
    {
        isFreezeCollected = true;
        isFreezeAvailable = true;
        UserData.SetBool(UserData.freezeCollected, true);
        HUDController.instance.SetFreezeAbilityCollected();
        HUDController.instance.SetFreezeAbilityAvailable();
    }

    public bool IsSpeedActive()     //will be used by other classes to check if the ability is active
    {
        return currentActiveAbility == Ability.Speed;
    }

    public bool IsJumpActive()
    {
        return currentActiveAbility == Ability.Jump;
    }

    public bool IsFreezeActive()
    {
        return currentActiveAbility == Ability.Freeze;
    }

    private void SetSpeed()     //user activated speed ability be clicking ability key "SHIFT"
    {
        currentActiveAbility = Ability.Speed;
        currentAbilityTimeLimit = speedTimeLimit;
        HUDController.instance.SetBoostAbilityActive();
        isSpeedAvailable = false;
        UserData.IncrementInt(UserData.numOfAbilitiesUsed);     //increase the ability used number, will be used in statistics
    }

    private void SetJump()
    {
        currentActiveAbility = Ability.Jump;
        currentAbilityTimeLimit = jumpTimeLimit;
        HUDController.instance.SetJumpAbilityActive();
        isJumpAvailable = false;
        UserData.IncrementInt(UserData.numOfAbilitiesUsed);
    }

    private void SetFreeze()
    {
        currentActiveAbility = Ability.Freeze;
        currentAbilityTimeLimit = freezeTimeLimit;
        HUDController.instance.SetFreezeAbilityActive();
        TimerController.instance.SetFreezeColor();
        isFreezeAvailable = false;
        if (levelTerrain != null)   //freeze terrain
        {
            levelTerrain.terrainData.wavingGrassSpeed = 0f;
            levelTerrain.terrainData.wavingGrassStrength = 0f;
        }
        AudioManager.instance.Stop(GameController.instance.currentLevel + "Theme"); //stop background theme sound
        FreezeParticles();  
        UserData.IncrementInt(UserData.numOfAbilitiesUsed);
    }

    private void ResetAbility()     //after the ability time finished, this function will be called
    {
        switch (currentActiveAbility)
        {
            case Ability.Speed:
                {
                    isSpeedCoolingDown = true;
                    HUDController.instance.SetBoostAbilityCooldown();
                    break;
                }
            case Ability.Jump:
                {
                    isJumpCoolingDown = true;
                    HUDController.instance.SetJumpAbilityCooldown();
                    break;
                }
            case Ability.Freeze:
                {
                    isFreezeCoolingDown = true;
                    HUDController.instance.SetFreezeAbilityCooldown();
                    TimerController.instance.SetNormalColor();
                    if (levelTerrain != null)
                    {
                        levelTerrain.terrainData.wavingGrassSpeed = terrainDefaultWindSpeed;    //resume the terrain
                        levelTerrain.terrainData.wavingGrassStrength = terrianDefaultBending;
                    }
                    AudioManager.instance.Play(GameController.instance.currentLevel + "Theme");     //resume the theme sound
                    ResumeParticles();
                    break;
                }
            default: break;
        }
        currentActiveAbility = Ability.None;
        activeAbilitytimer = 0f;
    }

    private void CoolingDownAbilites()      //will be called in update to check which abilities need to cooldown
    {
        if(isSpeedCoolingDown)
        {
            if(speedCoolDownTimer < speedCoolDownTimeLimit)
            {
                speedCoolDownTimer += Time.deltaTime;
            }
            else
            {
                isSpeedAvailable = true;
                HUDController.instance.SetBoostAbilityAvailable();
                isSpeedCoolingDown = false;
                speedCoolDownTimer = 0;
            }
        }
        if(isJumpCoolingDown)
        {
            if(jumpCoolDownTimer < jumpCoolDownTimeLimit)
            {
                jumpCoolDownTimer += Time.deltaTime;
            }
            else
            {
                isJumpAvailable = true;
                HUDController.instance.SetJumpAbilityAvailable();
                isJumpCoolingDown = false;
                jumpCoolDownTimer = 0;
            }
        }
        if (isFreezeCoolingDown)
        {
            if (freezeCoolDownTimer < freezeCoolDownTimeLimit)
            {
                freezeCoolDownTimer += Time.deltaTime;
            }
            else
            {
                isFreezeAvailable = true;
                HUDController.instance.SetFreezeAbilityAvailable();
                isFreezeCoolingDown = false;
                freezeCoolDownTimer = 0;
            }
        }
    }

    private void FreezeParticles()  // freeze all particles, will be called when freeze ability activated 
    {
        if (particles == null) return;
        for (int i = 0; i< particles.Length; i++)
        {
            ParticleSystem.MainModule tempMain = particles[i].main;
            tempMain.simulationSpeed = 0;
        }
    }

    private void ResumeParticles()  // resume all particles, will be called when freeze ability activated
    {
        if (particles == null) return;
        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystem.MainModule tempMain = particles[i].main;
            tempMain.simulationSpeed = particlesSpeed[i];
        }
    }
}
