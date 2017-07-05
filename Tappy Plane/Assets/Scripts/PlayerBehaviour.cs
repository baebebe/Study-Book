using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RigidbodyConstraints))]

public class PlayerBehaviour : MonoBehaviour {

	[Tooltip("플레이어가 점프할 때 더해지는 힘")]
	public Vector2 jumpForce = new Vector2(0, 300);

	// 부딪히면 더 이상 점프하지 못한다.
	private bool beenHit;

	private Rigidbody2D rigid2D;

	// Use this for initialization
	void Start () {
		beenHit = false;
		rigid2D = GetComponent<Rigidbody2D>();
	}
	
	void LateUpdate(){
		// 부딪히지 않았다면 점프를 할 수 있는지 없는지 체크
		if((Input.GetKeyUp("space") || Input.GetMouseButtonDown(0)) && !beenHit){
			// 속도를 리셋하고 다시 점프한다
			rigid2D.velocity = Vector2.zero;
			rigid2D.AddForce(jumpForce);
		}
	}

	// 다른 폴라곤 콜라이더와 충돌하면 추락한다.
	void OnCollisionEnter2D(Collision2D other){
		// 충돌한다
		beenHit = true;
		GameController.speedModifier = 0;

		// 더 이상 애니메이션이 재생되지 않게 하고, 속도를 0으로 설정하고 파괴시킨다.
		GetComponent<Animator>().speed = 0.0f;

		// 마지막으로 GameEndBehaviour를 생성해서 재시작하자
		if(!gameObject.GetComponent<GameEndBehaviour>()){
			gameObject.AddComponent<GameEndBehaviour>();	
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
