using UnityEngine;

public class BasicBulletPool : ObjectPool
{
    public static BasicBulletPool Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public Projectile GetProjectile()
    {
        return GetObject().GetComponent<Projectile>();
    }

}
