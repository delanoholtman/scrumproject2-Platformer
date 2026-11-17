using UnityEngine;
using UnityEngine.SceneManagement;

public class SawScript : MonoBehaviour
{
    public float spinSpeed = 360f;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "LevelPlay")
        {
            enabled = false;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, -spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name != "LevelPlay") return;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>()?.Die();
        }
    }
}