using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sorcery : MonoBehaviour
{
    private Vector3 offset;

    void Start() {
        
    }

    void OnMouseDown()
    {
        //offset = gameObject.transform.GetChild(0).transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {
        //Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        //transform.GetChild(0).transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }
    
    void Update() {
        
    }
}
