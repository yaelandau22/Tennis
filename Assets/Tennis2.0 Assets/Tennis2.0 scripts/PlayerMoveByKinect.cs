using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;


public class PlayerMoveByKinect : MonoBehaviour
{

    public GameObject BodySrcManager; 
    public JointType HeadTrackedJoint;
    public JointType RightHandTrackedJoint;
    public Transform ball;
    public float magnitudeMultiplayer = 400;


    BodySourceManager bodyManager;
    Body[] bodies;
    Animator animator;
    Vector3 headPrevPos = new Vector3();
    Vector3 rightPrevPos = new Vector3();
    Vector3 currFilteredHandDelta;
    MovingAverageFilter filterDeltaX = new MovingAverageFilter();
    MovingAverageFilter filterDeltaY = new MovingAverageFilter();
    MovingAverageFilter filterDeltaZ = new MovingAverageFilter();


    public float speed = 150f;
    public float force = 13f;
    
    bool isFirstKinect = true;
    bool isGameStarted = false;

    bool hitting; 

    void Start()
    {
        if (BodySrcManager == null)
        {
            Debug.Log("Asign Game Object with Body Source Manager");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
            animator = GetComponent<Animator>();
        }

    }

    void FixedUpdate () 
    {      
        if(isGameStarted == false) return;

        if(BodySrcManager == null)
        {
            return;
        }

        bodies = bodyManager.GetData();

        if(bodies == null)
        {
            return;
        }

        foreach (var body in bodies)
        {
            if(body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                var headPos = body.Joints[HeadTrackedJoint].Position;
                Vector3 headPosAsVector = new Vector3(headPos.X,headPos.Y,headPos.Z);

                var rightPos = body.Joints[RightHandTrackedJoint].Position;
                Vector3 rightPosAsVector = new Vector3(rightPos.X,rightPos.Y,rightPos.Z);


                if(isFirstKinect)
                {
                    isFirstKinect = false;
                    headPrevPos = headPosAsVector;
                    rightPrevPos = rightPosAsVector;
                }

                //movePlayer
                var deltaHeadPos = headPosAsVector - headPrevPos;
                transform.Translate(new Vector3(deltaHeadPos.x, 0, -deltaHeadPos.z) * speed * Time.deltaTime);


                Vector3 currHandDelta = rightPosAsVector - rightPrevPos;
                float x = filterDeltaX.filter(currHandDelta.x);
                float y = filterDeltaY.filter(currHandDelta.y);
                float z = filterDeltaZ.filter(currHandDelta.z);
                currFilteredHandDelta = new Vector3(x,y,z);


                headPrevPos = headPosAsVector;
                rightPrevPos = rightPosAsVector;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))  
        {
            Vector3 dir = new Vector3(currFilteredHandDelta.x,currFilteredHandDelta.y, - currFilteredHandDelta.z);
            
            if(dir.z < 0)
                dir.z = 0;

            if(dir.x < -0.1f)
                dir.x = -0.1f;
            if(dir.x > 0.1f)
                dir.x = 0.1f;

            dir.y = 0; 

            Debug.Log(dir.magnitude + "");
            Vector3 dirNormal = dir.normalized;

            other.GetComponent<Rigidbody>().velocity = new Vector3(
                dirNormal.x * 4, dirNormal.y * dir.magnitude * magnitudeMultiplayer, dirNormal.z * dir.magnitude *magnitudeMultiplayer)  + new Vector3(0,6,0);
            
            
            if(dir.x <= 0)
                animator.Play("forehand");
            else
                animator.Play("backhand");

            ball.GetComponent<Ball>().hitter = "player";

            playHitSound();
        }
    }

    public void playHitSound()
    {
        FindObjectOfType<AudioManager>().Play("Hit");
    }
    public void enablePlayer()
    {
        isGameStarted = true;
    }
    
}
