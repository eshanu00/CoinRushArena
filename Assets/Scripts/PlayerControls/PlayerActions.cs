using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private int _playerHealth = 100;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField] private float _playerSpeed = 2.5f;
    [SerializeField] private GameObject[] _obsticals;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovementHandle();
        // Attack
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _animator.SetTrigger("Attack");
        }
    }

    private void Update()
    {
        // Attack
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _animator.SetTrigger("Attack");
        }
    }

    private void MovementHandle()
    {
        // Normalization movement of the player
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        _rb.MovePosition(_rb.position + _playerSpeed * Time.fixedDeltaTime * _movement);

        // Walking animation
        _animator.SetBool("IsWalking", _movement != Vector2.zero);
    }
}
