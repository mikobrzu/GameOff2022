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
            // Check if can spawn customer.
                // can spawn in max number not reached.

                // can spawn if empty slot was found.
            // Spawn customer.
            // Send customer to available slot.
            if (numberOfCustomers < maxNumberOfCustomers){

                //bool emptySpotFound = false;
                //List<GameObject> possibleCSOptions = new List<GameObject>();
                //foreach (GameObject cS in customerSlots)
                //{
                    //if (cS.GetComponent<CustomerSlot>().CheckOccupied() == false){
                        //possibleCSOptions.Add(cS);
                    //}
                //}

                int slotToGo;
                slotToGo = maxNumberOfCustomers - (numberOfCustomers + 1);

                foreach (GameObject cS in customerSlots){
                    if (cS.GetComponent<CustomerSlot>().GetSlotID() == slotToGo){
                        if (CustomerSpawner != null){
                            GameObject NewCustomer = Instantiate(CustomerPrefab, CustomerSpawner.transform.position, Quaternion.identity);
                            NewCustomer.GetComponent<Customer>().SetTargetTransform(cS.transform);
                            SoundManager.Instance.PlaySound(customerBell);
                        }
                    }
                }

                numberOfCustomers += 1;
                GenWaitTime();
            }
        }
    }

    private void GenWaitTime(){
        countdownToSpawn = Random.Range(minPossibleWaitTime, maxPossibleWaitTime);
    }

    private void SpawnCustomer(){

    }
}
