using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private float _playerSpeed = 1.0f;
    private int _playerHealth = 100;

    [SerializeField] private GameObject[] _obsticals;

    private void Start()
    {
        
    }

    private void Update()
    {
        // Player Movements with normalization
        Vector2 movement = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(_playerSpeed * 5 * Time.deltaTime * movement.normalized);
    }
}
