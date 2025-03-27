using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject menu;
    


    private void Awake()
    {
        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        Assert.IsNotNull(menu);
    }

    private void Update()
    {
        updateLevel();
        updateXp();
    }

    public void updateLevel() // Updated so its more effcient and not updating every frame and is more controlled
    {
         levelText.text = GameManager.Instance.CurrentPlayer.Level.ToString(); // Using gamemanager to refence the player level directly 
    }
    public void updateXp()
    {
        xpText.text = GameManager.Instance.CurrentPlayer.Xp + " / " + GameManager.Instance.CurrentPlayer.RequiredXp; // Using gamemanager to refence the player xp and required Xp
    }

    public void menuBTNClicked()
    {
        toggleMenu();
    }

    public void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }

}
