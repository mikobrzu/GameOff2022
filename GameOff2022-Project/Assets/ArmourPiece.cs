using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        CalculatePiecePrice();
    }

    // Update is called once per frame
    void Update()
    {
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
}
