using UnityEngine;
using System.Collections;

public class SpeedOrb : MonoBehaviour
{
    public float boostMultiplier = 1.4f;
    public float duration = 5f; 

    private bool used = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (used || !other.CompareTag("Player")) return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
            StartCoroutine(Boost(player));
    }

    IEnumerator Boost(PlayerMovement player)
    {
        used = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        float originalSpeed = player.speed;
        player.speed = originalSpeed * boostMultiplier;

        yield return new WaitForSeconds(duration);

        player.speed = originalSpeed;
        Destroy(gameObject);
    }
}