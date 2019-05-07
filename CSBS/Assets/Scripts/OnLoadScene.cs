using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadScene : MonoBehaviour
{
    public GameObject clock;

    void Awake() {
        DontDestroyOnLoad(clock);
    }
}
