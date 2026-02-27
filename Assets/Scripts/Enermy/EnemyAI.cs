using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    public int nextMove = 1;  // 이동방향

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Collider2D col;

    public LayerMask groundLayer;

    void FixedUpdate()
    {
        // 앞으로 이동.
        rb.linearVelocity = new Vector2(nextMove * moveSpeed, rb.linearVelocity.y);

        // 시작점 : 몬스터의 앞쪽 바닥 (콜라이더 크기의 절반만큼 앞으로 가서 쏜다)
        Vector2 frontVec = new Vector2(rb.position.x + nextMove * 0.5f, rb.position.y);

        // 광선 쏘기.
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1.0f, groundLayer);
        RaycastHit2D rayHit2 = Physics2D.Raycast(frontVec, Vector3.right * nextMove, 0.2f, groundLayer);

        // 광선에 맞은 대상이 없을 경우 낭떠러지라고 판단하고 방향을 튼다.
        if (rayHit.collider == null || rayHit2.collider != null)
        {
            Turn();
        }
    }

    void Turn()
    {
        // 방향 뒤집기. (1 > -1, -1 > 1)
        nextMove = nextMove * -1;

        if (nextMove == 1)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        // 즉시 속도 반전
        rb.linearVelocity = new Vector2(nextMove * moveSpeed, rb.linearVelocity.y);
    }
}
