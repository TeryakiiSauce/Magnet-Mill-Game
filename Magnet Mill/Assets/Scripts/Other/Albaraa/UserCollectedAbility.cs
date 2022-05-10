using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCollectedAbility : MonoBehaviour
{
    void Start()
    {
        AbilityCollectible abilitySC = GetComponentInChildren<AbilityCollectible>();
        if (abilitySC.currentAbility == AbilityCollectible.Ability.Jump
            && UserData.GetBool(UserData.jumpCollected))
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
