using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyIndicator : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public float mTlifeTime = 0.6f;
    public float mTminDist = 2f;
    public float mTmaxDist = 3f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        iniPos = transform.position;
        float dist = Random.Range(mTminDist, mTmaxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = mTlifeTime / 2f;

        if (timer > mTlifeTime)
        {
            Destroy(gameObject);
        }
        else if (timer > fraction)
        {
            moneyText.color = Color.Lerp(moneyText.color, Color.clear, (timer - fraction) / (mTlifeTime - fraction));
        }

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / mTlifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / mTlifeTime));
    }

    public void SetMoneyText(float moneyAmount)
    {
        moneyText.text = "+" + moneyAmount.ToString("F0");
    }
}
