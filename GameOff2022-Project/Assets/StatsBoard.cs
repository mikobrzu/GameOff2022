using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsBoard : MonoBehaviour
{
    [SerializeField] private PlayerData PDRef;

    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private TextMeshProUGUI totalGoldText;
    [SerializeField] private TextMeshProUGUI customersServedText;
    [SerializeField] private TextMeshProUGUI mostGoldReceivedText;
    [SerializeField] private TextMeshProUGUI armourPiecesMadeText;
    [SerializeField] private TextMeshProUGUI heaviestPieceMadeText;
    [SerializeField] private TextMeshProUGUI oreSmeltedText;

    // Start is called before the first frame update
    void Start()
    {
        PDRef = GameObject.Find("Player").GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        int hours = (int)(PDRef.totalPlayTime / 3600) % 24;
        int minutes = (int)(PDRef.totalPlayTime / 60) % 60;
        float seconds = (PDRef.totalPlayTime % 60);

        //totalTimeText.text = "Total Time: " +  hours + ":" + minutes + ":" + seconds.ToString("F0");
        totalTimeText.text = "Total Time: " +  string.Format("{0:00}:{1:00}:{2:00}",hours,minutes,seconds);

        totalGoldText.text = "Total Gold: " + PDRef.playerGold.ToString("F0");
        customersServedText.text = "Customers Served: " + PDRef.totalCustomersServed.ToString("F0");
        mostGoldReceivedText.text = "Most Gold Received: " + PDRef.mostGoldReceived.ToString("F0");
        armourPiecesMadeText.text = "Armour Pieces Made: " + PDRef.armourPiecesMade.ToString("F0");
        heaviestPieceMadeText.text = "Heaviest Armour: " + PDRef.heaviestArmourPieceMade.ToString("F0");
        oreSmeltedText.text = "Ore Smelted: " + PDRef.totalOreSmelted.ToString("F0");
    }
}
