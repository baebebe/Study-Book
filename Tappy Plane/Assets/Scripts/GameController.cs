using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	[HideInInspector] // 아래 변수를 숨긴다.
	public static float speedModifier;
	
	[Header("장애물 정보")]

	[Tooltip("생성될 장애물")]
	public GameObject obstacleReference;

	[Tooltip("장애물에 쓰일 최소 Y 값")]
	public float obstacleMinY = -1.3f;

	[Tooltip("장애물에 쓰일 최대 Y 값")]
	public float obstacleMaxY = 1.3f;

	private static Text scoreText;
	private static int score;
	public static int Score{
		get{
			return score;
		}set{
			score = value;
			scoreText.text = score.ToString();
		}
	}

	// Use this for initialization
	void Start () {
		speedModifier = 1.0f;
		InvokeRepeating("CreateObstacle", 1.5f, 1.0f);
		gameObject.AddComponent<GameStartBehaviour>();
		score = 0;
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 장애물을 생성하고 위치를 초기화한다.
	void CreateObstacle(){
		// 무작위 Y 값을 사용해 화면 밖에 생성한다.
		Instantiate(obstacleReference, new Vector3(RepeatingBackground.ScrollWidth, Random.Range(obstacleMinY, obstacleMaxY), 0.0f), Quaternion.identity);
	}
}
