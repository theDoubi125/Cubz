using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HilightRenderer : MonoBehaviour
{
    private Camera m_camera;

    public Shader m_altRenderShader;
    public Shader m_blurShader;
    private Material m_blurMat;

    private RenderTexture m_texture;

	void Start ()
    {
        m_camera = GetComponent<Camera>();
        m_camera.backgroundColor = Color.black;
        m_blurMat = new Material(m_blurShader);
        m_camera.SetReplacementShader(m_altRenderShader, "Entity");
	}

    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        if(m_texture == null || m_camera.pixelWidth != m_texture.width || m_camera.pixelHeight != m_texture.height)
            m_texture = new RenderTexture(m_camera.pixelWidth, m_camera.pixelHeight, 16);

        m_blurMat.SetFloat("_WScreen", m_camera.pixelWidth);
        m_blurMat.SetFloat("_HScreen", m_camera.pixelHeight);
        m_blurMat.SetTexture("_OutlineTex", source);
        m_blurMat.SetTexture("_RenderTex", m_texture);
        Graphics.Blit(source, destination, m_blurMat);
    }

    public RenderTexture RenderTexture { get { return m_texture; } }
}
