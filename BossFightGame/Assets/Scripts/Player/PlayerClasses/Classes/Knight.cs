using System.Collections;
using UnityEngine;

public class Knight : PlayerClass
{
    public Player Player;

    [Header("Auto aim projectile")]
    [Space]
    [SerializeField]
    protected float _projectileMaxMoveSpeed = 1f;
    protected float _projectileMaxHeight = 1f;
    [SerializeField]
    private AnimationCurve _projectileCurve, _axisCorrectionCurve, _speedCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartClass();
        Debug.Log("Knight class");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override int GetMeleDamage()
    {
        return _basePrimaryDamage;
    }

    public override int GetGunDamage()
    {
        return _baseSecondaryDamage;
    }

    public override int GetSpecialDamage()
    {
        return _baseSpecialDamage;
    }

    public override void OnPrimaryAbility()
    {
        Debug.Log("Primary ability");

        Player.GetPlayerAnimator().SetTrigger("Attack");
    }

    public override void OnGunAbility()
    {
        Debug.Log("Gun ability");
        //Implementing a basic always hitting projectile system

        var projectile = BasicBulletPool.Instance.GetProjectile();

        projectile.gameObject.SetActive(true);

        projectile.InitializeProjectile(Player.GetProjectilePoint().transform, Player.GetCurrentTarget().transform, _projectileMaxMoveSpeed, _projectileMaxHeight);
        projectile.InitializeAnimationCurve(_projectileCurve, _axisCorrectionCurve, _speedCurve);
    }

    public override void OnSpecialAbility()
    {
        Debug.Log("Special ability");
    }

    public override void OnMobilityAbility()
    {
        Debug.Log("Mobility ability");

        if(Player.PlayerMovement.CanSlide)
            StartCoroutine(Player.PlayerMovement.PerformSlide());

        Player.GetPlayerAnimator().SetTrigger("Slide");
    }

    
}
