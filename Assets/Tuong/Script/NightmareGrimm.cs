using System.Collections;
using UnityEngine;

public class NightmareGrimm : MonoBehaviour
{
    public Transform[] teleportPoints; // Các điểm dịch chuyển xung quanh màn hình
    public GameObject fireballPrefab; // Quả cầu lửa
    public GameObject fireColumnPrefab; // Cột lửa
    public float speed;
    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    public float damage;
    public float pushForce;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking)
            return;

        StartCoroutine(AttackPattern());
    }

    private IEnumerator AttackPattern()
    {
        isAttacking = true;

        // Dịch chuyển đến một vị trí ngẫu nhiên
        Transform randomPoint = teleportPoints[Random.Range(0, teleportPoints.Length)];
        transform.position = Vector2.Lerp(transform.position, randomPoint.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);

        // Xác định đòn tấn công dựa trên xác suất
        float attackChance = Random.Range(0f, 1f);

        if (attackChance <= 0.4f) // 40% xác suất
        {
            // Tấn công bằng quả cầu lửa
            anim.SetTrigger("fireball");
            Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        else if (attackChance > 0.4f && attackChance <= 0.7f) // 30% xác suất
        {
            // Tấn công bằng cột lửa
            anim.SetTrigger("fireColumn");
            Instantiate(fireColumnPrefab, player.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        else // 30% xác suất còn lại
        {
            // Lướt về phía người chơi
            anim.SetTrigger("dash");
            Vector2 dashDirection = (player.position - transform.position).normalized;
            rb.AddForce(dashDirection * pushForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
        }

        isAttacking = false;
    }

    private void DealDamage()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0);
        if (hitPlayer != null && hitPlayer.CompareTag("Player"))
        {
            Health health = hitPlayer.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Rigidbody2D playerRb = hitPlayer.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 pushDirection = hitPlayer.transform.position - transform.position;
                    pushDirection.Normalize();
                    playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Transform point in teleportPoints)
        {
            Gizmos.DrawWireSphere(point.position, 0.5f);
        }
    }
}
