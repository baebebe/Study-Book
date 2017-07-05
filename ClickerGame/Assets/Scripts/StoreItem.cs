using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType{
	ClickPower, PerSecondIncrease
};

public class StoreItem : MonoBehaviour {

	[Tooltip("이 업그레이드의 가격이 얼만인지")]
	public int cost;

	public ItemType itemType;

	[Tooltip("구매한다면 얼마나 증가될 것인지")]
	public float increaseAmount;

	private int qty;

	public Text costText;
	public Text qtyText;

	private GameController controller;
	private Button button;

	// Use this for initialization
	void Start () {
		qty = 0;
		qtyText.text = qty.ToString();
		costText.text = "$" + cost.ToString();

		button = transform.GetComponent<Button>();

		// 버튼을 클릭하면 ButtonClicked 함수를 실행
		button.onClick.AddListener(this.ButtonClicked);

		// 코드를 통해 GameController를 레퍼런스
		controller = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	private void Update () {
		button.interactable = (controller.Cash >= cost);
	}

	public void ButtonClicked(){
		controller.Cash -= cost;
		switch(itemType){
			case ItemType.ClickPower: 
				controller.cashPerClick += increaseAmount;
				break;
			case ItemType.PerSecondIncrease:
				controller.CashPerSecond += increaseAmount;
				break;
		}

		qty++;
		qtyText.text = qty.ToString();
	}
}
