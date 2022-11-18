using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandoverBox : MonoBehaviour
{
    [SerializeField] private GameObject CustomerRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Armour"){
            CustomerRef.GetComponent<Customer>().inHandoverBox.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Armour"){
            CustomerRef.GetComponent<Customer>().inHandoverBox.Remove(other.gameObject);
        }
    }
}
