using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VisibleObjectMenu
{
    [MenuItem("GameObject/Create Empty 3D Object", false, 1)]
    private static void CreateVisibleObject()
    {
        GameObject parent = new GameObject("new3DObject");
        if (Selection.activeGameObject != null)
            parent.transform.parent = Selection.activeGameObject.transform;

        GameObject v = new GameObject("Visuals");
        v.transform.parent = parent.transform;

        GameObject c = new GameObject("Colliders");
        c.transform.parent = parent.transform;

        Selection.activeObject = parent;
    }
}
