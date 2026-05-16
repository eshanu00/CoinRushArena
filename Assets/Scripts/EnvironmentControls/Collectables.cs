using UnityEngine;

public class Collectables : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private CircleCollider2D _circleCollider;

    [SerializeField] private GameObject _paticleEffect;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Player"))
        {
            _circleCollider.enabled = false;
            audioSource.Play();

            _spriteRenderer.enabled = false;
            _animator.enabled = false;
            _paticleEffect.SetActive(true);

            Invoke(nameof(DestroySelf), 2f);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}