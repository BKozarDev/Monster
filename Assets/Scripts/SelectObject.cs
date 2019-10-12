using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void Select()
    {
        material.SetInt("_DisplayOutline", 1);
    }

    public void Deselect()
    {
        material.SetInt("_DisplayOutline", 0);
    }
}
