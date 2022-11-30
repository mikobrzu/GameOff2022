using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Blueprint : MonoBehaviour
{
    public string armourPiece;

    public TextMeshProUGUI blueprintHeader;

    public GameObject blueprintTargetGO;
    public Transform blueprintTargetPosition;
    public Rigidbody RB;
    public Collider col;

    public GameObject playerCam;
    
    public Quaternion blueprintTargetRotation;

    public bool blueprintEquipped;

    public int ingotRequirement;

    public Sprite arHelmet;
    public Sprite arShield;
    public Sprite arChestplate;
    public Sprite arLeggings;
    
    public Image arImage;

    public bool blueprintInCZ;

    [SerializeField] private GameObject[] ingotImageGO;

    [SerializeField] private GameObject PlayerRef;
    [SerializeField] private SoundManager SMRef;

    [SerializeField] private AudioClip blueprintPickupAC;

    // Start is called before the first frame update
    void Start()
    {
        SMRef = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        //blueprintEquipped = false;
        PlayerRef = GameObject.Find("Player");
        blueprintInCZ = false;
        //playerCam = GameObject.Find("Camera");
        blueprintTargetGO = GameObject.Find("BlueprintTarget");
        blueprintTargetPosition = blueprintTargetGO.transform;

        blueprintHeader.text = armourPiece;

        if (armourPiece == "Helmet"){
            ingotRequirement = 2;
            arImage.sprite = arHelmet;

        }
        else if (armourPiece == "Shield"){
            ingotRequirement = 2;
            arImage.sprite = arShield;
        }
        else if (armourPiece == "Chestplate"){
            ingotRequirement = 4;
            arImage.sprite = arChestplate;
        }
        else if (armourPiece == "Leggings"){
            ingotRequirement = 3;
            arImage.sprite = arLeggings;
        }

        foreach (GameObject i in ingotImageGO){
            i.SetActive(false);
        }

        for (int a = 0; a < ingotRequirement; a++){
            ingotImageGO[a].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SMRef == null){
            SMRef = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        }

        if (blueprintEquipped == true){
            if (Input.GetKeyDown(KeyCode.Q)){
                UnequipBlueprint();
            }

            transform.position = blueprintTargetPosition.transform.position;
            transform.rotation = blueprintTargetPosition.transform.rotation;
        }
    }

    public void EquipBlueprint(){
        if (PlayerRef.GetComponent<PlayerController>().GetHoldingBP() == false){
            if (SMRef != null){
                SMRef.PlaySound(blueprintPickupAC);
            }
            RB.isKinematic = true;
            col.enabled = false;
            
            transform.SetParent(playerCam.transform, true);
            blueprintInCZ = false;
            
            PlayerRef.GetComponent<PlayerController>().SetHoldingBP(true);
            blueprintEquipped = true;
        }
        
    }

    public void UnequipBlueprint(){
        if (PlayerRef.GetComponent<PlayerController>().GetHoldingBP() == true){
            blueprintEquipped = false;
            RB.isKinematic = false;
            col.enabled = true;
            transform.parent = null;
            Debug.Log("Unequip called.");
            PlayerRef.GetComponent<PlayerController>().SetHoldingBP(false);
        }
    }

    public void SetPlayerRef(GameObject p){
        PlayerRef = p;
    }

    public void SetSMRef(SoundManager sm){
        SMRef = sm;
    }
}
