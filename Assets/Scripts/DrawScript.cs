using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    [Range(0, 10)]
    public float brushSize;
    [Range(0, 1)]
    public float brushStrength;

    public Camera mainCamera;
    public Shader drawShader;

    private RenderTexture splatMap;
    private Material snowMaterial;
    private Material drawMaterial;

    private RaycastHit hit;

    void Start()
    {
        drawMaterial = new Material(drawShader);
        drawMaterial.SetColor("_Color", Color.red);

        snowMaterial = GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);
    }

    public void DrawOnPoint(Vector2 point)
    {
        drawMaterial.SetVector("_Coordinate", new Vector4(point.x, point.y, 0, 0));
        drawMaterial.SetFloat("_Strength", brushStrength);
        drawMaterial.SetFloat("_Size", brushSize);
        RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(splatMap, temp);
        Graphics.Blit(temp, splatMap, drawMaterial);
        RenderTexture.ReleaseTemporary(temp);
    }
}
