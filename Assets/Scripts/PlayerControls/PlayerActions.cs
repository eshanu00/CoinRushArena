using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private float _playerSpeed = 1.0f;
    private int _playerHealth = 100;
    private Vector2 _movement;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject[] _obsticals;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        // Normalization movement of the player
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _playerSpeed * Time.fixedDeltaTime);
    }
}
