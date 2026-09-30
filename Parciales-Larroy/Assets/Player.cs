using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]
    private float moveSpeed = 5f;

    [Header("Salto")]
    [SerializeField]
    private float jumpForce = 7f;

    [SerializeField]
    private float jumpStaminaCost = 5f;

    [Header("Vida")]
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float currentHealth;

    [Header("Estamina")]
    [SerializeField]
    private float maxStamina = 10f;

    [SerializeField]
    private float staminaRegen = 2f;

    private Rigidbody rb;

    
    [SerializeField]
    private float currentStamina;

    private bool isDead;
    private bool isGrounded;

    public bool IsDead => isDead;
    public float CurrentHealth => currentHealth;
    public float CurrentStamina => currentStamina;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        currentStamina = maxStamina;

        isDead = false;
        isGrounded = false;
    }

    private void Update()
    {
        RegenerateStamina();

        if (isDead)
            return;

        Jump();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = new Vector3(
                0f,
                rb.linearVelocity.y,
                0f
            );

            return;
        }

        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 movement =
            forward * vertical +
            right * horizontal;

        movement = Vector3.ClampMagnitude(movement, 1f);

        Vector3 velocity = movement * moveSpeed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    private void Jump()
    {
        if (!isGrounded)
            return;

        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (currentStamina < jumpStaminaCost)
            return;

        // Consumir estamina
        currentStamina -= jumpStaminaCost;

        // Evitar acumular velocidad vertical
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        // Realizar salto
        rb.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );

        // Ya no estamos en el suelo
        isGrounded = false;
    }

    private void RegenerateStamina()
    {
        if (isDead)
            return;

        currentStamina += staminaRegen * Time.deltaTime;

        currentStamina = Mathf.Clamp(
            currentStamina,
            0f,
            maxStamina
        );
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        currentHealth = 0f;
    }
}
