using UnityEngine;
using System.Collections;

public class JumpOrb : MonoBehaviour
{
    public float boostMultiplier = 1.5f;
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

        float originalJump = player.jumpForce;
        player.jumpForce = originalJump * boostMultiplier;

        yield return new WaitForSeconds(duration);

        player.jumpForce = originalJump;
        Destroy(gameObject);
    }
}