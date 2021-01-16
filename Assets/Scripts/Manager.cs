using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public PointCloudRenderer pointCloud;
    public string Path;
    public float maxColorGradient = 0f;

    public Text FilePathText;
    public GameObject UI;
    private bool enableUI = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) {
            UI.SetActive(enableUI);
            enableUI = !enableUI;
        }
    }
    
    public void Import() {
        try {
            string file = System.IO.File.ReadAllText(Path);
            pointCloud.Render(file.Trim().Split(new char[]{'\n'}));
        } catch (FileNotFoundException e) {
            FilePathText.text = "File does not exist";
        } catch (DirectoryNotFoundException e) {
            FilePathText.text = "Path does not exist";
        } catch (Exception e) {
            FilePathText.text = "Something went wrong when opening the CSV file";
        }
    }

    public static void QuitApplication() {
        Application.Quit();
    }
}
