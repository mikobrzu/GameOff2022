using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmourPiece : MonoBehaviour
{
    [SerializeField] private string aPiece;
    [SerializeField] private string aType;
    [SerializeField] private bool lightVarient;

    [SerializeField] private float pieceWeight;
    [SerializeField] private float pieceQuality;
    [SerializeField] private float piecePrice;

    [SerializeField] private bool beingHeld;

    [SerializeField] private Material defaultMat;
    [SerializeField] private MeshRenderer[] allMR;
    [SerializeField] private Material transparentMat;

    [SerializeField] private MeshRenderer mR;

    [SerializeField] private PlayerData PDRef;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        CalculatePiecePrice();

        if (SceneManager.GetActiveScene().name != "GameOver" || SceneManager.GetActiveScene().name != "Start"){
            PDRef = GameObject.Find("Player").GetComponent<PlayerData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PDRef == null && SceneManager.GetActiveScene().name != "GameOver" || SceneManager.GetActiveScene().name != "Start"){
            PDRef = GameObject.Find("Player").GetComponent<PlayerData>();
        }

        if (beingHeld == true){
            foreach (MeshRenderer meshr in allMR){
                meshr.material = transparentMat;
            }
            //mR.material = transparentMat;
        }
        else{
            foreach (MeshRenderer meshr in allMR){
                meshr.material = defaultMat;
            }
            //mR.material = defaultMat;
        }
    }

    public void SetWeight(float materialsWeight){
        pieceWeight = materialsWeight * 1.1f;

        if (PDRef != null){
            if (pieceWeight > PDRef.heaviestArmourPieceMade){
                PDRef.heaviestArmourPieceMade = pieceWeight;
            }
        }
        else{
            if (SceneManager.GetActiveScene().name != "GameOver" || SceneManager.GetActiveScene().name != "Start"){
                PDRef = GameObject.Find("Player").GetComponent<PlayerData>();
            }

            if (pieceWeight > PDRef.heaviestArmourPieceMade){
                PDRef.heaviestArmourPieceMade = pieceWeight;
            }
        }
        
    }

    public void SetQuality(float materialsQuality){
        pieceQuality = materialsQuality;
    }

    private void CalculatePiecePrice(){
        piecePrice = pieceWeight * (((pieceQuality * 10) / 100) + 1.0f);
    }

    public float GetPieceWeight(){
        return pieceWeight;
    }

    public float GetPiecePrice(){
        return piecePrice;
    }

    public string GetArmourPieceString(){
        return aPiece;
    }

    public string GetArmourTypeString(){
        return aType;
    }

    public float GetPieceQuality(){
        return pieceQuality;
    }

    public void SetBeingHeld(bool h){
        beingHeld = h;
    }

    public bool GetLightVarientStatus(){
        return lightVarient;
    }
}
