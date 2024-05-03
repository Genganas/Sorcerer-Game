using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthTile : MonoBehaviour
{
    private bool powerUpTriggered = false;
    private Collider powerUpCollider;
    private bool reset = false;

    private void Start()
    {
        // Get reference to the collider component
        powerUpCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a player and the power-up logic hasn't been triggered yet
        if (!powerUpTriggered && (other.CompareTag("Player1")))
        {
            // Increase the player's health by 5 points
            other.GetComponent<Health>().IncreaseHealth(15);
            TextMeshProUGUI player1powerup = GameObject.FindGameObjectWithTag("Player1Powerup")?.GetComponent<TextMeshProUGUI>();
            player1powerup.text = "Health effect to Player 1 +15.";
            player1powerup.color = Color.green;
            // Disable the collider temporarily to prevent multiple triggers
            powerUpTriggered = true;
         
        
        }
        else
        if (!powerUpTriggered && other.CompareTag("Player2"))
        {
            // Increase the player's health by 5 points
            other.GetComponent<Health>().IncreaseHealth(15);

            // Disable the collider temporarily to prevent multiple triggers
            powerUpTriggered = true;
          
            TextMeshProUGUI player2powerup = GameObject.FindGameObjectWithTag("Player2Powerup")?.GetComponent<TextMeshProUGUI>();
            player2powerup.text = "Health effect to Player 2 +15.";
            player2powerup.color = Color.green;
          
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") && powerUpTriggered)
        {
            // Re-enable the collider after the delay
            powerUpTriggered = false;
            DisableTile();
        }
        else
        if (other.CompareTag("Player2") && powerUpTriggered)
        {
            powerUpTriggered = false;
            DisableTile();
        }
    }

  
    private new Renderer renderer;
    public Material originalMaterial;
    public Material greyMaterial;
    private void DisableTile()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = greyMaterial;
        reset = true;
        powerUpCollider.enabled = false;
    }

    public void EnableTile()
    {
        if (GameManagerNEW.Instance != null && GameManagerNEW.Instance.GetTurnCount() == 0)
        {
            renderer.material = originalMaterial;
            powerUpCollider.enabled = true;
        }
    }
    private void Update()
    {
        if (reset)
        {
            if (GameManagerNEW.Instance != null && GameManagerNEW.Instance.GetTurnCount() == 0)
            {
                renderer.material = originalMaterial;
                Collider collider = GetComponent<Collider>();
                collider.enabled = true;
                reset = false;
            }

        }
    }
}