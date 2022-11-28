using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComplaintsBoard : MonoBehaviour
{
    [SerializeField] private int currentNumberOfComplaints;

    [SerializeField] private GameObject[] complaintPaperGO;
    [SerializeField] private GameObject[] arrowGO;

    [SerializeField] private GameObject ShowFloorRef;

    [SerializeField] private TextMeshProUGUI complaintsText;

    // Start is called before the first frame update
    void Start()
    {
        ShowFloorRef = GameObject.Find("ShopFloorController");
    }

    // Update is called once per frame
    void Update()
    {
        currentNumberOfComplaints = ShowFloorRef.GetComponent<ShopFloorController>().GetCurrentNumberOfComplaints();
        complaintsText.text = "Complaints: " + currentNumberOfComplaints.ToString("F0") + "/" + ShowFloorRef.GetComponent<ShopFloorController>().GetMaxComplaints().ToString("F0");

        if (currentNumberOfComplaints == 0){
            foreach (GameObject c in complaintPaperGO){
                c.SetActive(false);
            }

            foreach (GameObject a in arrowGO){
                a.SetActive(false);
            }
        }
        else if (currentNumberOfComplaints == 1){

            foreach (GameObject c in complaintPaperGO){
                c.SetActive(false);
            }

            foreach (GameObject a in arrowGO){
                a.SetActive(false);
            }

            complaintPaperGO[0].SetActive(true);
            arrowGO[0].SetActive(true);
        }
        else if (currentNumberOfComplaints == 2){

            foreach (GameObject c in complaintPaperGO){
                c.SetActive(false);
            }

            foreach (GameObject a in arrowGO){
                a.SetActive(false);
            }

            complaintPaperGO[0].SetActive(true);
            arrowGO[0].SetActive(true);

            complaintPaperGO[1].SetActive(true);
            arrowGO[1].SetActive(true);
        }
        else if (currentNumberOfComplaints == 3){

            foreach (GameObject c in complaintPaperGO){
                c.SetActive(false);
            }

            foreach (GameObject a in arrowGO){
                a.SetActive(false);
            }

            complaintPaperGO[0].SetActive(true);
            arrowGO[0].SetActive(true);

            complaintPaperGO[1].SetActive(true);
            arrowGO[1].SetActive(true);

            complaintPaperGO[2].SetActive(true);
            arrowGO[2].SetActive(true);
        }
        else if (currentNumberOfComplaints == 4){

            foreach (GameObject c in complaintPaperGO){
                c.SetActive(false);
            }

            foreach (GameObject a in arrowGO){
                a.SetActive(false);
            }

            complaintPaperGO[0].SetActive(true);
            arrowGO[0].SetActive(true);

            complaintPaperGO[1].SetActive(true);
            arrowGO[1].SetActive(true);

            complaintPaperGO[2].SetActive(true);
            arrowGO[2].SetActive(true);

            complaintPaperGO[3].SetActive(true);
            arrowGO[3].SetActive(true);
        }
    }
}
