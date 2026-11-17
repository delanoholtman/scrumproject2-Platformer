using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "LevelPlay")
        {
            enabled = false;
        }
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