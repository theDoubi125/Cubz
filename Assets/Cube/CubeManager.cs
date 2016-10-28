using UnityEngine;
using System.Collections.Generic;

public class CubeManager : MonoBehaviour
{
    public List<CubeController> controllers = new List<CubeController>();
    public CameraController mainCamera;
    public int currentFocus = 0;

    public void Start()
    {
        UpdateFocus();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            currentFocus = (currentFocus + 1) % controllers.Count;
            UpdateFocus();
        }
    }

    public void UpdateFocus()
    {
        for (int i = 0; i < controllers.Count; i++)
            controllers[i].hasFocus = (i == currentFocus);
        mainCamera.target = controllers[currentFocus].transform;
    }
}
