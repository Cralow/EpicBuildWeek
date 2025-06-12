using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cam;
    private void Awake()
    {
         cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void LateUpdate()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);   
    }
}
