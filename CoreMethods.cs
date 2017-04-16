using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    private bool needGeneric;
    private bool needRandom;
    private bool needPick2;
    private bool needPick3;
    private bool needPick4;
    private bool needPick5;
    private bool needBan2;
    private string randomHero;
    private int randomNumber;

    public static string chosenHero;


    Dictionary<string, string> playerTeam = new Dictionary<string, string>();
    Dictionary<string, string> enemyComp = new Dictionary<string, string>();
    Dictionary<string, string> finalEnemyComp = new Dictionary<string, string>();

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
        finalEnemyComp.Add("Pick1", "");
        finalEnemyComp.Add("Pick2", "");
        finalEnemyComp.Add("Pick3", "");
        finalEnemyComp.Add("Pick4", "");
        finalEnemyComp.Add("Pick5", "");
        CompCheckCount = 0;
        needGeneric = false;
        needRandom = false;
        randomHero = "none";

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
            if (draftStage == "Pick67")
                activeTeam = "Enemy";
            if (draftStage == "Pick89")
                activeTeam = "Player";
            if (draftStage == "Pick10")
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
            if (draftStage == "Pick67")
                activeTeam = "Player";
            if (draftStage == "Pick89")
                activeTeam = "Enemy";
            if (draftStage == "Pick10")
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


    public void SetCMMap()
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
            playerTeam["Ban1"] = P1Ban1;
            PlayerBan1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Ban1);
        }
        if ((draftStage == "Ban3") || (draftStage == "Ban4"))
        {
            P1Ban2 = chosenHero;
            playerTeam["Ban2"] = P1Ban2;
            PlayerBan2.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Ban2);
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

    //  Each GetXYZComp() checks several JSON files:  the map specific one, and the TotalComps one.
    //  It uses the TotalComps one to figure out the possible number and randomly pick a valid one.
    //  Then below, it will check via a series of IF statements if any of the heroes in the selected comp
    //  are already a part of the currently chosen heroes by each side.


    IDictionary<string, int> totalCompsJSONDict;
    IDictionary<int, IDictionary<string, string>> AllCompsDict;

    //  Replacing all instances of Application.dataPath with Resources.Load instead.
    //  Apparently dataPath only works while editing inside of Unity, but not in the .EXE


    public void GetEnemyComp()
    {
        Debug.Log("Method reached:   GetEnemyComp");
        enemyCompReady = "checking";
        enemyComp.Clear();
        map = ddm.map;
        totalCompsJSON = Resources.Load("HotSMapStuff/TotalComps", typeof(TextAsset)).ToString();
        totalCompsJSONDict = JsonConvert.DeserializeObject<IDictionary<string, int>>(totalCompsJSON);

        if (needRandom)
        {
            ChooseRandomHeroes();
        }

        if (needGeneric &&
            !needRandom)
        {
            Debug.Log("cm.GetEnemyComp is sending us to GetGenericEnemyComp()");
            GetGenericEnemyComp();
        }
        if (!needGeneric &&
            !needRandom)
        {
            GetSpecificEnemyComp();
        }
    }

    public void GetSpecificEnemyComp()
    {
        Debug.Log("Method reached:   GetSpecificEnemyComp");
        if ((map == "BoE") || (map == "Braxis") || (map == "CursedH") || (map == "DShire") || (map == "InfernalS") || (map == "SkyT") || (map == "SpiderQueen") || (map == "TowersofD") || (map == "WarheadJ") && (!needGeneric))
        {
            totalCompsJSONDict.TryGetValue("Total" + map + "Comps", out totalComps);
            Debug.Log("Total" + map + "Comps = " + totalComps);
            jTeamID = Random.Range(1, totalComps + 1);
            Debug.Log("JSONTest says: Comp chosen = " + map + " number " + jTeamID);
            jsonString = Resources.Load("HotSMapStuff/" + map + "Comps", typeof(TextAsset)).ToString();
            LoadEnemyComp();
        }
        if ((map == "BBay") || (map == "Garden") || (map == "HMines"))
        {
            needGeneric = true;
            GetGenericEnemyComp();
        }
    }
    public void GetGenericEnemyComp()
    {
        Debug.Log("Method reached:   GetGenericEnemyComp");
        totalCompsJSONDict.TryGetValue("TotalGenericComps", out totalComps);
        Debug.Log("TotalGenericComps = " + totalComps);
        jTeamID = Random.Range(1, totalComps + 1);
        Debug.Log("JSONTest says: Generic comp chosen = Generic number" + jTeamID);
        jsonString = Resources.Load("HotSMapStuff/GenericComps", typeof(TextAsset)).ToString();
        LoadEnemyComp();
    }
    public void LoadEnemyComp()
    {
        Debug.Log("Method reached:   LoadEnemyComp");
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
        Debug.Log("All variables added to enemyComp");

        EnemyCompCount();
    }

    //  CompCheckCount is so that after checking a certain number of comps for that particular map, it breaks out of the
    //  cycle in order to just put heroes on the table.

    int EnemyCompMatches;
    int CompCheckCount;

    private void EnemyCompCount()
    {
        CompCheckCount++;
        Debug.Log("cm.EnemyCompCount declares this many comps used so far: " + CompCheckCount);
        Debug.Log("cm.EnemyCompCount declares that needGeneric initially says:  " + needGeneric);

        if (CompCheckCount < 20)
        {
            ConfirmComp();
        }
        if (CompCheckCount >= 20)
        {
            if (needRandom)
            {

            }
            if (needGeneric &&
                !needRandom)
            {
                needRandom = true;
                needPick2 = false;
                needPick3 = false;
                needPick4 = false;
                needPick5 = false;
                needBan2 = false;
                enemyComp["Ban1"] = EBan1;
                enemyComp["Pick1"] = EPick1;
                GetEnemyComp();
            }
            if (!needGeneric &&
                !needRandom)
            {
                CompCheckCount = 0;
                needGeneric = true;
                Debug.Log("cm.EnemyCompCount declares need for needGeneric!  needGeneric =  " + needGeneric);
                Debug.Log("CompCheckCount reset to:  " + CompCheckCount);
                GetEnemyComp();
            }
        }
    }

    public void ConfirmComp()
    {
        Debug.Log("Method reached:   ConfirmComp");
        enemyCompReady = "checking";
        if (needRandom)
            ChooseRandomHeroes();
        if (needRandom == false)
            ConfirmChosenComp();
    }

    private void ConfirmChosenComp()
    {
        if (playerTeam.ContainsValue(enemyComp["Ban1"]) ||
            playerTeam.ContainsValue(enemyComp["Ban2"]) ||
            playerTeam.ContainsValue(enemyComp["Pick1"]) ||
            playerTeam.ContainsValue(enemyComp["Pick2"]) ||
            playerTeam.ContainsValue(enemyComp["Pick3"]) ||
            playerTeam.ContainsValue(enemyComp["Pick4"]) ||
            playerTeam.ContainsValue(enemyComp["Pick5"]))
        {
            Debug.Log("Enemy" + map + "Comp number " + jTeamID + " contains duplicate of hero in playerTeam.  Going to GetEnemyComp()");
            GetEnemyComp();
        }
        else
        {
            Debug.Log("cm.ConfirmComp says:  enemyComp contains no picks or bans on playerTeam for " + map + " comp number " + jTeamID + ". Going to CheckDuplicates()");
            EnemyCompMatch1();
        }
    }

    //  EnemyCompMatches is a count of just how many picks within the newly chosen comp
    //  actually align with the picks already on the table for Enemy in the hopes of forcing the teams to not be mixed.

    string firstPick;
    string secondPick;
    string thirdPick;
    string fourthPick;
    string fifthPick;

    private void EnemyCompMatch1()
    {
        Debug.Log("Method reached:   EnemyCompMatch1");
        EnemyCompMatches = 0;
        if (enemyComp.ContainsValue(EPick1))
            EnemyCompMatches++;
        if (enemyComp.ContainsValue(EPick2))
            EnemyCompMatches++;
        if (enemyComp.ContainsValue(EPick3))
            EnemyCompMatches++;
        if (enemyComp.ContainsValue(EPick4))
            EnemyCompMatches++;
        if (enemyComp.ContainsValue(EPick5))
            EnemyCompMatches++;

        CheckEnemyCompBans();
    }

    private void CheckEnemyCompBans()
    {
        Debug.Log("Method reached:   CheckEnemyCompBans");
        if ((enemyComp["Pick1"] == EBan1) || (enemyComp["Pick1"] == EBan2) || (enemyComp["Pick2"] == EBan1) || (enemyComp["Pick2"] == EBan2) || (enemyComp["Pick3"] == EBan1) || (enemyComp["Pick3"] == EBan2) || (enemyComp["Pick4"] == EBan1) || (enemyComp["Pick4"] == EBan2) || (enemyComp["Pick5"] == EBan1) || (enemyComp["Pick5"] == EBan2) || (enemyComp["Ban2"] == EBan1))
        {
            Debug.Log("CheckEnemyCompBans declares ban conflict with " + jTeamID + " comp in " + map + ".  Generic = " + needGeneric);
            GetEnemyComp();
        }
        else
        {
            Debug.Log("cm.CheckEnemyCompBans declares no ban conflicts.");
            CheckEnemyCompPicks();
        }
    }

    //  4 matches means that Enemy's Pick5 is ready.  3 matches means Pick4 is ready, etc.
    private void CheckEnemyCompPicks()
    {
        Debug.Log("cm.CheckEnemyCompPicks() reached.");
        Debug.Log("cm.CheckEnemyCompPicks says that there are " + EnemyCompMatches + " matches in " + map + " comp number " + jTeamID + "(or Generic = " + needGeneric + ")");
        if ((EPick4 != "none") && (EnemyCompMatches == 4))
        {
            Debug.Log("Moving to ShuffleEnemyPicks4()");
            ShuffleEnemyPicks4();
        }
        if ((EPick4 != "none") && (EnemyCompMatches != 4))
        {
            Debug.Log("Not enough EnemyCompMatches.  Getting new comp.");
            GetEnemyComp();
        }
        else
        {
            CheckECompPick3();
        }
    }
    private void CheckECompPick3()
    {
        Debug.Log("Method reached:   CheckECompPick3");
        if ((EPick3 != "none") && (EnemyCompMatches == 3))
        {
            Debug.Log("Moving to ShuffleEnemyPicks3()");
            ShuffleEnemyPicks3();
        }
        if ((EPick3 != "none") && (EnemyCompMatches != 3))
        {
            Debug.Log("Not enough EnemyCompMatches.  Getting new comp.");
            GetEnemyComp();
        }
        else
        {
            CheckECompPick2();
        }
    }
    private void CheckECompPick2()
    {
        Debug.Log("Method reached:   CheckECompPick2");
        if ((EPick2 != "none") && (EnemyCompMatches == 2))
        {
            Debug.Log("Moving to ShuffleEnemyPicks2()");
            ShuffleEnemyPicks2();
        }
        if ((EPick2 != "none") && (EnemyCompMatches != 2))
        {
            Debug.Log("Not enough EnemyCompMatches.  Getting new comp.");
            GetEnemyComp();
        }
        else
        {
            CheckECompPick1();
        }
    }
    private void CheckECompPick1()
    {
        Debug.Log("Method reached:   CheckECompPick1");
        if ((EPick1 != "none") && (EnemyCompMatches == 1))
        {
            Debug.Log("Moving to ShuffleEnemyPicks1()");
            ShuffleEnemyPicks1();
        }
        if ((EPick1 != "none") && (EnemyCompMatches != 1))
        {
            Debug.Log("Not enough EnemyCompMatches.  Getting new comp.");
            GetEnemyComp();
        }
        else
        {
            CheckECompPick1B();
        }
    }
    private void CheckECompPick1B()
    {
        Debug.Log("Method reached:   CheckECompPick1B");
        if (EPick1 == "none")
        {
            firstPick = enemyComp["Pick1"];
            secondPick = enemyComp["Pick2"];
            thirdPick = enemyComp["Pick3"];
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            enemyCompReady = "yes";
            Debug.Log("EnemyComp is now:  1st Pick: " + firstPick + " -- 2nd Pick: " + secondPick + " -- 3rd Pick: " + thirdPick + " -- 4th Pick: " + fourthPick + " -- 5th Pick: " + fifthPick);
        }
        else
        {
            Debug.Log("Insufficient EnemyCompMatches - getting new enemyComp.");
            GetEnemyComp();
        }
        Debug.Log("cm.EnemyCompMatches = " + EnemyCompMatches + " matches within " + map + " number " + jTeamID + "(or Generic = " + needGeneric + ")");
    }



    private void ShuffleEnemyPicks1()
    {
        Debug.Log("cm.ShuffleEnemyPicks1 reached.");
        if (finalEnemyComp.ContainsValue(enemyComp["Pick1"]))
        {
            secondPick = enemyComp["Pick2"];
            thirdPick = enemyComp["Pick3"];
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks1 declares match with enemyComp[Pick1]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks1B();
        }
    }
    private void ShuffleEPicks1B()
    {
        Debug.Log("Method reached:   ShuffleEPicks1B");
        if (finalEnemyComp.ContainsValue(enemyComp["Pick2"]))
        {
            secondPick = enemyComp["Pick1"];
            thirdPick = enemyComp["Pick3"];
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks1 declares match with enemyComp[Pick2]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks1C();
        }
    }
    private void ShuffleEPicks1C()
    {
        Debug.Log("Method reached:   ShuffleEPicks1C");
        if (!finalEnemyComp.ContainsValue(enemyComp["Pick3"]))
        {
            secondPick = enemyComp["Pick1"];
            thirdPick = enemyComp["Pick2"];
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks1 declares match with enemyComp[Pick3]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks1D();
        }
    }
    private void ShuffleEPicks1D()
    {
        Debug.Log("Method reached:   ShuffleEPicks1D");
        if (!finalEnemyComp.ContainsValue(enemyComp["Pick4"]))
        {
            secondPick = enemyComp["Pick1"];
            thirdPick = enemyComp["Pick2"];
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks1 declares match with enemyComp[Pick4]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks1E();
        }
    }
    private void ShuffleEPicks1E()
    {
        Debug.Log("Method reached:   ShuffleEPicks1E");
        if (!finalEnemyComp.ContainsValue(enemyComp["Pick5"]))
        {
            secondPick = enemyComp["Pick1"];
            thirdPick = enemyComp["Pick2"];
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick4"];
            Debug.Log("cm.ShuffleEnemyPicks1 declares match with enemyComp[Pick5]");
            enemyCompReady = "yes";
        }
        else
        {
            Debug.Log("Something wrong with ShuffleEPicks1");
        }
    }
    private void ShuffleEnemyPicks2()
    {
        Debug.Log("Method reached:   ShuffleEnemyPicks2");
        Debug.Log("cm.ShuffleEnemyPicks2 reached.");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick2"])))
        {
            thirdPick = enemyComp["Pick3"];
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick1] and [Pick2]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2B();
        }
    }
    private void ShuffleEPicks2B()
    {
        Debug.Log("Method reached:   ShuffleEPicks2B");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])))
        {
            thirdPick = enemyComp["Pick2"];
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick1] and [Pick3]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2C();
        }
    }
    private void ShuffleEPicks2C()
    {
        Debug.Log("Method reached:   ShuffleEPicks2C");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])))
        {
            thirdPick = enemyComp["Pick2"];
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick1] and [Pick4]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2D();
        }
    }
    private void ShuffleEPicks2D()
    {
        Debug.Log("Method reached:   ShuffleEPicks2D");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            thirdPick = enemyComp["Pick2"];
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick4"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick1] and [Pick5]"); enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2E();
        }
    }
    private void ShuffleEPicks2E()
    {
        Debug.Log("Method reached:   ShuffleEPicks2E");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])))
        {
            thirdPick = enemyComp["Pick1"];
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick2] and [Pick3]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2F();
        }
    }
    private void ShuffleEPicks2F()
    {
        Debug.Log("Method reached:   ShuffleEPicks2F");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])))
        {
            thirdPick = enemyComp["Pick1"];
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick2] and [Pick4]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2G();
        }
    }
    private void ShuffleEPicks2G()
    {
        Debug.Log("Method reached:   ShuffleEPicks2G");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            thirdPick = enemyComp["Pick1"];
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick4"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick2] and [Pick5]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2H();
        }
    }
    private void ShuffleEPicks2H()
    {
        Debug.Log("Method reached:   ShuffleEPicks2H");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])))
        {
            thirdPick = enemyComp["Pick1"];
            fourthPick = enemyComp["Pick2"];
            fifthPick = enemyComp["Pick5"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick3] and [Pick4]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2I();
        }
    }
    private void ShuffleEPicks2I()
    {
        Debug.Log("Method reached:   ShuffleEPicks2I");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            thirdPick = enemyComp["Pick1"];
            fourthPick = enemyComp["Pick2"];
            fifthPick = enemyComp["Pick4"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick3] and [Pick5]");
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks2J();
        }
    }
    private void ShuffleEPicks2J()
    {
        Debug.Log("Method reached:   ShuffleEPicks2J");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick4"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            thirdPick = enemyComp["Pick1"];
            fourthPick = enemyComp["Pick2"];
            fifthPick = enemyComp["Pick3"];
            Debug.Log("cm.ShuffleEnemyPicks2 declares match with enemyComp[Pick4] and [Pick5]");
            enemyCompReady = "yes";
        }
        else
        {
            Debug.Log("Something wrong with ShuffleEPicks2");
        }
    }
    private void ShuffleEnemyPicks3()
    {
        Debug.Log("cm.ShuffleEnemyPicks3 reached.");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])))
        {
            fourthPick = enemyComp["Pick4"];
            fifthPick = enemyComp["Pick5"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks3B();
        }
    }
    private void ShuffleEPicks3B()
    {
        Debug.Log("Method reached:   ShuffleEPicks3B");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])))
        {
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick5"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks3C();
        }
    }
    private void ShuffleEPicks3C()
    {
        Debug.Log("Method reached:   ShuffleEPicks3C");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fourthPick = enemyComp["Pick3"];
            fifthPick = enemyComp["Pick4"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks3D();
        }
    }
    private void ShuffleEPicks3D()
    {
        Debug.Log("Method reached:   ShuffleEPicks3D");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])))
        {
            fourthPick = enemyComp["Pick1"];
            fifthPick = enemyComp["Pick5"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks3E();
        }
    }
    private void ShuffleEPicks3E()
    {
        Debug.Log("Method reached:   ShuffleEPicks3E");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fourthPick = enemyComp["Pick1"];
            fifthPick = enemyComp["Pick4"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks3F();
        }
    }
    private void ShuffleEPicks3F()
    {
        Debug.Log("Method reached:   ShuffleEPicks3F");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fourthPick = enemyComp["Pick1"];
            fifthPick = enemyComp["Pick3"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks3G();
        }
    }
    private void ShuffleEPicks3G()
    {
        Debug.Log("Method reached:   ShuffleEPicks3G");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fourthPick = enemyComp["Pick1"];
            fifthPick = enemyComp["Pick2"];
            enemyCompReady = "yes";
        }
        else
        {
            Debug.Log("Something wrong with ShuffleEPicks3");
        }
    }
    private void ShuffleEnemyPicks4()
    {
        Debug.Log("Method reached:   ShuffleEnemyPicks4");
        Debug.Log("cm.ShuffleEnemyPicks4 reached.");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])))
        {
            fifthPick = enemyComp["Pick5"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks4B();
        }
    }
    private void ShuffleEPicks4B()
    {
        Debug.Log("Method reached:   ShuffleEPicks4B");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fifthPick = enemyComp["Pick4"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks4C();
        }
    }
    private void ShuffleEPicks4C()
    {
        Debug.Log("Method reached:   ShuffleEPicks4C");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fifthPick = enemyComp["Pick3"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks4D();
        }
    }
    private void ShuffleEPicks4D()
    {
        Debug.Log("Method reached:   ShuffleEPicks4D");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick1"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fifthPick = enemyComp["Pick2"];
            enemyCompReady = "yes";
        }
        else
        {
            ShuffleEPicks4E();
        }
    }
    private void ShuffleEPicks4E()
    {
        Debug.Log("Method reached:   ShuffleEPicks4E");
        if ((finalEnemyComp.ContainsValue(enemyComp["Pick2"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick3"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick4"])) && (finalEnemyComp.ContainsValue(enemyComp["Pick5"])))
        {
            fifthPick = enemyComp["Pick1"];
            enemyCompReady = "yes";
        }
        else
        {
            Debug.Log("Something wrong with ShuffleEPicks4");
        }
    }

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
            Debug.Log("Enemy " + draftStage + " locked " + EBan1);
            finalEnemyComp["Ban1"] = EBan1;
        }
        if ((draftStage == "Ban3") || (draftStage == "Ban4"))
        {
            EBan2 = enemyComp["Ban2"];
            portraitAssetPath = "HotSPortraits/" + EBan2;
            EnemyBan2.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EBan2 + "Pic";
            Debug.Log("Enemy " + draftStage + " locked " + EBan2);
            finalEnemyComp["Ban2"] = EBan2;
        }
        NewStageVariableClear();
    }

    public void NewStageVariableClear()
    {
        chosenHero = "none";
        activeTeam = "none";
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
            playerTeam["Pick1"] = P1Pick1;
            PlayerPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick1);
        }
        if (draftStage == "Pick6")
        {
            P1Pick3 = chosenHero;
            playerTeam["Pick3"] = P1Pick3;
            PlayerPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick3);
        }
        if (draftStage == "Pick10")
        {
            P1Pick5 = chosenHero;
            playerTeam["Pick5"] = P1Pick5;
            PlayerPick5.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick5);
        }
        NewStageVariableClear();
    }

    public void EnemySinglePickPicPortrait()
    {
        draftStage = GM.draftStage;
        EnemySinglePickText.SetActive(false);
        if (draftStage == "Pick1")
        {
            EPick1 = firstPick;
            portraitAssetPath = "HotSPortraits/" + EPick1;
            EnemyPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EPick1 + "Pic";
            Debug.Log("Enemy " + draftStage + " locked " + EPick1);
            finalEnemyComp["Pick1"] = EPick1;
        }
        if (draftStage == "Pick6")
        {
            EPick3 = thirdPick;
            portraitAssetPath = "HotSPortraits/" + EPick3;
            EnemyPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EPick3 + "Pic";
            Debug.Log("Enemy " + draftStage + " locked " + EPick3);
            finalEnemyComp["Pick3"] = EPick3;
        }
        if (draftStage == "Pick10")
        {
            EPick5 = fifthPick;
            portraitAssetPath = "HotSPortraits/" + EPick5;
            EnemyPick5.sprite = Resources.Load<Sprite>(portraitAssetPath);
            picAssetPath = "HotsPics/" + EPick5 + "Pic";
            Debug.Log("Enemy " + draftStage + " locked " + EPick5);
            finalEnemyComp["Pick5"] = EPick5;
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
            playerTeam["Pick1"] = P1Pick1;
            PlayerPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick1);
        }
        if (draftStage == "Pick45")
        {
            P1Pick2 = chosenHero;
            playerTeam["Pick2"] = P1Pick2;
            PlayerPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick2);
        }
        if (draftStage == "Pick67")
        {
            P1Pick3 = chosenHero;
            playerTeam["Pick3"] = P1Pick3;
            PlayerPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick3);
        }
        if (draftStage == "Pick89")
        {
            P1Pick4 = chosenHero;
            playerTeam["Pick4"] = P1Pick4;
            PlayerPick4.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick4);
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
            playerTeam["Pick2"] = P1Pick2;
            PlayerPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick2);
        }
        if (draftStage == "Pick45")
        {
            P1Pick3 = chosenHero;
            playerTeam["Pick3"] = P1Pick3;
            PlayerPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick3);
        }
        if (draftStage == "Pick67")
        {
            P1Pick4 = chosenHero;
            playerTeam["Pick4"] = P1Pick4;
            PlayerPick4.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick4);
        }
        if (draftStage == "Pick89")
        {
            P1Pick5 = chosenHero;
            playerTeam["Pick5"] = P1Pick5;
            PlayerPick5.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Player " + draftStage + " locked " + P1Pick5);
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
            EPick1 = firstPick;
            portraitAssetPath = "HotSPortraits/" + EPick1;
            picAssetPath = "HotsPics/" + EPick1 + "Pic";
            EnemyPick1.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick1);
            finalEnemyComp["Pick1"] = EPick1;

        }
        if (draftStage == "Pick45")
        {
            EPick2 = secondPick;
            portraitAssetPath = "HotSPortraits/" + EPick2;
            picAssetPath = "HotsPics/" + EPick2 + "Pic";
            EnemyPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick2);
            finalEnemyComp["Pick2"] = EPick2;
        }
        if (draftStage == "Pick67")
        {
            EPick3 = thirdPick;
            portraitAssetPath = "HotSPortraits/" + EPick3;
            picAssetPath = "HotsPics/" + EPick3 + "Pic";
            EnemyPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick3);
            finalEnemyComp["Pick3"] = EPick3;
        }
        if (draftStage == "Pick89")
        {
            EPick4 = fourthPick;
            portraitAssetPath = "HotSPortraits/" + EPick4;
            picAssetPath = "HotsPics/" + EPick4 + "Pic";
            EnemyPick4.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick4);
            finalEnemyComp["Pick4"] = EPick4;
        }
        else { }
    }

    public void EnemySecondDoublePickPicPortrait()
    {
        if (draftStage == "Pick23")
        {
            EPick2 = secondPick;
            portraitAssetPath = "HotSPortraits/" + EPick2;
            picAssetPath = "HotsPics/" + EPick2 + "Pic";
            EnemyPick2.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick2);
            finalEnemyComp["Pick2"] = EPick2;
        }
        if (draftStage == "Pick45")
        {
            EPick3 = thirdPick;
            portraitAssetPath = "HotSPortraits/" + EPick3;
            picAssetPath = "HotsPics/" + EPick3 + "Pic";
            EnemyPick3.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick3);
            finalEnemyComp["Pick3"] = EPick3;
        }
        if (draftStage == "Pick67")
        {
            EPick4 = fourthPick;
            portraitAssetPath = "HotSPortraits/" + EPick4;
            picAssetPath = "HotsPics/" + EPick4 + "Pic";
            EnemyPick4.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick4);
            finalEnemyComp["Pick4"] = EPick4;
        }
        if (draftStage == "Pick89")
        {
            EPick5 = fifthPick;
            portraitAssetPath = "HotSPortraits/" + EPick5;
            picAssetPath = "HotsPics/" + EPick5 + "Pic";
            EnemyPick5.sprite = Resources.Load<Sprite>(portraitAssetPath);
            Debug.Log("Enemy " + draftStage + " locked " + EPick5);
            finalEnemyComp["Pick5"] = EPick5;
        }

        NewStageVariableClear();
    }

    private void ChooseRandomHeroes()
    {
        enemyComp.Clear();
        enemyComp["Ban1"] = EBan1;
        enemyComp["Ban2"] = EBan2;
        enemyComp["Pick1"] = EPick1;
        enemyComp["Pick2"] = EPick2;
        enemyComp["Pick3"] = EPick3;
        enemyComp["Pick4"] = EPick4;
        enemyComp["Pick5"] = EPick5;
        Debug.Log("ChooseRandomHeroes:  Current enemyComp consists in:  Ban1=" + enemyComp["Ban1"] + ", Ban2=" + enemyComp["Ban2"] + ", Pick1=" + enemyComp["Pick1"] + ", Pick2=" + enemyComp["Pick2"] + ", Pick3=" + enemyComp["Pick3"] + ", Pick4=" + enemyComp["Pick4"] + ", Pick5=" + enemyComp["Pick5"]);
        ERandomCheckBan2();
    }

    private void ERandomCheckBan2()
    {
        Debug.Log("Method reached:   ERandomCheckBan2");
        if (enemyComp["Ban2"] == "none")
        {
            GetRandomBan2();
        }
        else
        {
            ERandomCheckPick2();
        }
    }

    private void ERandomCheckPick2()
    {
        Debug.Log("Method reached:   ERandomCheckPick2");
        if (enemyComp["Pick2"] == "none")
        {
            needPick2 = true;
            GetRandomPick();
        }
        else
        {
            ERandomCheckPick3();
        }
    }

    private void ERandomCheckPick3()
    {
        Debug.Log("Method reached:   ERandomCheckPick3");
        if (enemyComp["Pick3"] == "none")
        {
            needPick3 = true;
            GetRandomPick();
        }
        else
        {
            ERandomCheckPick4();
        }
    }

    private void ERandomCheckPick4()
    {
        Debug.Log("Method reached:   ERandomCheckPick4");
        if (enemyComp["Pick4"] == "none")
        {
            needPick4 = true;
            GetRandomPick();
        }
        else
        {
            ERandomCheckPick5();
        }
    }

    private void ERandomCheckPick5()
    {
        Debug.Log("Method reached:   ERandomCheckPick5");
        if (enemyComp["Pick5"] == "none")
        {
            needPick5 = true;
            GetRandomPick();
        }
        else
        {
            enemyCompReady = "yes";
        }
    }

    private void GetRandomBan2()
    {
        randomNumber = Random.Range(1, 21);
        if (randomNumber == 1)
            randomHero = "Muradin";
        if (randomNumber == 2)
            randomHero = "ETC";
        if (randomNumber == 3)
            randomHero = "Tyrael";
        if (randomNumber == 4)
            randomHero = "Diablo";
        if (randomNumber == 5)
            randomHero = "Ragnaros";
        if (randomNumber == 6)
            randomHero = "Arthas";
        if (randomNumber == 7)
            randomHero = "Anub'arak";
        if (randomNumber == 8)
            randomHero = "Auriel";
        if (randomNumber == 9)
            randomHero = "Brightwing";
        if (randomNumber == 10)
            randomHero = "Kharazim";
        if (randomNumber == 11)
            randomHero = "Li Li";
        if (randomNumber == 12)
            randomHero = "Lt Morales";
        if (randomNumber == 13)
            randomHero = "L\u00FAcio";
        if (randomNumber == 14)
            randomHero = "Malfurion";
        if (randomNumber == 15)
            randomHero = "Rehgar";
        if (randomNumber == 16)
            randomHero = "Valla";
        if (randomNumber == 17)
            randomHero = "Tassadar";
        if (randomNumber == 18)
            randomHero = "Tyrande";
        if (randomNumber == 19)
            randomHero = "Tracer";
        if (randomNumber == 20)
            randomHero = "Zarya";

        Debug.Log("GetRandomBan2 has randomly selected " + randomHero);
        ERandomSetBan2();
    }
    private void ERandomSetBan2()
    {
        if (playerTeam.ContainsValue(randomHero) ||
            enemyComp.ContainsValue(randomHero))
        {
            Debug.Log("ERandomSetBan2 is trying again.");
            GetRandomBan2();
        }
        else
        {
            enemyComp["Ban2"] = randomHero;
            Debug.Log("ERandomSetBan2 has added " + randomHero + " to enemyComp[Ban2]");
            ERandomCheckPick2(); 
        }
    }

    private void GetRandomPick()
    {
        Debug.Log("Method reached:  GetRandomPick");
        if (enemyComp.ContainsValue("Muradin") || enemyComp.ContainsValue("ETC") || enemyComp.ContainsValue("Tyrael") || enemyComp.ContainsValue("Diablo") || enemyComp.ContainsValue("Johanna") || enemyComp.ContainsValue("Arthas") || enemyComp.ContainsValue("Anub'arak"))
        {
            Debug.Log("GetRandomPick:  Tank already on team.  Going to GetRandomPickSupport");
            GetRandomPickSupport();
        }
        else
        {
            randomNumber = Random.Range(1, 8);
            if (randomNumber == 1)
                randomHero = "Muradin";
            if (randomNumber == 2)
                randomHero = "ETC";
            if (randomNumber == 3)
                randomHero = "Tyrael";
            if (randomNumber == 4)
                randomHero = "Diablo";
            if (randomNumber == 5)
                randomHero = "Johanna";
            if (randomNumber == 6)
                randomHero = "Arthas";
            if (randomNumber == 7)
                randomHero = "Anub'arak";

            Debug.Log("enemyComp tank chosen:  " + randomHero);
            ERandomCheckNeeded();
        }
    }

    private void GetRandomPickSupport()
    {
        Debug.Log("Method reached:  GetRandomPickSupport");
        if (enemyComp.ContainsValue("Auriel") || enemyComp.ContainsValue("Brightwing") || enemyComp.ContainsValue("Kharazim") || enemyComp.ContainsValue("Li Li") || enemyComp.ContainsValue("Lt Morales") || enemyComp.ContainsValue("L\u00FAcio") || enemyComp.ContainsValue("Malfurion") || enemyComp.ContainsValue("Rehgar") || enemyComp.ContainsValue("Uther"))
        {
            Debug.Log("GetRandomPickSupport says enemyComp already contains support.  Checking for DPS");
            GetRandomPickDPS();
        }
        else
        {
            randomNumber = Random.Range(1, 10);
            if (randomNumber == 1)
                randomHero = "Auriel";
            if (randomNumber == 2)
                randomHero = "Brightwing";
            if (randomNumber == 3)
                randomHero = "Kharazim";
            if (randomNumber == 4)
                randomHero = "Li Li";
            if (randomNumber == 5)
                randomHero = "Lt Morales";
            if (randomNumber == 6)
                randomHero = "L\u00FAcio";
            if (randomNumber == 7)
                randomHero = "Malfurion";
            if (randomNumber == 8)
                randomHero = "Rehgar";
            if (randomNumber == 9)
                randomHero = "Uther";

            Debug.Log("enemyComp support chosen:  " + randomHero);
            ERandomCheckNeeded();
        }
    }

    private void GetRandomPickDPS()
    {
        Debug.Log("Method reached:  GetRandomPickDPS");
        randomNumber = Random.Range(1, 36);
        if (randomNumber == 1)
            randomHero = "Nazeebo";
        if (randomNumber == 2)
            randomHero = "Ragnaros";
        if (randomNumber == 3)
            randomHero = "Tychus";
        if (randomNumber == 4)
            randomHero = "Li-Ming";
        if (randomNumber == 5)
            randomHero = "Chromie";
        if (randomNumber == 6)
            randomHero = "Sonya";
        if (randomNumber == 7)
            randomHero = "Artanis";
        if (randomNumber == 8)
            randomHero = "Falstad";
        if (randomNumber == 9)
            randomHero = "Lunara";
        if (randomNumber == 10)
            randomHero = "Gul'dan";
        if (randomNumber == 11)
            randomHero = "Alarak";
        if (randomNumber == 12)
            randomHero = "Dehaka";
        if (randomNumber == 13)
            randomHero = "Jaina";
        if (randomNumber == 14)
            randomHero = "Valla";
        if (randomNumber == 15)
            randomHero = "Zeratul";
        if (randomNumber == 16)
            randomHero = "Zul'jin";
        if (randomNumber == 17)
            randomHero = "Tassadar";
        if (randomNumber == 18)
            randomHero = "Tyrande";
        if (randomNumber == 19)
            randomHero = "Tracer";
        if (randomNumber == 20)
            randomHero = "Zarya";
        if (randomNumber == 21)
            randomHero = "Murky";
        if (randomNumber == 22)
            randomHero = "Lost Vikings";
        if (randomNumber == 23)
            randomHero = "Probius";
        if (randomNumber == 24)
            randomHero = "Varian";
        if (randomNumber == 25)
            randomHero = "Kael'thas";
        if (randomNumber == 26)
            randomHero = "Greymane";
        if (randomNumber == 27)
            randomHero = "Illidan";
        if (randomNumber == 28)
            randomHero = "Valeera";
        if (randomNumber == 29)
            randomHero = "The Butcher";
        if (randomNumber == 30)
            randomHero = "Xul";
        if (randomNumber == 31)
            randomHero = "Thrall";
        if (randomNumber == 32)
            randomHero = "Kerrigan";
        if (randomNumber == 33)
            randomHero = "Sylvanas";
        if (randomNumber == 34)
            randomHero = "Medivh";
        if (randomNumber == 35)
            randomHero = "Abathur";

        Debug.Log("enemyComp DPS chosen:  " + randomHero);
        ERandomCheckNeeded();
    }

    private void ERandomCheckNeeded()
    {
        Debug.Log("Method reached:  ERandomCheckNeeded");
        if (enemyComp.ContainsValue(randomHero) ||
            playerTeam.ContainsValue(randomHero))
        {
            Debug.Log("Either enemyComp or playerTeam contains " + randomHero);
            GetRandomPick();
        }
        else
        {
            if (needPick2)
            {
                enemyComp["Pick2"] = randomHero;
                Debug.Log("enemyComp[Pick2] = " + randomHero);
                secondPick = randomHero;
                needPick2 = false;
                ERandomCheckPick3();
            }
            if (needPick3)
            {
                enemyComp["Pick3"] = randomHero;
                thirdPick = randomHero;
                Debug.Log("enemyComp[Pick3] = " + randomHero);
                needPick3 = false;
                ERandomCheckPick4();
            }
            if (needPick4)
            {
                enemyComp["Pick4"] = randomHero;
                fourthPick = randomHero;
                Debug.Log("enemyComp[Pick4] = " + randomHero);
                needPick4 = false;
                ERandomCheckPick5(); 
            }
            if (needPick5)
            {
                enemyComp["Pick5"] = randomHero;
                fifthPick = randomHero;
                Debug.Log("enemyComp[Pick5] = " + randomHero);
                needPick5 = false;
                randomHero = "none";
                enemyCompReady = "yes";
                Debug.Log("ERandomCheckNeeded:  Chosen team is now:  " + enemyComp["Pick1"] + ", " + enemyComp["Pick2"] + ", " + enemyComp["Pick3"] + ", " + enemyComp["Pick4"] + ", " + enemyComp["Pick5"] + ", and is banning " + enemyComp["Ban2"]);
            }
        }
    }

    public void DebugTeams()
    {
        string playerTeamList = P1Pick1 + ", " + P1Pick2 + ", " + P1Pick3 + ", " + P1Pick4 + ", " + P1Pick5 + " with " + P1Ban1 + " and " + P1Ban2 + " banned.";
        Debug.Log("Current playerTeam Listing is:  " + playerTeamList);
        string enemyCompList = EPick1 + ", " + EPick2 + ", " + EPick3 + ", " + EPick4 + ", " + EPick5 + " with " + EBan1 + " and " + EBan2 + " banned.";
        Debug.Log("Current enemyComp Listing is:  " + enemyCompList);
    }
}
