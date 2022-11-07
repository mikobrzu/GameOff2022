using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResource : MonoBehaviour
{
    public ItemSO itemData;
    public float sizeMax = 2.5f;
    public float sizeMin = 0.5f;
    [Range(0.5f, 2.5f)]
    public float itemSize;
    public float itemQuality;
    public float itemWeight;
    public float itemPrice;

    // Start is called before the first frame update
    void Start()
    {
        if (itemData != null){
            LoadResource(itemData);
            CalculateSize(itemData);
            CalculateWeight();
            CalculateQuality();
            CalculatePrice();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadResource(ItemSO item){
        GameObject itemVisual = Instantiate(item.itemModel);
        itemVisual.transform.SetParent(this.transform);
        itemVisual.transform.localPosition = Vector3.zero;
        itemVisual.transform.rotation = Quaternion.identity;
        itemVisual.transform.rotation = Quaternion.Euler(-90, 0, 45);
    }

    private void CalculateSize(ItemSO item){
        itemSize = Random.Range(item.minSize, item.maxSize);
        transform.localScale = new Vector3(itemSize, itemSize, itemSize);
    }

    private void CalculateWeight(){
        itemWeight = itemSize * 10f;
    }

    private void CalculateQuality(){
        itemQuality = Random.Range(1,5);
    }

    private void CalculatePrice(){
        itemPrice = itemQuality * itemWeight;
    }
}
