using UnityEngine;

public class TimedDamageCirclePool : ObjectPool
{
    public static TimedDamageCirclePool Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
