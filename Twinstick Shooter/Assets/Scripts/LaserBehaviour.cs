using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {
	// 레이저가 얼마 동안 존재할지
	public float lifetime = 2.0f;
	// 레이저가 얼마나 빠르게 움직이는지
	public float speed = 5.0f;
	// 레이저 피해
	public int damage = 1;

	// Use this for initialization
	void Start () {
		// 이 컴포넌트를 담고 있는 게임 오브젝트는 lifetime 초가 지나면 소멸한다.
		Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
