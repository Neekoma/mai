/// <summary>
/// Button.
/// sgteam.ru
/// </summary>
using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {
	public static event Action<bool> onButtonClick;
	public CarLogic Car;
	public Material material;
	
	public float timer = 1;
	private float timeDown;
	private bool b_active = false;
	public GameObject TV;
	
	// Update is called once per frame
	void Update () {
		if(b_active) {
			if(timeDown > 0) timeDown -= Time.deltaTime;
			if(timeDown < 0) timeDown = 0;
			if(timeDown == 0) { 
				
				//Car.Status = false;
				b_active = false;
			}
		}
	}
	
	void OnMouseDown () {
		
			timeDown = timer;
			b_active = !b_active;
			Car.Status = b_active;
			material.color = b_active == true ? Color.red : Color.white;
			TV.SetActive(true);
			CVLogic.status = !CVLogic.status;
			onButtonClick?.Invoke(CVLogic.status);
		
	}
}
