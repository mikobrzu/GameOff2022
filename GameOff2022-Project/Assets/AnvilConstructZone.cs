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

    public float ironWeightOnAnvil;
    public float copperWeightOnAnvil;

    public int ingotRequirement = 0;

    public bool canBuild = false;
    private string armourTypeToBuild;

    // Remove the one variable below...
    public GameObject ironHelmetPrefab;

    [SerializeField] private GameObject Helmet_CL;
    [SerializeField] private GameObject Helmet_CH;
    [SerializeField] private GameObject Helmet_IL;
    [SerializeField] private GameObject Helmet_IH;

    [SerializeField] private GameObject Chestplate_CL;
    [SerializeField] private GameObject Chestplate_CH;
    [SerializeField] private GameObject Chestplate_IL;
    [SerializeField] private GameObject Chestplate_IH;

    [SerializeField] private GameObject Leggings_CL;
    [SerializeField] private GameObject Leggings_CH;
    [SerializeField] private GameObject Leggings_IL;
    [SerializeField] private GameObject Leggings_IH;

    [SerializeField] private GameObject Shield_CL;
    [SerializeField] private GameObject Shield_CH;
    [SerializeField] private GameObject Shield_IL;
    [SerializeField] private GameObject Shield_IH;

    [SerializeField] private GameObject SmokeEffect;

    [SerializeField] private float lightArmourMaxWeight;

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

        CalculateWeightOnAnvil();
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

    public void CalculateWeightOnAnvil(){
        ironWeightOnAnvil = 0f;
        copperWeightOnAnvil = 0f;

        GameObject[] childrenOfAnvil;
        childrenOfAnvil = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenOfAnvil[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject cObj in childrenOfAnvil){
            if (cObj.GetComponent<Ingot>() != null){
                if (cObj.GetComponent<Ingot>().ingotType == "Iron"){
                    ironWeightOnAnvil = ironWeightOnAnvil + cObj.GetComponent<Ingot>().weight;
                }
                else {
                    copperWeightOnAnvil = copperWeightOnAnvil + cObj.GetComponent<Ingot>().weight;
                }
            }
        }
    }

    private float CalculateAverageQualityOnAnvil(){
        float returnQuality;

        GameObject[] childrenOfAnvil;
        childrenOfAnvil = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenOfAnvil[i] = transform.GetChild(i).gameObject;
        }

        float totalQualityValue = new float();
        foreach (GameObject cObj in childrenOfAnvil){
            if (cObj.GetComponent<Ingot>() != null){
                totalQualityValue = totalQualityValue + cObj.GetComponent<Ingot>().quality;
            }
        }

        returnQuality = totalQualityValue / (ironIngotNumber + copperIngotNumber);

        return returnQuality;
    }

    public void ConstructArmourPiece(){
        if (canBuild == true){
            foreach (Transform child in transform){
                Destroy(child.gameObject);
            }

            //Instantiate(ironHelmetPrefab, transform.position, Quaternion.identity);

            // Determine metal type.
            

            if (ironIngotNumber > copperIngotNumber){
                armourTypeToBuild = "Iron";
            }
            else if (ironIngotNumber < copperIngotNumber){
                armourTypeToBuild = "Copper";
            }
            else if (ironIngotNumber == copperIngotNumber){
                // Split difference by weight.
                if (ironWeightOnAnvil > copperWeightOnAnvil){
                    // Armour will be iron.
                    armourTypeToBuild = "Iron";
                }
                else if (ironWeightOnAnvil < copperWeightOnAnvil){
                    // Armour will be copper.
                    armourTypeToBuild = "Copper";
                }
                else if (ironWeightOnAnvil == copperWeightOnAnvil){
                    // Armour will be random.
                    int randomTypeChoice = 0;
                    randomTypeChoice = Random.Range(0,2);
                    if (randomTypeChoice == 0){
                        armourTypeToBuild = "Iron";
                    }
                    else {
                        armourTypeToBuild = "Copper";
                    }
                }
            }

            // Determine armour piece.
            string armourPieceToBuild;
            armourPieceToBuild = activeConstructionBP;
            if (armourPieceToBuild == "Helmet"){
                if (armourTypeToBuild == "Copper"){
                    if (copperWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Helmet_CH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else{
                        GameObject CraftedArmour = Instantiate(Helmet_CL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
                else {
                    if (ironWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Helmet_IH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else{
                        GameObject CraftedArmour = Instantiate(Helmet_IL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
            }
            else if (armourPieceToBuild == "Chestplate"){
                if (armourTypeToBuild == "Copper"){
                    if (copperWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Chestplate_CH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else{
                        GameObject CraftedArmour = Instantiate(Chestplate_CL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
                else {
                    if (ironWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Chestplate_IH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else{
                        GameObject CraftedArmour = Instantiate(Chestplate_IL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
            }
            else if (armourPieceToBuild == "Leggings"){
                if (armourTypeToBuild == "Copper"){
                    if (copperWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Leggings_CH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else{
                        GameObject CraftedArmour = Instantiate(Leggings_CL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
                else {
                    if (ironWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Leggings_IH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else{
                        GameObject CraftedArmour = Instantiate(Leggings_IL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
            }
            else if (armourPieceToBuild == "Shield"){
                if (armourTypeToBuild == "Copper"){
                    if (copperWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Shield_CH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else{
                        GameObject CraftedArmour = Instantiate(Shield_CL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(copperWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
                else {
                    if (ironWeightOnAnvil > lightArmourMaxWeight){
                        GameObject CraftedArmour = Instantiate(Shield_IH, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                    else {
                        GameObject CraftedArmour = Instantiate(Shield_IL, transform.position, Quaternion.identity);
                        CraftedArmour.GetComponent<ArmourPiece>().SetWeight(ironWeightOnAnvil);
                        CraftedArmour.GetComponent<ArmourPiece>().SetQuality(CalculateAverageQualityOnAnvil());
                    }
                }
            }

            canBuild = false;
            ironIngotNumber = 0;
            copperIngotNumber = 0;
            numberOfBPInZone = 0;
            numberOfHelmetBP = 0;
            numberOfChestplateBP = 0;
            numberOfLeggingsBP = 0;
            numberOfShieldBP = 0;

            Instantiate(SmokeEffect, transform.position, Quaternion.identity);
        }
    }
}
