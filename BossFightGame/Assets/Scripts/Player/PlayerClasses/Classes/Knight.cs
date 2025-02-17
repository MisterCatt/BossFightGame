using System.Collections;
using UnityEngine;

public class Knight : PlayerClass
{
    public Player Player;

    public float SlideSpeed;

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

    public override void OnSecondaryAbility()
    {
        Debug.Log("Secondary ability");
    }

    public override void OnSpecialAbility()
    {
        Debug.Log("Special ability");
    }

    public override void OnMobilityAbility()
    {
        
    }

    private IEnumerator PerformSlide()
    {
        Player.RigidBody.AddForce(Player.PlayerMovement.GetMoveDirection() * SlideSpeed);
        yield return new WaitForSeconds(1f);
    }
}
