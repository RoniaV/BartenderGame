using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ConsumableType", fileName = "ConsumableObjectType")]
public class ConsumableType : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] GameObject model;
    [SerializeField] Sprite image;

    public string Name { get { return name; } }
    public GameObject Model { get { return model; } }
    public Sprite Image { get { return image; } }
}
