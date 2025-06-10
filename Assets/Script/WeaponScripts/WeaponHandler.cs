using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
 public Transform shootPosition;


    private void Start()
    {
        shootPosition = GetComponent<Transform>();
    }



}
