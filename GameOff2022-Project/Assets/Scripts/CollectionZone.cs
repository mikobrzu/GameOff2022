using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionZone : MonoBehaviour
{
    public int countInBox = 0;
    public int countIron = 0;
    public int countCopper = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Pickup"){
            countInBox += 1;
            other.GetComponent<Ore>().inCollectionZone = true;
            //if (other.GetComponent<>())
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Pickup"){
            countInBox -= 1;
            other.GetComponent<Ore>().inCollectionZone = false;
            other.transform.parent = null;
        }
    }

    public void SaveBoxContents(){
        foreach (Transform child in transform){
            DataManager.Instance.AddOreToBox(child.gameObject, child.GetComponent<Ore>().oreType, child.GetComponent<Ore>().size, child.GetComponent<Ore>().weight, child.GetComponent<Ore>().quality, child.GetComponent<Ore>().price);
        }
    }
}
