using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;
using System;
using Config.TextConfig;

public class Main : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var time = Time.realtimeSinceStartup;
        for (int i = 0; i < 1000; i++)
        {
            ConfigSOManager.Instance.Init();
        }
        Debug.LogError(Time.realtimeSinceStartup - time);
        time = Time.realtimeSinceStartup;
        for (int i = 0; i < 1000; i++)
        {
            ConfigTextManager.Instance.Init();
        }
        Debug.LogError(Time.realtimeSinceStartup - time);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
