using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]
    private float moveSpeed = 3f;

    [Header("Persecución")]
    [SerializeField]
    private float detectionRange = 5f;

    [SerializeField]
    private Transform player;

    private Rigidbody rb;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void FixedUpdate()
    {
        if (enemyHealth.IsDead)
        {
            StopMovement();
            return;
        }

        if (player == null)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= detectionRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMovement();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;

        // No queremos que el enemigo intente subir/bajar hacia el jugador.
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.01f)
        {
            StopMovement();
            return;
        }

        direction.Normalize();

        Vector3 velocity = direction * moveSpeed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;

        transform.forward = direction;
    }

    private void StopMovement()
    {
        rb.linearVelocity = new Vector3(
            0f,
            rb.linearVelocity.y,
            0f
        );
    }
}