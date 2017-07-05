using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private float _cash;
	public float Cash{
		get{
			return _cash;
		}set{
			_cash = value;
			cashText.text = "You have: $" + _cash.ToString("0.00");
		}
	}

	private float _cashPerSecond;
	public float CashPerSecond{
		get{
			return _cashPerSecond;
		}set{
			_cashPerSecond = value;
			rateText.text = "per second: " + _cashPerSecond.ToString("0.0");
		}
	}

	[Tooltip("버튼을 누를 때마다 플레이어가 벌어들이는 돈.")]
	public float cashPerClick = 1;

	[Header("Obejct References")]
	public Text cashText;
	public Text rateText;

	// Use this for initialization
	void Start () {
		Cash = 0;
		CashPerSecond = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Cash += CashPerSecond * Time.deltaTime;
	}

	public void ClickedButton(){
		Cash += cashPerClick;
	}
}
