using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnvilConstructZone : MonoBehaviour
{
    public bool blueprintInZone;

    public string activeConstructionBP;
    public string firstBP;

    public GameObject UICanvas;
    public TextMeshProUGUI armourPieceUIText;
    public TextMeshProUGUI ingotRequirementUIText;

    //public GameObject[] currentBPInZone;

    public int numberOfBPInZone;

    public int numberOfHelmetBP;
    public int numberOfShieldBP;
    public int numberOfChestplateBP;
    public int numberOfLeggingsBP;

    public int ironIngotNumber;
    public int copperIngotNumber;

    public int ingotRequirement = 0;

    public bool canBuild = false;

    public GameObject ironHelmetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfBPInZone > 0){
            blueprintInZone = true;
        }
        else{
            blueprintInZone = false;
        }

        if (blueprintInZone == true){
            UICanvas.SetActive(true);
        }
        else{
            UICanvas.SetActive(false);
        }

        armourPieceUIText.text = activeConstructionBP;
        ingotRequirementUIText.text = "Ingot Requirement: " + ingotRequirement.ToString("F0");

        if (numberOfBPInZone > 0 && (ironIngotNumber >= ingotRequirement || copperIngotNumber >= ingotRequirement)){
            canBuild = true;
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Blueprint"){
            numberOfBPInZone = numberOfBPInZone + 1;
            if (numberOfBPInZone == 1){
                activeConstructionBP = other.gameObject.GetComponent<Blueprint>().armourPiece;
                ingotRequirement = other.gameObject.GetComponent<Blueprint>().ingotRequirement;
                //firstBP = activeConstructionBP;
            }

            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Helmet"){
                numberOfHelmetBP = numberOfHelmetBP + 1;
            }
            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Shield"){
                numberOfShieldBP = numberOfShieldBP + 1;
            }
            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Chestplate"){
                numberOfChestplateBP = numberOfChestplateBP + 1;
            }
            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Leggings"){
                numberOfLeggingsBP = numberOfLeggingsBP + 1;
            }

            other.transform.parent = transform;
        }

        if (other.tag == "Pickup"){
            if (other.GetComponent<Ingot>() != null){
                if (other.GetComponent<Ingot>().ingotType == "Iron"){
                    ironIngotNumber = ironIngotNumber + 1;
                }
                else if (other.GetComponent<Ingot>().ingotType == "Copper"){
                    copperIngotNumber = copperIngotNumber + 1;
                }
            }

            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Blueprint"){
            numberOfBPInZone = numberOfBPInZone - 1;

            // Check current blueprint in collider against armour piece.
            // If none found, change active armour piece to something else.

            // 

            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Helmet"){
                numberOfHelmetBP = numberOfHelmetBP - 1;
            }
            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Shield"){
                numberOfShieldBP = numberOfShieldBP - 1;
            }
            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Chestplate"){
                numberOfChestplateBP = numberOfChestplateBP - 1;
            }
            if (other.gameObject.GetComponent<Blueprint>().armourPiece == "Leggings"){
                numberOfLeggingsBP = numberOfLeggingsBP - 1;
            }

            if (activeConstructionBP == other.gameObject.GetComponent<Blueprint>().armourPiece){
            //     return;
                if (numberOfBPInZone > 0){
                    ChangeActiveBlueprint();
                }
            }

            other.transform.parent = null;
        }

        if (other.tag == "Pickup"){
            if (other.GetComponent<Ingot>() != null){
                if (other.GetComponent<Ingot>().ingotType == "Iron"){
                    ironIngotNumber = ironIngotNumber - 1;
                }
                else if (other.GetComponent<Ingot>().ingotType == "Copper"){
                    copperIngotNumber = copperIngotNumber - 1;
                }
            }

            other.transform.parent = null;
        }
    }

    void ChangeActiveBlueprint(){
        List<string> possibleOptions = new List<string>();

        if (numberOfHelmetBP > 0){
            possibleOptions.Add("Helmet");
        }
        
        if (numberOfShieldBP > 0){
            possibleOptions.Add("Shield");
        }

        if (numberOfChestplateBP > 0){
            possibleOptions.Add("Chestplate");
        }

        if (numberOfLeggingsBP > 0){
            possibleOptions.Add("Leggings");
        }

        int randomNum = Random.Range(0, possibleOptions.Count);
        string randomString = possibleOptions[randomNum];

        activeConstructionBP = randomString;
    }

    public void ConstructArmourPiece(){
        if (canBuild == true){
            foreach (Transform child in transform){
                Destroy(child.gameObject);
            }

            canBuild = false;
            

            Instantiate(ironHelmetPrefab, transform.position, Quaternion.identity);
        }
    }
}
