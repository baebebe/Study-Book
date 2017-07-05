using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndBehaviour : MonoBehaviour {

	// 플레이어가 일정한 시간이 지나기전에 게임에서 나가는 것을 막는다.
	private bool canQuit = false;

	// 게임에서 졌으므로 장애물 생성을 중단한다.
	// Use this for initialization
	void Start () {
		// 타이머 코루틴을 시작한다
		StartCoroutine(DelayQuit());

		// 더 이상 장애물을 생성할 필요가 없다.
		GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
		controller.CancelInvoke();
	}

	// 플레이어가 스페이스키, 혹은 마우스를 클릭했는지 체크한다. 재시작이 가능하면 다시 시작한다
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyUp("space") || Input.GetMouseButtonDown(0)) && canQuit){
			// 현재 실행되고 있는 같은 레벨을 다시 시작한다
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	// 펠리이어의 재시작을 지연시킨다
	IEnumerator DelayQuit(){
		// 플레이어가 게임을 끝내기 전에 시작을 준다
		yield return new WaitForSeconds(.5f);

		// .5초가 지나고 나면 이 시점에 도달한다
		canQuit = true;
	}
}
