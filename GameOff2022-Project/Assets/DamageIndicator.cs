using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    public float lifeTime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    public Color critColour;
    public bool crit = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (crit == true){
            damageText.color = Color.yellow;
        }

        float fraction = lifeTime / 2f;

        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        else if (timer > fraction)
        {
            damageText.color = Color.Lerp(damageText.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
        };

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));
    }

    public void SetDamageText(float damage)
    {
        damageText.text = damage.ToString("F0");
    }
}
