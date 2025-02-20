using UnityEngine;

public class Player : Unit
{
    [Header("Player class")]
    [Space]
    public PlayerClass PlayerClass;
    public PlayerMovement PlayerMovement;

    [SerializeField] private GameObject _targetEnemy;
    [SerializeField] private GameObject _projectileSpawnPoint;
    [SerializeField] private Animator _playerAnimator;

    private void Start()
    {
        if(PlayerManager.Instance)
            PlayerManager.Instance.SetPlayer(this);
    }

    public GameObject GetProjectilePoint() => _projectileSpawnPoint;
    public Animator GetPlayerAnimator() => _playerAnimator;

    public GameObject GetCurrentTarget()
    {
        if (!_targetEnemy)
           _targetEnemy = UnitManager.Instance.GetTargets()[0];

        return !_targetEnemy ? null : _targetEnemy;
    }
    public void ChangeTarget(GameObject newTarget)
    {
        _targetEnemy = newTarget;
    }

    
}
