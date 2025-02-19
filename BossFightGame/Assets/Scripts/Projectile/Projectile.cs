using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileVisual _projectileVisual;

    private Transform _target;
    private float _moveSpeed, _maxMoveSpeed;

    private float _distanceToTargetToDestroyProjectile = 1f, _trajectoryMaxRelativeHeight;

    private AnimationCurve _projectileCurve, _axisCorrectionCurve, _speedCurve;

    private Vector3 _trajectoryStartPoint, _projectileMoveDir, _trajectoryRange;

    private float nextYTrajectoryPosition, nextPositionYCorrectionAbsolute, nextXTrajectoryPosition, nextPositionXCorrectionAbsolute;

    public void Update()
    {
        if(!_target) return;

        UpdateProjectilePosition();

        if (Vector3.Distance(transform.position, _target.position) < _distanceToTargetToDestroyProjectile)
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
        float nextPositionY = transform.position.y + _moveSpeed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - _trajectoryStartPoint.y) / _trajectoryRange.y;

        float nextPositionXNormalized = _projectileCurve.Evaluate(nextPositionYNormalized);
        nextXTrajectoryPosition = nextPositionXNormalized * _trajectoryMaxRelativeHeight;


        float nextPositionXCorrectionNormalized = _axisCorrectionCurve.Evaluate(nextPositionYNormalized);
        nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalized * _trajectoryRange.x;

        if(_trajectoryRange.x > 0 && _trajectoryRange.y > 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        if (_trajectoryRange.x < 0 && _trajectoryRange.y < 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        float nextPositionX = _trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbsolute;
        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateProjectileSpeed(nextPositionYNormalized);
        _projectileMoveDir = newPosition - transform.position;

        transform.position = newPosition;
    }

    private void UpdatePositionWithYCurve()
    {
        float nextPositionX = transform.position.x + _moveSpeed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - _trajectoryStartPoint.x) / _trajectoryRange.x;

        float nextPositionYNormalized = _projectileCurve.Evaluate(nextPositionXNormalized);
        nextYTrajectoryPosition = nextPositionYNormalized * _trajectoryMaxRelativeHeight;


        float nextPositionYCorrectionNormalized = _axisCorrectionCurve.Evaluate(nextPositionXNormalized);
        nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * _trajectoryRange.y;

        float nextPositionY = _trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbsolute;
        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateProjectileSpeed(nextPositionXNormalized);
        _projectileMoveDir = newPosition - transform.position;

        transform.position = newPosition;
    }

    private void CalculateProjectileSpeed(float nextPositionXNormalized)
    {
        float nextMoveSpeedNormalized = _speedCurve.Evaluate(nextPositionXNormalized);

        _moveSpeed = nextMoveSpeedNormalized * _maxMoveSpeed;
    }

    public void InitializeProjectile(Transform spawnPoint, Transform target, float maxMoveSpeed, float trajectoryMaxHeight)
    {
        this._trajectoryStartPoint = spawnPoint.position;
        transform.position = spawnPoint.position;
        _projectileVisual.TrajectoryStartPosition = spawnPoint.position;
        this._target = target;
        this._maxMoveSpeed = maxMoveSpeed;

        float xDistanceToTarget = target.position.x - transform.position.x;
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
        return nextYTrajectoryPosition;
    }

    public float GetNextPositionYCorrectionAbsolute()
    {
        return nextPositionYCorrectionAbsolute;
    }

    public float GetNextXTrajectoryPosition()
    {
        return nextXTrajectoryPosition;
    }

    public float GetNextPositionXCorrectionAbsolute()
    {
        return nextPositionXCorrectionAbsolute;
    }
}
