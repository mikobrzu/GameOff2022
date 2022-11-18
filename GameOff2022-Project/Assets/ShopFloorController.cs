using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFloorController : MonoBehaviour
{
    [SerializeField] private int numberOfCustomers;
    [SerializeField] private int maxNumberOfCustomers;

    [SerializeField] private float minPossibleWaitTime, maxPossibleWaitTime;
    [SerializeField] private float countdownToSpawn;

    [SerializeField] private GameObject CustomerSpawner;
    [SerializeField] private GameObject CustomerPrefab;

    [SerializeField] private AudioClip customerBell;

    [SerializeField] private GameObject[] customerSlots;
    [SerializeField] List<GameObject> customersInShop;
    //[SerializeField] private GameObject[] emptyCustomerSlots;

    // Start is called before the first frame update
    void Start()
    {
        CustomerSpawner = GameObject.Find("CustomerSpawnLocation(Clone)");

        customerSlots = GameObject.FindGameObjectsWithTag("CustomerSlot");

        maxNumberOfCustomers = customerSlots.Length;

        GenWaitTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownToSpawn >= 0.0f){
            countdownToSpawn -= Time.deltaTime;
        }
        else{
            if (numberOfCustomers < maxNumberOfCustomers){

                int slotToGo;
                slotToGo = maxNumberOfCustomers - (numberOfCustomers + 1);

                foreach (GameObject cS in customerSlots){
                    if (cS.GetComponent<CustomerSlot>().GetSlotID() == slotToGo){
                        if (CustomerSpawner != null){
                            GameObject NewCustomer = Instantiate(CustomerPrefab, CustomerSpawner.transform.position, Quaternion.identity);
                            NewCustomer.GetComponent<Customer>().SetTargetTransform(cS.transform);
                            NewCustomer.GetComponent<Customer>().AssignSlot(slotToGo);
                            customersInShop.Add(NewCustomer);

                            // Set customer slot as occupied.
                            cS.GetComponent<CustomerSlot>().SetOccupied(true);

                            SoundManager.Instance.PlaySound(customerBell);
                        }
                    }
                }

                numberOfCustomers += 1;
                GenWaitTime();
            }
        }
        
        for (int i = 0; i < customersInShop.Count; i++){
            if (customersInShop[i].GetComponent<Customer>().GetAssignedSlot() != 3){

                int currentSlotAssigned = new int();
                currentSlotAssigned = customersInShop[i].GetComponent<Customer>().GetAssignedSlot();

                if (customerSlots[currentSlotAssigned + 1].GetComponent<CustomerSlot>().CheckOccupied() == false){
                    Debug.Log("Next position is free: moving...");
                    customersInShop[i].GetComponent<Customer>().SetTargetTransform(customerSlots[currentSlotAssigned + 1].transform);
                    customersInShop[i].GetComponent<Customer>().AssignSlot(customerSlots[currentSlotAssigned + 1].GetComponent<CustomerSlot>().GetSlotID());
                    customerSlots[currentSlotAssigned + 1].GetComponent<CustomerSlot>().SetOccupied(true);

                    customerSlots[currentSlotAssigned].GetComponent<CustomerSlot>().SetOccupied(false);
                }
            }
        }
    }

    private void GenWaitTime(){
        countdownToSpawn = Random.Range(minPossibleWaitTime, maxPossibleWaitTime);
    }

    private void SpawnCustomer(){

    }

    public void CustomerServed(int slotPreviouslyOccupied){
        
        for (int i = 0; i < customersInShop.Count; i++){
            if (customersInShop[i].GetComponent<Customer>().GetAssignedSlot() == slotPreviouslyOccupied){
                customersInShop.Remove(customersInShop[i]);
                numberOfCustomers -= 1;
            }
        }
        
        // Set slot as unoccupied.
        foreach (GameObject cS in customerSlots){
            if (cS.GetComponent<CustomerSlot>().GetSlotID() == slotPreviouslyOccupied){
                cS.GetComponent<CustomerSlot>().SetOccupied(false);
            }
        }
    }
}
