using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMinigame : MonoBehaviour
{
    public Animator pickaxeAnimator;
    public Animation pickaxeAnim;

    public Animator mineMinigameAnimator;
    
    public GameObject miniGameCanvas;

    private bool strikeTime = false;
    private bool bestTime = false;

    private bool playingMinigame = false;
    private bool hitOnceBeforeRest = false;

    // Start is called before the first frame update
    void Start()
    {
        miniGameCanvas.SetActive(false);

        mineMinigameAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playingMinigame == true){
            if (Input.GetMouseButtonDown(0)){
                if (strikeTime == true && hitOnceBeforeRest == false){
                    Debug.Log("You hit it!");
                    if (bestTime == true && hitOnceBeforeRest == false){
                        Debug.Log("You hit it very well!!");
                    }
                }
            }
        }
    }

    public void StrikeTimeEnable(){
        strikeTime = true;
    }

    public void StrikeTimeDisable(){
        strikeTime = false;
    }

    public void BestTimeEnable(){
        bestTime = true;
    }

    public void BestTimeDisable(){
        bestTime = false;
    }

    public void StartMining(int timesToHit){
        StartCoroutine(MiningMinigame(timesToHit));
    }

    IEnumerator MiningMinigame(int timesToHitRock){
        miniGameCanvas.SetActive(true);
        playingMinigame = true;

        while (timesToHitRock > 0){

            pickaxeAnimator.SetTrigger("Mine");
            mineMinigameAnimator.SetTrigger("MineMiniGameStart");
            yield return new WaitForSeconds(1f);

            timesToHitRock = timesToHitRock - 1;
        }

        miniGameCanvas.SetActive(false);
        playingMinigame = false;
    }
}
