using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contrast_thresholds : MonoBehaviour
{
	public float current_contrast_level = 1.0f;
	public int reversals = 0;
	public float true_weibull_slope = 2.5f;
	public float true_weibull_threshold = 0.3f;

	public List<float> neighbourcontrastslist = new List<float>();

	public List<float> contrast_level =new List<float>();	
	public List<string> responses = new();
	
	public List<GameObject> neighbouring_prev_group_members;
	public List<Transform> all_members = new();

	float exp_power;
	float result;

	Contrast_thresholds parameters;

	private void Start()
    {
		foreach(GameObject go in neighbouring_prev_group_members)
		{
			Transform child = go.transform;
			all_members.Add(child);
		}
		//Debug.Log(all_members);

		

	}
	void Update()
	{
		neighbourcontrastslist.Clear();

		// we estimate the starting contrast level based on the all_members contrasts
		foreach (Transform child_element in all_members)
		{
			parameters = child_element.GetComponentInChildren<Contrast_thresholds>();
			neighbourcontrastslist.Add(parameters.current_contrast_level);
		}

		if (neighbourcontrastslist.Count != 0)
		{
			float sum = 0;

			foreach (float element in neighbourcontrastslist)
			{
				sum = sum + element;
			}

			current_contrast_level = sum / neighbourcontrastslist.Count;
            //Debug.Log("Group 2 c_level :");
			//Debug.Log(sum.ToString());
		}

		// Now we see the responses received for the corresponding contrast level and update the current_contrast_level
		
	}

	float Weibull(double x, double location_g)
    {
		exp_power = Mathf.Pow((float)(x / true_weibull_threshold),(float)true_weibull_slope);
		result = (float)(location_g + (1 - location_g) * (1 - Mathf.Exp(exp_power)));
		return result;
    }
}