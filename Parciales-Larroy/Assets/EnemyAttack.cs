
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Ataque")]
    [SerializeField]
    private float attackRange = 5f;

    [SerializeField]
    private float damage = 20f;

    [SerializeField]
    private float fireRate = 2f;

    [Header("Objetivo")]
    [SerializeField]
    private Transform player;

    private EnemyHealth enemyHealth;

    private float nextAttackTime;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (enemyHealth.IsDead)
            return;

        if (player == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance > attackRange)
            return;

        if (Time.time < nextAttackTime)
            return;

        Attack();
    }

    private void Attack()
    {
        nextAttackTime = Time.time + fireRate;

        Vector3 direction = player.position - transform.position;

        direction.Normalize();

        Ray ray = new Ray(
            transform.position,
            direction
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            attackRange))
        {
            Debug.Log("Enemigo disparó a: " + hit.collider.name);

            Player playerHealth =
                hit.collider.GetComponent<Player>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                Debug.Log("El jugador recibió " + damage + " de daño.");
            }
        }
    }
}
