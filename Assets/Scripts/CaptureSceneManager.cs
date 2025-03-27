using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSceneManager : GameSceneManager
{

    public override void monsterTapped(GameObject monster)
    {
        print("CapturingSceneManager.MonsterTapped");
    }

    public override void playerTapped(GameObject player)
    {
        print("CapturingSceneManager.PlayerTapped");
    }
}
