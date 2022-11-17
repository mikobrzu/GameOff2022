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

    // Start is called before the first frame update
    void Start()
    {
        CalculatePiecePrice();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
