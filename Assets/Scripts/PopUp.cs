using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;

public class PopUp : MonoBehaviour
{
    public AbstractMap popUpMap; // Assigning the map from mapbox inside inspector
    public GameObject popUpPrefab; // Assigning popup object prefabs
    public Transform mapCanvasUI; // Parenting the UI that popups use
    public float spawnRadius = 0.001f; // Adjusting spawn radius from player (0.001f = 100m in radius)
    public int maxPopups = 5;

    private int activePopups = 0;

    void Start()
    {
        StartCoroutine(SpawnPopups());
    }

    IEnumerator SpawnPopups()
    {
        while (true) 
        {
            if (activePopups < maxPopups) 
            {
                SpawnFunction();
                activePopups++;
            }
            yield return new WaitForSeconds(10f); // Spawning frequency adjusters 
        }
    }

     void SpawnFunction()
    {
        if (!Input.location.isEnabledByUser || Input.location.status != LocationServiceStatus.Running) // Check GPS system is running before attempting to spawn
            return;

        //Getting current player location via GPS 
        float playerLAT = Input.location.lastData.latitude;
        float playerLON = Input.location.lastData.longitude;

        //Generating GPS coords nearby (Random)
        float randomoffsetLAT = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        float randomoffsetLON = UnityEngine.Random.Range(-spawnRadius, spawnRadius);

        //New Popup location based on GPS calculations
        float popupLAT = playerLAT + randomoffsetLAT;
        float popupLON = playerLON + randomoffsetLON;

        Vector2d popupCoord = new Vector2d(popupLAT, popupLON);

        //Converting GPS Calculations to Mapbox world positions 
        Vector3 worldPos = popUpMap.GeoToWorldPosition(popupCoord, true);

        //Instantiate popups
        GameObject popup = Instantiate(popUpPrefab, worldPos, Quaternion.identity, mapCanvasUI);

        //Collection Functionality 
        popup.GetComponent<Button>().onClick.AddListener(() => CollectPopup(popup));


    }

     void CollectPopup(GameObject popup)
    {
        activePopups--;
        Destroy(popup);
        Debug.Log("Collected!");
    }
}
