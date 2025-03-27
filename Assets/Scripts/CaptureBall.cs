using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CaptureBall : MonoBehaviour
{
    [SerializeField] private float throwSpeed = 30.0f;
    [SerializeField] private float collisionStallTime = 2.0f; //Time before ball is destroyed based on collision 
    [SerializeField] private float stallTime = 5.0f; // ball life time after throwing

    private float lastX;
    private float lastY;
    private bool released;
    private bool holding;
    private bool trackingCollisions = false; //Safeguarding so monsters dont get captured by a dropped ball after the first initial throw
    private Rigidbody rigidbody;
    private InputStatus inputstatus;

    private enum InputStatus
    {
        Grabbing,
        Holding,
        Releasing,
        None
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Assert.IsNotNull(rigidbody);

    }

    private void Update()
    {
        if (released)
        {
            return;
        }

        if (holding) 
        {
            
        }
    }

}
