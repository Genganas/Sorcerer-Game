using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth; 
    public TextMeshProUGUI healthText; 
    private string playerName; // Name of the player (for display purposes)

    private void Start()
    {
       
        currentHealth = maxHealth;
        playerName = gameObject.name;

        // Update the UI text element to display the initial health
        UpdateHealthUI();
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        // Subtract damage from current health
        currentHealth -= damage;

        // Update the UI text element to reflect the new health
        UpdateHealthUI();

        // Check if the player's health drops below zero
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if health is zero or below
        }
    }

    public void Heal(int healAmount)
    {
        // Add healAmount to current health, but ensure it doesn't exceed max health
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);

        // Update the UI text element to reflect the new health
        UpdateHealthUI();
    }

    // Method to handle player death
    private void Die()
    {
      //  Debug.Log(playerName + " has died.");

        // Check which player is not null and load the corresponding win scene
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");

        if (player1 != null && player1.GetComponent<Health>().currentHealth <= 0)
        {
            SceneManager.LoadScene("Player2Win");
        }
        else if (player2 != null && player2.GetComponent<Health>().currentHealth <= 0)
        {
            SceneManager.LoadScene("Player1Win");
        }
    }

    // Method to update the UI text element with the current health
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = playerName + " Health: " + currentHealth.ToString();
        }
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health doesn't exceed maxHealth
      //  Debug.Log("Health increased by " + amount + ". Current health: " + currentHealth);
        UpdateHealthUI();
    }
}
