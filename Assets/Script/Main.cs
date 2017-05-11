using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;
using System;

public class Main : MonoBehaviour
{
	// Use this for initialization
	void Start () {
        Debug.LogError(typeof(Config.TextConfig.CfgTest).ReflectedType.FullName);
        //Debug.LogError(Type.GetType("Config.ScriptableConfig." + "CfgTest"));
        //ConfigManager.Init();
        //var data = ConfigManager.GetConfig<CfgTest1>(1);		
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
