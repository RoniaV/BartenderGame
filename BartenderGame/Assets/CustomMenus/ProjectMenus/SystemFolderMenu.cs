using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SystemFolderMenu
{
    [MenuItem("Assets/Create/System Folder", false, 20)]
    private static void CreateSystemFolder()
    {
        string guid = 
            AssetDatabase.CreateFolder(AssetDatabase.GetAssetPath(Selection.activeObject), "newSystem");
        string mainFolderPath = AssetDatabase.GUIDToAssetPath(guid);

        AssetDatabase.CreateFolder(mainFolderPath, "Scripts");

        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(mainFolderPath);
    }
}
