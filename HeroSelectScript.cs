using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectScript : MonoBehaviour {
private Button HeroButton;
public static string chosenHero;
private string chosenHeroAlpha;

	void Start ()
	{
		chosenHero = "none";
		chosenHeroAlpha = "none";
		HeroButton = gameObject.GetComponent<Button>();
        HeroButton.onClick.AddListener(delegate() { SetChosenHero(); });
	}
	
	void SetChosenHero ()
	{	
		Text selectChosenHero = GetComponentInChildren<Text>();
		chosenHero = chosenHeroAlpha = selectChosenHero.text;

		if (chosenHeroAlpha == "Anub'arak")
			{
				chosenHero = "Anubarak";
			}
		if (chosenHeroAlpha == "Cho'Gall")
			{
				chosenHero = "ChoGall";
			}
		if (chosenHeroAlpha == "Lúcio")
			{
				chosenHero = "Lucio";
			}
		if (chosenHeroAlpha == "Gul'dan")
			{
				chosenHero = "Guldan";
			}
		if (chosenHeroAlpha == "Kael'thas")
			{
				chosenHero = "Kaelthas";
			}
		if (chosenHeroAlpha == "Li-Ming")
			{
				chosenHero = "LiMing";
			}
		if (chosenHeroAlpha == "Zul'jin")
			{
				chosenHero = "Zuljin";
			}
		Debug.Log("HeroSelectScript: chosenHero = " + chosenHero);
		StartCoroutine(ClickClear());
	}
	IEnumerator ClickClear()
	{
		yield return new WaitForSeconds(0.7f);
		chosenHero = "none";
	}
}
