using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxColorGradient : MonoBehaviour
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
        manager.GetComponent<Manager>().maxColorGradient = mainSlider.value;
        text.text = "Max Gradient: " + manager.GetComponent<Manager>().maxColorGradient.ToString();
    }
}
