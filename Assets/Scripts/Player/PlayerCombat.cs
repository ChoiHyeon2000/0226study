using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemyLayer;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z) == true) 
        {
            Attack();
        }
    }

    void Attack() 
    {
        anim.SetTrigger("Attack");
    }

    public void DealDamage() 
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        for (int i = 0; i < hitEnemies.Length; i++) 
        {
            EnemyHealth enemyHealth = hitEnemies[i].GetComponent<EnemyHealth>();
            if (enemyHealth != null) 
            {
                enemyHealth.TakeDamage(1);
            }
        }
    }
}
