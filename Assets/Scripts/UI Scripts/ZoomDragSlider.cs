using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoomDragSlider : MonoBehaviour
{
    public GameObject camera;
    public float zoomDragRatio = 3;
    public Slider mainSlider;
	
	public void Start()
	{
		//Adds a listener to the main slider and invokes a method when the value changes.
		mainSlider = GetComponent<Slider>();
		mainSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}

    public void ValueChangeCheck()
	{
        CameraMovement cam = camera.GetComponent<CameraMovement>();
		cam.zoomSpeed = mainSlider.value;
        cam.dragSpeed = mainSlider.value * zoomDragRatio;
	}
}
