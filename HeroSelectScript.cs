using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroSelectScript : MonoBehaviour {
	private Button HeroButton;
	public static string chosenHero;
	public string draftStage;
	public GameManager gm;
	public GameObject LeftPicPanel;
	public GameObject HeroDetails;
	public GameObject FullMiddlePanel;
	private Image picImage;
	private Text heroText;

	void Start ()
	{
		chosenHero = "none";
		picImage = LeftPicPanel.GetComponent<Image>();
		heroText = HeroDetails.GetComponent<Text>();
		HeroButton = gameObject.GetComponent<Button>();
		HeroButton.onClick.AddListener(delegate() { SetChosenHero(); });
	}

	void SetChosenHero()
	{
		gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
		Text selectChosenHero = GetComponentInChildren<Text>();
		chosenHero = selectChosenHero.text;
		draftStage = gm.draftStage;

		if (draftStage == "none")
		{
			ShowHeroDetails();
		}

		StartCoroutine(ClickClear());
	}

	IEnumerator ClickClear()
	{
		yield return new WaitForSeconds(1);
		chosenHero = "none";
	}

	void ShowHeroDetails()
	{
		FullMiddlePanel.SetActive(true);
		LeftPicPanel.SetActive(true);
		HeroDetails.SetActive(true);
		string portraitPath = "HotSPortraits/" + chosenHero;
		picImage.sprite = Resources.Load<Sprite>(portraitPath);
	}
}
