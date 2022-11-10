using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlate : MonoBehaviour
{
    public GameObject ScaleRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Pickup"){
            ScaleRef.GetComponent<Scale>().onScale.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "Pickup"){
            ScaleRef.GetComponent<Scale>().onScale.Remove(collision.gameObject);
        }
    }
}
