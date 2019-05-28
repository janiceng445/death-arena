using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Fix sorting order
        gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
