/*
 * Copyright John's Hopkins University
 */
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class PointCloudRenderer : MonoBehaviour
{
    // Constants
    private static int numColumn = 4;
    int texSize = 1024;
    
    VisualEffect vfx;
    List<string[]> data = new List<string[]>();
    List<float> WSS = new List<float>();
    Texture2D tex;
    float maxWSS;
    uint outlierCount;
    public Slider slider;
    public Text midWSSText, maxWSSText, sliderMax, outlierText;

    public void Render(string[] lines) {
        vfx = GetComponent<VisualEffect>();
        data.Clear();
        WSS.Clear();
        outlierCount = 0;

        maxWSS = 1;

        for (int i = 0; i < lines.Length; i++) {
            // Skip irrelevant points where WSS = 0
            if (!lines[i][lines[i].Length - 2].Equals('0')) {
                
                string[] line = lines[i].Trim().Split(new char[]{','});
            
                // Save data
                data.Add(line);
                
                // Outliers calculation
                float wss = float.Parse(line[3]);
                WSS.Add(wss);
                if (wss > 1) outlierCount++;

                // Caculate max WSS
                maxWSS = Mathf.Max(maxWSS, wss);
            }
        }

        outlierText.text = $"Outliers: {((float)outlierCount/lines.Length).ToString()} %";
        
        setParticles();
    }
    
    private void setParticles() {
        /* This approach uses pcache files as a medium for the vfx graph to get textures. There are no problems
        with this except the graph somehow doesn't detect changes to to the pcache files at runtime and the program
        has to be restarted everytime to see the change

        NOTE If using pcache file simply replacing the commas in the csv files with spaces

        string path = Application.dataPath + "/VFX Graph/pcache_header.txt";
        string output = System.IO.File.ReadAllText(path);
        output = output.Replace("INSERT_NUM_ELEMENTS", WSS.Count.ToString());
        
        path = Application.dataPath + "/VFX Graph/" + (transform.GetSiblingIndex() + 1) + ".pcache";
        System.IO.File.WriteAllText(path, output);
        
        
        using (StreamWriter sw = File.AppendText(path)) {
            for (int i = 0; i < WSS.Count; i++) {
                sw.WriteLine($"{data[i][0]} {data[i][1]} {data[i][2]} {WSS[i]}");

                if (i % 100000 == 0)
                    yield return null;
            }
        }
        */

        tex = new Texture2D(texSize, texSize, TextureFormat.RGBAFloat, false);

        int x = 0, y = 0;
        for (int i = 0; i < WSS.Count; i++)
        {
            if (WSS[i] != 0)
            {
                float r,g,b;
                r = float.Parse(data[i][0]);
                g = float.Parse(data[i][1]);
                b = float.Parse(data[i][2]);
                tex.SetPixel(x, y, new Color(r, g, b, WSS[i]));
                
                x++;
                if (x == texSize)
                {
                    x = 0;
                    y++;
                }
            }
        }
        tex.Apply();
        vfx.SetInt(Shader.PropertyToID("count"), WSS.Count);
        vfx.SetInt(Shader.PropertyToID("texSize"), texSize);
        vfx.SetTexture(Shader.PropertyToID("tex"), tex);
        vfx.SetFloat(Shader.PropertyToID("WSS"), 1f);
        vfx.Reinit();

        enableDisplay();
    }

    public void enableDisplay()
    {
        slider.maxValue = maxWSS;
        sliderMax.text = maxWSS.ToString();
        midWSSText.text = (maxWSS/2).ToString();
        maxWSSText.text = maxWSS.ToString();
    }

    public void UpdateWSS(float newVal)
    {
        vfx.SetFloat(Shader.PropertyToID("WSS"), newVal);
        midWSSText.text = (maxWSS/2).ToString();
        maxWSSText.text = maxWSS.ToString();
    }
}
