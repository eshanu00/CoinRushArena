using System.Collections;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _playerHealth = 100;

    private bool _isDead = false;
    private bool _canHeal = true;
    private bool _isAttacking = false;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private PolygonCollider2D _enemyAttackCollider;

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerSpriteTransform;
    [SerializeField] private GameObject _attackBox;

    [Header("UI")]
    [SerializeField] private GameObject healthBarPanel;
    [SerializeField] private Transform healthBar;

    [Header("Movement")]
    [SerializeField] private float _playerSpeed = 2.5f;

    [Header("Attack")]
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float attackCooldown = 0.4f;

    [Header("Audio")]
    [SerializeField] private AudioSource _ASWalk;
    [SerializeField] private AudioSource _ASHit;
    [SerializeField] private AudioSource _ASAttack;
    [SerializeField] private AudioSource _ASDead;

    public bool IsDead => _isDead;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.freezeRotation = true;

        _enemyAttackCollider = _attackBox.GetComponent<PolygonCollider2D>();

        _enemyAttackCollider.enabled = false;

        UpdateHealthBar();
    }

    private void Update()
    {
        if (_isDead)
            return;

        HandleAttack();

        FlipToMouse();

        if (_playerHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (_isDead || _isAttacking)
            return;

        MovementHandle();
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !_isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;

        _ASAttack.PlayOneShot(_ASAttack.clip);

        _animator.SetTrigger("Attack");

        // Enable hitbox
        _enemyAttackCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        // Disable hitbox
        _enemyAttackCollider.enabled = false;

        yield return new WaitForSeconds(attackCooldown);

        _isAttacking = false;
    }

    private void MovementHandle()
    {
        _movement = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;

        _rb.MovePosition(
            _rb.position + _playerSpeed * Time.fixedDeltaTime * _movement
        );

        bool isWalking = _movement != Vector2.zero;

        _animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!_ASWalk.isPlaying)
            {
                _ASWalk.Play();
            }
        }
        else
        {
            _ASWalk.Stop();
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heart"))
        {
            TakeHeal();
        }

        if (collision.CompareTag("EnemyMale") || collision.CompareTag("EnemyFemale"))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damage)
    {
        if (_isDead)
            return;

        _ASHit.PlayOneShot(_ASHit.clip);

        _playerHealth -= damage;

        _playerHealth = Mathf.Clamp(_playerHealth, 0, _maxHealth);

        UpdateHealthBar();
    }

    private void TakeHeal()
    {
        if (!_canHeal)
            return;

        _canHeal = false;

        _playerHealth += 10;

        _playerHealth = Mathf.Clamp(_playerHealth, 0, _maxHealth);

        UpdateHealthBar();

        Invoke(nameof(EnableHeal), 0.2f);
    }

    private void EnableHeal()
    {
        _canHeal = true;
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float)_playerHealth / _maxHealth;

        healthBar.localScale = new Vector3(healthPercent, 1f, 1f);

        healthBarPanel.SetActive(healthPercent < 1);
    }

    private void Die()
    {
        _isDead = true;
        _ASWalk.Stop();

        _playerSpeed = 0f;

        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsDead", true);

        _rb.linearVelocity = Vector2.zero;

        _ASDead.PlayOneShot(_ASDead.clip);
    }

    public void OnDeathAnimationFinished()
    {
        StopGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }
}