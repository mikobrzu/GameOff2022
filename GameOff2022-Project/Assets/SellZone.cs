using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellZone : MonoBehaviour
{
    public GameObject player;
    public PlayerData pData;

    // Start is called before the first frame update
    void Start()
    {
        pData = player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Pickup"){
            if (other.gameObject.GetComponent<Ore>() != null){
                pData.playerGold = pData.playerGold + other.gameObject.GetComponent<Ore>().price;
                Destroy(other.gameObject);
            }
            else if (other.gameObject.GetComponent<Ingot>() != null){
                pData.playerGold = pData.playerGold + other.gameObject.GetComponent<Ingot>().price;
                Destroy(other.gameObject);
            }
            else {
                Debug.Log("Sell error.");
            }
        }
    }
}
