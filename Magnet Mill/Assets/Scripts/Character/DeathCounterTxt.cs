using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathCounterTxt : MonoBehaviour
{
    private TextMeshPro deathCounterText;
    private int currentDeath;
    void Start()
    {
        deathCounterText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDeath != GameController.instance.deathCount)
        {
            currentDeath = GameController.instance.deathCount;
            deathCounterText.text = currentDeath.ToString().PadLeft(4, '0');
        }
    }
}
