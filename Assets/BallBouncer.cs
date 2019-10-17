using UnityEngine;

public class BallBouncer : MonoBehaviour
{
    public Vector3 initialVelocity;

    public float minVelocity = 10f;
    public bool isIntroEnded = false;
    private Vector3 lastFrameVelocity;
    private Rigidbody rb;
    
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();        
    }

    private void FixedUpdate()
    {
        if(isIntroEnded == false) return;

        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }

    public void activate()
    {
        rb.useGravity = true;
        rb.velocity = initialVelocity;
        isIntroEnded = true;
    }
}