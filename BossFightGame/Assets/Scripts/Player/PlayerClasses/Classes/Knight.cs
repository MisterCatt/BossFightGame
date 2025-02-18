using System.Collections;
using UnityEngine;

public class Knight : PlayerClass
{
    public Player Player;

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
    }

    public override void OnGunAbility()
    {
        Debug.Log("Gun ability");
        //Implementing a basic always hitting projectile system
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
    }

    
}
