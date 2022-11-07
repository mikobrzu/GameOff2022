using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnOre : MonoBehaviour
{
    public GameObject OrePrefab;
    public Transform spawnLocation;

    public void SpawnRandomOre(){
        GameObject Ore = Instantiate(OrePrefab, spawnLocation.position, Quaternion.identity);
        int randomInt = Random.Range(0,2);
        if (randomInt == 1){
            Ore.GetComponent<Ore>().oreType = "Iron";
        }
        else{
            Ore.GetComponent<Ore>().oreType = "Copper";
        }
    }
}
