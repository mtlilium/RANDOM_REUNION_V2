using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerMapTransfer : MonoBehaviour
{
	private GameObject ParentGrid;//オブジェクトがGridの子であるとする
	private GameObject NextMap;
	public bool warped = false;//無限ワープ防止のためのフラグ
	[SerializeField]
	private InnerMapTransfer NextPoint = null;
	
	void OnTriggerEnter2D(Collider2D other){
        if (other.tag != "Player") return;
		if(warped) return;
		ParentGrid.SetActive(false);
		NextPoint.warped = true;
		NextMap.SetActive(true);
		other.gameObject.transform.parent = NextMap.transform;
		other.gameObject.transform.position = NextPoint.transform.position;
	}

	void OnTriggerExit2D(){
		warped = false;
	}

	void Awake(){
		ParentGrid = transform.root.gameObject;
		NextMap = NextPoint.transform.root.gameObject;
	}

}
