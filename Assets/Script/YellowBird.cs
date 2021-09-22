using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    [SerializeField]
    public float boostForce;
    public bool hasBoost = false;

    public void Boost()
    {
        if (State == BirdState.Thrown && !hasBoost)
        {
            RigidBody.AddForce(RigidBody.velocity * boostForce);
            hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }
    
    
}
