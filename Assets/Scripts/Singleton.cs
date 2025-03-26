using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            DontDestroyOnLoad(instance); // Made changes to single so that it will now ignore everything trying to take over itself and only look for this specific instance 
            return instance;
        }
    }
}
  

