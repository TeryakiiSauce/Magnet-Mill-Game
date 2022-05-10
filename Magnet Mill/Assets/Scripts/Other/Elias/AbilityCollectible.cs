using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectible : MonoBehaviour
{   public enum Ability {Jump, Speed, Freeze};
    public Ability currentAbility;
    private bool collected;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCube") &&!collected)
        {
            collected = true;
            if(currentAbility == Ability.Jump)
            {
                AbilityController.instance.JumpCollected();
            }
            else if (currentAbility == Ability.Speed)
            {
                AbilityController.instance.SpeedCollected();
            }
            else if (currentAbility == Ability.Freeze)
            {
                AbilityController.instance.FreezeCollected();
            }
            animator.SetTrigger("collected");
        }
    }
}
