using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; set; }

    public float playerGold;
    public int playerLevel;
    public float playerCurrentExperience;
    public float playerTargetExperience;

    public PlayerDataStoreDB PlayerDataStoreDB;

    public TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + playerGold.ToString("F0");
    }

    void LoadPlayerData(){
        if (!File.Exists(Application.persistentDataPath + "playerdata.xml")){
            return;
        }
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerDataStoreDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "playerdata.xml", FileMode.Open);
        PlayerDataStoreDB = xmlSerializer.Deserialize(stream) as PlayerDataStoreDB;
        stream.Close();

        foreach(PlayerDataStore pData in PlayerDataStoreDB.dataItems){
            playerGold = pData.pGold;
            playerLevel = pData.pLevel;
            playerCurrentExperience = pData.pCurrentExperience;
        }

        PlayerDataStoreDB.dataItems.Clear();
    }

    public void SavePlayerData(){
        PlayerDataStoreDB.dataItems.Clear();

        PlayerDataStore dataItem = new PlayerDataStore();
        dataItem.pGold = playerGold;
        dataItem.pLevel = playerLevel;
        dataItem.pCurrentExperience = playerCurrentExperience;
        PlayerDataStoreDB.dataItems.Add(dataItem);

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerDataStoreDB));
        string path = Application.persistentDataPath + "playerdata.xml";
        FileStream stream = new FileStream(path, FileMode.Create);
        xmlSerializer.Serialize(stream, PlayerDataStoreDB);
        stream.Close();
    }
}

[System.Serializable]
public class PlayerDataStoreDB{
    public List<PlayerDataStore> dataItems = new List<PlayerDataStore>();
}

[System.Serializable]
public class PlayerDataStore{
    public float pGold;
    public int pLevel;
    public float pCurrentExperience;
}