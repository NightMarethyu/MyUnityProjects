using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    public Material material;
    public float red = 0.5f;
    private float redMod = 0.001f;
    public float green = 1.0f;
    private float greenMod = -0.001f;
    public float blue = 0.3f;
    private float blueMod = 0.001f;
    public float alpha = 0.4f;
    private float alphaMod = 0.001f;
    
    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        
        material = Renderer.material;
        
        material.color = new Color(red, green, blue, alpha);
    }
    
    void Update()
    {
        transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);
        red = ChangeColorVal(red, ref redMod);
        green = ChangeColorVal(green, ref greenMod);
        blue = ChangeColorVal(blue, ref blueMod);
        alpha = ChangeColorVal(alpha, ref alphaMod);

        material.color = new Color(red, green, blue, alpha);
    }

    float ChangeColorVal(float col, ref float mod)
    {
        col += mod;
        if (col >= 1 || col <= 0)
        {
            mod = -mod;
        }
        return col;
    }
}
