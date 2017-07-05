using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBehaviour : MonoBehaviour {

	// 플레이어 오브젝트에 대한 레퍼런스
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Plane");
		player.GetComponent<Rigidbody2D>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		// 게임을 시작
		if((Input.GetKeyUp("space") || Input.GetMouseButtonDown(0))){
			// 1초가 지나고 나면, 1.5초 마다 생성
			GameController controller = GetComponent<GameController>();
			controller.InvokeRepeating("CreateObstacle", 1f, 1.5f);

			// 비행기가 떨어지기 시작한다
			player.GetComponent<Rigidbody2D>().isKinematic = false;

			// 오브젝트가 아닌 이 컴포넌트만 삭제한다.
			Destroy(this);
		}
	}
}
