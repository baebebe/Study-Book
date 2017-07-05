using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

	[Tooltip("이 오브젝트가 얼마나 빨리 움직이는가")]
	public float scrollSpeed;

	public const float ScrollWidth = 8;

	private void FixedUpdate(){
		// 현재 위치를 구한다
		Vector3 pos = transform.position;

		// 오브젝트를 정해진 수치만큼 왼쪽으로 움직인다
		// (x축에서 음수 방향으로)
		pos.x -= scrollSpeed * Time.deltaTime * GameController.speedModifier;

		// 오브젝트가 스크린 밖으로 나갔는지 체큰한다
		if (transform.position.x < -ScrollWidth){
			Offscreen(ref pos);
		}

		// 파괴되지 않았다면 새로운 위치를 설정한다
		transform.position = pos;
	}

	protected virtual void Offscreen(ref Vector3 pos){
		// 오브젝트를 화면 바깥 오른쪽으로 이동시킨다.
		pos.x += 2 * ScrollWidth;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
