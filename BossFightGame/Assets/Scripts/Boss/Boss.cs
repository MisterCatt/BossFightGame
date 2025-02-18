using UnityEngine;

public class Boss : Unit, ITargetable
{
    private void Start()
    {
        if(UnitManager.Instance) 
            UnitManager.Instance.BossesInLevel.Add(gameObject);
    }
    public GameObject ReturnTarget()
    {
        return gameObject;
    }
}
