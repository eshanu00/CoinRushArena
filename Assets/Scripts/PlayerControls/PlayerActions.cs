using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _playerHealth = 100;
    private bool _isDead = false;

    private Vector2 _movement;
    private Rigidbody2D _rb;

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerSpriteTransform;
    [SerializeField] private Transform healthBar;
    [SerializeField] private float _playerSpeed = 2.5f;
    [SerializeField] private GameObject[] _obsticals;

    public bool IsDead { get { return _isDead; } }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        UpdateHealthBar();
    }

    private void FixedUpdate()
    {
        MovementHandle();
    }

    private void Update()
    {
        // Attack
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _animator.SetTrigger("Attack");
        }

        if (_playerHealth <= 0 && !_isDead)
        {
            Die();
        }

        if (!IsDead)
        {
            FlipToMouse();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            print(_playerHealth);
            TakeDamage(10);
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

    private void FlipToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x)
        {
            _playerSpriteTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _playerSpriteTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float)_playerHealth / _maxHealth;

        healthBar.localScale = new Vector3(healthPercent, 1f, 1f);
    }

    private void TakeDamage(int damage)
    {
        _playerHealth -= damage;

        _playerHealth = Mathf.Clamp(_playerHealth, 0, _maxHealth);

        UpdateHealthBar();
    }

    private void Die()
    {
        _isDead = true;

        _playerSpeed = 0f;

        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsDead", true);

        _rb.linearVelocity = Vector2.zero;
    }

    public void OnDeathAnimationFinished()
    {
        StopGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }
}// CLASS-END
