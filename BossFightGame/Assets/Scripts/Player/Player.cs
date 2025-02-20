using System;
using UnityEngine;

public class Player : Unit, IHealable
{
    [Header("Player class")]
    [Space]
    [Header("Manual assignment")]
    [Space]
    public PlayerMovement PlayerMovement;

    [SerializeField] private Transform _shootTargetPosition;
    [SerializeField] private GameObject _projectileSpawnPoint;
    [SerializeField] private Animator _playerAnimator;

    [Header("Automatic assignment, dont touch")]
    [Space]
    public PlayerClass PlayerClass;
    [SerializeField] private GameObject _targetEnemy;

    public event Action<int> OnPlayerTakeDamage, OnPlayerHeal;

    private void Start()
    {
        if(PlayerManager.Instance)
            PlayerManager.Instance.SetPlayer(this);

        _shootTargetPosition.parent = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.O)) HealUnit(10);
    }

    public GameObject GetProjectilePoint() => _projectileSpawnPoint;
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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnPlayerTakeDamage?.Invoke(damage);
    }

    public override void HealUnit(int healAmmount)
    {
        base.HealUnit(healAmmount);
        OnPlayerHeal?.Invoke(healAmmount);
    }
}
