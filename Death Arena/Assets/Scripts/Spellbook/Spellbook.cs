using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spellbook : MonoBehaviour
{
    public static int page;
    private int numPages;
    private GameObject currPage;
    private Animator animator;
    private AudioSource audiosource;
    int counter;

    void Start() {
        page = 0;
        numPages = 6;
        currPage = GameObject.Find("pg" + page.ToString());
        counter = numPages;
        audiosource = GameObject.Find("Right").GetComponent<AudioSource>();
        animator = GameObject.Find("pg" + page.ToString()).GetComponent<Animator>();
    }

    public void Update() {
        if (page == numPages - 1) {
            GameObject.Find("Next").GetComponent<Button>().interactable = false;
        }
        if (page == 0) {
            GameObject.Find("Previous").GetComponent<Button>().interactable = false;
        }
    }

    public void NextPage() {
        if (page != numPages - 1) {
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

    public void ReturnTitle() {
        SceneManager.LoadScene("MainMenu");
    }
}
