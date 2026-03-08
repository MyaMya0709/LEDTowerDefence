using UnityEngine;
using UnityEngine.Purchasing;
using Unity.Services.Core;
using System.Collections.Generic;

public class SptGameInstance : MonoBehaviour
{
    private static SptGameInstance _instance;
    public static SptGameInstance Instance => _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        InitialProcess();
    }

    void InitialProcess()
    {
        Debug.Log("init");
    }
}
