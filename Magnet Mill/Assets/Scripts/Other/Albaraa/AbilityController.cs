using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public static AbilityController instance;
    public enum Ability { None, Speed, Jump, Freeze};
    [SerializeField] private bool isSpeedCollected;
    [SerializeField] private bool isJumpCollected;
    [SerializeField] private bool isFreezeCollected;

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

    const float speedTimeLimit = 4f;
    const float speedCoolDownTimeLimit = 14f;
    const float jumpTimeLimit = 3f;
    const float jumpCoolDownTimeLimit = 10f;
    const float freezeTimeLimit = 5f;
    const float freezeCoolDownTimeLimit = 20f;


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
        if(isSpeedCollected)
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
        if(currentActiveAbility != Ability.None)
        {
            if(activeAbilitytimer < currentAbilityTimeLimit)
            {
                activeAbilitytimer += Time.deltaTime;
            }
            else
            {
                ResetAbility();
            }
            return;
        }

        if(isSpeedCollected)
        {
            if(isSpeedAvailable)
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

    public void SpeedCollected()
    {
        isSpeedCollected = true;
        isSpeedAvailable = true;
        HUDController.instance.SetBoostAbilityCollected();
        HUDController.instance.SetBoostAbilityAvailable();
    }

    public void JumpCollected()
    {
        isJumpCollected = true;
        isJumpAvailable = true;
        HUDController.instance.SetJumpAbilityCollected();
        HUDController.instance.SetJumpAbilityAvailable();
    }

    public void FreezeCollected()
    {
        isFreezeCollected = true;
        isFreezeAvailable = true;
        HUDController.instance.SetFreezeAbilityCollected();
        HUDController.instance.SetFreezeAbilityAvailable();
    }

    public bool IsSpeedActive()
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

    private void SetSpeed()
    {
        currentActiveAbility = Ability.Speed;
        currentAbilityTimeLimit = speedTimeLimit;
        HUDController.instance.SetBoostAbilityActive();
        isSpeedAvailable = false;
    }

    private void SetJump()
    {
        currentActiveAbility = Ability.Jump;
        currentAbilityTimeLimit = jumpTimeLimit;
        HUDController.instance.SetJumpAbilityActive();
        isJumpAvailable = false;
    }

    private void SetFreeze()
    {
        currentActiveAbility = Ability.Freeze;
        currentAbilityTimeLimit = freezeTimeLimit;
        HUDController.instance.SetFreezeAbilityActive();
        isFreezeAvailable = false;
    }

    private void ResetAbility()
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
                    break;
                }
            default: break;
        }
        currentActiveAbility = Ability.None;
        activeAbilitytimer = 0f;
    }

    private void CoolingDownAbilites()
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
}
