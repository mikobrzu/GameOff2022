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
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Pickup"){
            countInBox -= 1;
        }
    }
}
