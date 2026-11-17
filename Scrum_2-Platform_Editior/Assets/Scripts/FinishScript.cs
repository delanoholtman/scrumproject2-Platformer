using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && SceneManager.GetActiveScene().name != "LevelPlay")
        {
            rb.simulated = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name != "LevelPlay") return;

        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.stopTimer();
            SceneManager.LoadScene("EndScreen");
        }
    }
}