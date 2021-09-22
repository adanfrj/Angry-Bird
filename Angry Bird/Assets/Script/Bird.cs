using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState { Idle, Thrown, HitSomething }
    public GameObject Parent;
    public Rigidbody2D RigidBody;
    public CircleCollider2D Collider;

    //Untuk Event Delegate
    public UnityAction OnBirdDestroyed = delegate {};
    public UnityAction<Bird> OnBirdShot = delegate {};

    private BirdState state;
    public BirdState State { get { return state; } }

    private float minVelocity = 0.05f;
    private bool flagDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        RigidBody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        state = BirdState.Idle;
    }

    //Untuk memberitahu burung yg sedang dilempar bahwa player menekan tap
    //dan memanggil fungsi OnTap dalam burung
    public virtual void OnTap()
    {
        //Do nothing
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == BirdState.Idle && RigidBody.velocity.sqrMagnitude >= minVelocity)
        {
            state = BirdState.Thrown;
        }

        if ((state == BirdState.Thrown || state == BirdState.HitSomething) && RigidBody.velocity.sqrMagnitude < minVelocity && !flagDestroy)
        {
            //Hancurkan gameobject setelah 2 detik jika kecepatannya sudah kurang dari batas minimum
            flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    // Setting lemparan burung
    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        Collider.enabled = true;
        RigidBody.bodyType = RigidbodyType2D.Dynamic;
        RigidBody.velocity = velocity * speed * distance;
        OnBirdShot(this);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        state = BirdState.HitSomething;
    }


    void OnDestroy()
    {
        if(state == BirdState.Thrown || state == BirdState.HitSomething)
        OnBirdDestroyed();
    }
}
