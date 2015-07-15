using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 originalPosition;
	private bool isPlayer;
	private GameObject player;
	
	void Start(){
		isPlayer = false;
	}
	void OnMouseDown()
	{ 
		originalPosition = gameObject.transform.position;
		screenPoint = Camera.main.WorldToScreenPoint(originalPosition);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
	}
	
	void OnMouseDrag() 
	{  
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		Vector3 curPosition   = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}
	void OnMouseUp(){
		if (isPlayer) {
			Debug.Log("Equip to" + player.name);
			//TODO

			Destroy(gameObject);

		} else {
			transform.position = originalPosition;
			transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
		}
	}

	void OnTriggerEnter(Collider other) {

		if(other.gameObject.CompareTag("Player")){
			isPlayer = true;
			other.gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
			gameObject.GetComponent<MeshRenderer> ().enabled = false;
			Debug.Log(other.gameObject.name);
			player = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("Player")){
			other.gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
			gameObject.GetComponent<MeshRenderer> ().enabled = true;
			isPlayer = false;
			player = null;
		}
	}
}
