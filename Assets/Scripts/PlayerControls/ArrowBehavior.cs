using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    private Camera _cam;

    [SerializeField] private float radius = 1.0f;

    private Transform _player;
    private PlayerActions playerActions;

    private void Start()
    {
        _cam = Camera.main;
        _player = transform.parent;
        playerActions = _player.GetComponent<PlayerActions>();
    }

    private void Update()
    {
        if (!playerActions.IsDead)
        {
            RotateArrow();
        }
    }

    private void RotateArrow()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - _player.position).normalized;

        transform.localPosition = direction * radius;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}