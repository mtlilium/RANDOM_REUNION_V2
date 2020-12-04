using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerMapTransfer : MonoBehaviour
{
    private GameObject ParentGrid;//オブジェクトがGridの子であるとする
   
    private GameObject NextMap;
	public bool warped { get; private set; }//無限ワープ防止のためのフラグ
	[SerializeField]
	private InnerMapTransfer NextPoint = null;
	
	void OnTriggerEnter2D(Collider2D other){
        if (other.tag != "Player") return;
		if(warped) return;
		ParentGrid.SetActive(false);
		NextPoint.warped = true;
		NextMap.SetActive(true);
        // アニメーションが動かなくなってしまうので、PlayerはGridの外に置きPlayerの親をいじらない仕様に変更　
        //other.gameObject.transform.parent = NextMap.transform;　
        other.gameObject.transform.position = NextPoint.transform.position;
	}

	void OnTriggerExit2D(){
		warped = false;
	}

	void Awake(){
		ParentGrid = transform.parent.gameObject;
        NextMap = NextPoint.transform.parent.gameObject;
	}

}
