using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Test : MonoBehaviour
{
    private GameObject target;
    public float damage;
    public LayerMask WhatIsPlayers;
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;
    private bool hasDealDamage = false;
    public float desTroyDistance = 0.1f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
        if (hasDealDamage)
            return;
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance <= desTroyDistance)
            {
                DealDametoPlayer();
                hasDealDamage = true;
                Destroy(gameObject);
            }
        }
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void DealDametoPlayer()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, projectileRange, WhatIsPlayers);

        foreach (Collider2D hitPlayer in hitPlayers)
        {
            Health health = hitPlayer.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }



    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}