using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float spawnRate = 0.10f;
    [SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 10;

    public float SpawnRate
    {
        get { return spawnRate; } 
    }

    public float CatchRate
    {
        get { return catchRate; } 
    }

    public int Attack
    { 
        get { return attack; }
    }

    public int Defense
    { 
        get { return defense; }
    }

    public int Hp
    { 
        get { return hp; }
    }

    private void OnMouseDown()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
