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

    public override void OnPrimaryAbility()
    {
        Debug.Log("Primary ability");
    }

    public override void OnGunAbility()
    {
        Debug.Log("Gun ability");
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
