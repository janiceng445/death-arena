using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class NavDisable : MonoBehaviour
{

    private GameObject prevBtn;
    private GameObject nextBtn;
    private GameObject container;

    void Start() {
        prevBtn = GameObject.Find("Previous");
        nextBtn = GameObject.Find("Next");
        container = gameObject.transform.GetChild(0).gameObject;
        container.SetActive(true);
    }

    void Update() {
        GetComponent<SortingGroup>().sortingOrder = transform.parent.GetComponent<Canvas>().sortingOrder + 1;
    }

    public void Disable() {
        prevBtn.GetComponent<Button>().interactable = false;
        nextBtn.GetComponent<Button>().interactable = false;
    }

    public void Enable() {
        prevBtn.GetComponent<Button>().interactable = true;
        nextBtn.GetComponent<Button>().interactable = true;
    }

    public void Hide() {
        container.SetActive(false);
    }

    public void Unhide() {
        container.SetActive(true);
    }
}
