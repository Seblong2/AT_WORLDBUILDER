using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50;
    [SerializeField] private float floatAmp = 0.5f;
    [SerializeField] private float floatFreq = 0.5f;

    private Vector3 startPos;

    public FloatAndRotate()
    {
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);   
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFreq) * floatAmp;

        transform.position = tempPos;
    }
}
