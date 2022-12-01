using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GameOverStats : MonoBehaviour
{
    private float endPlayerGold;
    private int endTotalCustomersServed;
    private float endMostGoldReceived;
    private int endArmourPiecesMade;
    private float endHeaviestArmourPieceMade;
    private int endTotalOreSmelted;

    private float endTotalPlayTime;

    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private TextMeshProUGUI totalGoldText;
    [SerializeField] private TextMeshProUGUI customersServedText;
    [SerializeField] private TextMeshProUGUI mostGoldReceivedText;
    [SerializeField] private TextMeshProUGUI armourPiecesMadeText;
    [SerializeField] private TextMeshProUGUI heaviestPieceMadeText;
    [SerializeField] private TextMeshProUGUI oreSmeltedText;

    private PlayerDataStoreDB PlayerDataStoreDB;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        int hours = (int)(endTotalPlayTime / 3600) % 24;
        int minutes = (int)(endTotalPlayTime / 60) % 60;
        float seconds = (endTotalPlayTime % 60);

        totalTimeText.text = "Total Time: " +  string.Format("{0:00}:{1:00}:{2:00}",hours,minutes,seconds);

        totalGoldText.text = "Total Gold: " + endPlayerGold.ToString("F0");
        customersServedText.text = "Customers Served: " + endTotalCustomersServed.ToString("F0");
        mostGoldReceivedText.text = "Most Gold Received: " + endMostGoldReceived.ToString("F0");
        armourPiecesMadeText.text = "Armour Pieces Made: " + endArmourPiecesMade.ToString("F0");
        heaviestPieceMadeText.text = "Heaviest Armour: " + endHeaviestArmourPieceMade.ToString("F0");
        oreSmeltedText.text = "Ore Smelted: " + endTotalOreSmelted.ToString("F0");
    }

    private void LoadData(){
        if (!File.Exists(Application.persistentDataPath + "playerdata.xml")){
            return;
        }
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerDataStoreDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "playerdata.xml", FileMode.Open);
        PlayerDataStoreDB = xmlSerializer.Deserialize(stream) as PlayerDataStoreDB;
        stream.Close();

        foreach(PlayerDataStore pData in PlayerDataStoreDB.dataItems){
            endPlayerGold = pData.pGold;
            endTotalPlayTime = pData.pTotalTime;
            endTotalCustomersServed = pData.pCustomersServed;
            endMostGoldReceived = pData.pMostGoldReceived;
            endArmourPiecesMade = pData.pArmourPiecesMade;
            endHeaviestArmourPieceMade = pData.pHeaviestArmourPieceMade;
            endTotalOreSmelted = pData.pOreSmelted;
        }

        PlayerDataStoreDB.dataItems.Clear();
    }
}
