using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	// 방향성 움직임에 적용되는 움직임 변경자
	public float playerSpeed = 4.0f;
	// 플레이어의 현재 속도
	private float currentSpeed = 0.0f;
	// 마지막으로 행한 움직임
	private Vector3 lastMovement = new Vector3();
	public Transform laser;
	public float laserDistance = .2f;
	// 다시 발사할 때까지 기다려야 하는 시간
	public float timeBetweenFires = .3f;
	// 값이0보다 작거나 같으면 다시 발사할 수 있다.
	private float timeTilNextFire = 0.0f;
	// 레이저를 발사하기 위해 사용하는 버튼들
	public List<KeyCode> shootButton;
	public AudioClip shootSound;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>	();
	}
	
	// Update is called once per frame
	void Update () {
		if(!PauseMenuBehaviour.isPaused){
			// 마우스를 바라보게 플레이어를 회전시킨다.
			Rotation();
			// 플레이어를 움직인다
			Movement();

			foreach(KeyCode element in shootButton){
				if(Input.GetKey(element) && timeTilNextFire < 0 ){
					timeTilNextFire = timeBetweenFires;
					ShootLaser();
					break;
				}
			}
			timeTilNextFire -= Time.deltaTime;
		}
	}

	void Rotation(){
		// 플레이어를 기준으로 마우스의 위치를 구함
		Vector3 worldPos = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint(worldPos);
		// 각 축을 기준으로 거리 차이를 구함
		float dx = this.transform.position.x - worldPos.x;
		float dy = this.transform.position.y - worldPos.y;

		// 두 오브젝트 사이의 각도를 구함
		float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

		// transform의 회전 속성은 4원수(quaternion)를 사용한다. 따라서 각도를 벡터로 변환할 필요가 있음.
		// Z축은 2D의 회전에 사용

		Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle +90));

		// 우주선의 회전을 지정
		this.transform.rotation = rot;
	}

	// 눌린 키에따라 플레이어를 움직인다
	void Movement(){
		// 현재 프레임에 일어나야 할 움직임
		Vector3 movement = new Vector3();

		// 입력 체크
		movement.x += Input.GetAxis("Horizontal");
		movement.y += Input.GetAxis("Vertical");

		movement.Normalize();

		// 무엇이든지 눌렸는지 여부 확인
		if(movement.magnitude > 0 ){
			// 눌렸으면 그 방향으로 움직인다
			currentSpeed = playerSpeed;
			this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
			lastMovement = movement;
		}else{
			// 그렇지 않다면 가던 방향으로 움직인다.
			this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
			// 시간이 지날수록 느려진다
			currentSpeed *= .9f;

		}
	}

	// 레이저를 생성하고 초기 위치를 우주선 앞으로 지정한다.
	void ShootLaser(){
		audioSource.PlayOneShot(shootSound);
		// 레이저의 위치를 플레이어의 위치에 따라 지정한다.
		Vector3 laserPos = this.transform.position;
		// 레이저의 각도를 가운데에서 밖으로 향하도록 한다.
		float rotationAngle = transform.localEulerAngles.z - 90;
		// 우주선에서 laserDistance만큼 떨어진 우주선 바로 앞의 위치를 계산한다.
		laserPos.x += (Mathf.Cos((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
		laserPos.y += (Mathf.Sin((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);

		Instantiate(laser, laserPos, this.transform.rotation);
	}
}
