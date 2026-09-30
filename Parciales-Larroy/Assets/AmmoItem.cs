using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    [SerializeField]
    private int ammoAmount = 5;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();

        if (playerAttack != null)
        {
            playerAttack.AddAmmo(ammoAmount);

            Destroy(gameObject);
        }
    }
}
