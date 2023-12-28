using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Player Life")]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int damagenumber = 1;
    [SerializeField] private int currentHealth;

    public HealthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(currentHealth);
    }

    public int getcurrentHealth()
    {
        return currentHealth;
    }

    public void setcurrentHealth()
    {
        Debug.Log("FUNCTION TO REDUCE HEALTH CALLED");
        currentHealth -= damagenumber;
        healthBar.SetHealth(currentHealth);
    }

    public void resethealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
