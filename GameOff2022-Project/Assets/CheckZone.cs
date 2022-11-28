using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    [SerializeField] private QualityCheckMachine QCM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col){
        if (col.tag == "Armour"){
            QCM.AddToList(col.GetComponent<ArmourPiece>());
        }
    }

    private void OnTriggerExit(Collider col){
        if (col.tag == "Armour"){
            QCM.RemoveFromList(col.GetComponent<ArmourPiece>());
        }
    }
}
