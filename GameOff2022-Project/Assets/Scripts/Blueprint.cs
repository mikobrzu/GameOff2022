using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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


    // Start is called before the first frame update
    void Start()
    {
        //playerCam = GameObject.Find("Camera");
        blueprintTargetGO = GameObject.Find("BlueprintTarget");
        blueprintTargetPosition = blueprintTargetGO.transform;

        blueprintHeader.text = armourPiece;

        if (armourPiece == "Helmet"){
            ingotRequirement = 2;
        }
        else if (armourPiece == "Shield"){
            ingotRequirement = 2;
        }
        else if (armourPiece == "Chestplate"){
            ingotRequirement = 4;
        }
        else if (armourPiece == "Leggings"){
            ingotRequirement = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (blueprintEquipped == true){
            if (Input.GetKeyDown(KeyCode.Q)){
                UnequipBlueprint();
            }

            transform.position = blueprintTargetPosition.transform.position;
            transform.rotation = blueprintTargetPosition.transform.rotation;
        }
    }

    public void EquipBlueprint(){
        RB.isKinematic = true;
        //col.enabled = false;
        blueprintEquipped = true;
        transform.SetParent(playerCam.transform, true);
        //gameObject.layer = LayerMask.NameToLayer("Tool");
        //foreach (Transform child in transform.GetComponentsInChildren<Transform>()){
            //child.gameObject.layer = LayerMask.NameToLayer("Tool");
        //}
        //transform.position = new Vector3(0f,0f,0f);
    }

    public void UnequipBlueprint(){
        blueprintEquipped = false;
        RB.isKinematic = false;
        //col.enabled = true;
        transform.parent = null;
        //gameObject.layer = LayerMask.NameToLayer("Interactable");
        //foreach (Transform child in transform.GetComponentsInChildren<Transform>()){
        //    child.gameObject.layer = LayerMask.NameToLayer("Interactable");
        //}
    }
}
