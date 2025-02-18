using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Player Player;

    public enum PlayerMoveState{STILL, MOVING, SLIDING}
    public PlayerMoveState MoveState = PlayerMoveState.STILL;

    public bool CanSlide = true;

    private Rigidbody2D rb;

    [SerializeField] private float _walkingSpeed = 3.0f;
    [SerializeField] private Vector2 _moveDirection;
    public float SlideSpeed = 10f;
    [SerializeField] private float _slideTime = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (MoveState == PlayerMoveState.SLIDING) return;
        MoveState = _moveDirection == new Vector2() ? PlayerMoveState.STILL : PlayerMoveState.MOVING;
        rb.linearVelocity = _moveDirection * _walkingSpeed;
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();
    }

    public IEnumerator PerformSlide()
    {
        Debug.Log("SLIDING");

        Player.RigidBody.AddForce(Player.PlayerMovement.GetMoveDirection() * SlideSpeed);

        MoveState = PlayerMoveState.SLIDING;
        CanSlide = false;

        float time = 0;

        while (time <= _slideTime)
        {
            time += Time.deltaTime;

            yield return null;
        }

        yield return null;
        
        MoveState = PlayerMoveState.STILL;
        CanSlide = true;
    }
}
