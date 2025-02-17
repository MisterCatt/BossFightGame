using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float walkingSpeed = 3.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    void OnMove(InputValue value)
    {
        var moveDirection = value.Get<Vector2>();

        rb.linearVelocity = moveDirection * walkingSpeed;
    }
}
