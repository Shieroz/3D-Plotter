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
    
    public void Render() {
        float[,] pointsData = null;
        try {
            pointsData = CSVParser.parser(Path);
        } catch (FileNotFoundException e) {
            FilePathText.text = "File does not exist";
            return;
        } catch (DirectoryNotFoundException e) {
            FilePathText.text = "Path does not exist";
            return;
        } catch (Exception e) {
            FilePathText.text = "Something went wrong with reading the file";
            return;
        }

        // Generate Position and Color for each point from input data
        Vector3[] positions = new Vector3[pointsData.GetLength(0)];
        Color[] colors = new Color[pointsData.GetLength(0)];
        for (int i = 0; i < positions.Length; i++) {
            colors[i] = colorBand(pointsData[i, 3] / maxColorGradient);
            positions[i] = new Vector3(pointsData[i, 0], pointsData[i, 1], pointsData[i, 2]);
        }

        pointCloud.SetParticles(positions, colors);
    }

    private Color colorBand(float t) {
        float r = 0, g = 0, b = 0, a = 1;
        if (t < 0 || t > 1)
            a = 0;
        else if (t < 0.2f) {
            r = 1;
            g = t / 0.2f;
            b = 0;
        } else if (t < 0.4f) {
            r = (0.4f - t) / 0.2f;
            g = 1;
            b = 0;
        } else if (t < 0.6f) {
            r = 0;
            g = 1;
            b = (t - 0.4f) / 0.2f;
        } else if (t < 0.8f) {
            r = 0;
            g = (0.8f - t) / 0.2f;
            b = 1;
        } else {
            r = (t - 0.8f) / 0.2f;
            g = 0;
            b = 1;
        }
        return new Color(r, g, b, a);
    }
}
