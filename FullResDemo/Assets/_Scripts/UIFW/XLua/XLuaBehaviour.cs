using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;



[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}
public enum LuaLoadMode
{
    TextAsset = 1,
    FilePath = 2,
}
[LuaCallCSharp]
public class XLuaBehaviour : MonoBehaviour
{
    public TextAsset luaScript;
    public string luaFilePath;
    public LuaLoadMode loadMode = LuaLoadMode.FilePath;
    public Injection[] injections;
    
    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    private LuaTable scriptEnv;

    private string GetLuaText()
    {
        if (loadMode == LuaLoadMode.FilePath)
        {
            return XLuaClient.Instance.LoaderString(luaFilePath);
        }
        else 
        {
            return luaScript.text;
        }
    }
    void Awake()
    {
        scriptEnv = luaEnv.NewTable();

        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        foreach (var injection in injections)
        {
            scriptEnv.Set(injection.name, injection.value);
        }
        var stringText = GetLuaText();
        luaEnv.DoString(stringText, "LuaTestScript", scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("awake");
        scriptEnv.Get("start", out luaStart);
        scriptEnv.Get("update", out luaUpdate);
        scriptEnv.Get("ondestroy", out luaOnDestroy);

        if (luaAwake != null)
        {
            luaAwake();
        }
    }

    // Use this for initialization
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
        if (Time.time - XLuaBehaviour.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            XLuaBehaviour.lastGCTime = Time.time;
        }
    }

    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        scriptEnv.Dispose();
        injections = null;
    }
}
