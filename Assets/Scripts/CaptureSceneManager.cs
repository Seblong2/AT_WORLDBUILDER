using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSceneManager : GameSceneManager
{
    [SerializeField] private int maxAttempts = 3;
    [SerializeField] private GameObject captureNet;
    [SerializeField] private Vector3 spawnPoint;

    private int currentAttemps;

    public int MaxAttempts
    {
        get { return maxAttempts; }
    }
    public int CurrentAttemps
    {
        get { return currentAttemps; } 
    }

    private void Start()
    {
        calculateMaxThrow();
        currentAttemps = maxAttempts;
    }

    private void calculateMaxThrow()
    {
        //maxAttempts += GameManager.Instance.CurrentPlayer.Level / 5; //depending on player level they will get more ball throws for capturing, every 5 levels is +1 throw
    }

    public void CaptureNetDestroyed()
    {
        currentAttemps--;
        if(currentAttemps <= 0)
        {

        }
        else
        {
            Instantiate (captureNet, spawnPoint, Quaternion.identity);
        }
    }

    public override void monsterTapped(GameObject monster)
    {
        print("CapturingSceneManager.MonsterTapped");
    }

    public override void playerTapped(GameObject player)
    {
        print("CapturingSceneManager.PlayerTapped");
    }
}
