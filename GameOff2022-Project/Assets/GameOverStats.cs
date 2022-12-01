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

    private bool newTotalTimeHighscore = false;
    private bool newTotalGoldHighscore = false;
    private bool newCustomersServedHighscore = false;
    private bool newMostGoldReceivedHighscore = false;
    private bool newArmourPiecesMadeHighscore = false;
    private bool newHeaviestArmourPieceMadeHighscore = false;
    private bool newOreSmeltedHighscore = false;

    private PlayerDataStoreDB PlayerDataStoreDB;

    [SerializeField] private SoundManager SMRef;
    [SerializeField] private AudioClip GameOverAC;

    // Start is called before the first frame update
    void Start()
    {
        SMRef = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        SMRef.PlaySound(GameOverAC);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        LoadData();
        CheckHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        int hours = (int)(endTotalPlayTime / 3600) % 24;
        int minutes = (int)(endTotalPlayTime / 60) % 60;
        float seconds = (endTotalPlayTime % 60);

        int besthours = (int)(PlayerPrefs.GetFloat("bestTime") / 3600) % 24;
        int bestminutes = (int)(PlayerPrefs.GetFloat("bestTime") / 60) % 60;
        float bestseconds = (PlayerPrefs.GetFloat("bestTime") % 60);

        if (newTotalTimeHighscore == false){
            totalTimeText.text = "Total Time: " +  string.Format("{0:00}:{1:00}:{2:00}",hours,minutes,seconds) + "<color=yellow>    [ Best: " + string.Format("{0:00}:{1:00}:{2:00}",besthours, bestminutes, bestseconds)+" ]</color>";
        }
        else{
            totalTimeText.text = "Total Time: " +  string.Format("{0:00}:{1:00}:{2:00}",hours,minutes,seconds) + "<color=yellow>    [ NEW Best! ]</color>";
        }

        if (newTotalGoldHighscore == false){
            totalGoldText.text = "Total Gold: " + endPlayerGold.ToString("F0") + "<color=yellow>    [ Best: " + PlayerPrefs.GetFloat("bestMostGold").ToString("F0") + " ]</color>";
        }
        else{
            totalGoldText.text = "Total Gold: " + endPlayerGold.ToString("F0") + "<color=yellow>    [ NEW Best! ]</color>";
        }
        
        if (newCustomersServedHighscore == false){
            customersServedText.text = "Customers Served: " + endTotalCustomersServed.ToString("F0") + "<color=yellow>    [ Best: " + PlayerPrefs.GetInt("bestMostCustomersServed").ToString("F0") + " ]</color>";
        }
        else{
            customersServedText.text = "Customers Served: " + endTotalCustomersServed.ToString("F0") + "<color=yellow>    [ NEW Best! ]</color>";
        }
        
        if (newMostGoldReceivedHighscore == false){
            mostGoldReceivedText.text = "Most Gold Received: " + endMostGoldReceived.ToString("F0") + "<color=yellow>    [ Best: " + PlayerPrefs.GetFloat("bestMostGoldReceived").ToString("F0") + " ]</color>";
        }
        else{
            mostGoldReceivedText.text = "Most Gold Received: " + endMostGoldReceived.ToString("F0") + "<color=yellow>    [ NEW Best! ]</color>";
        }
        
        if (newArmourPiecesMadeHighscore == false){
            armourPiecesMadeText.text = "Armour Pieces Made: " + endArmourPiecesMade.ToString("F0") + "<color=yellow>    [ Best: " + PlayerPrefs.GetInt("bestAPMade").ToString("F0") + " ]</color>";
        }
        else{
            armourPiecesMadeText.text = "Armour Pieces Made: " + endArmourPiecesMade.ToString("F0") + "<color=yellow>    [ NEW Best! ]</color>";
        }
        
        if (newHeaviestArmourPieceMadeHighscore == false){
            heaviestPieceMadeText.text = "Heaviest Armour: " + endHeaviestArmourPieceMade.ToString("F0") + "<color=yellow>    [ Best: " + PlayerPrefs.GetFloat("bestHeaviestAMade").ToString("F0") + " ]</color>";
        }
        else{
            heaviestPieceMadeText.text = "Heaviest Armour: " + endHeaviestArmourPieceMade.ToString("F0") + "<color=yellow>    [ NEW Best! ]</color>";
        }
        
        if (newOreSmeltedHighscore == false){
            oreSmeltedText.text = "Ore Smelted: " + endTotalOreSmelted.ToString("F0") + "<color=yellow>    [ Best: " + PlayerPrefs.GetInt("bestOreSmelted").ToString("F0") + " ]</color>";
        }
        else{
            oreSmeltedText.text = "Ore Smelted: " + endTotalOreSmelted.ToString("F0") + "<color=yellow>    [ NEW Best! ]</color>";
        }
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

    private void CheckHighScores(){
        if (endTotalPlayTime > PlayerPrefs.GetFloat("bestTime")){
            PlayerPrefs.SetFloat("bestTime", endTotalPlayTime);
            newTotalTimeHighscore = true;
        }
        else{
            newTotalTimeHighscore = false;
        }

        if (endPlayerGold > PlayerPrefs.GetFloat("bestMostGold")){
            PlayerPrefs.SetFloat("bestMostGold", endPlayerGold);
            newTotalGoldHighscore = true;
        }
        else{
            newTotalGoldHighscore = false;
        }

        if (endTotalCustomersServed > PlayerPrefs.GetInt("bestMostCustomersServed")){
            PlayerPrefs.SetInt("bestMostCustomersServed", endTotalCustomersServed);
            newCustomersServedHighscore = true;
        }
        else{
            newCustomersServedHighscore = false;
        }

        if (endMostGoldReceived > PlayerPrefs.GetFloat("bestMostGoldReceived")){
            PlayerPrefs.SetFloat("bestMostGoldReceived", endMostGoldReceived);
            newMostGoldReceivedHighscore = true;
        }
        else{
            newMostGoldReceivedHighscore = false;
        }

        if (endArmourPiecesMade > PlayerPrefs.GetInt("bestAPMade")){
            PlayerPrefs.SetInt("bestAPMade", endArmourPiecesMade);
            newArmourPiecesMadeHighscore = true;
        }
        else{
            newArmourPiecesMadeHighscore = false;
        }

        if (endHeaviestArmourPieceMade > PlayerPrefs.GetFloat("bestHeaviestAMade")){
            PlayerPrefs.SetFloat("bestHeaviestAMade", endHeaviestArmourPieceMade);
            newHeaviestArmourPieceMadeHighscore = true;
        }
        else{
            newHeaviestArmourPieceMadeHighscore = false;
        }

        if (endTotalOreSmelted > PlayerPrefs.GetInt("bestOreSmelted")){
            PlayerPrefs.SetInt("bestOreSmelted", endTotalOreSmelted);
            newOreSmeltedHighscore = true;
        }
        else{
            newOreSmeltedHighscore = false;
        }
    }
}
