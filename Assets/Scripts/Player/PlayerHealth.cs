using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public bool isInvincible = false;

    public SpriteRenderer spriteRenderer;

    public Image imageHPBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // HP 초기화
        currentHealth = maxHealth;

        UpdateUI();
    }

    /// <summary>
    /// 대미지 적용
    /// </summary>
    /// <param name="amount">대미지 양</param>
    public void TakeDamage(int amount)
    {
        if (isInvincible == true)
        {
            return;
        }

        currentHealth = currentHealth - amount;

        UpdateUI();

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
    public void Heal(int amount) 
    {
        currentHealth = currentHealth + amount;

        UpdateUI();
    }
    // Update is called once per frame
    void Die()
    {
        Debug.Log("플레이어 사망!");
        
        GameManager.instance.SetGameOver(true);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap") == true)
        {
            TakeDamage(1);
        }
    }
    
    void UpdateUI()
    {
        if(currentHealth < 0) 
        {
            imageHPBar.fillAmount = 0.0f;
            return;
        }

        imageHPBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
