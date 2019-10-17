using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
 {
    public float xMin, xMax, yMin, yMax;
    Vector3 m_Movement;
    Rigidbody m_Rigidbody;
    public float speed = 5f;
    public float tilt;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        float forward = Input.GetAxis ("Jump");
        Debug.Log(forward);

        m_Movement.Set(horizontal, vertical, 0f);
        m_Movement.Normalize ();

    //new -- start
        GetComponent<Rigidbody>().MovePosition( new Vector3 
        (
            Mathf.Clamp (m_Rigidbody.position.x, xMin, xMax) + m_Movement.x * speed, 
            Mathf.Clamp (m_Rigidbody.position.y, yMin, yMax) + m_Movement.y * speed,
            0.0f
        )
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    //new -- end

        //m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * turnSpeed);
    }
}