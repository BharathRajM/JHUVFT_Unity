using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBColor : MonoBehaviour {

    Color32 color;
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        objects[0].GetComponent<Renderer>().material.color = new Color32(100, 80, 30, 255);
        objects[1].GetComponent<Renderer>().material.color = new Color32(70, 90, 50, 255);
        objects[2].GetComponent<Renderer>().material.color = new Color32(120, 100, 90, 255);
        objects[3].GetComponent<Renderer>().material.color = new Color32(150, 120, 110, 255);
        objects[4].GetComponent<Renderer>().material.color = new Color32(120, 150, 100, 255);
        objects[5].GetComponent<Renderer>().material.color = new Color32(140, 150, 160, 255);
        objects[6].GetComponent<Renderer>().material.color = new Color32(160, 180, 140, 255);
        objects[7].GetComponent<Renderer>().material.color = new Color32(175, 95, 85, 255);
        objects[8].GetComponent<Renderer>().material.color = new Color32(200, 180, 170, 255);
        objects[9].GetComponent<Renderer>().material.color = new Color32(175, 180, 130, 255);
        objects[10].GetComponent<Renderer>().material.color = new Color32(130, 170, 130, 255);
        objects[11].GetComponent<Renderer>().material.color = new Color32(155, 180, 75, 255);
        objects[12].GetComponent<Renderer>().material.color = new Color32(145, 160, 120, 255);
        objects[13].GetComponent<Renderer>().material.color = new Color32(178, 120, 130, 255);
        objects[14].GetComponent<Renderer>().material.color = new Color32(125, 170, 145, 255);
        objects[15].GetComponent<Renderer>().material.color = new Color32(160, 180, 140, 255);
        objects[16].GetComponent<Renderer>().material.color = new Color32(180, 180, 180, 255);
    }
}
