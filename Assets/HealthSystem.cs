using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    
    public int maxHealth = 3;
    public  int currentHealth;

    public HealthBarScript healthBar; // referrence to the health bar script


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
    }
}
