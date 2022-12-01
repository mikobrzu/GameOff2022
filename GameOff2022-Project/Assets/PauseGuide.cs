using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGuide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerText;

    [SerializeField] private GameObject basicsPage;
    [SerializeField] private GameObject gameObjectivePage;
    [SerializeField] private GameObject controlsPage;
    [SerializeField] private GameObject miningPage;
    [SerializeField] private GameObject smeltingPage;
    [SerializeField] private GameObject qualityPage;
    [SerializeField] private GameObject craftingArmourPage;
    [SerializeField] private GameObject servingCustomersPage;

    // Start is called before the first frame update
    void Start()
    {
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(false);
        miningPage.SetActive(false);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(false);
        OpenBasics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBasics(){
        headerText.text = "Basics";
        basicsPage.SetActive(true);

        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(false);
        miningPage.SetActive(false);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(false);
    }

    public void OpenGameObjective(){
        headerText.text = "Game Objective";
        
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(true);
        controlsPage.SetActive(false);
        miningPage.SetActive(false);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(false);
    }

    public void OpenControlsGuide(){
        headerText.text = "Controls Guide";
        
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(true);
        miningPage.SetActive(false);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(false);
    }

    public void OpenMining(){
        headerText.text = "Mining";
        
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(false);
        miningPage.SetActive(true);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(false);
    }

    public void OpenSmelting(){
        headerText.text = "Smelting";
        
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(false);
        miningPage.SetActive(false);
        smeltingPage.SetActive(true);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(false);
    }

    public void OpenQuality(){
        headerText.text = "Quality";
        
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(false);
        miningPage.SetActive(false);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(true);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(false);
    }

    public void OpenCraftingArmour(){
        headerText.text = "Crafting Armour";
        
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(false);
        miningPage.SetActive(false);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(true);
        servingCustomersPage.SetActive(false);
    }

    public void OpenServingCustomers(){
        headerText.text = "Serving Customers";
        
        basicsPage.SetActive(false);
        gameObjectivePage.SetActive(false);
        controlsPage.SetActive(false);
        miningPage.SetActive(false);
        smeltingPage.SetActive(false);
        qualityPage.SetActive(false);
        craftingArmourPage.SetActive(false);
        servingCustomersPage.SetActive(true);
    }
}
