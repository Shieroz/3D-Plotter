using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSize : MonoBehaviour
{
    public GameObject manager;
    public InputField inputField;
    public Text text;
	
	public void Start()
	{
		text.text = "Point Size: " + manager.GetComponent<PointCloudRenderer>().particleSize.ToString();
	}

    public void ValueChangeCheck() {
        float input = 0;
        if (float.TryParse(inputField.text, out input)) {
            manager.GetComponent<PointCloudRenderer>().particleSize = input;
            text.text = "Point Size: " + manager.GetComponent<PointCloudRenderer>().particleSize.ToString();
        }   
    }
}
