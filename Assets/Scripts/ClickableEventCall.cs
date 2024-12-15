using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableEventCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public void ClickInside()
    {
        GameObject.Find("GameManagerOfBusiness").GetComponent<GameManagerOfBusiness>().PressedOnRight(transform.name);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
