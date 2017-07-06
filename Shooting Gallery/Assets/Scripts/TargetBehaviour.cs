using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour {

	private bool beenHit = false;
	private Animator animator;
	private GameObject parent;
	private bool activated;
	private Vector3 originalPos;
	public float moveSpeed = 1f;
	public float frequency = 5f;
	public float magnitude = 0.1f;

	void Awake(){
		GameController._instance.targets.Add(this);
	}

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		animator = parent.GetComponent<Animator>();

		originalPos = parent.transform.position;
	}
	
	void OnMouseDown(){
		// 맞추는 것이 가능한가
		if(!beenHit && activated){
			GameController._instance.IncreaseScore();

			beenHit = true;
			animator.Play("Flip");

			StopAllCoroutines();

			StartCoroutine(HideTarget());
		}
	}

	public void ShowTarget(){
		if(!activated){
			activated = true;
			beenHit = false;
			animator.Play("Idle");
			iTween.MoveBy(parent, iTween.Hash("y", 1.4, "easeType", "easeInOutExpo", "time", 0.5, "oncomplete", "OnShown", "oncompletetarget", gameObject));
		}
	}

	public IEnumerator HideTarget(){
		yield return new WaitForSeconds(.5f);

		// 처음 위치로 내려간다
		iTween.MoveBy(parent.gameObject, iTween.Hash("y", (originalPos.y - parent.transform.position.y), "easeType","easeOutQuad", "loopType", "none", "time", 0.5, "oncomplete", "OnHidden"));
	}

	void OnHidden(){
		// 오브젝트의 위치가 리셋되게 한다
		parent.transform.position = originalPos;
		activated = false;
	}

	void OnShown(){
		StartCoroutine("MoveTarget");
	}

	IEnumerator MoveTarget(){
		var relativeEndPos = parent.transform.position;

		// 보고 있는 쪽이 왼쪽인가? 오른쪽인가?
		if(transform.eulerAngles == Vector3.zero){
			// 오른쪽으로 가고 있다면 양수
			relativeEndPos.x = 6;
		}else{
			// 아니라면 음수
			relativeEndPos.x = -6;
		}

		var movementTime = Vector3.Distance(parent.transform.position, relativeEndPos) * moveSpeed;
		var pos = parent.transform.position;
		var time = 0f;

		while(time < movementTime){
			time += Time.deltaTime;

			pos += parent.transform.right * Time.deltaTime * moveSpeed;
			parent.transform.position = pos + (parent.transform.up * Mathf.Sin(Time.time * frequency) * magnitude);

			yield return new WaitForSeconds(0);
		}

		StartCoroutine(HideTarget());
	}

	// Update is called once per frame
	void Update () {
		
	}
}
