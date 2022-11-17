using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scale : MonoBehaviour
{
    public GameObject scalePlate;

    public List<GameObject> onScale;

    public TextMeshProUGUI scaleText;

    public float weightOnScale = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weightOnScale = 0f;
        for (int i = 0; i <= onScale.Count -1; i++){
            if (onScale[i].GetComponent<Ore>() != null){
                weightOnScale = weightOnScale + onScale[i].GetComponent<Ore>().weight;
            }
            else if (onScale[i].GetComponent<Ingot>() != null){
                weightOnScale = weightOnScale + onScale[i].GetComponent<Ingot>().weight;
            }
            else if (onScale[i].GetComponent<ArmourPiece>() != null){
                weightOnScale = weightOnScale + onScale[i].GetComponent<ArmourPiece>().GetPieceWeight();
            }
        }

        scaleText.text = weightOnScale.ToString("F1");
    }
}
