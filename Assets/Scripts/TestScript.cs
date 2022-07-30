using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import System;

public class TestScript : MonoBehaviour
{
    public bool GoToNextLocation = false;
    public GameObject Group1;
    public GameObject Group2;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {

        GameObject Group1 = transform.GetChild(0).gameObject;
        GameObject Group2 = transform.GetChild(1).gameObject;

        
        Debug.Log("Everything is set inactive now");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)==true)
            {
                Debug.Log("Active:"+System.DateTime.Now.ToString("s.ff"));
                GoToNextLocation=true;
                Debug.Log("Pressed Space Bar");
                count = 0;
            }
    }


    // unity physics engine updates 50times per second (DEFAULT) ~ every 20ms
    // FixedUpdate is called once every physics update
    // we need to show the stimulus every 200ms, so we show the game object every
    private void FixedUpdate()
    {   
    	if(GoToNextLocation)
        {   
            count = count + 1;
            Group1.SetActive(true);
            

            if(count>=10)
            {
                Group1.SetActive(false);
                GoToNextLocation = false;
                Debug.Log("Inactive:"+System.DateTime.Now.ToString("s.ff"));
            }
        }
    }
}