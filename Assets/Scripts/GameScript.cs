using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerComponent : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameObject crystalsPrefab;
    public GameObject robotsPrefab;
    private Rigidbody rb;

    public TMP_Text scoreText;
    private int score = 0;

    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score: " + score;
        InvokeRepeating(nameof(SpawnCrystals), 2f, 2f);
        InvokeRepeating(nameof(SpawnRobots), 3f, 3f);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void SpawnCrystals()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-7f, 7f), 3f, 3f);
        Instantiate(crystalsPrefab, spawnPosition, Quaternion.identity);

        GameObject crystals = Instantiate(crystalsPrefab, spawnPosition, Quaternion.identity);
        Destroy(crystalsPrefab, 4f);
    }

    void SpawnRobots()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-7f, 7f), 3f, 3f);

        Instantiate(robotsPrefab, spawnPosition, Quaternion.identity);

        GameObject robots = Instantiate(robotsPrefab, spawnPosition, Quaternion.identity);
        Destroy(robotsPrefab, 4f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(horizontalInput * movementSpeed, rb.linearVelocity.y, verticalInput * movementSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            score += 50;
            scoreText.text = "Score: " + score;
            Destroy(other.gameObject);

            if (score < 0)
                RestartScene();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            score -= 100;
            scoreText.text = "Score: " + score;
            Destroy(other.gameObject);

            if (score < 0)
                RestartScene();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            score += 50;
            scoreText.text = "Score: " + score;
            Destroy(other.gameObject);

            if (score < 0)
                RestartScene();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            score -= 100;
            scoreText.text = "Score: " + score;
            Destroy(other.gameObject);

            if (score < 0)
                RestartScene();
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(
        SceneManager.GetActiveScene().buildIndex
        );
    }
}