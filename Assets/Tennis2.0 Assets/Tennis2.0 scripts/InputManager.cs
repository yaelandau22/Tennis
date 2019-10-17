using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Player player;

    public void isUseKinect(bool ans)
    {
        //Debug.Log(ans);
        GameObject obj = GameObject.FindWithTag("Player"); 

        Player playerScript = GetComponent<Player>();
        Destroy(playerScript);


        //obj.GetComponent<Player>().enabled = true;
        //obj.GetComponent<PlayerMoveByKinect>().enabled = false;

    }
}
