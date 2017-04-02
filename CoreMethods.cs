using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class CoreMethods : MonoBehaviour
{
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
    public GameObject DropDownMap;
    public DropdownMapSwitcher ddm;
    public GameManager GM;

    private string fullJSON;
    private string picAssetPath;
    private string portraitAssetPath;
    private int whoGoesFirst;
    private int randomMap;
    public string map;
    public bool PlayerGoesFirst;
    public string P1Ban1;
    public string P1Ban2;
    public string P1Pick1;
    public string P1Pick2;
    public string P1Pick3;
    public string P1Pick4;
    public string P1Pick5;
    public string EBan1;
    public string EBan2;
    public string EPick1;
    public string EPick2;
    public string EPick3;
    public string EPick4;
    public string EPick5;
    public string draftStage;
    private string jsonString;
    public string totalCompsPossible;
    private int numberOfPossibleComps;
    public int possibleEnemyComp;
    public string activeTeam;
    public bool enemyCompReady;

    public static string chosenHero;


    Dictionary<string, string> playerTeam = new Dictionary<string, string>();
    Dictionary<string, string> enemyComp = new Dictionary<string, string>();

    public void StartDraft()
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
        enemyCompReady = false;
        enemyComp.Clear();
        activeTeam = "none";
        HeroSelectionPanel.SetActive(true);
        ddm = DropDownMap.GetComponent<DropdownMapSwitcher>();
        GM = gameObject.GetComponent<GameManager>();
        whoGoesFirst = Random.Range(1, 3);
        Debug.Log("Player " + whoGoesFirst + " goes first.");

        if (whoGoesFirst == 1)
        {
            PlayerGoesFirst = true;
            PlayerGoesFirstText.SetActive(true);
        }
        else
        {
            PlayerGoesFirst = false;
            EnemyGoesFirstText.SetActive(true);
        }
    }
    public void ActiveTeamCheck()
    {
        draftStage = GM.draftStage;
        Debug.Log("PlayerGoesFirst = " + PlayerGoesFirst);
        Debug.Log("CM draftStage = " + draftStage);
        if (!PlayerGoesFirst)
        {
            if (draftStage == "FirstBan1")
            {
                activeTeam = "Player";
            }
            if (draftStage == "FirstPick1")
            {
                activeTeam = "Player";
            }
            if (draftStage == "FirstPick2")
            {
                activeTeam = "Player";
            }
            if (draftStage == "FirstPick3")
            {
                activeTeam = "Player";
            }
            if (draftStage == "FirstPick4")
            {
                activeTeam = "Player";
            }
            if (draftStage == "FirstPick5")
            {
                activeTeam = "Player";
            }
            else
            {
                activeTeam = "Enemy";
            }
        }
        if (PlayerGoesFirst)
        {
            if (draftStage == "FirstBan1")
            {
                activeTeam = "Enemy";
            }
            if (draftStage == "FirstPick1")
            {
                activeTeam = "Enemy";
            }
            if (draftStage == "FirstPick2")
            {
                activeTeam = "Enemy";
            }
            if (draftStage == "FirstPick3")
            {
                activeTeam = "Enemy";
            }
            if (draftStage == "FirstPick4")
            {
                activeTeam = "Enemy";
            }
            if (draftStage == "FirstPick5")
            {
                activeTeam = "Enemy";
            }
            else
            {
                activeTeam = "Player";
            }
        }
        Debug.Log("cm activeTeam = " + activeTeam);
    }

    public void FirstBanStart()
    {
        map = GM.draftMap;
        if (activeTeam == "Player")
        {
            PlayerGoesFirstText.SetActive(false);
            PlayerBanText.SetActive(true);
        }
        else
        {
            EnemyGoesFirstText.SetActive(false);
            EnemyBanText.SetActive(true);
        }
    }


    public void Ban1HeroChosen()
    {
        if (PlayerGoesFirst)
        {
            chosenHero = GM.myChosenHero;
            P1Ban1 = chosenHero;
            PlayerBanText.SetActive(false);
            playerTeam.Add("P1Ban1", P1Ban1);
            portraitAssetPath = "HotSPortraits/" + chosenHero;
            PlayerBan1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + chosenHero + "Pic";
        }
        else { }
    }

    public void ActivateFullMiddlePic()
    {

        fullPicMiddle.SetActive(true);
        fullPicMiddleImage.sprite = Resources.Load<Sprite>(picAssetPath);
    }


    //  The following variables are for access to the temporary team selected below, taken from the relevant map's JSON.

    private int jTeamID;
    public static int totalComps;
    private static string totalCompsJSON;

    //  Each GetXYZComp() checks 2 JSON files:  the map specific one, and the TotalComps one.
    //  It uses the TotalComps one to figure out the possible number and randomly pick a valid one.
    //  Then below, it will check via a series of IF statements if any of the heroes in the selected comp
    //  are already a part of the currently chosen heroes by each side.


    public void GetEnemyComp()
    {
        map = GM.draftMap;
        enemyComp.Clear();
        if (map == "BoE")
        {
            Debug.Log("Method reached:   GetEnemyComp");
            totalCompsJSON = File.ReadAllText(Application.dataPath + "/Resources/HotSMapStuff/TotalComps.json");
            var totalCompsObj = JsonConvert.DeserializeObject<Dictionary<string, int>>(totalCompsJSON);
            totalComps = totalCompsObj["TotalBoEComps"];
            Debug.Log("totalComps = " + totalComps);
            jTeamID = Random.Range(1, totalComps + 1);
            Debug.Log("JSONTest says: Comp chosen = " + jTeamID);
            jsonString = File.ReadAllText(Application.dataPath + "/Resources/HotSMapStuff/BoEComps.json");
            var BoEComp = JsonConvert.DeserializeObject<IDictionary<int, IDictionary<string, string>>>(jsonString);

            enemyComp.Add("Won", BoEComp[jTeamID]["Won"]);
            enemyComp.Add("Team", BoEComp[jTeamID]["Team"]);
            enemyComp.Add("When", BoEComp[jTeamID]["When"]);
            enemyComp.Add("Place", BoEComp[jTeamID]["Place"]);
            enemyComp.Add("Against", BoEComp[jTeamID]["Against"]);
            enemyComp.Add("Ban1", BoEComp[jTeamID]["Ban1"]);
            enemyComp.Add("Ban2", BoEComp[jTeamID]["Ban2"]);
            enemyComp.Add("Pick1", BoEComp[jTeamID]["Pick1"]);
            enemyComp.Add("Pick2", BoEComp[jTeamID]["Pick2"]);
            enemyComp.Add("Pick3", BoEComp[jTeamID]["Pick3"]);
            enemyComp.Add("Pick4", BoEComp[jTeamID]["Pick4"]);
            enemyComp.Add("Pick5", BoEComp[jTeamID]["Pick5"]);
            enemyComp.Add("Strengths", BoEComp[jTeamID]["Strengths"]);
            enemyComp.Add("Weaknesses", BoEComp[jTeamID]["Weaknesses"]);
            enemyComp.Add("Counter1", BoEComp[jTeamID]["Counter1"]);
            enemyComp.Add("Counter2", BoEComp[jTeamID]["Counter2"]);
            enemyComp.Add("Counter3", BoEComp[jTeamID]["Counter3"]);
            enemyComp.Add("Counter4", BoEComp[jTeamID]["Counter4"]);
            Debug.Log("All variables added to enemyComp");
        }
        else { }
        //  Need to set up all other map composition JSONs as IF statements as well.
    }
    public void ConfirmComp()
    {
        if (playerTeam.ContainsValue(enemyComp["Ban1"]))
            enemyCompReady = false;
        if (playerTeam.ContainsValue(enemyComp["Ban2"]))
            enemyCompReady = false;
        if (playerTeam.ContainsValue(enemyComp["Pick1"]))
            enemyCompReady = false;
        if (playerTeam.ContainsValue(enemyComp["Pick2"]))
            enemyCompReady = false;
        if (playerTeam.ContainsValue(enemyComp["Pick3"]))
            enemyCompReady = false;
        if (playerTeam.ContainsValue(enemyComp["Pick4"]))
            enemyCompReady = false;
        if (playerTeam.ContainsValue(enemyComp["Pick5"]))
            enemyCompReady = false;
        //  Successfully selected team gets placed into the enemyComp IDictionary to keep it easily accessible.
        else
        {
            enemyCompReady = true;
            Debug.Log("enemyComp Ban1 will be: " + enemyComp["Ban1"]);
        }
    }

    //  Creating a method that can hopefully be called upon to pause things as needed later down the line
    //  using the waitTime variable which will need to be redefined for each individual wait.
    float waitTime;


    public void EnemyFirstBanPicPortrait()
    {
        EBan1 = enemyComp["Ban1"];
        portraitAssetPath = "HotSPortraits/" + EBan1;
        EnemyBanText.SetActive(false);
        EnemyBan1.sprite = Resources.Load<Sprite>(portraitAssetPath);
        picAssetPath = "HotsPics/" + EBan1 + "Pic";
    }
    
}
