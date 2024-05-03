using System.Collections;
using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    private Material originalMaterial;
    private new Renderer renderer;
    private GameManagerNEW gameManager;

    public Material hoverMaterial;
    public Material clickMaterial;
    public Material greenMaterial; // Material for reachable tiles
    public Material redMaterial;   // Material for unreachable tiles
    public float flashDuration = 0.2f;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
        gameManager = FindObjectOfType<GameManagerNEW>();
    }

    private void OnMouseEnter()
    {
        renderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        renderer.material = originalMaterial;
    }

    private void OnMouseDown()
    {
        StartCoroutine(FlashTile());

        // Check if the clicked tile is reachable
        bool isReachable = IsTileReachable();

        // Change the tile color based on reachability
        renderer.material = isReachable ? greenMaterial : redMaterial;
    }

    private IEnumerator FlashTile()
    {
        renderer.material = clickMaterial;
        yield return new WaitForSeconds(flashDuration);
        renderer.material = originalMaterial;
    }

    private bool IsTileReachable()
    {
        // Get references to the player controllers
        PlayerMovement1 player1 = gameManager.player1Controller;
        PlayerMovement2 player2 = gameManager.player2Controller;

        float distanceToPlayer1 = Vector3.Distance(player1.transform.position, transform.position);
        float distanceToPlayer2 = Vector3.Distance(player2.transform.position, transform.position);

        bool isPlayer1Reachable = distanceToPlayer1 <= player1.maxMoveDistance;
        bool isPlayer2Reachable = distanceToPlayer2 <= player2.maxMoveDistance;

   
        return isPlayer1Reachable || isPlayer2Reachable;
    }
}
