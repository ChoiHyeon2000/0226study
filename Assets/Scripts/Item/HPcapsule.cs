using UnityEngine;

public class HPcapsule : MonoBehaviour
{
    public int healAmount = 1;   // 회복량

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player 태그를 가진 오브젝트인지 확인
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
            }

            Destroy(gameObject);  // 아이템 제거
        }
    }
}
