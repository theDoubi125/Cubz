using UnityEngine;
using System.Collections;

public class SimpleActivatableBlock : ActivatableObject {

	private bool anim = false;

	public int nbActivate;
	private int currentActivate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (anim) {
			float deltaTime = Time.deltaTime;
			float diffScale = deltaTime * 0.8F;
			if (getActivated()) {
				
				transform.localScale -= new Vector3 (diffScale, diffScale, diffScale);
				transform.Rotate (Vector3.up * 360 * deltaTime);
				if (transform.localScale.x <= 0) {
					transform.localScale = new Vector3 (0, 0, 0);
					transform.gameObject.GetComponent<Collider>().enabled=false;
					anim = false;
					}
			} else {
				transform.localScale+= new Vector3(diffScale, diffScale, diffScale);
				transform.Rotate (-Vector3.up*360 * deltaTime);
				if (transform.localScale.x >= 1) {
					transform.gameObject.GetComponent<Collider>().enabled=true;
					transform.localScale = new Vector3 (1F, 1F, 1F);
					transform.rotation = Quaternion.identity;
					anim = false;
				}
			}
		}
	}

	public override void activate(){
		currentActivate++;
		if(currentActivate>=nbActivate){
			setActivated(true);
			anim = true;
		}

	}

	public override void desactivate(){
		currentActivate--;
		if(currentActivate<nbActivate){
			setActivated(false);
			anim = true;
		}
	}

}
