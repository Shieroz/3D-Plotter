using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxColorGradient : MonoBehaviour
{
    public GameObject manager;
    public InputField inputField;
    public Text text, colorBandT1, colorBandT2;
	
	public void Start()
	{
		text.text = "Gradient: 0 - " + manager.GetComponent<Manager>().maxColorGradient.ToString();
	}

    public void ValueChangeCheck() {
        float input = 0;
        if (float.TryParse(inputField.text, out input)) {
            input = Mathf.Clamp(input, 0.05f, float.MaxValue); //Clamping input
            manager.GetComponent<Manager>().maxColorGradient = input;
            text.text = "Gradient: 0 - " + input.ToString();
            colorBandT1.text = (input / 2f).ToString();
            colorBandT2.text = input.ToString();
        }   
    }
}
