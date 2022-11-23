using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlueprintSpawner : MonoBehaviour
{
    public string armourPiece;

    public TextMeshProUGUI blueprintHeader;

    public GameObject blueprintPrefab;

    public GameObject effectPrefab;

    public GameObject playerCam;

    public Sprite arHelmet;
    public Sprite arShield;
    public Sprite arChestplate;
    public Sprite arLeggings;
    
    public Image arImage;

    [SerializeField] private int ingotsNeeded;
    [SerializeField] private GameObject[] ingotImageGO;

    //private int spawnedBlueprints = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (armourPiece == null || armourPiece == ""){
            blueprintHeader.text = "Not set.";
        }
        blueprintHeader.text = armourPiece;

        if (armourPiece == "Helmet"){
            arImage.sprite = arHelmet;
            ingotsNeeded = 2;
        }
        else if (armourPiece == "Shield"){
            arImage.sprite = arShield;
            ingotsNeeded = 2;
        }
        else if (armourPiece == "Chestplate"){
            arImage.sprite = arChestplate;
            ingotsNeeded = 4;
        }
        else if (armourPiece == "Leggings"){
            arImage.sprite = arLeggings;
            ingotsNeeded = 3;
        }

        foreach (GameObject i in ingotImageGO){
            i.SetActive(false);
        }

        for (int a = 0; a < ingotsNeeded; a++){
            ingotImageGO[a].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GiveBlueprintToHand(){
        if (armourPiece == null || armourPiece == ""){
            return;
        }
        else{
            // Find other game objects with blueprint tag
            GameObject[] blueprintObjectsInScene;
            blueprintObjectsInScene = GameObject.FindGameObjectsWithTag("Blueprint");
            bool canCreate = true;
            if(blueprintObjectsInScene.Length > 0){   
                foreach (GameObject bp in blueprintObjectsInScene){
                    if (bp.GetComponent<Blueprint>().armourPiece == armourPiece){
                        canCreate = false;
                    }
                }
            }

            if (canCreate == true){
                GameObject spawnedBlueprint = Instantiate(blueprintPrefab, transform.position, Quaternion.identity);
                spawnedBlueprint.GetComponent<Blueprint>().armourPiece = armourPiece;
                spawnedBlueprint.GetComponent<Blueprint>().playerCam = playerCam;
                spawnedBlueprint.GetComponent<Blueprint>().EquipBlueprint();
            }
            else{
                //return;

                // Equip the blueprint in the scene.
                foreach (GameObject bp in blueprintObjectsInScene){
                    if (bp.GetComponent<Blueprint>().armourPiece == armourPiece){
                        bp.GetComponent<Blueprint>().EquipBlueprint();
                    }
                }
            }
        }
    }
}
