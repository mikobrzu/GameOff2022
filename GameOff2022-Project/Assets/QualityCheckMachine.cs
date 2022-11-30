using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QualityCheckMachine : MonoBehaviour
{
    [SerializeField] private bool playerInZone;

    [SerializeField] private List<ArmourPiece> armourOnChecker = new List<ArmourPiece>();
    //[SerializeField] private List<Ingot> ingotOnChecker = new List<Ingot>();

    [SerializeField] private TextMeshProUGUI qualityOutputText;

    [SerializeField] private SoundManager SMRef;
    [SerializeField] private AudioClip errorAC;
    [SerializeField] private AudioClip successAC;

    // Start is called before the first frame update
    void Start()
    {
        SMRef = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverPulled(){
        Debug.Log("Quality check machine: lever was pulled.");
        Debug.Log("Armour on checker: " + armourOnChecker.Count);
    }

    public void SetPlayerInZone(bool pIZ){
        playerInZone = pIZ;
    }

    public bool GetPlayerInZone(){
        return playerInZone;
    }

    public void AddToList(ArmourPiece ap){
        armourOnChecker.Add(ap);
    }

    public void RemoveFromList(ArmourPiece ap){
        armourOnChecker.Remove(ap);
    }

    public void DisplayOutputQualityText(string textToOutput){
        qualityOutputText.text = textToOutput;
    }

    public int GetNumberOfPiecesInCheckZone(){
        return armourOnChecker.Count;
    }

    public void OutputQuality(){
        if (armourOnChecker.Count > 1){
            DisplayOutputQualityText("Error");
            SMRef.PlaySound(errorAC);
        }
        else if (armourOnChecker.Count == 1){
            DisplayOutputQualityText(armourOnChecker[0].GetComponent<ArmourPiece>().GetPieceQuality().ToString("F0"));
            SMRef.PlaySound(successAC);
        }
        else{
            DisplayOutputQualityText("...");
            SMRef.PlaySound(errorAC);
        }
    }
}
