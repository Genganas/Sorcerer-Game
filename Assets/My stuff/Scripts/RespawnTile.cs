using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RespawnTile : MonoBehaviour
{
    private bool respawnTriggered = false;
    private Collider respawnCollider;
    private bool reset = false;

    private void Start()
    {
        respawnCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a player and respawn logic hasn't been triggered yet
        if (!respawnTriggered && (other.CompareTag("Player1")))
        {
            // Respawn both players
            TextMeshProUGUI player1powerup = GameObject.FindGameObjectWithTag("Player1Powerup")?.GetComponent<TextMeshProUGUI>();
            player1powerup.text = "Time spell has activated, return to the past";
            TextMeshProUGUI player2powerup = GameObject.FindGameObjectWithTag("Player2Powerup")?.GetComponent<TextMeshProUGUI>();
            player2powerup.text = "Time spell has activated, return to the past";
            player1powerup.color = Color.yellow;
            player2powerup.color = Color.yellow;
            respawnTriggered = true;
     
            StartCoroutine(EnableColliderAfterDelay(0.8f));
        }
        else
        if (!respawnTriggered && (other.CompareTag("Player2")))
        {
            // Respawn both players
            TextMeshProUGUI player2powerup = GameObject.FindGameObjectWithTag("Player2Powerup")?.GetComponent<TextMeshProUGUI>();
            player2powerup.text = "Time spell has activated, return to the past";
            TextMeshProUGUI player1powerup = GameObject.FindGameObjectWithTag("Player1Powerup")?.GetComponent<TextMeshProUGUI>();
            player1powerup.text = "Time spell has activated, return to the past";
            player1powerup.color = Color.yellow;
            player2powerup.color = Color.yellow;
            respawnTriggered = true;
          

            StartCoroutine(EnableColliderAfterDelay(0.8f));
        }
    }

    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManagerNEW.Instance.RespawnPlayers();
    
        reset = true;
    }
    public void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player1") && respawnTriggered)
        {
            respawnTriggered = false;
            DisableTile();
        }
        else
        if (other.CompareTag("Player2") && respawnTriggered)
        {
            respawnTriggered = false;
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
      
        respawnCollider.enabled = false;
        
    }

    public void EnableTile()
    {
        if (GameManagerNEW.Instance != null && GameManagerNEW.Instance.GetTurnCount() == 0)
        {
            renderer.material = originalMaterial;
          
        }
    }
    private void Update()
    {
        if (reset)
        {
            if (GameManagerNEW.Instance != null && GameManagerNEW.Instance.GetTurnCount() == 0)
            {
                renderer.material = originalMaterial;
                respawnCollider.enabled = true;
                reset = false;
            }

        }
    }

}