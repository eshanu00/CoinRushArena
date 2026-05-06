using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private int _playerHealth = 100;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    [SerializeField] private float _playerSpeed = 2.5f;
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
