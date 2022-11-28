using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourToPlacementTrigger : MonoBehaviour
{
    //[SerializeField] private GameObject PlayerRef;
    //[SerializeField] private Rigidbody Prb;

    [SerializeField] private QualityCheckMachine QCM;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerRef = GameObject.Find("Player");
        //Prb = PlayerRef.rigidbody;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            Debug.Log("Player in armour placement zone.");
            QCM.SetPlayerInZone(true);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            Debug.Log("Left placement zone.");
            QCM.SetPlayerInZone(false);
        }
    }
}
