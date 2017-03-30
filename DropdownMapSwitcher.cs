using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class DropdownMapSwitcher : MonoBehaviour
{
	public Dropdown mapDropdown;
	public Image mapBoE;
	public Image mapBBay;
	public Image mapBraxxis;
	public Image mapCursedH;
	public Image mapDShire;
	public Image mapGarden;
	public Image mapHMines;
	public Image mapInfernalS;
	public Image mapSkyT;
	public Image mapSpiderQueen;
	public Image mapTowersofD;
	public Image mapWarheadJ;
	public GameObject PlayerPicksBansPanel;
	public GameObject EnemyPicksBansPanel;
	public GameObject PlayerGoesFirstText;
	public GameObject EnemyGoesFirstText;
	public GameObject HeroSelectionPanel;
	public GameObject EnemyBanText;
	public GameObject PlayerBanText;
	public GameObject EnemySinglePickText;
	public GameObject PlayerSinglePickText;
	public GameObject EnemyDoublePickText;
	public GameObject PlayerDoublePickText;
	public GameObject PlayerSecondDoublePickText;
	public GameObject fullPicMiddle;
	public GameObject fullPicLeft;
	public GameObject fullPicRight;
	public Image fullPicMiddleImage;
	public Image fullPicLeftImage;
	public Image fullPicRightImage;
	public Image PlayerBan1;
	public Image PlayerBan2;
	public Image EnemyBan1;
	public Image EnemyBan2;
	public Image PlayerPick1;
	public Image PlayerPick2;
	public Image PlayerPick3;
	public Image PlayerPick4;
	public Image PlayerPick5;
	public Image EnemyPick1;
	public Image EnemyPick2;
	public Image EnemyPick3;
	public Image EnemyPick4;
	public Image EnemyPick5;

	private object fullJSON;
	private string picAssetPath;
	private string portraitAssetPath;
	private int whoGoesFirst;
	private int randomMap;
	private string map;
	private bool PlayerGoesFirst;
	private string P1Ban1;
	private string P1Ban2;
	private string P1Pick1;
	private string P1Pick2;
	private string P1Pick3;
	private string P1Pick4;
	private string P1Pick5;
	private string EBan1;
	private string EBan2;
	private string EPick1;
	private string EPick2;
	private string EPick3;
	private string EPick4;
	private string EPick5;
	private string draftStage;
	private string jsonString;
	public string totalCompsPossible;
	private int numberOfPossibleComps;
	public int possibleEnemyComp;
	public string currentEnemyComp;
	
	public static string chosenHero;

	void Start()
	{
		mapDropdown.onValueChanged.AddListener(delegate { DropDownSelection(); });
	}

	void DropDownSelection()
	{
		HeroSelectionPanel.SetActive(false);
		
		switch (mapDropdown.value)
		{
			case 0:
			mapBoE.enabled = false;
			mapBBay.enabled = false;
			mapBraxxis.enabled = false;
			mapCursedH.enabled = false;
			mapDShire.enabled = false;
			mapGarden.enabled = false;
			mapHMines.enabled = false;
			mapInfernalS.enabled = false;
			mapSkyT.enabled = false;
			mapSpiderQueen.enabled = false;
			mapTowersofD.enabled = false;
			mapWarheadJ.enabled = false;
			break;
		
			case 1:		
			mapBoE.enabled = false;
			mapBBay.enabled = false;
			mapBraxxis.enabled = false;
			mapCursedH.enabled = false;
			mapDShire.enabled = false;
			mapGarden.enabled = false;
			mapHMines.enabled = false;
			mapInfernalS.enabled = false;
			mapSkyT.enabled = false;
			mapSpiderQueen.enabled = false;
			mapTowersofD.enabled = false;
			mapWarheadJ.enabled = false;
			randomMap = Random.Range(2, 14);
			mapDropdown.value = randomMap;
			DropDownSelection();
			break;
		
			case 2:
				mapBoE.enabled = true;
				map = "BoE";
				StartDraft();
			break;
			
			case 3:
				mapBBay.enabled = true;
				map = "BBay";
				StartDraft();
			break;

			case 4:
				mapBraxxis.enabled = true;
				map = "Braxxis";
				StartDraft();
			break;
			
			case 5:
				mapCursedH.enabled = true;
				map = "CursedH";
				StartDraft();
			break;
				
			case 6:
				mapDShire.enabled = true;
				map = "DShire";
				StartDraft();
			break;

			case 7:
				mapGarden.enabled = true;
				map = "Garden";
				StartDraft();
			break;
			
			case 8:
				mapHMines.enabled = true;
				map = "HMines";
				StartDraft();
			break;

			case 9:
				mapInfernalS.enabled = true;
				map = "InfernalS";
				StartDraft();
			break;

			case 10:
				mapSkyT.enabled = true;
				map = "SkyT";
				StartDraft();
			break;

			case 11:
				mapSpiderQueen.enabled = true;
				map = "SpiderQueen";
				StartDraft();
			break;

			case 12:
				mapTowersofD.enabled = true;
				map = "TowersofD";
				StartDraft();
			break;

			case 13:
				mapWarheadJ.enabled = true;
				map = "WarheadJ";
				StartDraft();
			break;
		}
	}

	void StartDraft ()
	{
		P1Ban1 = "none";
		P1Ban2 = "none";
		P1Pick1 = "none";
		P1Pick2 = "none";
		P1Pick3 = "none";
		P1Pick4 = "none";
		P1Pick5 = "none";
		EBan1 = "none";
		EBan2 = "none";
		EPick1 = "none";
		EPick2 = "none";
		EPick3 = "none";
		EPick4 = "none";
		EPick5 = "none";
		
// Delete the line that defines whoGoesFirst = 1, and the two lines commented out below.
// ...that will restore the randomness in who begins.  1 = Player, 2 = Enemy.
		
//		whoGoesFirst = Random.Range(1, 3);
//		Debug.Log("Player " + whoGoesFirst + " goes first.");
		whoGoesFirst = 1;
		if (whoGoesFirst == 1)
			{
				PlayerGoesFirst = true;
				PlayerGoesFirstText.SetActive(true);
		// Then pick order will be:
		//	PlayerFirstBan  then  EnemyFirstBan
		//  PlayerFirstPick then  EnemyFirstPick and EnemySecondPick
		//  PlayerSecondPick and PlayerThirdPick
		//  EnemySecondBan  then  PlayerSecondBan
		//  EnemyThirdPick and EnemyFourthPick
		//  then PlayerFourthPick and PlayerFifthPick
		//  then EnemyFifthPick
				StartCoroutine(PauseForWhoGoesFirst());
			}
		else
			{
				PlayerGoesFirst = false;
				EnemyGoesFirstText.SetActive(true);
		// Then pick order will be:
		//	EnemyFirstBan  then  PlayerFirstBan
		//  EnemyFirstPick then  PlayerFirstPick and PlayerSecondPick
		//  EnemySecondPick and EnemyThirdPick
		//  PlayerSecondBan  then  EnemySecondBan
		//  PlayerThirdPick and PlayerFourthPick
		//  then EnemyFourthPick and EnemyFifthPick
		//  then PlayerFifthPick	
				StartCoroutine(PauseForWhoGoesFirst());
			}
	}
	
	IEnumerator PauseForWhoGoesFirst()
	{
		yield return new WaitForSeconds(1);
		HeroSelectionPanel.SetActive(true);
		PlayerPicksBansPanel.SetActive(true);
		EnemyPicksBansPanel.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		SplitForGoesFirst();
	}
	void SplitForGoesFirst()
	{
		Debug.Log(PlayerGoesFirst);
		if (PlayerGoesFirst == true)
			{
				PlayerGoesFirstText.SetActive(false);
				Debug.Log("Player Goes First Reached.");
				P1FirstBan();
			}
		else
			{
				EnemyGoesFirstText.SetActive(false);
				EnemyBanText.SetActive(true);
			}
	}
	
	void P1FirstBan()
	{
		draftStage = "P1FirstBan";
		chosenHero = "none";
		PlayerBanText.SetActive(true);
		P1FirstBanCheckHero();
	}
	void P1FirstBanCheckHero()
	{	
		if (chosenHero == "none")
			{
				StartCoroutine(P1FirstBanWaitForInput());
			}
		else
			{
				P1Ban1 = chosenHero;
				PlayerBanText.SetActive(false);
				portraitAssetPath = "HotSPortraits/" + chosenHero;
				PlayerBan1.sprite = Resources.Load<Sprite>(portraitAssetPath);
				FullMiddlePic();
				StartCoroutine(PauseForP1FirstBanPic());
			}
	}
	IEnumerator P1FirstBanWaitForInput()
	{
		yield return new WaitForSeconds(0.5f);
		chosenHero = HeroSelectScript.chosenHero;
		P1FirstBanCheckHero();
	}

	void FullMiddlePic()
	{
		picAssetPath = "HotsPics/" + chosenHero + "Pic";
		fullPicMiddle.SetActive(true);
		fullPicMiddleImage.sprite = Resources.Load<Sprite>(picAssetPath);
	}

	IEnumerator PauseForP1FirstBanPic()
	{
		yield return new WaitForSeconds(2.2f);
		fullPicMiddle.SetActive(false);
		GetBoEComp();

	}

	/*	void EnemyMapCheck()
		{
			if (map == "BoE")
				{

					jsonString = File.ReadAllText(Application.dataPath + "/Resources/HotSMapStuff/BoEComps.json");
				//	fullJSON = JsonMapper.ToObject(jsonString);
					fullJSON = JsonConvert.DeserializeObject(Application.dataPath + "/Resources/HotSMapStuff/BoeComps.json");
					totalCompsPossible = fullJSON["TotalComps"];
					int.TryParse(totalCompsPossible, out numberOfPossibleComps);
					GetBoEComp();
				}
		}
	*/

	int TotalComps = 0;

	[System.Serializable]
	public class jsonBasics
	{
		public int TotalComps { get; set; }
		public string JMap { get; set; }
		public List<JComp1> Comp1 { get; set; }
	}
	[System.Serializable]
	public class JComp1
	{
		public string JTeam { get; set; }
		public string JWhen { get; set; }
		public string JPlace { get; set; }
		public string JAgainst { get; set; }
		public string JBan1 { get; set; }
		public string JBan2 { get; set; }
		public string JPick1 { get; set; }
		public string JPick2 { get; set; }
		public string JPick3 { get; set; }
		public string JPick4 { get; set; }
		public string JPick5 { get; set; }

	}
	private int jID;
	

	void GetBoEComp()
	{
		//  Debug.Log("Using comp # " + possibleEnemyComp + "of a possible" + numberOfPossibleComps);
		//  jsonComp = fullJSON[jID]["Comp"].ToString();
		//  Debug.Log(jsonComp);
		
		fullJSON = File.ReadAllText(Application.dataPath + "/Resources/HotSMapStuff/BoEComps.json");
		JsonUtility.FromJson<jsonBasics>(fullJSON);
		jID = possibleEnemyComp = Random.Range(1, TotalComps);


		Debug.Log(fullJSON);
		Debug.Log(jID);

	}

	
	

	void P1FirstPick ()
	{
		draftStage = "P1FirstPick";
		chosenHero = "none";
		if (PlayerGoesFirst == true)
			{
			PlayerSinglePickText.SetActive(true);	
			}
		else
			{
			PlayerDoublePickText.SetActive(true);
			}
	}

	void EnemyMapComp()
	{
		if (map == "BoE")
		{
			BoEDraft();
		}

		if (map == "BBay")
		{
			BBayDraft();
		}

		if (map == "Braxxis")
		{
			BraxxisDraft();
		}
	
		if (map == "CursedH")
		{
			CursedHDraft();
		}
	
		if (map == "DShire")
		{
			DShireDraft();
		}
	
		if (map == "Garden")
		{
			GardenDraft();
		}

		if (map == "HMines")
		{
			HMinesDraft();
		}
	
		if (map == "InfernalS")
		{
			InfernalSDraft();
		}
	
		if (map == "SkyT")
		{
			SkyTDraft();
		}
	
		if (map == "SpiderQueen")
		{
			SpiderQueenDraft();
		}
	
		if (map == "TowersofD")
		{
			TowersofDDraft();
		}
	
		if (map == "WarheadJ")
		{
			WarheadJDraft();
		}
	}

	void BoEDraft()
	{

	}

	void BBayDraft()
	{

	}

	void BraxxisDraft()
	{

	}

	void CursedHDraft()
	{

	}

	void DShireDraft()
	{

	}

	void GardenDraft()
	{

	}

	void HMinesDraft()
	{

	}

	void InfernalSDraft()
	{

	}

	void SkyTDraft()
	{

	}

	void SpiderQueenDraft()
	{

	}

	void TowersofDDraft()
	{

	}

	void WarheadJDraft()
	{

	}


		void EnemyFirstPick()
		{

		}
		void EnemySecondPick()
		{

		}
		void P1SecondPick()
		{

		}
		void P1ThirdPick()
		{

		}
		void EnemySecondBan()
		{

		}
		void P1SecondBan()
		{

		}
		void EnemyThirdPick()
		{

		}
		void EnemyFourthPick()
		{

		}
		void P1FourthPick()
		{

		}
		void P1FifthPick()
		{

		}
		void EnemyFifthPick()
		{

		}
}