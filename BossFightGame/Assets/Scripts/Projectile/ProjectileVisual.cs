using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{
    [SerializeField] private Transform _projectileVisual;
    [SerializeField] private Transform _projectileShadow;
    [SerializeField] private Projectile _projectile;

    private Transform _target;
    public Vector3 TrajectoryStartPosition;

    private float _shadowPositionDivider = 6f;

    // Update is called once per frame
    void Update()
    {
        if(!_target) return;

        UpdateProjectileRotation();
        UpdateShadowPosition();

        float trajectoryProgressMagnitude = (transform.position - TrajectoryStartPosition).magnitude;
        float trajectoryMagnitude = (_target.position - TrajectoryStartPosition).magnitude;

        float trajectoryProgressNormalized = trajectoryProgressMagnitude / trajectoryMagnitude;

        if(trajectoryProgressNormalized < 0.7f)
        {
            UpdateProjectileShadowRotation();
        }
    }

    private void UpdateShadowPosition()
    {
        Vector3 trajectoryRange = _target.position - TrajectoryStartPosition;

        Vector3 newPosition = transform.position;

        if(Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            newPosition.x = TrajectoryStartPosition.x + _projectile.GetNextXTrajectoryPosition() / _shadowPositionDivider + _projectile.GetNextPositionXCorrectionAbsolute();
        }
        else
        {
            newPosition.y = TrajectoryStartPosition.y + _projectile.GetNextYTrajectoryPosition()/ _shadowPositionDivider + _projectile.GetNextPositionYCorrectionAbsolute();
        }


        _projectileShadow.position = newPosition;
    }

    private void UpdateProjectileRotation()
    {
        Vector3 projectileMoveDir = _projectile.GetMoveDir();

        _projectileVisual.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x)*Mathf.Rad2Deg);
    }

    private void UpdateProjectileShadowRotation()
    {
        Vector3 projectileMoveDir = _projectile.GetMoveDir();

        _projectileShadow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg);
    }

    public void SetTarget(Transform target)
    {
        this._target = target;
    }
}
