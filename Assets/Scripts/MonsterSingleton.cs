using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MonsterSingleton : Singleton<MonsterSingleton>
{
    [SerializeField] private Monster[] availableMonsters;
    [SerializeField] private Player player;
    [SerializeField] private float waitTime = 30.0f;
    [SerializeField] private int startAmount = 5;
    [SerializeField] private float minRange = 5.0f;
    [SerializeField] private float maxRange = 50.0f;

    private List<Monster> activeMonsters = new List<Monster>();
    private Monster selectedMonster;

    public List<Monster> ActiveMonsters
    { 
        get { return activeMonsters; }
    }

    public Monster SelectedMonster
    {
        get { return selectedMonster; } 
    }

    private void Awake()
    {
        Assert.IsNotNull(availableMonsters); // Debugging to check monsters spawn correction on init
        Assert.IsNotNull(player); // Debugging to check player spawn correction on init
    }


    void Start()
    {
        for (int i = 0; i < startAmount; i++)
        {
            InstantMonster();
        }
        StartCoroutine(GenerateMonsters());
    }

    public void MonsterWasSelected(Monster monster)
    {
        selectedMonster = monster;
    }

    private IEnumerator GenerateMonsters()
    {
        while (true)
        {
            InstantMonster();
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void InstantMonster()
    {
        int index = Random.Range(0, availableMonsters.Length);
        float x = player.transform.position.x + GenerateRange();
        float z = player.transform.position.z + GenerateRange();
        float y = player.transform.position.y;
        activeMonsters.Add(Instantiate(availableMonsters[index], new Vector3(x, y, z), Quaternion.identity));
    }

    private float GenerateRange()
    {
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 10) < 5;
        return randomNum * (isPositive ? 1 : -1); // If range is positive x1 if not x-1 this provide randomness and varition of instantiation of monsters
    }
    
    
}
