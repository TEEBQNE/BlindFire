using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretShop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public static TurretShop turretShop;

	public List<Turret> turretList = new List<Turret>();

	public GameObject itemHolderLeftPrefab;
	public GameObject itemHolderRightPrefab;

	public Transform leftGrid;
	public Transform rightGrid;

	private List<GameObject> leftItemHolderList = new List<GameObject>();
	private List<GameObject> rightItemHolderList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		turretShop = this;
		fillList ();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		/*if (upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret != null) {
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret.transform.Find ("ViewField").gameObject.SetActive (false);
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().showShop = false;
			upgradePanelScript.upgradePanel.GetComponent<upgradePanelScript> ().currentSelectedTurret = null;
		}*/
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	void Update()
	{
		for (int x = 0; x < turretList.Count; x++) {
			updateButtons (turretList[x].turretID);
		}
	}

	void updateButtons(int currentID)
	{
		if ((currentID - 1) % 2 == 0) {
			for (int x = 0; x < leftItemHolderList.Count; x++) {
				ItemHolder leftHolder = leftItemHolderList [x].transform.Find ("itemHolder").GetComponent<ItemHolder> ();

				// FOUND CURRENT ITEM HOLDER
				if (leftHolder.turretID.CompareTo(currentID.ToString ()) == 0) {
					//Debug.Log (currentID);
					for (int y = 0; y < turretList.Count; y++) {
						// FOUND CURRENT TURRET IN LIST BY ID
						if (turretList [y].turretID == currentID) {
							//Debug.Log (y);
							if (turretList [y].unlocked == true && turretList [currentID - 1].turretPrice > GameManager.gameManager.GetComponent<GameManager> ().Resource) {
								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Animator> ().enabled = false;
								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().color = Color.red;
								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().color = new Color (1f, 0f, 0f, 0.6f);

								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().sprite = Resources.Load<Sprite>("ShopSprites/" +turretList [y].unlockedSprite);
								//leftHolder.buyButton.SetActive (false);

							} else if (turretList [y].unlocked == true && turretList [currentID - 1].turretPrice <= GameManager.gameManager.GetComponent<GameManager> ().Resource) {
								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Animator> ().enabled = false;
								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().color = Color.white;
								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().sprite = Resources.Load<Sprite>("ShopSprites/" +turretList [y].unlockedSprite);
								//leftHolder.buyButton.SetActive (true);
							} else if (!turretList [y].unlocked) {
								leftItemHolderList [x].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Animator> ().enabled = true;
							}
						}
					}
				}
			}
			// right side
		} else {
			for (int i = 0; i < rightItemHolderList.Count; i++) {
				ItemHolder rightHolder = rightItemHolderList [i].transform.Find ("itemHolder").GetComponent<ItemHolder> ();
				// FOUND CURRENT ITEM HOLDER
				if (rightHolder.turretID == currentID.ToString ()) {
					for (int j = 0; j < turretList.Count; j++) {
						// FOUND CURRENT TURRET IN LIST BY ID
						if (turretList [j].turretID == currentID) {

							if (turretList [j].unlocked == true && turretList [currentID - 1].turretPrice > GameManager.gameManager.GetComponent<GameManager> ().Resource) {
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().color = Color.red;
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().color = new Color (1f, 0f, 0f, 0.6f);
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Animator> ().enabled = false;
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().sprite = Resources.Load<Sprite>("ShopSprites/" +turretList [j].unlockedSprite);
								//leftHolder.buyButton.SetActive (false);

							} else if (turretList [j].unlocked == true && turretList [currentID - 1].turretPrice <= GameManager.gameManager.GetComponent<GameManager> ().Resource) {
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().color = Color.white;
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Animator> ().enabled = false;
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Image> ().sprite = Resources.Load<Sprite>("ShopSprites/" +turretList [j].unlockedSprite);
								//leftHolder.buyButton.SetActive (true);
							} else if (!turretList [i].unlocked) {
								rightItemHolderList [i].transform.Find ("itemHolder").transform.Find ("itemImage").GetComponent<Animator> ().enabled = true;
							}
						}
					}
				}

			}
		}
	}
	
	void fillList()
	{
		for(int x = 0; x < turretList.Count; x++)
		{
			// left side
			if (x % 2 == 0) {
				GameObject leftHolder = Instantiate (itemHolderLeftPrefab, leftGrid, false);
				ItemHolder leftScript = leftHolder.transform.Find ("itemHolder").GetComponent<ItemHolder> ();
				leftScript.itemName.text = turretList [x].turretName;
				leftScript.itemDescription.text = turretList [x].turretDescription;
				leftScript.itemPrice.text = "$ " + turretList [x].turretPrice.ToString();
				leftScript.turretID = turretList [x].turretID.ToString();

				// BUY BUTTON
				leftScript.buyButton.GetComponent<BuyButton>().turretID = turretList[x].turretID;
				// HANDLE LISTS
				leftItemHolderList.Add(leftHolder);
				//Debug.Log (leftScript.itemName.text);

				// if it is unlocked, set the sprite to unlocked sprite
				if (turretList [x].unlocked == true) {
					leftScript.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" + turretList [x].unlockedSprite);
				} else {
					leftScript.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" + turretList [x].lockedSprite);
				}
			// right side
			} else {
				GameObject rightHolder = Instantiate (itemHolderRightPrefab, rightGrid, false);
				ItemHolder rightScript = rightHolder.transform.Find ("itemHolder").GetComponent<ItemHolder> ();
				rightScript.itemName.text = turretList [x].turretName;
				rightScript.itemDescription.text = turretList [x].turretDescription;
				rightScript.itemPrice.text = "$ " + turretList [x].turretPrice.ToString();
				rightScript.turretID = turretList [x].turretID.ToString();

				// BUY BUTTON
				rightScript.buyButton.GetComponent<BuyButton>().turretID = turretList[x].turretID;

				// HANDLE LISTS
				rightItemHolderList.Add(rightHolder);

				// if it is unlocked, set the sprite to unlocked sprite
				if (turretList [x].unlocked == true) {
					rightScript.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" + turretList [x].unlockedSprite);
				} else {
					rightScript.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" +turretList [x].lockedSprite);
				}
			}
		}
	}

	public void UpdateSprite(int currentTurretID)
	{
		// left side
		if ((currentTurretID-1) % 2 == 0) {
				for (int x = 0; x < leftItemHolderList.Count; x++) {
					ItemHolder leftHolder = leftItemHolderList [x].transform.Find ("itemHolder").GetComponent<ItemHolder> ();

					// FOUND CURRENT ITEM HOLDER
					if (leftHolder.turretID == currentTurretID.ToString()) {
						for (int y = 0; y < turretList.Count; y++) {
							// FOUND CURRENT TURRET IN LIST BY ID
							if (turretList [y].turretID == currentTurretID) {

								// ONLY IF UNLOCKED––UPDATE
								if (turretList [y].unlocked == true) {
									leftHolder.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" + turretList [y].unlockedSprite);
								} else {
									leftHolder.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" +turretList [y].lockedSprite);
								}
							}
						}
					}
				}
			// right side
			} else {
				for (int i = 0; i < rightItemHolderList.Count; i++) {
					ItemHolder rightHolder = rightItemHolderList [i].transform.Find ("itemHolder").GetComponent<ItemHolder> ();
					// FOUND CURRENT ITEM HOLDER
					if (rightHolder.turretID == currentTurretID.ToString()) {
						for (int j = 0; j < turretList.Count; j++) {
							// FOUND CURRENT TURRET IN LIST BY ID
							if (turretList [j].turretID == currentTurretID) {

								// ONLY IF UNLOCKED––UPDATE
								if (turretList [j].unlocked == true) {
									rightHolder.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" + turretList [j].unlockedSprite);
								} else {
									rightHolder.itemImage.sprite = Resources.Load<Sprite>("ShopSprites/" +turretList [j].lockedSprite);
								}
							}
						}
					}

				}
			}
	}
}