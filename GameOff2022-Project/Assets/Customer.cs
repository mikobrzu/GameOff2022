using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Customer : MonoBehaviour
{
    [SerializeField] private GameObject ShopFloorControllerRef;

    private bool atPosition = false;
    [SerializeField] private float customerSpeed;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform spawnerTransform;

    [SerializeField] private int assignedSlot;

    [SerializeField] private float customerMaxPotentialWaitTime, customerMinPotentialWaitTime;
    [SerializeField] private float customerWaitTime;
    [SerializeField] private float customerStartWaitTime;
    [SerializeField] private TextMeshProUGUI waitTimeText;
    [SerializeField] private TextMeshProUGUI wantsText;

    [Range(1, 4)]
    [SerializeField] private int difficulty;

    [SerializeField] private string wantArmour;
    [SerializeField] private string[] possibleArmour = new string[]{"Helmet", "Chestplate", "Leggings", "Shield"};

    [SerializeField] private string wantMaterialType;
    [SerializeField] private string[] possibleMaterial = new string[]{"Iron", "Copper"};

    [SerializeField] private string wantWeightType;
    [SerializeField] private string[] possibleWeightType = new string[]{"Light", "Heavy"};

    [SerializeField] private int wantQuality;
    [SerializeField] private int[] possibleQuality = new int[]{1,2,3,4,5};

    public List<GameObject> inHandoverBox = new List<GameObject>();

    [SerializeField] private bool satisfied = false;
    [SerializeField] private bool complained = false;

    [SerializeField] private Slider timerSlide;

    [SerializeField] private Image sliderFill;
    [SerializeField] private bool startWaiting;
    [SerializeField] private bool orderUIOn;

    [SerializeField] private GameObject orderWantCanvas;
    [SerializeField] private GameObject orderTimerCanvas;

    [SerializeField] private GameObject unhappyCanvas;
    [SerializeField] private GameObject happyCanvas;

    [SerializeField] private Scene currentOpenScene;

    [SerializeField] private SoundManager SMRef;
    [SerializeField] private AudioClip moneyGetAC;
    [SerializeField] private AudioClip happyCustomerAC;
    [SerializeField] private AudioClip angryCustomerAC;

    [SerializeField] private GameObject HandoverBox;

    [SerializeField] private PlayerData PlayerDataRef;
    [SerializeField] private GameObject PlayerRef;

    [SerializeField] private GameObject moneyTextIndicator;
    [SerializeField] private GameObject targetMoneyIndicator;

    // Start is called before the first frame update
    void Start()
    {   
        DontDestroyOnLoad(transform.gameObject);
        startWaiting = false;
        orderUIOn = false;
        orderTimerCanvas.SetActive(false);
        orderWantCanvas.SetActive(false);
        
        ShopFloorControllerRef = GameObject.Find("ShopFloorController");
        spawnerTransform = GameObject.Find("CustomerSpawnLocation(Clone)").transform;

        PlayerRef = GameObject.Find("Player");
        PlayerDataRef = PlayerRef.GetComponent<PlayerData>();

        HandoverBox.SetActive(false);

        // Set wait time.
        customerMinPotentialWaitTime = ShopFloorControllerRef.GetComponent<ShopFloorController>().GetCustomerOrderMinWaitTime();
        customerMaxPotentialWaitTime = ShopFloorControllerRef.GetComponent<ShopFloorController>().GetCustomerOrderMaxWaitTime();
        customerWaitTime = Random.Range(customerMinPotentialWaitTime, customerMaxPotentialWaitTime);

        customerStartWaitTime = customerWaitTime;
        // Set Difficulty
        //difficulty = Random.Range(1,5); // will generate a number from 1 to 4.
        difficulty = Random.Range(1,ShopFloorControllerRef.GetComponent<ShopFloorController>().GetCustomerMaxDifficulty() + 1);

        if (difficulty == 1){
            // Wants specific armour piece.

            int arrayRPointer;
            arrayRPointer = Random.Range(0, possibleArmour.Length);
            wantArmour = possibleArmour[arrayRPointer];

            if (wantArmour == "Helmet" || wantArmour == "Shield" || wantArmour == "Chestplate"){
                wantsText.text = "I'd like a <color=yellow>" + wantArmour + "</color> please!";
            }
            else {
                wantsText.text = "I'd like some <color=yellow>" + wantArmour + "</color> please!";
            }
        }
        else if (difficulty == 2){
            // Wants specific armour piece.
            // Wants specific material.

            int arrayRPointer;
            arrayRPointer = Random.Range(0, possibleArmour.Length);
            wantArmour = possibleArmour[arrayRPointer];
            arrayRPointer = Random.Range(0, possibleMaterial.Length);
            wantMaterialType = possibleMaterial[arrayRPointer];

            // if (wantArmour == "Helmet" || wantArmour == "Shield" || wantArmour == "Chestplate"){
            //     wantsText.text = "I'd like an " + wantMaterialType + " " + wantArmour + " please!";
            // }
            // else {
            //     wantsText.text = "I'd like some " + wantMaterialType + " " + wantArmour + " please!";
            // }

            if (wantArmour == "Leggings"){
                wantsText.text = "I'd like some <color=yellow>" + wantMaterialType + " " + wantArmour + "</color> please!";
            }
            else{
                if (wantMaterialType == "Iron"){
                    wantsText.text = "I'd like an <color=yellow>" + wantMaterialType + " " + wantArmour + "</color> please!";
                }
                else{
                    wantsText.text = "I'd like a <color=yellow>" + wantMaterialType + " " + wantArmour + "</color> please!";
                }
            }
        }
        else if (difficulty == 3){
            // Wants specific armour piece.
            // Wants specific material.
            // Wants specific weight type.

            int arrayRPointer;
            arrayRPointer = Random.Range(0, possibleArmour.Length);
            wantArmour = possibleArmour[arrayRPointer];
            arrayRPointer = Random.Range(0, possibleMaterial.Length);
            wantMaterialType = possibleMaterial[arrayRPointer];
            arrayRPointer = Random.Range(0, possibleWeightType.Length);
            wantWeightType = possibleWeightType[arrayRPointer];

            if (wantArmour == "Leggings"){
                wantsText.text = "I'd like some <color=yellow>" + wantWeightType + " " + wantMaterialType + " " + wantArmour + "</color> please!";
            }
            else{
                wantsText.text = "I'd like a <color=yellow>" + wantWeightType + " " + wantMaterialType + " " + wantArmour + "</color> please!";
            }
        }
        else if (difficulty == 4){
            // Wants specific armour piece.
            // Wants specific material.
            // Wants specific weight type.
            // Wants specific quality.

            int arrayRPointer;
            arrayRPointer = Random.Range(0, possibleArmour.Length);
            wantArmour = possibleArmour[arrayRPointer];
            arrayRPointer = Random.Range(0, possibleMaterial.Length);
            wantMaterialType = possibleMaterial[arrayRPointer];
            arrayRPointer = Random.Range(0, possibleWeightType.Length);
            wantWeightType = possibleWeightType[arrayRPointer];
            arrayRPointer = Random.Range(0, possibleQuality.Length);
            wantQuality = possibleQuality[arrayRPointer];
            
            if (wantArmour == "Leggings"){
                wantsText.text = "I'd like some <color=yellow>" + wantWeightType + " " + wantMaterialType + " " + wantArmour + "</color> please! Quality of at least <color=yellow>" + wantQuality.ToString("F0") + "</color>!";
            }
            else{
                wantsText.text = "I'd like a <color=yellow>" + wantWeightType + " " + wantMaterialType + " " + wantArmour + "</color> please! Quality of at least <color=yellow>" + wantQuality.ToString("F0") + "</color>!";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SMRef == null){
            SMRef = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        }

        currentOpenScene = SceneManager.GetActiveScene();

        if (transform.position == targetTransform.position){
            atPosition = true;
            startWaiting = true;
        }
        else{
            atPosition = false;
        }

        if (atPosition == false){
            MoveToSlot();
        }

        waitTimeText.text = customerWaitTime.ToString("F0") +"s";

        

        if (customerWaitTime <= 0.0f){
            if (complained == false){
                ShopFloorControllerRef.GetComponent<ShopFloorController>().AddComplaint();
                complained = true;
                orderWantCanvas.SetActive(false);
                orderTimerCanvas.SetActive(false);
                unhappyCanvas.SetActive(true);
                SMRef.PlaySound(angryCustomerAC);
                Served(false);
            }
        }

        if (startWaiting == true){

            if (orderUIOn == false){
                orderWantCanvas.SetActive(true);
                orderTimerCanvas.SetActive(true);
                HandoverBox.SetActive(true);
                orderUIOn = true;
            }

            if (customerWaitTime >= 0.0f && currentOpenScene.name == "Shop" && satisfied == false){
                customerWaitTime -= Time.deltaTime;
            }

            timerSlide.value = 1.0f + ((customerWaitTime - customerStartWaitTime) / customerStartWaitTime);
            if (timerSlide.value >= 0.5f){
                sliderFill.color = new Color32(69, 255, 0, 100);
            }
            else if (timerSlide.value < 0.5f && timerSlide.value > 0.25f){
                sliderFill.color = new Color32(255, 162, 0, 100);
            }
            else{
                sliderFill.color = new Color32(255, 25, 0, 100);
            }
            
        }

        // Check if correct armour is in handover box.
        CheckHandoverBox();
    }

    public void MoveToSlot(){
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, customerSpeed * Time.deltaTime);
    }

    public void SetTargetTransform(Transform tTransform){
        targetTransform = tTransform;
    }

    public void Served(bool happy){
        // The spot that they occupy -> Set to free.
        
        ShopFloorControllerRef.GetComponent<ShopFloorController>().CustomerServed(assignedSlot, happy);

        if (happy){
            SMRef.PlaySound(moneyGetAC);
        }

        SetTargetTransform(spawnerTransform);
        //Destroy(gameObject);
    }

    public void AssignSlot(int s){
        assignedSlot = s;
    }

    public int GetAssignedSlot(){
        return assignedSlot;
    }

    private void CalculateAndGiveGold(ArmourPiece aP){
        float goldToGive = 0.0f;

        // Gold To Give = Armour Price
        goldToGive = aP.GetComponent<ArmourPiece>().GetPiecePrice();

        // (20% if above 75% wait time, 10% if above 50% wait time, -10% if below 25% of wait time)

        if ((customerWaitTime/customerStartWaitTime) > 0.75f){
            goldToGive = goldToGive * 1.2f;
        }
        else if ((customerWaitTime/customerStartWaitTime) > 0.5f){
            goldToGive = goldToGive * 1.1f;
        }
        else if ((customerWaitTime/customerStartWaitTime) < 0.25f){
            goldToGive = goldToGive * 0.9f;
        }

        if (difficulty == 4){
            goldToGive = goldToGive + 20.0f;
        }
        else if (difficulty == 3){
            goldToGive = goldToGive + 10.0f;
        }

        PlayerDataRef.playerGold = PlayerDataRef.playerGold + goldToGive;
        
        // Check if this is the most gold received.
        if (PlayerDataRef.mostGoldReceived < goldToGive){
            PlayerDataRef.mostGoldReceived = goldToGive;
        }

        MoneyIndicator indicator = Instantiate(moneyTextIndicator, targetMoneyIndicator.transform.position, Quaternion.identity).GetComponent<MoneyIndicator>();
        indicator.SetMoneyText(goldToGive);
    }

    private void CheckHandoverBox(){
        //string inBoxArmourPiece;
        //string inBoxArmourMaterial;

        foreach(GameObject a in inHandoverBox){
            if (difficulty == 1){
                if (a.GetComponent<ArmourPiece>().GetArmourPieceString() == wantArmour){
                    CalculateAndGiveGold(a.GetComponent<ArmourPiece>());
                    inHandoverBox.Remove(a);
                    Destroy(a);
                    satisfied = true;
                    SMRef.PlaySound(happyCustomerAC);
                    
                    PlayerDataRef.totalCustomersServed = PlayerDataRef.totalCustomersServed + 1;

                    Served(true);
                }
            }
            else if (difficulty == 2){
                if (a.GetComponent<ArmourPiece>().GetArmourPieceString() == wantArmour && a.GetComponent<ArmourPiece>().GetArmourTypeString() == wantMaterialType){
                    CalculateAndGiveGold(a.GetComponent<ArmourPiece>());
                    inHandoverBox.Remove(a);
                    Destroy(a);
                    satisfied = true;
                    SMRef.PlaySound(happyCustomerAC);

                    PlayerDataRef.totalCustomersServed = PlayerDataRef.totalCustomersServed + 1;

                    Served(true);
                }
            }
            else if (difficulty == 3){
                if (a.GetComponent<ArmourPiece>().GetArmourPieceString() == wantArmour && a.GetComponent<ArmourPiece>().GetArmourTypeString() == wantMaterialType){
                    // Check the weight against wants.
                    if (wantWeightType == "Light"){
                        if (a.GetComponent<ArmourPiece>().GetLightVarientStatus() == true){
                            // This is the armour.
                            CalculateAndGiveGold(a.GetComponent<ArmourPiece>());
                            inHandoverBox.Remove(a);
                            Destroy(a);
                            satisfied = true;
                            SMRef.PlaySound(happyCustomerAC);

                            PlayerDataRef.totalCustomersServed = PlayerDataRef.totalCustomersServed + 1;

                            Served(true);
                        }
                    }
                    else if (wantWeightType == "Heavy"){
                        if (a.GetComponent<ArmourPiece>().GetLightVarientStatus() == false){
                            // This is the armour.
                            CalculateAndGiveGold(a.GetComponent<ArmourPiece>());
                            inHandoverBox.Remove(a);
                            Destroy(a);
                            satisfied = true;
                            SMRef.PlaySound(happyCustomerAC);

                            PlayerDataRef.totalCustomersServed = PlayerDataRef.totalCustomersServed + 1;

                            Served(true);
                        }
                    }
                }
            }
            else if (difficulty == 4){
                if (a.GetComponent<ArmourPiece>().GetArmourPieceString() == wantArmour && a.GetComponent<ArmourPiece>().GetArmourTypeString() == wantMaterialType && a.GetComponent<ArmourPiece>().GetPieceQuality() >= wantQuality){
                    // Check the weight against wants.
                    // Check quality against wants.
                    if (wantWeightType == "Light"){
                        if (a.GetComponent<ArmourPiece>().GetLightVarientStatus() == true){
                            // This is the armour.
                            CalculateAndGiveGold(a.GetComponent<ArmourPiece>());
                            inHandoverBox.Remove(a);
                            Destroy(a);
                            satisfied = true;
                            SMRef.PlaySound(happyCustomerAC);

                            PlayerDataRef.totalCustomersServed = PlayerDataRef.totalCustomersServed + 1;

                            Served(true);
                        }
                    }
                    else if (wantWeightType == "Heavy"){
                        if (a.GetComponent<ArmourPiece>().GetLightVarientStatus() == false){
                            // This is the armour.
                            CalculateAndGiveGold(a.GetComponent<ArmourPiece>());
                            inHandoverBox.Remove(a);
                            Destroy(a);
                            satisfied = true;
                            SMRef.PlaySound(happyCustomerAC);

                            PlayerDataRef.totalCustomersServed = PlayerDataRef.totalCustomersServed + 1;

                            Served(true);
                        }
                    }
                }
            }
        }
    }
}
