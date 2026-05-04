using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }
}