using UnityEngine;

public class HealthSystemManager : MonoBehaviour
{
    private static HealthSystemManager instance;

    public int maxHealth = 3;
    private int currentHealth;

    public HealthBarScript healthBar; // Reference to the health bar script

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of the manager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the manager when loading new scenes
            InitializeHealthSystem();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate managers
        }
    }

    private void InitializeHealthSystem()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public static HealthSystemManager GetInstance()
    {
        return instance;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        // Ensure health stays within the bounds of 0 to maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);
    }

    public HealthSystem GetHealthSystem()
    {
        HealthSystem newHealthSystem = new HealthSystem();
        newHealthSystem.maxHealth = maxHealth;
        newHealthSystem.currentHealth = currentHealth;
        newHealthSystem.healthBar = healthBar;

        return newHealthSystem;
    }


    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}
