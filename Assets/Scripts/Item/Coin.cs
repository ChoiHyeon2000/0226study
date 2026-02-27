using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip audioCoin;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true) 
        {
            Debug.Log("µ¿Àü È¹µæ!!!");
            GameManager.instance.AddScore(100);
            AudioManager. instance.PlaySFX(audioCoin);

            Destroy(gameObject);
        }
    }
}
