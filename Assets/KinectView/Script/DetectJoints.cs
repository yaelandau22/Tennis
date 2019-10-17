using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DetectJoints : MonoBehaviour
{
    public GameObject BodySrcManager; 
    public JointType TrackedJoint;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    public float speed = 10f;
    Rigidbody m_Rigidbody;
    private Vector3 m_Movement;
    public float tilt;

    public float xMin, xMax, yMin, yMax;


    void Start ()
    {

        if (BodySrcManager == null)
        {
            Debug.Log("Asign Game Object with Body Source Manager");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySourceManager>();
            m_Rigidbody = GetComponent<Rigidbody> ();
        }
        
    }

    void FixedUpdate () 
    {      
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
                var pos = body.Joints[TrackedJoint].Position;
                //gameObject.transform.position = new Vector3(pos.X*speed, pos.Y*speed);

                GetComponent<Rigidbody>().MovePosition( new Vector3 
                (
                   Mathf.Clamp (pos.X * speed, xMin, xMax) , 
                   Mathf.Clamp (pos.Y * speed, yMin, yMax) ,
                   0.0f
                ));

              //GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);

            }
        }
    }
}
