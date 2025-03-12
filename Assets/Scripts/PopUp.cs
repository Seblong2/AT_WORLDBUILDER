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
        if(Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }

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


        //Ensuring popups spawn at least 50m (0.0005f) away from player
        while (Mathf.Abs(randomoffsetLAT) < 0.0005f && Mathf.Abs(randomoffsetLON) < 0.0005f )
        {
            randomoffsetLAT = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
            randomoffsetLON = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        }
             

        //New Popup location based on GPS calculations
        float popupLAT = playerLAT + randomoffsetLAT;
        float popupLON = playerLON + randomoffsetLON;

        Vector2d popupCoord = new Vector2d(popupLAT, popupLON);

        //Converting GPS Calculations to Mapbox world positions 
        Vector3 worldPos = popUpMap.GeoToWorldPosition(popupCoord, true);

        //Making popup above ground
        worldPos.y += 2f;

        //Instantiate popups
        GameObject popup = Instantiate(popUpPrefab, worldPos, Quaternion.identity);
        popup.tag = "Popup"; //Tagging object for OnClick to see

      


    }


    void Update()
    {
      if(Input.GetMouseButtonDown(0)) // Touch or click down
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.CompareTag("Popup"))
                {
                    CollectPopup(hit.collider.gameObject);
                }
            }
        }
    }
    void CollectPopup(GameObject popup)
    {
        activePopups--;
        Destroy(popup);
        Debug.Log("Collected!");
    }
}
