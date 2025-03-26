using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : GameSceneManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void monsterTapped(GameObject monster) // This function is a safeguard to ensure that the monsters can only be tapped when needed as i dont want to capture a monster that is already in the capture scene or inventory
    {
        SceneManager.LoadScene(GameConstants.SCENE_CAPTURE, LoadSceneMode.Additive);
    }

    public override void playerTapped(GameObject player)
    {

    }
}
