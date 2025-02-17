using UnityEngine;

public abstract class PlayerClass : MonoBehaviour
{
    [SerializeField] private SOPlayerClass _classData;

    protected float _basePrimaryDamage, _baseSecondaryDamage, _baseSpecialDamage;

    protected void StartClass()
    {
        _basePrimaryDamage = _classData.BasePrimaryDamage;
        _baseSecondaryDamage = _classData.BaseSecondaryDamage;
        _baseSpecialDamage = _classData.BaseSpecialDamage;

        Debug.Log("Default class");
    }

    public abstract void OnPrimaryAbility();
    public abstract void OnSecondaryAbility();
    public abstract void OnSpecialAbility();
    public abstract void OnMobilityAbility();
}
