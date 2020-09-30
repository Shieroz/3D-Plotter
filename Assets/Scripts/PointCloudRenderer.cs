using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PointCloudRenderer : MonoBehaviour
{
    Texture2D texColor, texPosScale;
    VisualEffect vfx;
    uint resolution = 2048;

    public float particleSize = 0.01f;
    bool toUpdate = false;
    uint particleCount;

    private void Start() {
        vfx = GetComponent<VisualEffect>();
    }

    private void Update() {
        if (toUpdate) {
            toUpdate = false;

            vfx.Reinit();
            vfx.SetUInt(Shader.PropertyToID("ParticleCount"), particleCount);
            vfx.SetTexture(Shader.PropertyToID("TexColor"), texColor);
            vfx.SetTexture(Shader.PropertyToID("TexPosScale"), texPosScale);
            vfx.SetUInt(Shader.PropertyToID("Resolution"), resolution);
        }
    }

    public void SetParticles(Vector3[] positions, Color[] colors) {
        int texWidth = positions.Length > (int)resolution ? (int)resolution : positions.Length;
        int texHeight = Mathf.Clamp(positions.Length / (int)resolution, 1, (int)resolution);
        
        texColor = new Texture2D(texWidth, texHeight, TextureFormat.RGBAFloat, false);
        texPosScale = new Texture2D(texWidth, texHeight, TextureFormat.RGBAFloat, false);
 
        for (int y = 0; y < texHeight; y++) {
            for (int x = 0; x < texWidth; x++) {
                int index = x + y * texWidth;
                texColor.SetPixel(x, y, colors[index]);
                var data = new Color(positions[index].x, positions[index].y, positions[index].z, particleSize);
                texPosScale.SetPixel(x, y, data);
            }
        }
 
        texColor.Apply();
        texPosScale.Apply();
        particleCount = (uint)positions.Length;
        toUpdate = true;
    }
}
