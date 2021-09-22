using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate {};
    private bool isHit = false;

    //OnCollisionEnter2D akan dipanggil ketika ada collider lain di luar game object Enemy 
    //bersentuhan dengan collider dari game object Enemy.
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Mengambil Komponen Rigidbody2d pada enemy
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) 
        {
            return;
        }

        //Apabila bird mengenai enemy maka enemy di destroy
        if (collision.gameObject.tag == "Bird")
        {
            isHit = true;
            Destroy(gameObject);
        }

        //Apabila obstacle mengenai enemy maka enemy terkena damage
        else if(collision.gameObject.tag == "Obstacle")
        {
            //Hitung damage yang diperoleh
            float damage = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            health -= damage;

            //apabila health < 0 maka enemy modar
            if (health <= 0)
            {
                isHit = true;
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        //Apabila terkena hit maka mempengaruhi enemy
        if (isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
