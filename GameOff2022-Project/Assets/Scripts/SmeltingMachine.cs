using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmeltingMachine : MonoBehaviour
{
    //public List<GameObject> inMachine;

    // Weight in Copper
    // Weight in Iron

    // Total Weight

    // Average Quality

    // Total Price = Average * 1.10 (10%)

    


    public bool machineEmpty = true;

    public float oreInMachine = 0f;
    
    public float weightInCopper = 0f;
    public float weightInIron = 0f;

    public float totalWeight = 0f;

    public float qualityValue = 0f;
    public float outputQuality = 0f;

    public float totalPrice = 0f;
    public float totalOutputPrice = 0f;

    public Transform ingotSpawnLocation;
    public GameObject ingotPrefab;

    public TextMeshProUGUI typeText;
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI qualityText;
    public TextMeshProUGUI priceText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oreInMachine > 0f){
            machineEmpty = false;

            CalculateOutputQuality();
            CalculateOutputPrice();

            typeText.enabled = true;
            weightText.enabled = true;
            qualityText.enabled = true;
            priceText.enabled = true;
        }
        else if (oreInMachine <= 0f){
            machineEmpty = true;

            typeText.enabled = false;
            weightText.enabled = false;
            qualityText.enabled = false;
            priceText.enabled = false;
        }

        if (weightInCopper > weightInIron){
            typeText.text = "Copper Ingot";
        }
        else{
            typeText.text = "Iron Ingot";
        }

        weightText.text = "Weight: " + totalWeight.ToString("F0");
        qualityText.text = "Quality: " + outputQuality.ToString("F0");
        priceText.text = "£" + totalOutputPrice.ToString("F2");
    }

    public void AddOreToMachine(string type, float weight, float quality, float price){
        oreInMachine += 1.0f;

        totalWeight = totalWeight + weight;
        qualityValue = qualityValue + quality;
        totalPrice = totalPrice + price;

        if (type == "Iron"){
            weightInIron = weightInIron + weight;
        }
        else if (type == "Copper"){
            weightInCopper = weightInCopper + weight;
        }
    }

    void CalculateOutputQuality(){
        outputQuality = qualityValue / oreInMachine;
    }

    void CalculateOutputPrice(){
        totalOutputPrice = (totalPrice / oreInMachine) * 1.1f * outputQuality;
    }

    public void MakeIngot(){
        if (machineEmpty == false){

            //CalculateOutputQuality();
            //CalculateOutputPrice();

            if (weightInCopper > weightInIron){
                // Make Copper Ingot
                GameObject outputIngot = Instantiate(ingotPrefab, ingotSpawnLocation.position, Quaternion.identity);
                outputIngot.GetComponent<Ingot>().ingotType = "Copper";

                // Set Attributes
                outputIngot.GetComponent<Ingot>().weight = totalWeight;
                outputIngot.GetComponent<Ingot>().quality = outputQuality;
                outputIngot.GetComponent<Ingot>().price = totalOutputPrice;
            }
            else if (weightInIron > weightInCopper){
                // Make Iron Ingot
                GameObject outputIngot = Instantiate(ingotPrefab, ingotSpawnLocation.position, Quaternion.identity);
                outputIngot.GetComponent<Ingot>().ingotType = "Iron";
                
                // Set Attributes
                outputIngot.GetComponent<Ingot>().weight = totalWeight;
                outputIngot.GetComponent<Ingot>().quality = outputQuality;
                outputIngot.GetComponent<Ingot>().price = totalOutputPrice;
            }

            totalWeight = 0f;
            qualityValue = 0f;
            totalPrice = 0f;

            weightInCopper = 0f;
            weightInIron = 0f;

            outputQuality = 0f;
            totalOutputPrice = 0f;

            oreInMachine = 0f;

            machineEmpty = true;
        }
    }
}
