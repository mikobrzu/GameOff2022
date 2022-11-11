using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltZone : MonoBehaviour
{
    public int oresNumberOnBelt = 0;

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
            oresNumberOnBelt += 1;
            //oresOnBelt.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Pickup"){
            oresNumberOnBelt -= 1;
            //oresOnBelt.Remove(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other){
            if (other.attachedRigidbody){
                other.attachedRigidbody.AddForce(Vector3.left * 10);
            }
        }
}
