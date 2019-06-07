using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enhancement : MonoBehaviour
{
    private ParticleSystem particle;
    private bool isSelected;
    private bool isClicked;
    private Animator animator;
    private int requiredCost;
    private Spellbook spellbook;
    private AudioSource audiosource;

    void Start() {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
        isSelected = false;
        animator = GetComponent<Animator>();
        requiredCost = 1;
        spellbook = GameObject.Find("Right").GetComponent<Spellbook>();
        audiosource = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }

    void Update() {
        if (isSelected && !particle.isEmitting) {
            particle.Play();
        }
        if (!isSelected) {
            isClicked = false;
        }
        animator.SetBool("isHovering", isSelected);
        animator.SetBool("isClicked", isClicked);
    }

    void OnMouseOver() {
        isSelected = true;
        if (Input.GetMouseButtonDown(0) && !isClicked) {
            AudioClip sound = null;
            GameObject msgUpdate = Instantiate(Resources.Load<GameObject>("Prefabs/SP_MSG"), Vector3.zero, Quaternion.identity);
            if (WorldStats.knowledge_crystals >= requiredCost) {
                sound = (AudioClip) Resources.Load("SFX/Magic_Success", typeof(AudioClip));
                WorldStats.knowledge_crystals -= 1;
                spellbook.UpdateCrystals();
                if (this.name == "atk_cir") {
                    PlayerStats.atk += 5;
                    msgUpdate.GetComponentInChildren<Text>().text = "+5 Atk";
                }
                else if (this.name == "def_cir") {
                    PlayerStats.def += 5;
                    msgUpdate.GetComponentInChildren<Text>().text = "+5 Def";
                }
                else if (this.name == "hp_cir") {
                    PlayerStats.hp += 5;
                    msgUpdate.GetComponentInChildren<Text>().text = "+5 HP";
                }
                isClicked = true;
                SaveSystem.SaveWorldData();
            }
            else if (WorldStats.knowledge_crystals < requiredCost) {
                msgUpdate.GetComponentInChildren<Text>().text = "Insufficient crystals";
                sound = (AudioClip) Resources.Load("SFX/Insufficient_Crystals", typeof(AudioClip));
            }
            audiosource.PlayOneShot(sound);
            Destroy(msgUpdate, 1f);
        }
    }

    public void DisableClicked() {
        isClicked = false;
    }

    void OnMouseExit() {
        isSelected = false;
        particle.Stop();
    }
}
