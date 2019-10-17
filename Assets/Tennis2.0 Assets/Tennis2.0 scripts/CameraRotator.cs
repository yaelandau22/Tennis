using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public GameObject ball; 
    public PlayerMoveByKinect player;
    public float speed = 20;
    bool isStartRotate = true;
    Vector3 playPosition = new Vector3(-0.248438f, 2.947734f, -4.431719f);
    Vector3 rotatePosition = new Vector3(-0.248438f,2.947734f,7.06f);
    public GameObject camera;
    Vector3 initialRotation;
    Vector3 cameraInitialPos;
    Vector3 cameraInitialRotation;

    void Start()
    {
        initialRotation = transform.eulerAngles;
        cameraInitialPos = camera.transform.position;
        cameraInitialRotation = camera.transform.eulerAngles;

        camera.transform.position = cameraInitialPos + new Vector3(0, 0, 0);
        camera.transform.eulerAngles = cameraInitialRotation + new Vector3(0,0,0);
        ball.SetActive(false);
        //transform.position = rotatePosition;
    }

    void Update()
    {
        if(isStartRotate)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

    public void startRotate()
    {
        //transform.position = Vector3.Lerp(this.transform.position, rotatePosition, 1f);
        isStartRotate = true;
        
    }

    public void setCameraAtPlayPosition()
    {
        transform.eulerAngles = initialRotation;
        camera.transform.eulerAngles = cameraInitialRotation;
        camera.transform.position = cameraInitialPos;

        isStartRotate = false;
        ball.SetActive(true);
        player.enablePlayer();
    }
}
