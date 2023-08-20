using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VisibleObjectFolderMenu
{
    [MenuItem("Assets/Create/3D Object Folder", false, 19)]
    private static void CreateVisibleObjectFolder()
    {
        string guid =
            AssetDatabase.CreateFolder(AssetDatabase.GetAssetPath(Selection.activeObject), "new3DObject");
        string mainFolderPath = AssetDatabase.GUIDToAssetPath(guid);

        AssetDatabase.CreateFolder(mainFolderPath, "Scripts");
        AssetDatabase.CreateFolder(mainFolderPath, "Prefabs");
        AssetDatabase.CreateFolder(mainFolderPath, "Models");
        AssetDatabase.CreateFolder(mainFolderPath, "Materials");

        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(mainFolderPath);
        //ProjectWindowUtil.CreateFolder();
    }
}
