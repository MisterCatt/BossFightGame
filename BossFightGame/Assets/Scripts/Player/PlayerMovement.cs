using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Player Player;

    public enum PlayerMoveState{STILL, MOVING, SLIDING, SPAWNING}
    public PlayerMoveState MoveState = PlayerMoveState.STILL;

    [SerializeField] private Transform _SpawnTargetLocation;
    [SerializeField] private float _WalkToSpawnTargetLocationTime = 2f;

    public bool CanSlide = true;

    private Rigidbody2D rb;

    [SerializeField] private float _walkingSpeed = 3.0f;
    [SerializeField] private Vector2 _moveDirection;
    public float SlideSpeed = 10f;
    [SerializeField] private float _slideTime = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
        MoveState = PlayerMoveState.SPAWNING;
        _SpawnTargetLocation.parent = null;
        transform.DOMove(_SpawnTargetLocation.position, _WalkToSpawnTargetLocationTime);
        yield return new WaitForSeconds(_WalkToSpawnTargetLocationTime);
        MoveState = PlayerMoveState.STILL;
        Destroy(_SpawnTargetLocation.gameObject);
    }

    private void Update()
    {
        if (MoveState == PlayerMoveState.SLIDING || MoveState == PlayerMoveState.SPAWNING) return;
        MoveState = _moveDirection == new Vector2() ? PlayerMoveState.STILL : PlayerMoveState.MOVING;
        rb.linearVelocity = _moveDirection * _walkingSpeed;

        if(MoveState == PlayerMoveState.MOVING)
            Player.GetPlayerAnimator().SetBool("Running", true);
        else
            Player.GetPlayerAnimator().SetBool("Running", false);
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
