using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed = 40;
    Animator animator;
    public Transform ball;
    Vector3 targetposition;
    ShootManager shotmanager;
   
    void Start()
    {
        targetposition = transform.position;
        animator = GetComponent<Animator>();
        shotmanager = GetComponent<ShootManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
    }
    void Move()
    {
        targetposition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetposition, speed * Time.deltaTime);
    }

    Vector3 PickTarget()
    {
        float xPosition = 0;//Random.Range(-0.5f, 0.5f);
        Vector3 targetPosition = new Vector3(xPosition,0,0);
        return targetPosition;
    }




    Shot pickShot()
    {
        int randomvalue = Random.Range(0, 2);

        if (randomvalue == 0)
        {
            return shotmanager.topSpin;
        }
        else
        {
            return shotmanager.flat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Shot currentshot = pickShot();

        if (other.CompareTag("Ball"))
        {
            Vector3 dir = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentshot.hitforce + new Vector3(0, currentshot.upforce, 0);

            Vector3 ballDir = ball.position - transform.position;

            if (ballDir.x >= 0)
                animator.Play("forehand");
            else
                animator.Play("backhand");

            ball.GetComponent<Ball>().hitter = "bot";
            playHitSound();
        }
    }

    public void playHitSound()
    {
        FindObjectOfType<AudioManager>().Play("Hit");
    }

}
