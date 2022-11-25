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

    [SerializeField] private string currentOpenScene;

    // Start is called before the first frame update
    void Start()
    {   
        DontDestroyOnLoad(transform.gameObject);
        startWaiting = false;
        orderUIOn = false;
        orderTimerCanvas.SetActive(false);
        orderWantCanvas.SetActive(false);
        customerStartWaitTime = customerWaitTime;
        ShopFloorControllerRef = GameObject.Find("ShopFloorController");
        spawnerTransform = GameObject.Find("CustomerSpawnLocation(Clone)").transform;

        //difficulty = Random.Range(1,5); // will generate a number from 1 to 4.
        difficulty = Random.Range(1,3);

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
        }
        else if (difficulty == 4){
            // Wants specific armour piece.
            // Wants specific material.
            // Wants specific weight type.
            // Wants specific quality.
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

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
                Served();
            }
        }

        if (startWaiting == true){

            if (orderUIOn == false){
                orderWantCanvas.SetActive(true);
                orderTimerCanvas.SetActive(true);
                orderUIOn = true;
            }

            if (customerWaitTime >= 0.0f && currentScene == "Shop"){
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

    public void Served(){
        // The spot that they occupy -> Set to free.
        ShopFloorControllerRef.GetComponent<ShopFloorController>().CustomerServed(assignedSlot);


        SetTargetTransform(spawnerTransform);
        //Destroy(gameObject);
    }

    public void AssignSlot(int s){
        assignedSlot = s;
    }

    public int GetAssignedSlot(){
        return assignedSlot;
    }

    private void CheckHandoverBox(){
        //string inBoxArmourPiece;
        //string inBoxArmourMaterial;

        foreach(GameObject a in inHandoverBox){
            if (difficulty == 1){
                if (a.GetComponent<ArmourPiece>().GetArmourPieceString() == wantArmour){
                    inHandoverBox.Remove(a);
                    Destroy(a);
                    satisfied = true;
                    Served();
                }
            }
            else if (difficulty == 2){
                if (a.GetComponent<ArmourPiece>().GetArmourPieceString() == wantArmour && a.GetComponent<ArmourPiece>().GetArmourTypeString() == wantMaterialType){
                    inHandoverBox.Remove(a);
                    Destroy(a);
                    satisfied = true;
                    Served();
                }
            }
        }
    }
}
