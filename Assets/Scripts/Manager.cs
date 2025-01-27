﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly System.Lazy<T> LazyInstance = new System.Lazy<T>(CreateSingleton);

    public static T Instance => LazyInstance.Value;

    private static T CreateSingleton()
    {
        var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
        var instance = ownerObject.AddComponent<T>();
        DontDestroyOnLoad(ownerObject);
        return instance;
    }
}


public struct SaveData
{

}


public class Manager : Singleton<Manager>
{
    public int drugLevel = 0;

    public void OnPickup(GameObject item)
    {
        drugLevel += 1;
        Destroy(item);
    }

}
