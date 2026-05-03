using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Player took damage! Current Health: " + health);

        if (health <= 0)
        {
            Debug.Log("Game Over! Player died!");
            Destroy(gameObject);
        }
    }
}