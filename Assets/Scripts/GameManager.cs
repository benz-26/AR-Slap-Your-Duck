using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] spawnerOne;
    [SerializeField] private GameObject[] spawnerTwo;
    [SerializeField] private GameObject[] spawnerThree;

    public string triggerTag = "Player"; // Tag to trigger destruction
    public string borderTag = "Border"; // Tag for the border objects
    public float minSpawnInterval = 0.5f; // Minimum spawn interval
    public float maxSpawnInterval = 2f; // Maximum spawn interval
    public float minSpawnDistance = 2f; // Minimum distance between spawned objects
    public int borderTriggerCount = 5; // Number of triggers for game over
    public Transform[] spawnPoints; // Array of spawn points

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource1;
    


    [SerializeField] private int score = 0; // Player score
    [SerializeField] private TextMeshProUGUI scoreText; // Text to display the score
    [SerializeField] private TextMeshProUGUI[] scoringText; // Text to display the score
    [SerializeField] private TextMeshProUGUI triggerText; // Text to display the score

    [SerializeField] private GameObject gameOverCanvasCupu;
    [SerializeField] private GameObject gameOverCanvasLumayan;
    [SerializeField] private GameObject gameOverCanvasMantap;

    public float movementSpeed = 12f; // Speed of movement along the Z-axis

    private List<GameObject> spawnedObjects = new List<GameObject>(); // List to keep track of spawned objects


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverCanvasCupu.SetActive(false);
        gameOverCanvasLumayan.SetActive(false);
        gameOverCanvasMantap.SetActive(false);
        StartCoroutine(Spawn());

        scoreText.text = "Score: " + score;
        for (int i = 0; i < scoringText.Length; i++)
        {
            scoringText[i].text = "Score: " + score;
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        triggerText.text = "Trigger: " + borderTriggerCount.ToString();

        for (int i = 0; i < scoringText.Length; i++)
        {
            scoringText[i].text = "Score: " + score.ToString();
        }

        // Move spawned objects along the Z-axis
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                obj.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            // Randomize the spawn interval
            float spawnInterval = 1.2f;

            // Spawn objects from spawnerOne
            yield return new WaitForSeconds(spawnInterval);
            GameObject obj1 = Instantiate(spawnerOne[Random.Range(0, spawnerOne.Length)], GetRandomSpawnPoint(), Quaternion.identity);
            spawnedObjects.Add(obj1);

            // Spawn objects from spawnerTwo
            yield return new WaitForSeconds(spawnInterval);
            GameObject obj2 = Instantiate(spawnerTwo[Random.Range(0, spawnerTwo.Length)], GetRandomSpawnPoint(), Quaternion.identity);
            spawnedObjects.Add(obj2);

            // Spawn objects from spawnerThree
            yield return new WaitForSeconds(spawnInterval);
            GameObject obj3 = Instantiate(spawnerThree[Random.Range(0, spawnerThree.Length)], GetRandomSpawnPoint(), Quaternion.identity);
            spawnedObjects.Add(obj3);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // Choose a random spawn point from the array of Transform points
        Transform spawnTransform = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return spawnTransform.position;
    }

    public void UpdateScore()
    {
        // Increase score when player triggers the spawned object
        score++;
        audioSource.Play();
        Debug.Log("Score: " + score);
    }

    public void UpdateTrigger()
    {
        // Game over if the spawned object collides with the border
        borderTriggerCount--;
        Debug.Log("Kena");
        if (borderTriggerCount <= 0 && score < 10)
        {
            gameOverCanvasCupu.SetActive(true);
        }
        else if (borderTriggerCount <= 0 && score > 10)
        {
            gameOverCanvasLumayan.SetActive(true);
        }
        else if (borderTriggerCount <= 0 && score > 40)
        {
            gameOverCanvasMantap.SetActive(true);
        }
    }


}
