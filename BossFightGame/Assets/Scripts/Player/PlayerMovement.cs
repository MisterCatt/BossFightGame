using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float _walkingSpeed = 3.0f;
    [SerializeField] private Vector2 _moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();

        rb.linearVelocity = _moveDirection * _walkingSpeed;
    }
}
