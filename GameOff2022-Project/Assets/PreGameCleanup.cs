using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGameCleanup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameOver"){
            EndCleanup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginCleanup(){
        // Destroy any left over objects.

        List<GameObject> listToRemove = new List<GameObject>();

        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("Armour"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("ShopFloorController"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("Customer"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("CustomerSlot"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("CustomerSpawnLocation"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("ShopFloor"));

        for (int i = 0; i < listToRemove.Count; i++){
            Destroy(listToRemove[i].gameObject);
            listToRemove.Remove(listToRemove[i]);
        }
    }

    public void EndCleanup(){
        // Destroy any left over objects.

        List<GameObject> listToRemove = new List<GameObject>();

        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("Armour"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("ShopFloorController"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("Customer"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("CustomerSlot"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("CustomerSpawnLocation"));
        listToRemove.AddRange(GameObject.FindGameObjectsWithTag("ShopFloor"));

        for (int i = 0; i < listToRemove.Count; i++){
            Destroy(listToRemove[i].gameObject);
            listToRemove.Remove(listToRemove[i]);
        }
    }
}
