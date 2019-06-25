using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public ParticleSystem sandstorm;
    public GameObject circle;

    void Start() {

        if (WorldStats.level == 3) {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            foreach (SpriteRenderer s in sprites) {
                s.color = new Color32(0x0B, 0x09, 0x1D, 0xFF);
            }
            sprites[0].color = new Color32(0x4F, 0x41, 0x78, 0xFF);
            var main = sandstorm.main;
            main.startColor = Color.black;
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void ActivateDarknessBG() {
        sprites[0].color = new Color32(0x14, 0x63, 0x85, 0xFF);
        var main = sandstorm.main;
        main.startColor = new Color(0xff, 0xff, 0xff, 0x10);
    }
}