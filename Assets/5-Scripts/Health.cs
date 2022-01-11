using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public event Action<float> OnHealthPctChanges;
    public event Action LoseGame;


    private void OnEnable()
    {
        currentHealth = 0;
    }

    public void ModifyHealth(int amount)
    {
        if(amount>0 || currentHealth != 0)
        {
            currentHealth += amount;

            float currentHealthPct = (float)currentHealth / (float)maxHealth;
            OnHealthPctChanges(currentHealthPct);
         
        }
        else
        {
            LoseGame();
        }
        
    }

   
}
