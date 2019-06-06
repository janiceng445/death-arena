using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public static int page;
    private int numPages;
    private GameObject currPage;
    private Animator animator;

    void Start() {
        page = 0;
        numPages = 6;
        animator = GameObject.Find("Page_0" + page.ToString()).GetComponent<Animator>();
        for (int i = 1; i < numPages; i++) {
            //GameObject.Find("Page_0" + i.ToString()).GetComponent<Animator>().enabled = false;
        }
    }

    public void NextPage() {
        if (page != numPages - 1) {
            animator.Play("PageForward");
            page++;
            //animator.enabled = false;
            FindPage();
        }
    }

    public void PreviousPage() {
        if (page != 0) {
            page--;
            FindPage();
            animator.Play("PageBack");
        }
    }

    void FindPage() {
        Debug.Log(page);
        animator = GameObject.Find("Page_0" + page.ToString()).GetComponent<Animator>();
        //animator.enabled = true;
        currPage = GameObject.Find("Page_0" + page.ToString());
    }
}
