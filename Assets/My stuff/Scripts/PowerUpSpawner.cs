using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; 
    public Transform[] powerUpPositions; 

    void Start()
    {
        // Randomly choose a power-up prefab and position for each power-up tile
        for (int i = 0; i < powerUpPositions.Length; i++)
        {
           
            GameObject selectedPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

            Vector3 position = powerUpPositions[i].position;
            Instantiate(selectedPrefab, position, Quaternion.identity);
        }
    }
}
