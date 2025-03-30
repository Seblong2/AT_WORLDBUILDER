using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CaptureSceneUIManager : MonoBehaviour
{
    [SerializeField] private CaptureSceneManager manager;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private TextMeshProUGUI ballCountText;
    private void Awake()
    {
        Assert.IsNotNull(manager);
        Assert.IsNotNull(successScreen);
        Assert.IsNotNull(failScreen);
        Assert.IsNotNull(gameScreen);
    }


    void Update()
    {
        switch (manager.Status)
        {
            case CaptureSceneStatus.InProgess:
                HandleInProgess();
                break;
            case CaptureSceneStatus.Successful:
                HandleSuccess();
                break;
            case CaptureSceneStatus.Failed:
                HandleFailure();
                break;
            default:
                break;
        }
    }

    private void HandleInProgess()
    {
        UpdateVisableScreen();
        ballCountText.text = manager.CurrentAttemps.ToString();
    }

    private void HandleSuccess()
    {
        UpdateVisableScreen();
    }

    private void HandleFailure()
    {
        UpdateVisableScreen();
    }

    private void UpdateVisableScreen()
    {
        successScreen.SetActive(manager.Status == CaptureSceneStatus.Successful);
        failScreen.SetActive(manager.Status == CaptureSceneStatus.Failed);
        gameScreen.SetActive(manager.Status == CaptureSceneStatus.InProgess);
    }

}
