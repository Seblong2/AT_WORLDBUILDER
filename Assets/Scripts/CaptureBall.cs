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
            FollowInput();
        }

        UpdateInputStatus();

        //Switch case for different inputs and actions 
        switch (inputstatus)
        {
       
            case InputStatus.Grabbing:
                Grab();
                break;
            case InputStatus.Holding:
                Drag();
                break;
            case InputStatus.Releasing:
                Release();
                break;
            case InputStatus.None:
                return;
            default:
                return;

        }
    }
    private void UpdateInputStatus()
    {
        //Platform Dependent Compilation, Gives unity commands depending on the platform i am using (mobile, pc, etc)

        //PC PDC
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            inputstatus = InputStatus.Grabbing;
        }
        else if (Input.GetMouseButton(0))
        {
            inputstatus = InputStatus.Holding;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            inputstatus = InputStatus.Releasing;
        }
        else
        {
            inputstatus = InputStatus.None;
        }
#endif
        //MOBILE PDC
#if NOT_UNITY_EDITOR

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            inputstatus |= InputStatus.Grabbing;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            inputstatus = InputStatus.Releasing;
        }
        else if (Input.touchCount == 1)
        {
            inputstatus = InputStatus.Holding;
        }
        else
        {
            inputstatus = InputStatus.None;
        }
#endif
    }

    private void FollowInput()
    {
        Vector3 inputPos = GetInputPosition();
        inputPos.z = Camera.main.nearClipPlane * 7.5f;
        Vector3 pos = Camera.main.ScreenToWorldPoint(inputPos);

        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 50.0f * Time.deltaTime);
    }

    private void Grab()
    {
        Ray ray = Camera.main.ScreenPointToRay(GetInputPosition());
        RaycastHit point;

        if (Physics.Raycast(ray, out point, 100.0f) && point.transform == transform)
        {
            holding = true;
            transform.parent = null;
        }
    }

    private void Drag()
    {
        lastX = GetInputPosition().x;
        lastY = GetInputPosition().y;
    }

    private void Release()
    {
        if(lastY < GetInputPosition().y)
        {
            Throw(GetInputPosition());
        }
    }

    private Vector2 GetInputPosition()
    {
        Vector2 result = new Vector2();
#if UNITY_EDITOR
        result = Input.mousePosition;
#endif
#if NOT_UNITY_EDITOR
        result = input.GetTouch(0).position;
#endif
        return result;

    }

    private void Throw(Vector2 targetPos)
    {
        rigidbody.useGravity = true;
        trackingCollisions = true;

        float yDiff = (targetPos.y - lastY) / Screen.height * 100; //where player is going and their Velocity of throw over the y axis
        float speed = throwSpeed * yDiff;

        float x = (targetPos.x / Screen.width) - (lastX / Screen.width);
        x = Mathf.Abs(GetInputPosition().x - lastX) / Screen.width * 100 * x; //Where player is on X axis and where they are trying to throw(Left or right plus angles)

        Vector3 direction = new Vector3(x, 0.0f, 1.0f);//Where the directions are in terms of the game world
        direction = Camera.main.transform.TransformDirection(direction);
        
        rigidbody.AddForce((direction * speed / 2.0f + Vector3.up * speed));//Throwing Force

        released = true;
        holding = false;

        Invoke("Capture", stallTime);
    }

    private void Capture()
    {
        CaptureSceneManager manager = FindObjectOfType<CaptureSceneManager>();
        if(manager != null)
        {
            manager.CaptureNetDestroyed();
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!trackingCollisions)
        {
            return;
        }
        trackingCollisions = false;
        if(other.gameObject.CompareTag(GameConstants.TAG_MONSTER))
        {
            print("Monsters");
        }
        else
        {
            print("Not Monster");
        }

        Invoke("Capture", collisionStallTime);
    }
}
