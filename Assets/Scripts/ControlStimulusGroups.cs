using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Microsoft.Data.Analysis;

/* TODO: Fixation Cross
 * 
 * 
 * 
 * 
		METHODOLOGY:
<DONE>		1) Add all elements from each group into a list
				> all_group1 = list of child GameObject.Transform
			Do for all groups and have this list handy

			2) Start from group1:
				> randomise elements in the group
					- Show stimulus
					- traverse list once across all elements with contrast=contrast_start
					- update responses to child gameobject's public:parameters <FOR ALL GROUPS>
					- randomise elements in list where numberOfreversals<3
					- repeat until all elements in the group have numberOfreversals =3
				> For Group 2:
					- repeat above ^
				
*/

public class ControlStimulusGroups : MonoBehaviour
{

    public double contrast_start = 1.0;
    public double contrast_end = 0.0;
    public List<float> contrast_step = new List<float>(3);
    

    //private static readonly List<double> p = { 0.5, 0.25, 0.125 };
    //public List<double> contrast_step = p;

    public int stimulustime_in_ms = 1000;
    public int responsetime_in_ms = 5000;
    public int between_trials_time_in_ms = 3000;
    public int group = 1;
    public int REVERSALS = 3;

    public GameObject Group1;// = transform.GetChild(0).gameObject;
    public GameObject Group2;// = transform.GetChild(1).gameObject;
    public GameObject Group3;// = transform.GetChild(0).gameObject;
    public GameObject Group4;// = transform.GetChild(1).gameObject;

    // For live analysis
    public int[] done_groups = { };
    public int current_group = 1;

    private int random_index;
    
    private bool showStimulus = false;
    private string user_response = "No";

    private int count = 0;
    public Transform random_location;


    List<Transform> all_group1 = new();
    List<Transform> all_group2 = new();
    List<Transform> all_group3 = new();
    List<Transform> all_group4 = new();
    List<Transform> not_dones_in_group = new();
    Contrast_thresholds parameters, random_location_parameters;

    public float elapsed = 0f;
    public float interval_elapsed = 0f;

    System.DateTime show_time;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Welcome to JHU-VFT! \n Initializing Visual Field Testing");
          



        foreach (Transform child in Group1.transform)
        {
            //Debug.Log("Group1:"+child.name);
            all_group1.Add(child);
        }

        foreach (Transform child in Group2.transform)
        {
            //Debug.Log("Group2:"+child.name);
            all_group2.Add(child);
        }

        foreach (Transform child in Group3.transform)
        {
            //Debug.Log("Group3:"+child.name);
            all_group3.Add(child);
        }

        foreach (Transform child in Group4.transform)
        {

            //Debug.Log("Group4:"+child.name);
            all_group4.Add(child);
        }

        not_dones_in_group = get_not_done_group_elements(current_group);
        show_stimulus(not_dones_in_group);
    }


    int get_groupnumber()
    {
        //traverse through group 1-4 elements and check number of reversals and return the first group


        foreach (Transform child_element in all_group1)
        {
            parameters = child_element.GetComponentInChildren<Contrast_thresholds>();
            if (parameters.reversals < REVERSALS)
                return 1;
        }

        foreach (Transform child_element in all_group2)
        {
            parameters = child_element.GetComponentInChildren<Contrast_thresholds>();
            if (parameters.reversals < REVERSALS)
                return 2;
        }

        foreach (Transform child_element in all_group3)
        {
            parameters = child_element.GetComponentInChildren<Contrast_thresholds>();
            if (parameters.reversals < REVERSALS)
                return 3;
        }

        foreach (Transform child_element in all_group4)
        {
            parameters = child_element.GetComponentInChildren<Contrast_thresholds>();
            if (parameters.reversals < REVERSALS)
                return 4;    
        }




        return -1;
    }

    List<Transform> get_not_done_group_elements(int current_group)
    {
        List<Transform> not_dones_list = new ();
        List<Transform> current_group_list = new();
        if (current_group == 1)
            current_group_list = all_group1;

        if (current_group == 2)
            current_group_list = all_group2;

        if (current_group == 3)
            current_group_list = all_group3;

        if (current_group == 4)
            current_group_list = all_group4;


        foreach (Transform child_element in current_group_list)
        {
            parameters = child_element.GetComponentInChildren<Contrast_thresholds>();
            if (parameters.reversals < 3)
            {
                not_dones_list.Add(child_element);
            }
            
        }


        return not_dones_list;
    }

    // Update is called once per frame
    void Update()
    {
        // check for group with reversal<= 3

        current_group = get_groupnumber();
        if(current_group==-1)
        {
            print("Done with VFT");
            enabled = false; //Stops calling "Update()" as we are done with VFT
        }

        else
        {
            

            elapsed += Time.deltaTime;                                  //Since stimulus is now shown, start incrementing elapsedtime

            if (elapsed >= (stimulustime_in_ms / 1000f + responsetime_in_ms / 1000f))
            {
                interval_elapsed += Time.deltaTime;

                if (interval_elapsed >= (between_trials_time_in_ms) /1000)
                {
                    // add the current contrast level to contrast_threshold_list
                    random_location_parameters.contrast_level.Add(random_location_parameters.current_contrast_level);

                    // add the user response for current element
                    random_location_parameters.responses.Add(user_response); 
                    
                    // based on the user's current response increase or decrease contrast level
                    if(user_response=="Yes")
                    {
                        random_location_parameters.current_contrast_level = random_location_parameters.current_contrast_level - contrast_step[random_location_parameters.reversals];
                        random_location_parameters.current_contrast_level = (float)System.Math.Round(random_location_parameters.current_contrast_level, 5);
                        if (random_location_parameters.current_contrast_level<0.01)
                        {
                            random_location_parameters.current_contrast_level = 0.01f;
                        }
                    }
                    if(user_response=="No")
                    {
                        random_location_parameters.current_contrast_level = random_location_parameters.current_contrast_level + contrast_step[random_location_parameters.reversals];
                        random_location_parameters.current_contrast_level = (float)System.Math.Round(random_location_parameters.current_contrast_level, 5);
                        if (random_location_parameters.current_contrast_level >= 1.0)
                        {
                            random_location_parameters.current_contrast_level = 1.0f;
                        }
                    }


                    not_dones_in_group = get_not_done_group_elements(current_group);
                    show_stimulus(not_dones_in_group);
                    interval_elapsed = 0;

                }
            }


            if ( 0f < elapsed && elapsed < ((stimulustime_in_ms+ responsetime_in_ms) / 1000f))
            {
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Pressed Space Bar");
                    user_response = "Yes";
                }
            }

        }
    	

    }


    void show_stimulus(List<Transform> not_dones_list)
    {
        
        random_index = Random.Range(0, not_dones_list.Count);
        random_location = not_dones_list[random_index];                 // type: Transform
        random_location_parameters = random_location.GetComponentInChildren<Contrast_thresholds>();
        show_time = System.DateTime.Now;
        showStimulus = true;                                        //show object for 200ms
        elapsed = 0f;
        user_response = "No";
    }

    // unity physics engine updates 50times per second (DEFAULT) ~ every 20ms
    // FixedUpdate is called once every physics update
    // we need to show the stimulus every 200ms, so we show the game object every
    private void FixedUpdate()
    {
        if (showStimulus)
        {
            count = count + 1;
            //Debug.Log("Active:" + System.DateTime.Now.ToString("s.ff"));
            random_location.gameObject.SetActive(true);
            

            if (count >= (stimulustime_in_ms / 20))
            {
                random_location.gameObject.SetActive(false);
                showStimulus = false;
                //Debug.Log("Inactive:" + System.DateTime.Now.ToString("s.ff"));
                count = 0;
            }
        }
    }
}
