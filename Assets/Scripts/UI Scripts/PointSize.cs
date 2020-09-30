using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSize : MonoBehaviour
{
    public GameObject manager;
    public Slider mainSlider;
    public Text text;
	
	public void Start()
	{
		//Adds a listener to the main slider and invokes a method when the value changes.
		mainSlider = GetComponent<Slider>();
		mainSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}

    public void ValueChangeCheck() {
        manager.GetComponent<PointCloudRenderer>().particleSize = mainSlider.value;
        text.text = "Point Size: " + manager.GetComponent<PointCloudRenderer>().particleSize.ToString();
    }
}
