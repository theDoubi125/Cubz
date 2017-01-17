using System;
using UnityEngine;
using System.Collections.Generic;

public class RenderTextureHandler : MonoBehaviour
{
    public HilightRenderer renderer;
    private Camera m_camera;

    public void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    public void Update()
    {
        m_camera.targetTexture = renderer.RenderTexture;
    }
}

