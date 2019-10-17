using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public string hitter;
    public Vector3 initialPos;
    public int playerScore;
    public int botScore;
    public CameraRotator cameraRotator;
   [SerializeField] Text playerscoreT;
   [SerializeField] Text botscoreT;
   [SerializeField] Text gameOverT;

    int scoreToWin = 10;

    void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        gameOverT.enabled = false;
        updateScores();
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            //GameObject.Find("Player").GetComponent<Player>().Reset();

            if (hitter == "player")
            {
                botScore++;
            }
            else if (hitter == "bot")
            {
                playerScore++;

            }

            updateScores(); 
            resetBall();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out"))
        {
            if(hitter == "player")
            {
                botScore++;
            }
            else if(hitter == "bot")
            {
                playerScore++;

            }

            updateScores();
            resetBall();
        }
        else if (other.CompareTag("InPlayer"))
        {
            if(hitter == "player")
            {
                botScore++;
                updateScores();
                resetBall();
            }
            
            hitter = "player";
        }
        else if (other.CompareTag("InBot"))
        {
            if(hitter == "bot")
            {
                playerScore++;
                updateScores();
                resetBall();
            }

            hitter = "bot";
        }
    }

    void updateScores()
    {
        playerscoreT.text = "Player: " + playerScore;
        botscoreT.text = "Bot: " + botScore;

        if(botScore >= scoreToWin || playerScore >= scoreToWin) //finish game
        {
            cameraRotator.startRotate();
            gameOverT.enabled = true;
            gameObject.SetActive(false);
        }
    }

    private void resetBall()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = initialPos;
        hitter = "";
    }
}
