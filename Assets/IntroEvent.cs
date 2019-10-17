using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEvent : MonoBehaviour
{
    public BallBouncer ball;
    
    void Start()
    {
        //ball = GameObject.FindObjectOfType<BallBouncer>();
    }
    

    void Update()
    {
        
    }

    
    void IntroEnded()
    {
        ball.activate();
    }

}
