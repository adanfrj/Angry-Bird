using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : Bird
{
    [SerializeField]
    public float fieldImpact;
    public float power;
    public bool hasExplode = false;
    private bool isHit = false;

    public LayerMask layerToHit;
    public GameObject efekMeledak;

    public void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldImpact, layerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * power);
            
            if (State == BirdState.Thrown && !hasExplode)
            {
            obj.GetComponent<Rigidbody2D>().AddForce(direction * power);
            hasExplode = true;
            }
        }

        GameObject efekMeledakIns =  Instantiate(efekMeledak, transform.position,Quaternion.identity);
        Destroy(efekMeledakIns, 10);
        Destroy(gameObject);
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Mengambil Komponen Rigidbody2d pada enemy
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) 
        {
            return;
        }

         //Apabila brown bird mengenai object obstacle maka akan meledak
        if (collision.gameObject.tag == "Obstacle")
        {
            isHit = true;
            Destroy(gameObject);
            Explode();
        }
    }

    public override void OnTap()
    {
        Explode();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldImpact);
    }

}
