using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Item Data")]
public class ItemSO : ScriptableObject
{
    public string itemType;
    public GameObject itemModel;
    public float minSize = 0.5f;
    public float maxSize = 2.5f;
    //public float size;
    //public float weight;
    public float minQuality = 1f;
    public float maxQuality = 5f;
    //public float quality;
    //public float price;
}
