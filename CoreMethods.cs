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
    public GameObject WarriorPanel;
    public GameObject SupportPanel;
    public GameObject AssassinPanel;
    public GameObject SpecialistPanel;

    private string fullJSON;
    private string picAssetPath;
    private string portraitAssetPath;
    private int whoGoesFirst;
    private int randomMap;
    public string map;
    public string PlayerGoesFirst;
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
    public int possibleEnemyComp;
    public string activeTeam;
    public string enemyCompReady;

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
        enemyCompReady = "no";
        enemyComp.Clear();
        activeTeam = "none";
        playerTeam.Add("Ban1", "");
        playerTeam.Add("Ban2", "");
        playerTeam.Add("Pick1", "");
        playerTeam.Add("Pick2", "");
        playerTeam.Add("Pick3", "");
        playerTeam.Add("Pick4", "");
        playerTeam.Add("Pick5", "");
        ddm = DropDownMap.GetComponent<DropdownMapSwitcher>();
        GM = gameObject.GetComponent<GameManager>();
    }

        public void DisplayWhoGoesFirst()
        {
            whoGoesFirst = Random.Range(1, 3);
            Debug.Log("cm declares that:   " + whoGoesFirst + "   goes first.");
            if (whoGoesFirst == 1)
        {
            PlayerGoesFirst = "Player";
            PlayerGoesFirstText.SetActive(true);
        }
        else
        {
            PlayerGoesFirst = "Enemy";
            EnemyGoesFirstText.SetActive(true);
        }
    }
    public void ActiveTeamCheck()
    {
        draftStage = GM.draftStage;
        Debug.Log("cm draftStage = " + draftStage);
        if (PlayerGoesFirst == "Player")
        {
            if (draftStage == "Ban1")
                activeTeam = "Player";
            if (draftStage == "Ban2")
                activeTeam = "Enemy";
            if (draftStage == "Pick1")
                activeTeam = "Player";
            if (draftStage == "Pick23")
                activeTeam = "Enemy";
            if (draftStage == "Pick45")
                activeTeam = "Player";
            if (draftStage == "Ban3")
                activeTeam = "Enemy";
            if (draftStage == "Ban4")
                activeTeam = "Player";
            if (draftStage == "Pick6")
                activeTeam = "Enemy";
            if (draftStage == "Pick78")
                activeTeam = "Player";
            if (draftStage == "Pick910")
                activeTeam = "Enemy";
        }
        if (PlayerGoesFirst == "Enemy")
        {
            if (draftStage == "Ban1")
                activeTeam = "Enemy";
            if (draftStage == "Ban2")
                activeTeam = "Player";
            if (draftStage == "Pick1")
                activeTeam = "Enemy";
            if (draftStage == "Pick23")
                activeTeam = "Player";
            if (draftStage == "Pick45")
                activeTeam = "Enemy";
            if (draftStage == "Ban3")
                activeTeam = "Player";
            if (draftStage == "Ban4")
                activeTeam = "Enemy";
            if (draftStage == "Pick6")
                activeTeam = "Player";
            if (draftStage == "Pick78")
                activeTeam = "Enemy";
            if (draftStage == "Pick910")
                activeTeam = "Player";
        }
        Debug.Log("cm activeTeam = " + activeTeam);
    }

    public void DeactivateClassButtons()
    {
        WarriorPanel.SetActive(false);
        SupportPanel.SetActive(false);
        AssassinPanel.SetActive(false);
        SpecialistPanel.SetActive(false);
    }


    public void GetMap()
    {
        map = GM.draftMap;
    }

    public void OpeningBan()
    {
        ActiveTeamCheck();
        if (activeTeam == "Player")
        {
            PlayerGoesFirstText.SetActive(false);
        }
        else
        {
            EnemyGoesFirstText.SetActive(false);
        }
        DisplayBanText();
    }

    public void DisplayBanText()
    {
        if (activeTeam == "Player")
        {
            PlayerBanText.SetActive(true);
        }
        else
        {
            EnemyBanText.SetActive(true);
        }
    }

    public void PlayerBanChosen()
    {
        draftStage = GM.draftStage;
        chosenHero = GM.myChosenHero;
        PlayerBanText.SetActive(false);
        portraitAssetPath = "HotSPortraits/" + chosenHero;
        picAssetPath = "HotsPics/" + chosenHero + "Pic";
        if ((draftStage == "Ban1") || (draftStage == "Ban2"))
        {
            P1Ban1 = chosenHero;
            playerTeam["P1Ban1"] = P1Ban1;
            PlayerBan1.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if ((draftStage == "Ban3") || (draftStage == "Ban4"))
        {
            P1Ban2 = chosenHero;
            playerTeam["P1Ban2"] = P1Ban2;
            PlayerBan2.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
            
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


    IDictionary<string, int> totalCompsJSONDict;
    IDictionary<int, IDictionary<string, string>> AllCompsDict;

    public void GetEnemyComp()
    {
        map = ddm.map;
        enemyComp.Clear();
        Debug.Log("Method reached:   GetEnemyComp");
        totalCompsJSON = File.ReadAllText(Application.dataPath + "/Resources/HotSMapStuff/TotalComps.json");
        totalCompsJSONDict = JsonConvert.DeserializeObject<IDictionary<string, int>>(totalCompsJSON);

        if (map == "BoE")
        {
            totalCompsJSONDict.TryGetValue("TotalBoEComps", out totalComps);
            Debug.Log("totalBoEComps = " + totalComps);
            jTeamID = Random.Range(1, totalComps + 1);
            Debug.Log("JSONTest says: Comp chosen = " + jTeamID);
            jsonString = File.ReadAllText(Application.dataPath + "/Resources/HotSMapStuff/BoEComps.json");

        }
        //  Need to set up all other map composition JSONs as IF statements as well.
        else { }

        AllCompsDict = JsonConvert.DeserializeObject<IDictionary<int, IDictionary<string, string>>>(jsonString);
        enemyComp.Add("Won", AllCompsDict[jTeamID]["Won"]);
        enemyComp.Add("Team", AllCompsDict[jTeamID]["Team"]);
        enemyComp.Add("When", AllCompsDict[jTeamID]["When"]);
        enemyComp.Add("Place", AllCompsDict[jTeamID]["Place"]);
        enemyComp.Add("Against", AllCompsDict[jTeamID]["Against"]);
        enemyComp.Add("Ban1", AllCompsDict[jTeamID]["Ban1"]);
        enemyComp.Add("Ban2", AllCompsDict[jTeamID]["Ban2"]);
        enemyComp.Add("Pick1", AllCompsDict[jTeamID]["Pick1"]);
        enemyComp.Add("Pick2", AllCompsDict[jTeamID]["Pick2"]);
        enemyComp.Add("Pick3", AllCompsDict[jTeamID]["Pick3"]);
        enemyComp.Add("Pick4", AllCompsDict[jTeamID]["Pick4"]);
        enemyComp.Add("Pick5", AllCompsDict[jTeamID]["Pick5"]);
        enemyComp.Add("Strengths", AllCompsDict[jTeamID]["Strengths"]);
        enemyComp.Add("Weaknesses", AllCompsDict[jTeamID]["Weaknesses"]);
        enemyComp.Add("Counter1", AllCompsDict[jTeamID]["Counter1"]);
        enemyComp.Add("Counter2", AllCompsDict[jTeamID]["Counter2"]);
        enemyComp.Add("Counter3", AllCompsDict[jTeamID]["Counter3"]);
        enemyComp.Add("Counter4", AllCompsDict[jTeamID]["Counter4"]);
        Debug.Log("All variables added to enemyComp");
    }

    public void ConfirmComp()
    {
        if (playerTeam.ContainsValue(enemyComp["Ban1"]))
            enemyCompReady = "no";
        if (playerTeam.ContainsValue(enemyComp["Ban2"]))
            enemyCompReady = "no";
        if (playerTeam.ContainsValue(enemyComp["Pick1"]))
            enemyCompReady = "no";
        if (playerTeam.ContainsValue(enemyComp["Pick2"]))
            enemyCompReady = "no";
        if (playerTeam.ContainsValue(enemyComp["Pick3"]))
            enemyCompReady = "no";
        if (playerTeam.ContainsValue(enemyComp["Pick4"]))
            enemyCompReady = "no";
        if (playerTeam.ContainsValue(enemyComp["Pick5"]))
            enemyCompReady = "no";

        //  Successfully selected team gets placed into the enemyComp IDictionary to keep it easily accessible.
        else
        {
            enemyCompReady = "yes";
            Debug.Log("enemyComp Ban1 will be: " + enemyComp["Ban1"]);
        }
    }

    //  Creating a method that can hopefully be called upon to pause things as needed later down the line
    //  using the waitTime variable which will need to be redefined for each individual wait.
    float waitTime;


    public void EnemyBanPicPortrait()
    {
        draftStage = GM.draftStage;
        EnemyBanText.SetActive(false);
        if ((draftStage == "Ban1") || (draftStage == "Ban2"))
        {
            EBan1 = enemyComp["Ban1"];
            portraitAssetPath = "HotSPortraits/" + EBan1;
            EnemyBan1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EBan1 + "Pic";
        }
        if ((draftStage == "Ban3") || (draftStage == "Ban4"))
        {
            EBan2 = enemyComp["Ban2"];
            portraitAssetPath = "HotSPortraits/" + EBan2;
            EnemyBan2.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EBan2 + "Pic";
        }
        NewStageVariableClear();
    }

    public void NewStageVariableClear()
    {
        chosenHero = "none";
        activeTeam = "none";
        draftStage = "none";
        enemyCompReady = "no";
    }

    public void BanStart()
    {
        if (activeTeam == "Player")
        {
            PlayerBanText.SetActive(true);
        }
        else
        {
            EnemyBanText.SetActive(true);
        }
    }

    public void SinglePickStartText()
    {
        if (activeTeam == "Player")
        {
            PlayerSinglePickText.SetActive(true);
        }
        else
        {
            EnemySinglePickText.SetActive(true);
        }
    }

    public void DoublePickStartText()
    {
        if (activeTeam == "Player")
        {
            PlayerDoublePickText.SetActive(true);
        }
        else
        {
            EnemyDoublePickText.SetActive(true);
        }
    }

    public void PlayerSinglePickHeroChosen()
    {
        chosenHero = GM.myChosenHero;
        PlayerSinglePickText.SetActive(false);
        portraitAssetPath = "HotSPortraits/" + chosenHero;
        picAssetPath = "HotsPics/" + chosenHero + "Pic";

        if (draftStage == "Pick1")
        {
            P1Pick1 = chosenHero;
            playerTeam["P1Pick1"] = P1Pick1;
            PlayerPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if (draftStage == "Pick6")
        {
            P1Pick3 = chosenHero;
            playerTeam["P1Pick3"] = P1Pick3;
            PlayerPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        NewStageVariableClear();
    }

    public void EnemySinglePickPicPortrait()
    {
        draftStage = GM.draftStage;
        EnemySinglePickText.SetActive(false);
        if (draftStage == "Pick1")
        {
            EPick1 = enemyComp["Pick1"];
            Debug.Log("Enemy's first pick is:  " + EPick1);
            portraitAssetPath = "HotSPortraits/" + EPick1;
            EnemyPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EPick1 + "Pic";
        }
        if (draftStage == "Pick6")
        {
            EPick3 = enemyComp["Pick3"];
            Debug.Log("Enemy's first pick is:  " + EPick3);
            portraitAssetPath = "HotSPortraits/" + EPick3;
            EnemyPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EPick3 + "Pic";
        }
        NewStageVariableClear();
    }

    public void PlayerDoublePickFirstHeroChosen()
    {
        draftStage = GM.draftStage;
        chosenHero = GM.myChosenHero;
        PlayerDoublePickText.SetActive(false);
        PlayerSecondDoublePickText.SetActive(true);
        portraitAssetPath = "HotSPortraits/" + chosenHero;
        picAssetPath = "HotsPics/" + chosenHero + "Pic";

        if (draftStage == "Pick23")
        {
            P1Pick1 = chosenHero;
            playerTeam["P1Pick1"] = P1Pick1;
            PlayerPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if (draftStage == "Pick45")
        {
            P1Pick2 = chosenHero;
            playerTeam["P1Pick2"] = P1Pick2;
            PlayerPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if ((draftStage == "Pick78") || (draftStage == "Pick910"))
        {
            P1Pick4 = chosenHero;
            playerTeam["P1Pick4"] = P1Pick4;
            PlayerPick4.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }

    }

    public void PlayerDoublePickSecondHeroChosen()
    {
        draftStage = GM.draftStage;
        chosenHero = GM.myChosenHero;
        portraitAssetPath = "HotSPortraits/" + chosenHero;
        picAssetPath = "HotsPics/" + chosenHero + "Pic";
        PlayerSecondDoublePickText.SetActive(false);

        if (draftStage == "Pick23")
        {
            P1Pick2 = chosenHero;
            playerTeam["P1Pick2"] = P1Pick2;
            PlayerPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if (draftStage == "Pick45")
        {
            P1Pick3 = chosenHero;
            playerTeam["P1Pick3"] = P1Pick3;
            PlayerPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if ((draftStage == "Pick78") || (draftStage == "Pick910"))
        {
            P1Pick5 = chosenHero;
            playerTeam["P1Pick5"] = P1Pick5;
            PlayerPick5.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        NewStageVariableClear();
    }

    public void ActivateFullLeftPic()
    {
        fullPicLeft.SetActive(true);
        fullPicLeftImage.sprite = Resources.Load<Sprite>(picAssetPath);
    }

    public void ActivateFullRightPic()
    {
        fullPicRight.SetActive(true);
        fullPicRightImage.sprite = Resources.Load<Sprite>(picAssetPath);
    }

    public void EnemyFirstDoublePickPicPortrait()
    {
        EnemyDoublePickText.SetActive(false);
        if (draftStage == "Pick23")
        {
            EPick1 = enemyComp["Pick1"];
            portraitAssetPath = "HotSPortraits/" + EPick1;
            picAssetPath = "HotsPics/" + EPick1 + "Pic";
            EnemyPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if (draftStage == "Pick45")
        {
            EPick2 = enemyComp["Pick2"];
            portraitAssetPath = "HotSPortraits/" + EPick2;
            picAssetPath = "HotsPics/" + EPick2 + "Pic";
            EnemyPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if ((draftStage == "Pick78") || (draftStage == "Pick910"))
        {
            EPick4 = enemyComp["Pick4"];
            portraitAssetPath = "HotSPortraits/" + EPick4;
            picAssetPath = "HotsPics/" + EPick4 + "Pic";
            EnemyPick4.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        else { }
    }

    public void EnemySecondDoublePickPicPortrait()
    {
        if (draftStage == "Pick23")
        {
            EPick2 = enemyComp["Pick2"];
            portraitAssetPath = "HotSPortraits/" + EPick2;
            picAssetPath = "HotsPics/" + EPick2 + "Pic";
            EnemyPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if (draftStage == "Pick45")
        {
            EPick3 = enemyComp["Pick3"];
            portraitAssetPath = "HotSPortraits/" + EPick3;
            picAssetPath = "HotsPics/" + EPick3 + "Pic";
            EnemyPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        if ((draftStage == "Pick78") || (draftStage == "Pick910"))
        {
            EPick5 = enemyComp["Pick5"];
            portraitAssetPath = "HotSPortraits/" + EPick5;
            picAssetPath = "HotsPics/" + EPick5 + "Pic";
            EnemyPick5.sprite = Resources.Load<Sprite>(portraitAssetPath);
        }
        NewStageVariableClear();
    }



    public void DebugTeams()
    {
        string playerTeamList = P1Pick1 + ", " + P1Pick2 + ", " + P1Pick3 + ", " + P1Pick4 + ", " + P1Pick5 + " with " + P1Ban1 + " and " + P1Ban2 + " banned.";
        Debug.Log("Current playerTeam Listing is:  " + playerTeamList);
        string enemyCompList = EPick1 + ", " + EPick2 + ", " + EPick3 + ", " + EPick4 + ", " + EPick5 + " with " + EBan1 + " and " + EBan2 + " banned.";
        Debug.Log("Current enemyComp Listing is:  " + enemyCompList);
    }
}
