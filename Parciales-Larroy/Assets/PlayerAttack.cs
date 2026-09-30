

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Arma")]
    [SerializeField]
    private float range = 20f;

    [SerializeField]
    private float damage = 25f;

    [SerializeField]
    private float fireRate = 1.5f;

    [SerializeField]
    private int bullets = 10;

    [Header("Cámara")]
    [SerializeField]
    private Camera playerCamera;

    private Player player;

    private float nextFireTime;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.IsDead)
            return;

        if (bullets <= 0)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + fireRate;

        bullets--;

        // Centro de la pantalla
        Vector3 screenCenter = new Vector3(
            0.5f,
            0.5f,
            0f
        );

        Ray ray = playerCamera.ViewportPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Disparo impactó en: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Debug.Log("Disparo. Balas restantes: " + bullets);
    }
    public void AddAmmo(int amount)
    {
        bullets += amount;

        Debug.Log("Munición recogida. Balas disponibles: " + bullets);
    }
}