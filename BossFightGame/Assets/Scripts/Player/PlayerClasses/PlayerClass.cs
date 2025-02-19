using UnityEngine;

public abstract class PlayerClass : MonoBehaviour
{
    [SerializeField] private SOPlayerClass _classData;

    protected int _basePrimaryDamage, _baseSecondaryDamage, _baseSpecialDamage;

    

    protected void StartClass()
    {
        _basePrimaryDamage = _classData.BasePrimaryDamage;
        _baseSecondaryDamage = _classData.BaseSecondaryDamage;
        _baseSpecialDamage = _classData.BaseSpecialDamage;

        Debug.Log("Default class");
    }

    public abstract int GetMeleDamage();
    public abstract int GetGunDamage();
    public abstract int GetSpecialDamage();

    public abstract void OnPrimaryAbility();
    public abstract void OnGunAbility();
    public abstract void OnSpecialAbility();
    public abstract void OnMobilityAbility();
}
