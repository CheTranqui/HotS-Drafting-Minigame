using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadNewImage : MonoBehaviour {

public static string chosenHero;
public Sprite Auriel;
private string picAssetPath;
private string portraitAssetPath;

	// Use this for initialization
	
	void Update()
	{
		chosenHero = DropdownMapSwitcher.chosenHero;
		picAssetPath = "Resources/HotsPics/" + chosenHero + "Pic";
		portraitAssetPath = "Resources/HotSPortraits/" + chosenHero;

		if (chosenHero == "Auriel")
		{
			gameObject.GetComponent<Image>().sprite = Auriel;
		}
	}
}