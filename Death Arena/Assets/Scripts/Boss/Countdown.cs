using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public int countdown;
    public int countdownTimer;
    private TMPro.TextMeshProUGUI cdText;
    private Animator animator;
    public GameObject SpawnBossHolder;

    void Start()
    {
        countdown = 200; // should be 400
        countdownTimer = countdown;
        cdText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update() {
        if (!GameSettings.paused) {
            countdownTimer--;
            if (countdownTimer != 0 && countdownTimer % 100 == 0) {
                animator.Play("countdown", -1, 0f);
                cdText.text = (countdownTimer / 100).ToString();
                cdText.enabled = true;
            }
            if (countdownTimer <= 0) {
                cdText.enabled = false;
                SpawnBossHolder.GetComponent<BossManager>().bossReady = true;
            }
        }
    }
}
