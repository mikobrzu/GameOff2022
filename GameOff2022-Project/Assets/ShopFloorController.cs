using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    [SerializeField] private int complaints;
    [SerializeField] private int maxComplaints = 4;
    //[SerializeField] private TextMeshProUGUI complaintsText;
    [SerializeField] private bool closeShop;
    //[SerializeField] private GameObject[] emptyCustomerSlots;

    [SerializeField] private GameObject LevelLoaderRef;


    [SerializeField] private int customerMaxDifficulty = 1;
    [SerializeField] private int customersTotalServed = 0;

    [SerializeField] private float customerOrderMinWaitTime = 10.0f;
    [SerializeField] private float customerOrderMaxWaitTime = 60.0f;

    [SerializeField] private int currentShopLevel = 1;
    [SerializeField] private int maxShopLevel;
    [SerializeField] private float currentShopExperience;
    [SerializeField] private float shopExperienceNeeded;

    [SerializeField] private float experienceReward;

    //[SerializeField] private string currentScene;

    private void Awake(){
        GameObject[] obj = GameObject.FindGameObjectsWithTag("ShopFloorController");
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
        complaints = 0;
        CustomerSpawner = GameObject.Find("CustomerSpawnLocation(Clone)");

        customerSlots = GameObject.FindGameObjectsWithTag("CustomerSlot");

        maxNumberOfCustomers = customerSlots.Length;

        LevelLoaderRef = GameObject.Find("LevelLoader");

        GenWaitTime();
    }

    // Update is called once per frame
    void Update()
    {
        shopExperienceNeeded = currentShopLevel * 30;
        if (currentShopLevel < maxShopLevel){
            if (currentShopExperience > shopExperienceNeeded){
                currentShopLevel = currentShopLevel + 1;
            }
        }

        CalculateShopStats();
        SetOrderDifficulty();

        if (CustomerSpawner == null){
            CustomerSpawner = GameObject.Find("CustomerSpawnLocation(Clone)");
        }

        if (LevelLoaderRef == null){
            LevelLoaderRef = GameObject.Find("LevelLoader");
        }


        if (countdownToSpawn >= 0.0f && SceneManager.GetActiveScene().name == "Shop"){
            countdownToSpawn -= Time.deltaTime;
        }
        else if (countdownToSpawn >= 0.0f && SceneManager.GetActiveScene().name != "Shop") {
            countdownToSpawn = countdownToSpawn;
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

        //complaintsText.text = "Complaints: " + complaints.ToString("F0") + "/" + maxComplaints.ToString("F0");
        if (complaints >= maxComplaints){
            closeShop = true;
        }

        if (closeShop == true){
            PDRef.SavePlayerData();
            LevelLoaderRef.GetComponent<LevelLoader>().FinishGame();
        }
    }

    private void GenWaitTime(){
        countdownToSpawn = Random.Range(minPossibleWaitTime, maxPossibleWaitTime);
    }

    private void SpawnCustomer(){

    }

    public void CustomerServed(int slotPreviouslyOccupied, bool customerHappy){
        
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

        if (customerHappy){
            currentShopExperience = currentShopExperience + experienceReward;
            customersTotalServed = customersTotalServed + 1;
        }
    }

    public void AddComplaint(){
        complaints += 1;
    }

    private void CalculateShopStats(){
        minPossibleWaitTime = (120.0f / (float)currentShopLevel) * 1.8f;
        maxPossibleWaitTime = (20.0f / (float)currentShopLevel) * 16.0f;

        customerOrderMinWaitTime = ((100.0f / currentShopLevel) + (20.0f * 3.0f));
        customerOrderMaxWaitTime = (customerOrderMinWaitTime + 40.0f);
    }

    private void SetOrderDifficulty(){
        if (currentShopLevel == 1 || currentShopLevel == 2){
            customerMaxDifficulty = 1;
        }
        else if (currentShopLevel == 3 || currentShopLevel == 4){
            customerMaxDifficulty = 2;
        }
        else if (currentShopLevel == 5 || currentShopLevel == 6){
            customerMaxDifficulty = 3;
        }
        else if (currentShopLevel > 6){
            customerMaxDifficulty = 4;
        }
    }

    public int GetCurrentNumberOfComplaints(){
        return complaints;
    }

    public int GetMaxComplaints(){
        return maxComplaints;
    }

    public float GetMaxPossibleWaitTime(){
        return maxPossibleWaitTime;
    }

    public float GetMinPossibleWaitTime(){
        return minPossibleWaitTime;
    }

    public int GetCustomerMaxDifficulty(){
        return customerMaxDifficulty;
    }

    public int GetTotalServed(){
        return customersTotalServed;
    }

    public float GetCustomerOrderMinWaitTime(){
        return customerOrderMinWaitTime;
    }

    public float GetCustomerOrderMaxWaitTime(){
        return customerOrderMaxWaitTime;
    }

    public int GetCurrentShopLevel(){
        return currentShopLevel;
    }

    public float GetCurrentExperience(){
        return currentShopExperience;
    }

    public float GetNeededExperience(){
        return shopExperienceNeeded;
    }
}
