using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public float countdownInterval;
    public float countdownTimer;
    private TMPro.TextMeshProUGUI cdText;
    private Animator animator;
    public GameObject SpawnBossHolder;
    private int seconds;

    void Start()
    {
        countdownInterval = 2f; // should be 400
        seconds = 3;
        countdownTimer = countdownInterval;
        cdText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        cdText.enabled = false;
        animator = gameObject.GetComponent<Animator>();
    }

    void Update() {
        if (!GameSettings.paused) {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0 && seconds >= 0) {
                animator.Play("countdown", -1, 0f);
                cdText.text = (seconds).ToString();
                if (seconds != 0) cdText.enabled = true;
                seconds--;
                countdownTimer = countdownInterval;
            }
            if (seconds == -1 && !cdText.enabled) {
                SpawnBossHolder.GetComponent<BossManager>().bossReady = true;
            }
        }
    }

    void DisableText() {
        cdText.enabled = false;
    }
}
