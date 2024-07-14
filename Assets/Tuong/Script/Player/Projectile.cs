using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //[SerializeField] private float moveSpeed = 22f;
    //[SerializeField] private bool isEnemyProjectile = false;
    //[SerializeField] private float projectileRange = 10f;

    //private Vector3 startPosition;
    //private Player player;

    //private void Start()
    //{
    //    startPosition = transform.position;

    //    // Tìm đối tượng player bằng tag
    //    GameObject playerObject = GameObject.FindWithTag("Player");
    //    if (playerObject != null)
    //    {
    //        player = playerObject.GetComponent<Player>();
    //    }
    //}

    //private void Update()
    //{
    //    MoveProjectile();
    //    DetectFireDistance();
    //}

    //public void UpdateProjectileRange(float projectileRange)
    //{
    //    this.projectileRange = projectileRange;
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
    //    Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

    //    if (!other.isTrigger && (enemyHealth || indestructible || player != null))
    //    {
    //        if ((player != null && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
    //        {
    //            Destroy(gameObject);
    //        }
    //        else if (!other.isTrigger && indestructible)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    //private void DetectFireDistance()
    //{
    //    if (Vector3.Distance(transform.position, startPosition) > projectileRange)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void MoveProjectile()
    //{
    //    transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    //}
}
