using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {

	public Transform[] targets;
	public bool activate;
	public bool stayPressed;

	private bool animation = false;

	// Use this for initialization
	void Start () {
		activate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (animation) {
			float deltaTime = Time.deltaTime;
			float diffScale = deltaTime * 0.8F;
			if (activate) {
				foreach (Transform t in targets) {
					t.localScale -= new Vector3 (diffScale, diffScale, diffScale);
					t.Rotate (Vector3.up * 360 * deltaTime);
					if (t.localScale.x <= 0) {
						t.localScale = new Vector3 (0, 0, 0);
						t.gameObject.GetComponent<Collider>().enabled=false;
						animation = false;
					}
				}
			} else {
				foreach (Transform t in targets) {
					t.localScale+= new Vector3(diffScale, diffScale, diffScale);
					t.Rotate (-Vector3.up*360 * deltaTime);
					if (t.localScale.x >= 1) {
						t.gameObject.GetComponent<Collider>().enabled=true;
						t.localScale = new Vector3 (1F, 1F, 1F);
						t.rotation = Quaternion.identity;
						animation = false;
					}
				}
			}

		}
	}

	void OnTriggerEnter(Collider other) {
		activateButton ();
	}

	void OnTriggerStay(Collider other) {
	}

	void OnTriggerExit(Collider other) {
		if(stayPressed){
			desactivateButton();
		}
	}

	void activateButton(){
		activate = true;
		animation = true;
	}
	void desactivateButton(){
		activate = false;
		animation = true;
	}
}
