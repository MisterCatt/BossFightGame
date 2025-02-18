using UnityEngine;

public class Boss : Unit, ITargetable
{
    public GameObject ReturnTarget()
    {
        return gameObject;
    }
}
