using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonAction : ActivatableObject {

	public List<ActivatableObject> targets;
	public bool stayPressed;

	private bool anim = false;
	private float initialScaleY;

	// Use this for initialization
	void Start () {
		setActivated(false);
		initialScaleY = transform.localScale.y;
	}

	// Update is called once per frame
	void Update () {
		if(anim){
			float deltaTime = Time.deltaTime;

			if(getActivated()){
				float diffScale = deltaTime * 0.5F;
				transform.localScale -= new Vector3 (0, diffScale,0);
				if (transform.localScale.y <= 0.001) {
					transform.localScale = new Vector3 (transform.localScale.x, 0.001F, transform.localScale.z);
					anim = false;
				}
			}else{
				float diffScale = deltaTime * 0.03F;
				transform.localScale += new Vector3 (0, diffScale,0);
				if (transform.localScale.y >= initialScaleY) {
					transform.localScale = new Vector3 (transform.localScale.x, initialScaleY, transform.localScale.z);
					anim = false;
				}
			}
		}
	}

	public override void activate(){
		setActivated(true);
		anim = true;
	}

	public override void desactivate(){
		setActivated(false);
		if(stayPressed){
			anim = true;
		}

	}

	void OnTriggerEnter(Collider other) {
		if(!getActivated()){
			activate();
			foreach (ActivatableObject ao in targets) {
				ao.activate();
			}
		}
	}

	void OnTriggerStay(Collider other) {
	}

	void OnTriggerExit(Collider other) {
		if(stayPressed){
			desactivate();
			foreach (ActivatableObject ao in targets) {
				ao.desactivate();
			}
		}
	}
}
