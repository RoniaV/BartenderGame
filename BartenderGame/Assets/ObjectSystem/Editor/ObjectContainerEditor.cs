//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CanEditMultipleObjects()]
//[CustomEditor(typeof(ObjectContainer))]
//public class ObjectContainerEditor : Editor
//{
//    ObjectContainer objContainer;

//    SerializedObject so;
//    SerializedProperty propInfiniteContainer;
//    SerializedProperty propObjectPrefab;
//    SerializedProperty propMaxObjects;
//    SerializedProperty propDropPoints;

//    void OnEnable()
//    {
//        objContainer = (ObjectContainer)target;
//        so = serializedObject;
//        propInfiniteContainer = so.FindProperty("infiniteContainer");
//        propObjectPrefab = so.FindProperty("objectPrefab");
//        propMaxObjects = so.FindProperty("maxObjects");
//        propDropPoints = so.FindProperty("dropPoints");

//        objContainer.UpdateDropPoints();
//    }

//    public override void OnInspectorGUI()
//    {
//        so.Update();
//        EditorGUILayout.PropertyField(propInfiniteContainer);

//        if (propInfiniteContainer.boolValue)
//        {
//            EditorGUILayout.PropertyField(propObjectPrefab);

//            so.ApplyModifiedProperties();
//        }
//        else
//        {
//            EditorGUILayout.PropertyField(propMaxObjects);
//            propDropPoints.arraySize = propMaxObjects.intValue;
//            EditorGUILayout.PropertyField(propDropPoints);

//            so.ApplyModifiedProperties();
//            //if (so.ApplyModifiedProperties())
//            //{
//            //    objContainer.UpdateDropPoints();
//            //}
//        }
//    }
//}
