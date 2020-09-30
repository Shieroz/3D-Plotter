using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilePath : MonoBehaviour
{
    public Text textPath, inputField;
    public GameObject manager;

    public void UpdateTextPath() {
        textPath.text = "Path://" + inputField.text;
        manager.GetComponent<Manager>().Path = inputField.text;
    }
}
