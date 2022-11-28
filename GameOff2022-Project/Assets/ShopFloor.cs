using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFloor : MonoBehaviour
{

    [SerializeField] private float counterDistance;
    [SerializeField] private Transform counterPointA, counterPointB;
    private Transform slotSpawnPointA, slotSpawnPointB;

    [SerializeField] private int counterSlots;
    [SerializeField] private GameObject customerSlotPrefab;
    [SerializeField] private float spaceBetweenSlots;

    [SerializeField] private float floorStartXOffset;
    [SerializeField] private float floorStartZOffset;

    [SerializeField] private GameObject customerSpawnLocationPrefab;

    [SerializeField] private bool spawnedSlots;

    private void Awake(){
        GameObject[] obj = GameObject.FindGameObjectsWithTag("ShopFloor");
        if (obj.Length > 1){
            Destroy(this.gameObject);
        }
        else{
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(transform.gameObject);

        if (spawnedSlots == false || spawnedSlots == null){
            slotSpawnPointA = counterPointA;
            slotSpawnPointA.position = slotSpawnPointA.position + new Vector3(2f,0f,0f);
            slotSpawnPointB = counterPointB;
            slotSpawnPointB.position = slotSpawnPointB.position + new Vector3(-1f,0f,0f);
            counterDistance = Vector3.Distance(slotSpawnPointA.position, slotSpawnPointB.position);
            spaceBetweenSlots = counterDistance / counterSlots;

            transform.position = new Vector3(slotSpawnPointA.position.x + floorStartXOffset, 0f, slotSpawnPointA.position.z + floorStartZOffset);

            for (int i = 0; i < counterSlots; i++){
                GameObject slot = Instantiate(customerSlotPrefab, transform.position + new Vector3(spaceBetweenSlots * i, 0f, 0f), Quaternion.identity);
                slot.GetComponent<CustomerSlot>().SetSlotID(i);
            }

            Instantiate(customerSpawnLocationPrefab, transform.position + new Vector3(-5f, 0f, 0f), Quaternion.identity);
            spawnedSlots = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
