using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public PlayerMovement1 player1Controller; // Reference to the opponent player controller
    public float moveSpeed = 5f;
    public LayerMask tileLayer;
    public float maxMoveDistance = 1.1f; // Adjust this threshold as needed
    public float attackRange = 3f; // Adjust the attack range as needed
    public int attackDamage = 10; // Adjust the attack damage as needed
    bool selectedPlayer = false;
    [SerializeField] Transform startingPosition;
    private bool isMoving = false;
    public GameObject fireballPrefab;
    public int fireballSpeed;

    public Health health;
    public int defenseBonus;

    public TextMeshProUGUI notyourturnText;

    private void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
            {
                Vector3 targetPosition = hit.collider.transform.position;

                if (IsAdjacent(transform.position, targetPosition))
                {
                    StartCoroutine(MoveToTile(targetPosition));

                    // End player's turn when movement is complete
                    FindObjectOfType<GameManagerNEW>().EndPlayerTurn();
                }
            }
        }

        // Check for attack input
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
    }

    private bool IsAdjacent(Vector3 currentPosition, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(currentPosition, targetPosition);

        // Check if the distance is within the threshold
        return distance <= maxMoveDistance;
    }

    private IEnumerator MoveToTile(Vector3 targetPosition)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    public void Attack()
    {
        // Check if it's the player's turn
        if (!FindObjectOfType<GameManagerNEW>().IsPlayer2Turn())
        {
          //  Debug.Log("It's not your turn.");
            StartCoroutine(DisplayAndFade(notyourturnText, "Player 2 it's not your turn."));
            return;
        }

        // Check if the player is within attack range of the opponent
        float distanceToOpponent = Vector3.Distance(transform.position, player1Controller.transform.position);
        if (distanceToOpponent <= attackRange)
        {
            // Find the Health script attached to the opponent object
            Health opponentHealth = player1Controller.GetComponent<Health>();

            if (opponentHealth != null)
            {
                // Instantiate the fireball prefab at the opponent's position
                GameObject fireball = Instantiate(fireballPrefab, player1Controller.transform.position, Quaternion.identity);

                // Move the fireball towards the opponent player
                Vector3 direction = (player1Controller.transform.position - transform.position).normalized;
                fireball.GetComponent<Rigidbody>().velocity = direction * fireballSpeed;

                // Deal damage to the opponent
                opponentHealth.TakeDamage(attackDamage);

              //  Debug.Log("Player 2 attacks Player 1 with a fireball.");

                // End the player's turn
                FindObjectOfType<GameManagerNEW>().EndPlayerTurn();
            }
            else
            {
           //     Debug.Log("Opponent's health script not found.");
            }
        }
        else
        {
         //   Debug.Log("Cannot reach opponent.");
        }
    }



    public void SetSelectedPlayer(bool isSelected)
    {
        selectedPlayer = isSelected;
    }

    public void RespawnToStartingPosition()
    {
        // Reset the position of the player's character to the starting position
        transform.position = startingPosition.position;
    }
    public void TakeDamage(int damage)
    {
        damage = 10;
        health.TakeDamage(damage);
    }

    public void Defend()
    {
        // Check if it's the player's turn
        if (!FindObjectOfType<GameManagerNEW>().IsPlayer2Turn())
        {
        //    Debug.Log("It's not your turn.");
            notyourturnText.text = "Player 2 it's not your turn.";
            return;
        }

        health.Heal(defenseBonus); 

        Debug.Log("Player 2 defends and gains health.");
        notyourturnText.text = "Player 2 defends and gains health";
        // End the player's turn
        FindObjectOfType<GameManagerNEW>().EndPlayerTurnAfterDefense();
    }

    public void DoubleFireballDistance()
    {
        // Double the fireball distance for Player 1
        attackRange *= 2;
    }

    public void ResetFireballDistance()
    {
        // Reset the fireball distance for Player 1
        attackRange /= 2;
    }

    public void DoubleFireballDamageNextTurn()
    {
        // Double the fireball damage for Player 1's next turn
        attackDamage *= 2;
    }

    public void ResetFireballDamageNextTurn()
    {
        // Reset the fireball damage for Player 1's next turn
        attackDamage /= 2;
    }

    private IEnumerator DisplayAndFade(TextMeshProUGUI textMeshPro, string text)
    {
        textMeshPro.text = text;
        Color originalColor = textMeshPro.color;
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSeconds(2f); 

        float fadeDuration = 1.5f; 
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

}