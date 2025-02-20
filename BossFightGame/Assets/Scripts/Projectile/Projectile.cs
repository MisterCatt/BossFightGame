using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileVisual _projectileVisual;

    private Transform _target;
    private float _moveSpeed, _maxMoveSpeed;

    private const float DistanceToTargetToDestroyProjectile = 0.1f;
    private float _trajectoryMaxRelativeHeight;

    private AnimationCurve _projectileCurve, _axisCorrectionCurve, _speedCurve;

    private Vector3 _trajectoryStartPoint, _projectileMoveDir, _trajectoryRange;

    private float _nextYTrajectoryPosition, _nextPositionYCorrectionAbsolute, _nextXTrajectoryPosition, _nextPositionXCorrectionAbsolute;

    public int projectileDamage = 10;

    public void Update()
    {
        if(!_target) return;

        UpdateProjectilePosition();

        CheckHit();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageTarget = collision.GetComponentInParent<IDamageable>();
        if (damageTarget != null)
        {
            damageTarget.TakeDamage(projectileDamage);
            gameObject.SetActive(false);
        }
    }

    private void CheckHit()
    {
        if (Vector3.Distance(transform.position, _target.position) < DistanceToTargetToDestroyProjectile)
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateProjectilePosition()
    {
        _trajectoryRange = _target.position - _trajectoryStartPoint;

        if(Mathf.Abs(_trajectoryRange.normalized.x) < Mathf.Abs(_trajectoryRange.normalized.y))
        {
            if (_trajectoryRange.y < 0)
                _moveSpeed = -_moveSpeed;
            UpdatePositionWithXCurve();
        }
        else
        {
            if (_trajectoryRange.x < 0)
                _moveSpeed = -_moveSpeed;

            UpdatePositionWithYCurve();
        }
    }

    private void UpdatePositionWithXCurve()
    {
        var nextPositionY = transform.position.y + _moveSpeed * Time.deltaTime;
        var nextPositionYNormalized = (nextPositionY - _trajectoryStartPoint.y) / _trajectoryRange.y;

        var nextPositionXNormalized = _projectileCurve.Evaluate(nextPositionYNormalized);
        _nextXTrajectoryPosition = nextPositionXNormalized * _trajectoryMaxRelativeHeight;


        var nextPositionXCorrectionNormalized = _axisCorrectionCurve.Evaluate(nextPositionYNormalized);
        _nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalized * _trajectoryRange.x;

        if(_trajectoryRange is { x: > 0, y: > 0 })
        {
            _nextXTrajectoryPosition = -_nextXTrajectoryPosition;
        }

        if (_trajectoryRange is { x: < 0, y: < 0 })
        {
            _nextXTrajectoryPosition = -_nextXTrajectoryPosition;
        }

        var nextPositionX = _trajectoryStartPoint.x + _nextXTrajectoryPosition + _nextPositionXCorrectionAbsolute;
        var newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateProjectileSpeed(nextPositionYNormalized);
        _projectileMoveDir = newPosition - transform.position;

        transform.position = newPosition;
    }

    private void UpdatePositionWithYCurve()
    {
        var nextPositionX = transform.position.x + _moveSpeed * Time.deltaTime;
        var nextPositionXNormalized = (nextPositionX - _trajectoryStartPoint.x) / _trajectoryRange.x;

        var nextPositionYNormalized = _projectileCurve.Evaluate(nextPositionXNormalized);
        _nextYTrajectoryPosition = nextPositionYNormalized * _trajectoryMaxRelativeHeight;


        var nextPositionYCorrectionNormalized = _axisCorrectionCurve.Evaluate(nextPositionXNormalized);
        _nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * _trajectoryRange.y;

        var nextPositionY = _trajectoryStartPoint.y + _nextYTrajectoryPosition + _nextPositionYCorrectionAbsolute;
        var newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateProjectileSpeed(nextPositionXNormalized);
        _projectileMoveDir = newPosition - transform.position;

        transform.position = newPosition;
    }

    private void CalculateProjectileSpeed(float nextPositionXNormalized)
    {
        var nextMoveSpeedNormalized = _speedCurve.Evaluate(nextPositionXNormalized);

        _moveSpeed = nextMoveSpeedNormalized * _maxMoveSpeed;
    }

    public void InitializeProjectile(Transform spawnPoint, Transform target, float maxMoveSpeed, float trajectoryMaxHeight, int theProjectilesDamage = 10)
    {
        this._trajectoryStartPoint = spawnPoint.position;
        transform.position = spawnPoint.position;
        _projectileVisual.TrajectoryStartPosition = spawnPoint.position;
        this._target = target;
        this._maxMoveSpeed = maxMoveSpeed;
        this.projectileDamage = theProjectilesDamage;

        var xDistanceToTarget = target.position.x - transform.position.x;
        this._trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxHeight;

        _projectileVisual.SetTarget(target);
    }

    public void InitializeAnimationCurve(AnimationCurve curve, AnimationCurve axisCurve, AnimationCurve speedCurve)
    {
        this._projectileCurve = curve;
        this._axisCorrectionCurve = axisCurve;
        this._speedCurve = speedCurve;
    }

    public Vector3 GetMoveDir()
    {
        return _projectileMoveDir;
    }

    public float GetNextYTrajectoryPosition()
    {
        return _nextYTrajectoryPosition;
    }

    public float GetNextPositionYCorrectionAbsolute()
    {
        return _nextPositionYCorrectionAbsolute;
    }

    public float GetNextXTrajectoryPosition()
    {
        return _nextXTrajectoryPosition;
    }

    public float GetNextPositionXCorrectionAbsolute()
    {
        return _nextPositionXCorrectionAbsolute;
    }
}
