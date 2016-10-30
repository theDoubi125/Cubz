using UnityEngine;
using System.Collections;

public abstract class ActivatableObject : MonoBehaviour {

	private bool b_activated=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setActivated(bool b){
		b_activated=b;
	}

	public bool getActivated(){
		return b_activated;
	}

	public abstract void activate();
	public abstract void desactivate();


}
