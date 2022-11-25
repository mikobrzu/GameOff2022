using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSlot : MonoBehaviour
{
    [SerializeField] private int slotID;
    [SerializeField] private bool occupiedSlot;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlotID(int id){
        slotID = id;
    }

    public void SetOccupied(bool isOccupied){
        occupiedSlot = isOccupied;
    }

    public bool CheckOccupied(){
        return occupiedSlot;
    }

    public int GetSlotID(){
        return slotID;
    }
}
