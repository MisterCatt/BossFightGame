using UnityEngine;

public class Player : Unit
{
    public Rigidbody2D RigidBody;
    public PlayerClass PlayerClass;
    public PlayerMovement PlayerMovement;

    [SerializeField] private GameObject _targetEnemy;

    private void Start()
    {
        if(PlayerManager.Instance)
            PlayerManager.Instance.SetPlayer(this);
    }

    public GameObject GetCurrentTarget()
    {
        return !_targetEnemy ? null : _targetEnemy;
    }
    public void ChangeTarget(GameObject newTarget)
    {
        _targetEnemy = newTarget;
    }
}
