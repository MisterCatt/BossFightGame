using System;
using UnityEngine;

public class Player : Unit, IHealable
{
    public enum TargetDirection { Left, Right, Up, Down }
    public TargetDirection DirectionToTarget = TargetDirection.Right;

    [Header("Player class")]
    [Space]
    [Header("Manual assignment")]
    [Space]
    public PlayerMovement PlayerMovement;

    [SerializeField] private Transform _shootTargetPosition;
    [SerializeField] private GameObject _projectileSpawnPoint, _MeleHitbox;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private SpriteRenderer _PlayerSpriteRenderer;

    [Header("Automatic assignment, dont touch")]
    [Space]
    public PlayerClass PlayerClass;
    [SerializeField] private GameObject _targetEnemy;

    private void Start()
    {
        if(PlayerManager.Instance)
            PlayerManager.Instance.SetPlayer(this);

        _shootTargetPosition.parent = null;
    }

    private void Update()
    {
        if(GetCurrentTarget().transform.position.x < transform.position.x)
        {
            DirectionToTarget = TargetDirection.Left;
            _PlayerSpriteRenderer.flipX = true;
        }
        else
        {
            DirectionToTarget = TargetDirection.Right;
            _PlayerSpriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.P)) TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.O)) HealUnit(10);
    }

    public GameObject GetProjectilePoint() => _projectileSpawnPoint;
    public GameObject GetMeleHitbox() => _MeleHitbox;
    public Animator GetPlayerAnimator() => _playerAnimator;

    public GameObject GetCurrentTarget()
    {
        if (!_targetEnemy)
           _targetEnemy = UnitManager.Instance.GetTargets()[0];

        return !_targetEnemy ? null : _targetEnemy;
    }
    public Transform GetShootTargetPosition()
    {
        if (GetCurrentTarget())
            _shootTargetPosition.position = GetCurrentTarget().transform.position;
        else
            _shootTargetPosition.position = new Vector2(transform.position.x + 1, transform.position.y);


        return _shootTargetPosition;
    }

    public void ChangeTarget(GameObject newTarget)
    {
        _targetEnemy = newTarget;
    }
}
