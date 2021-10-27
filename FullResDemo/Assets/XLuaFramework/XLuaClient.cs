using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class XLuaClient : MonoBehaviour
{
    private static XLuaClient instance;
    public static XLuaClient Instance { get { return instance; } }
    private static LuaEnv luaEnv;
    private const string FileRoot = "/XLuaFramework/Lua/";
    private void Awake()
    {
        instance = this;
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(Loader);
        GameObject.DontDestroyOnLoad(this);
    }
    private byte[] Loader(ref string filePath)
    {
        var realFilePath = Application.dataPath + FileRoot + filePath + ".lua";
        var string_ = System.IO.File.ReadAllText(realFilePath);
        return System.Text.Encoding.UTF8.GetBytes(string_);
    }
    public void StartMain()
    {
        luaEnv.DoString("require 'GameStartUp'");
    }
    public void DoString(string sring_)
    {
        luaEnv.DoString(sring_);
    }
    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

}
