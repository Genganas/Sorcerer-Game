using TMPro;
using UnityEngine;

public class FireTile : MonoBehaviour
{
    private bool effectApplied = false;
    private bool reset = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!effectApplied && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            PlayerMovement1 player1 = other.GetComponent<PlayerMovement1>();
            PlayerMovement2 player2 = other.GetComponent<PlayerMovement2>();

            if (player1 != null)
            {
                TextMeshProUGUI player1powerup = GameObject.FindGameObjectWithTag("Player1Powerup")?.GetComponent<TextMeshProUGUI>();
                player1.DoubleFireballDamageNextTurn();
               // Debug.Log("Fire Tile effect applied to Player 1.");
                player1powerup.text = "Fire Tile effect applied to Player 1.";
                player1powerup.color = Color.magenta;
            }
            if (player2 != null)
            {
                TextMeshProUGUI player2powerup = GameObject.FindGameObjectWithTag("Player2Powerup")?.GetComponent<TextMeshProUGUI>();
                player2.DoubleFireballDamageNextTurn();
             //   Debug.Log("Fire Tile effect applied to Player 2.");
                player2powerup.text = "Fire Tile effect applied to Player 2.";
                player2powerup.color = Color.magenta;
            }

            effectApplied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (effectApplied && (other.CompareTag("Player1")))
        {
            PlayerMovement1 player1 = other.GetComponent<PlayerMovement1>();
            if (player1 != null)
            {
                player1.ResetFireballDamageNextTurn();
            }

            effectApplied = false;
 
          //  Debug.Log("Fire Tile effect removed.");
            DisableTile();
        }
        else
        if (effectApplied && other.CompareTag("Player2"))
        {
            PlayerMovement2 player2 = other.GetComponent<PlayerMovement2>();
            if (player2 != null)
            {
                player2.ResetFireballDamageNextTurn();
            }
            effectApplied = false;
          //  Debug.Log("Fire Tile effect removed.");
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
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        reset = true;
    }

    public void EnableTile()
    {
        if (GameManagerNEW.Instance != null && GameManagerNEW.Instance.GetTurnCount() == 0)
        {
            renderer.material = originalMaterial;
            Collider collider = GetComponent<Collider>();
            collider.enabled = true;
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