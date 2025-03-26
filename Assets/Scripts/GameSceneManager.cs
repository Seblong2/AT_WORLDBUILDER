using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSceneManager : MonoBehaviour
{
    public abstract void playerTapped(GameObject player);
    public abstract void monsterTapped(GameObject monster);
}
