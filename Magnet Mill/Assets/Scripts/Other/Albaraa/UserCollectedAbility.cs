using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCollectedAbility : MonoBehaviour
{
    void Start()    //This script will be assigned to the abilities collectible 
    {
        AbilityCollectible abilitySC = GetComponentInChildren<AbilityCollectible>();
        if (abilitySC.currentAbility == AbilityCollectible.Ability.Jump
            && UserData.GetBool(UserData.jumpCollected))    //If the current ability is unlocked destroy the collectible
        {
            Destroy(gameObject);
        }
        else if (abilitySC.currentAbility == AbilityCollectible.Ability.Speed
            && UserData.GetBool(UserData.speedCollected))
        {
            Destroy(gameObject);
        }
        else if (abilitySC.currentAbility == AbilityCollectible.Ability.Freeze
            && UserData.GetBool(UserData.freezeCollected))
        {
            Destroy(gameObject);
        }
    }
}
