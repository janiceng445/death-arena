using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Spellbook : MonoBehaviour
{
    public static int page;
    private int numPages;
    private GameObject currPage;
    private Animator animator;
    private AudioSource audiosource;
    private Text crystals_count;
    int counter;
    public GameObject[] LeftPages;

    // Save data
    public static int sorcerySetID;
    public static bool[] spellsUnlocked;

    // Sorcery
    public GameObject[] spellTransLocation;

    void Start() {
        page = 0;
        numPages = 6;
        currPage = GameObject.Find("pg" + page.ToString());
        counter = numPages;
        audiosource = GameObject.Find("Right").GetComponent<AudioSource>();
        animator = GameObject.Find("pg" + page.ToString()).GetComponent<Animator>();
        crystals_count = GameObject.Find("Knowledge_Crystals").GetComponent<Text>();
        PageSetActive();
        LeftPages[0].SetActive(true);
        UpdateCrystals();


        // Loading saved data
        // Load actual spellbook data file
        SpellbookData sd = SaveSystem.LoadSpellbookData();
        if (sd != null) {
            sorcerySetID = sd.sorceryID;
            spellsUnlocked = sd.spellsUnlocked;
            foreach(GameObject g in spellTransLocation) {
                g.GetComponent<SorcerySpells>().CheckCondition();
            }
        }
        else {
            // Save new spellbook data file
            sorcerySetID = 0;
            spellsUnlocked = new bool[7];
            SaveSystem.SaveNewSpellbookData();
        }
    }

    public void Update() {
        // if (page == numPages - 1) {
        //     GameObject.Find("Next").GetComponent<Button>().interactable = false;
        // }
        // if (page == 0) {
        //     GameObject.Find("Previous").GetComponent<Button>().interactable = false;
        // }
        
        // Loading the currently selected spell in <Sorcery>
        if (page == 1 && sorcerySetID != 0) {
            GameObject selector = GameObject.Find("Sorce_Selector");
            selector.GetComponent<SpriteRenderer>().enabled = true;
            selector.GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0x00, 0xDD, 0xFF);
            selector.transform.position = spellTransLocation[sorcerySetID - 1].transform.position;
        }
    }

    public void NextPage() {
        if (page != 1) {
            page++;
            currPage.transform.parent.GetComponent<Canvas>().sortingOrder = counter;
            counter++;
            animator.Play("PageForward");
            FindPage();
            AudioClip clip = (AudioClip) Resources.Load("SFX/pageflip_medium", typeof(AudioClip));
            audiosource.PlayOneShot(clip);
        }
    }

    public void PreviousPage() {
        if (page != 0) {
            page--;
            FindPage();
            currPage.transform.parent.GetComponent<Canvas>().sortingOrder = counter;
            counter++;
            animator.Play("PageBack");
            AudioClip clip = (AudioClip) Resources.Load("SFX/pageflip_light", typeof(AudioClip));
            audiosource.PlayOneShot(clip);
        }
    }

    void FindPage() {
        animator = GameObject.Find("pg" + page.ToString()).GetComponent<Animator>();
        currPage = GameObject.Find("pg" + page.ToString());
    }

    void PageLeftView(int layerOrder) {
        if (page != 0) {
            LeftPages[page].GetComponent<Canvas>().sortingOrder = layerOrder;
        }
    }

    public void PageSetActive() {
        foreach (GameObject g in LeftPages) {
            g.SetActive(false);
        }
        LeftPages[page].SetActive(true);
        PageLeftView(counter);
    }

    public void ReturnTitle() {
        SaveSystem.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateCrystals() {
        crystals_count.text = WorldStats.knowledge_crystals.ToString();
    }
}
