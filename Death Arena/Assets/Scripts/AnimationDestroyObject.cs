using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroyObject : MonoBehaviour
{
    public void DestroyMe() {
        Destroy(gameObject);
    }
}
