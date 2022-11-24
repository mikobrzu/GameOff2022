using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterEntryPoint : MonoBehaviour
{
    public GameObject SMRef;
    public GameObject BeltRef;

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

            // Remove from belt.
            BeltRef.GetComponent<ConveyorBelt>().onBelt.Remove(other.gameObject);

            // Check if Game Object was removed.

            SMRef.GetComponent<SmeltingMachine>().AddOreToMachine(other.GetComponent<Ore>().oreType, other.GetComponent<Ore>().weight, other.GetComponent<Ore>().quality, other.GetComponent<Ore>().price);
            Destroy(other.gameObject);
        }
    }
}
