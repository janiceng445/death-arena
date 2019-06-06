using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spellbook : MonoBehaviour
{
    public static int page;
    private int numPages;
    private GameObject prevPage;
    private GameObject currPage;
    private Animator animator;
    private GameObject[] canvases;
    private List<GameObject> sorting;

    void Start() {
        page = 0;
        numPages = 6;
        prevPage = null;
        sorting = new List<GameObject>();
        currPage = GameObject.Find("pg" + page.ToString());
        animator = GameObject.Find("pg" + page.ToString()).GetComponent<Animator>();
        canvases = new GameObject[numPages];
        for (int i = 0; i < numPages; i++) {
            canvases[i] = GameObject.Find("cv" + i);
        }
    }

    public void NextPage() {
        if (page != numPages - 1) {
            animator.Play("PageForward");
            
            if (prevPage != null) {
                Debug.Log(prevPage);
                sorting.Add(prevPage);
                ReSort();
            }
            else {
                currPage.transform.parent.GetComponent<Canvas>().sortingOrder--;
            }
            page++;
            
            prevPage = currPage;
            FindPage();
            
        }
    }

    public void PreviousPage() {
        if (page != 0) {
            prevPage = currPage;
            if (prevPage != null) prevPage.transform.parent.GetComponent<Canvas>().sortingOrder++;
            page--;
            FindPage();
            animator.Play("PageBack");
        }
    }

    void FindPage() {
        Debug.Log(page);
        animator = GameObject.Find("pg" + page.ToString()).GetComponent<Animator>();
        currPage = GameObject.Find("pg" + page.ToString());
    }

    void FixSortingLayers(bool direction) {
        // If true, increment sorting layers
        if (direction) {
            for (int i = 0; i < numPages; i++) {
                canvases[i].GetComponent<Canvas>().sortingOrder++;
                if (canvases[i].GetComponent<Canvas>().sortingOrder > numPages - 1) {
                    canvases[i].GetComponent<Canvas>().sortingOrder = 0;
                }
            }
        }
        // If false, decrement sorting layers
        else {
            for (int i = 0; i < numPages; i++) {
                canvases[i].GetComponent<Canvas>().sortingOrder--;
                if (canvases[i].GetComponent<Canvas>().sortingOrder < 0) {
                    canvases[i].GetComponent<Canvas>().sortingOrder = numPages - 1;
                }
            }
        }
    }

    void ReSort() {
        Debug.Log("resorting");
        Debug.Log(sorting.Count);
        foreach (GameObject g in sorting) {
            g.transform.parent.GetComponent<Canvas>().sortingOrder--;
        }
    }
}
