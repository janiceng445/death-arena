using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverAnimate : MonoBehaviour
{
    public GameObject coffee;
    public GameObject nap;
    public GameObject bed;
    bool shrink = false;

    void OnMouseOver() {
        gameObject.transform.localScale = new Vector3(1,3f,1);
        coffee.GetComponent<Image>().enabled = true;
        nap.GetComponent<Image>().enabled = true;
        bed.GetComponent<Image>().enabled = true;
    }
    void OnMouseExit() {
        gameObject.transform.localScale = new Vector3(1,1f,1);
        coffee.GetComponent<Image>().enabled = false;
        nap.GetComponent<Image>().enabled = false;
        bed.GetComponent<Image>().enabled = false;
    }
}
