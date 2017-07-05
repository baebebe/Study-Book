using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	// 적 체력
	public int health = 2;
	// 파티클
	public Transform explosion;
	public AudioClip hitSound;

	void OnCollisionEnter2D(Collision2D theCollision){
		// 충돌한 모든 것들 중 이름이 "laser"인것을 찾는다
		if(theCollision.gameObject.name.Contains("Laser")){
			LaserBehaviour laser = theCollision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
			health -= laser.damage;
			Destroy(theCollision.gameObject);
			// 사운드 재생
			GetComponent<AudioSource>().PlayOneShot(hitSound);
		}

		if(health <= 0){
			Destroy(this.gameObject);
			GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			controller.KilledEnemy();
			controller.IncreaseScore(10);

			if(explosion){
				GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
				Destroy(exploder, 2.0f);
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
