using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Shot
{
    public float upforce;
    public float hitforce;

}
public class ShootManager : MonoBehaviour
{
    public Shot topSpin;
    public Shot flat;
    public Shot flatServe;
    public Shot kickServe;
}
