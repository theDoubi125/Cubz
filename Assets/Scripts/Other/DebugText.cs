using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugText : MonoBehaviour {
    public CubeManager manager;
    private Text text;

	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Position : " + manager.currentCube.position + " Rotation : " + manager.currentCube.rotation.eulerAngles;
	}
}
