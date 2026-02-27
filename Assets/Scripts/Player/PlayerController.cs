using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float accel = 30f;

    public float jumpPower = 10.0f;
    public Rigidbody2D rb;

    public Animator anim;

    public SpriteRenderer spriteRenderer;

    public Transform groundCheck;   // 지면 체크를 위해 레이캐스트를 수행할 중심위치.
    public float rayLength = 0.2f;
    public LayerMask groundLayer;  // 

    public AudioClip audioJump;

    // 플레이어가 지면에 착지해 있는지 여부를 저장할 변수.
    bool isGrounded = false;
    
    float h = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGround();

        // 좌우 방향키 A, D
        h = Input.GetAxis("Horizontal");

        bool isLeftCtrl = Input.GetKey(KeyCode.LeftControl);
        if (isLeftCtrl == true)
        {
            accel = 1.5f;
        }
        else
        {
            accel = 1.0f;
        }
        
        // 스페이스 키를 누르면 위쪽 방향으로 순간적인 힘을 가한다.
        // 현재 지면 위에 서있는지 여부를 같이 체크한다.
        if (Input.GetKeyDown(KeyCode.Space) == true && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            AudioManager.instance.PlaySFX(audioJump);
        }

        // 방향키를 눌렀으면 Run 애니메이션으로 전환. 누르지 않았으면 Idle 애니메이션으로 전환.
        if (h != 0.0f)
        {
            anim.SetBool("isRun", true);
        }
        else if (h == 0.0f)
        {
            anim.SetBool("isRun", false);
        }

        if (h > 0.0f) 
        {
            spriteRenderer.flipX = false;
        }
        else if(h < 0.0f) 
        {
            spriteRenderer.flipX = true;
        }

        anim.SetBool("isGround", isGrounded);
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(h * speed * accel, rb.linearVelocity.y);
    }

    /// <summary>
    /// 플레이어가 지면에 있는지 체크하는 함수.
    /// </summary>
    /// <returns></returns>
    bool CheckGround() 
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);
        if(hit.collider != null) 
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Collider2D가 부착된 오브젝트끼리 충돌했을 때 유니티가 자동으로 호출해주는 함수.
    /// </summary>
    /// <param name="collision"></param>
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground") == true)
    //    {
    //        isGrounded = true;
    //        Debug.Log("땅에 닿았습니다.");
    //    }
    //}

    /// <summary>
    /// Trigger가 켜진 오브젝트와 겹쳤을 때 호출되는 함수
    /// </summary>
    /// <param name="collision"></param>
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Ground") == true) 
    //    {
    //        isGrounded = false;
    //        Debug.Log("땅에서 떨어졌습니다.");
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3.down * rayLength));
    }
}
