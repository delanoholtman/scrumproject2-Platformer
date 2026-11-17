using UnityEngine;
using UnityEngine.SceneManagement;

public class PatrolEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float edgeCheckDistance = 0.5f;

    private Rigidbody2D rb;
    private Collider2D col;
    private int direction = 1;
    private float flipCooldown = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (SceneManager.GetActiveScene().name != "LevelPlay")
        {
            if (rb != null) rb.simulated = false;
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        flipCooldown -= Time.fixedDeltaTime;

        if (flipCooldown <= 0f && IsGrounded() && !GroundAhead())
        {
            Flip();
        }
    }

    bool IsGrounded()
    {
        return Mathf.Abs(rb.linearVelocity.y) < 0.05f;
    }

    bool GroundAhead()
    {
        Bounds b = col.bounds;
        Vector2 origin = new Vector2(
            b.center.x + direction * (b.extents.x + 0.05f),
            b.min.y + 0.05f);

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, Vector2.down, edgeCheckDistance + 0.05f);
        foreach (RaycastHit2D h in hits)
        {
            if (h.collider != col && !h.collider.isTrigger)
                return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name != "LevelPlay") return;

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                direction = contact.normal.x > 0 ? 1 : -1;
                flipCooldown = 0.3f;
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name != "LevelPlay") return;

        string objName = other.gameObject.name.ToLower();
        if (objName.Contains("spike") || objName.Contains("saw") || objName.Contains("zaag") || other.CompareTag("Spike") || other.CompareTag("Saw"))
        {
            direction *= -1;
            flipCooldown = 0.3f;
        }
    }

    void Flip()
    {
        direction *= -1;
        flipCooldown = 0.3f;
    }

    void OnDrawGizmosSelected()
    {
        if (col == null) col = GetComponent<Collider2D>();
        if (col == null) return;
        Bounds b = col.bounds;
        Vector2 origin = new Vector2(b.center.x + direction * (b.extents.x + 0.05f), b.min.y + 0.05f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + Vector2.down * (edgeCheckDistance + 0.05f));
    }
}