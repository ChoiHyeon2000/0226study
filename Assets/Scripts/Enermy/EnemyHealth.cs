using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public bool isInvincible = false;

    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        Debug.Log("적이 큰 데미지를 입음" + amount);
        currentHealth = currentHealth - amount;

        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
        }

        if (isInvincible == true)
        {
            return;
        }

        currentHealth = currentHealth - amount;

        if (currentHealth <= 0)
        {
            // 사망 처리.
            Die();
        }
        else
        {
            // 잠시 무적 상태로 돌입. 코루틴 시작.
            StartCoroutine(OnDamageProcess());
        }
    }
    void Die()
    {
        Debug.Log("플레이어 사망!");

        // 게임 오브젝트를 비활성화 처리.
        gameObject.SetActive(false);
    }
    IEnumerator OnDamageProcess()
    {
        isInvincible = true;

        // 스프라이트 렌더러의 색상을 설정한다.
        // 투명도를 절반으로 내려서 반투명 상태로 변경
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        yield return new WaitForSeconds(0.2f);

        isInvincible = false;
    }
}
