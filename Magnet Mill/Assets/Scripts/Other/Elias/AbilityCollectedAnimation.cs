using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCollectedAnimation : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCube"))
        {
            animator.SetTrigger("collected");
        }
    }
}
