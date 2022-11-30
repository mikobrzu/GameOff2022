using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SellZone : MonoBehaviour
{
    public GameObject player;
    public PlayerData pData;

    [SerializeField] private float quickSellMultiplayer;

    [SerializeField] private UnityEvent PayCustomerEvent;

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
                pData.playerGold = pData.playerGold + (other.gameObject.GetComponent<Ore>().price * quickSellMultiplayer);
                Destroy(other.gameObject);
                PayCustomerEvent.Invoke();
            }
            else if (other.gameObject.GetComponent<Ingot>() != null){
                pData.playerGold = pData.playerGold + (other.gameObject.GetComponent<Ingot>().price * quickSellMultiplayer);
                Destroy(other.gameObject);
                PayCustomerEvent.Invoke();
            }
            else {
                Debug.Log("Sell error.");
            }
        }
        else if (other.tag == "Armour"){
            if (other.gameObject.GetComponent<ArmourPiece>() != null){
                pData.playerGold = pData.playerGold + (other.gameObject.GetComponent<ArmourPiece>().GetPiecePrice() * quickSellMultiplayer);
                Destroy(other.gameObject);
                PayCustomerEvent.Invoke();
            }
            else {
                Debug.Log("Sell error.");
            }
        }
    }
}
