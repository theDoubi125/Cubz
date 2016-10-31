using UnityEngine;
using System.Collections.Generic;

public class CubeManager : MonoBehaviour
{
    private List<CubeController> controllers = new List<CubeController>();
    public CameraController mainCamera;
    private int currentFocus = 0;

    public Transform currentCube { get { return controllers[currentFocus].transform; } }

    public void Start()
    {
        Debug.Log("Start Test");
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log(obj);

            CubeController controller = obj.GetComponent<CubeController>();
            if(controller != null)
                controllers.Add(controller);
        }
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
