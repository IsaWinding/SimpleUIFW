using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class AddressMenu 
{
    [MenuItem("AddressableEditor/SetAllDirectorToAddress", priority = 2)]
    public static void SetAllDirectorToAddress()
    {
        var arr = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);
        string folder = AssetDatabase.GetAssetPath(arr[0]);
        if(folder != null)
            LoopSetAllDirectorToAddress(folder);
    }

    private static void LoopSetAllDirectorToAddress(string pFileDirectorRoot)
    {
        if (Directory.Exists(pFileDirectorRoot) && !pFileDirectorRoot.Contains("_Bundles/Lua"))
        {
            SetDirectorABNameNull(pFileDirectorRoot);
            var dirctory = new DirectoryInfo(pFileDirectorRoot);
            var direcs = dirctory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            if (direcs.Length > 0)
            {
                for (var i = 0; i < direcs.Length; i++)
                {
                    if (direcs[i].FullName != pFileDirectorRoot)
                    {
                        LoopSetAllDirectorToAddress(direcs[i].FullName);
                    }
                }
            }
        }
    }
    private static void SetDirectorABNameNull(string pFileDirectorRoot)
    {
        if (Directory.Exists(pFileDirectorRoot) && !pFileDirectorRoot.Contains("_Bundles/Lua"))
        {
            var dirctory = new DirectoryInfo(pFileDirectorRoot);
            var files = dirctory.GetFiles("*", SearchOption.TopDirectoryOnly);
            bool isAdd = false;
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (file.Name.EndsWith(".meta"))
                    continue;
                if (file.Name.EndsWith(".txt"))
                    continue;
                string assetPath = file.FullName;
                assetPath = FormatFilePath(assetPath);
                var assetLength = UnityEngine.Application.dataPath.Length - 6;
                assetPath = assetPath.Substring(assetLength, assetPath.Length - assetLength);
                AutoGroup(dirctory.Name, assetPath);

                isAdd = true;
            }
            if (isAdd)
                AssetDatabase.Refresh();
        }

    }
    public static string FormatFilePath(string filePath)
    {
        var path = filePath.Replace('\\', '/');
        path = path.Replace("//", "/");
        return path;
    }
    public static void AutoGroup(string groupName, string assetPath)
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        AddressableAssetGroup group = settings.FindGroup(groupName);
        if (group == null)
        {
            group = CreatAssetGroup<System.Data.SchemaType>(settings, groupName);

        }

        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        var entry = settings.CreateOrMoveEntry(guid, group);
        entry.address = assetPath;
        entry.SetLabel(groupName, true, true);

    }
    private static AddressableAssetGroup CreatAssetGroup<SchemaType>(AddressableAssetSettings settings, string groupName)
    {
        return settings.CreateGroup(groupName, false, false, false,
            new List<AddressableAssetGroupSchema> { settings.DefaultGroup.Schemas[0], settings.DefaultGroup.Schemas[1] }, typeof(SchemaType));

    }
}
