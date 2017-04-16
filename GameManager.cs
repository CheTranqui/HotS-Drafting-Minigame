using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    public CoreMethods cm;
    public DropdownMapSwitcher ddm;
    public GameObject DropDownMap;
    public GameObject HeroSelectionPanel;
    public GameObject PlayerPicksBansPanel;
    public GameObject EnemyPicksBansPanel;
    public GameObject fullPicMiddle;
    public GameObject EnemyBanText;
    public GameObject ChooseBattlegroundText;
    public GameObject EnemySinglePickText;
    public GameObject EnemyDoublePickText;
    public GameObject PlayerDoublePickText;
    public GameObject PlayerSecondDoublePickText;
    public GameObject fullPicLeft;
    public GameObject fullPicRight;
    public GameObject FullMiddlePanel;
    public HeroSelectScript hs;
    public Button StartButton;
    public Button CreditsButton;
    public string myChosenHero;
    public string draftMap;
    public string myEnemyCompReady;
    public string draftStage;
    public string activeTeam;

    private void Start()
    {
        draftStage = "none";
        draftMap = "none";
    }

    public void StartTheDraft()
    {
        Debug.Log("GM = StartTheDraft()");
        draftMap = "none";
        draftStage = "none";
        myChosenHero = "none";
        HeroSelectScript.chosenHero = "none";
        cm = gameObject.GetComponent<CoreMethods>();
        ddm = DropDownMap.GetComponent<DropdownMapSwitcher>();
        ddm.Start();
        StartButton.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        FullMiddlePanel.SetActive(true);
        CheckForMap();
        cm.StartDraft();
    }

    public void CheckForMap()
    {
        draftMap = ddm.map;
        if (draftMap == "none")
        {
            ChooseBattlegroundText.SetActive(true);
            StartCoroutine(WaitForMap());
        }
        else
        {
            ChooseBattlegroundText.SetActive(false);
            HeroSelectionPanel.SetActive(true);
            cm.DisplayWhoGoesFirst();
            StartCoroutine(PauseForWhoGoesFirst());
        }
    }
    IEnumerator WaitForMap()
    {
        yield return new WaitForSeconds(1.0f);
        CheckForMap();
    }

    IEnumerator PauseForWhoGoesFirst()
    {
        Debug.Log("GM = PauseForWhoGoesFirst()");
        yield return new WaitForSeconds(1.0f);
        PlayerPicksBansPanel.SetActive(true);
        EnemyPicksBansPanel.SetActive(true);
        draftStage = "new";
        MidDraftLandingSite();
    }

    public void MidDraftLandingSite()
    {
        Debug.Log("MidDraftLandingSite says:  Previous draftStage = " + draftStage);
        cm.DeactivateClassButtons();
        cm.NewStageVariableClear();
        myEnemyCompReady = "no";
        myChosenHero = "none";

        if (draftStage == "Pick10")
        {
            draftStage = "Complete";
            DraftComplete();
        }
        if (draftStage == "Pick89")
        {
            draftStage = "Pick10";
            SinglePickStart();
        }
        if (draftStage == "Pick67")
        {
            draftStage = "Pick89";
            DoublePickStart();
        }
        if (draftStage == "Ban4")
        {
            draftStage = "Pick67";
            DoublePickStart();
        }
        if (draftStage == "Ban3")
        {
            draftStage = "Ban4";
            BanStart();
        }
        if (draftStage == "Pick45")
        {
            draftStage = "Ban3";
            BanStart();
        }
        if (draftStage == "Pick23")
        {
            draftStage = "Pick45";
            DoublePickStart();
        }
        if (draftStage == "Pick1")
        {
            draftStage = "Pick23";
            DoublePickStart();
        }
        if (draftStage == "Ban2")
        {
            draftStage = "Pick1";
            SinglePickStart();
        }
        if (draftStage == "Ban1")
        {
            draftStage = "Ban2";
            BanStart();
        }
        if (draftStage == "new")
        {
            draftStage = "Ban1";
            FirstBan();
        }
        Debug.Log("MidDraftLandingSite sending us to new draftStage:  " + draftStage);
    }

    private void FirstBan()
    {
        cm.SetCMMap();
        cm.OpeningBan();
        cm.GetEnemyComp();
        BanStart();
    }

    private void BanStart()
    {
        Debug.Log("GM = BanStart()");
        cm.ActiveTeamCheck();
        activeTeam = cm.activeTeam;
        cm.DisplayBanText();
        BanHeroCheck();   
    }

    IEnumerator BanHeroCheckWait()
    {
        yield return new WaitForSeconds(0.5f);
        myChosenHero = HeroSelectScript.chosenHero;
        BanHeroCheck();
    }

    public void BanHeroCheck()
    {
        if (activeTeam == "Player")
        {
            if (myChosenHero == "none")
            {
                StartCoroutine(BanHeroCheckWait());
            }
            else
            {
                cm.PlayerBanChosen();
                cm.ActivateFullMiddlePic();
                cm.DeactivateClassButtons();
                HeroSelectScript.chosenHero = "none";
                StartCoroutine(BanPicPauseAndDeactivate());
            }
        }
        else
        {
            EnemyBanCompCheck();
        }
    }
    public void EnemyBanCheck()
    {
        myEnemyCompReady = cm.enemyCompReady;
        Debug.Log("GM says:   myEnemyCompReady = " + myEnemyCompReady);
        if (myEnemyCompReady == "no")
        {
            EnemyBanText.SetActive(true);
            EnemyBanCompCheck();
        }
        if (myEnemyCompReady == "checking")
        {
            EnemyBanText.SetActive(true);
            StartCoroutine(EnemyBanCheckWait());
        }
        if (myEnemyCompReady == "yes")
        {
            StartCoroutine(EnemyBanLockWait());
        }
    }

    IEnumerator EnemyBanCheckWait()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyBanCheck();
    }

    private void EnemyBanCompCheck()
    {
        Debug.Log("GM = EnemyBanCompCheck()");
        cm.ConfirmComp();
        StartCoroutine(EnemyBanCheckWait());
    }

    IEnumerator EnemyBanLockWait()
    {
        Debug.Log("GM = EnemyBanLockWait()");
        yield return new WaitForSeconds(1.0f);
        EnemyBanLock();
    }

    public void EnemyBanLock()
    {
        Debug.Log("GM = EnemyBanLock()");
        cm.EnemyBanPicPortrait();
        cm.ActivateFullMiddlePic();
        StartCoroutine(BanPicPauseAndDeactivate());
    }

    IEnumerator BanPicPauseAndDeactivate()
    {
        Debug.Log("GM = BanPicPauseAndDeactivate()");
        yield return new WaitForSeconds(2f);
        fullPicMiddle.SetActive(false);
        MidDraftLandingSite();
    }

private void SinglePickStart()
    {
        cm.ActiveTeamCheck();
        activeTeam = cm.activeTeam;
        cm.SinglePickStartText();
        SinglePickHeroCheck();
    }

    IEnumerator SinglePickHeroCheckWait()
    {
        yield return new WaitForSeconds(0.5f);
        myChosenHero = HeroSelectScript.chosenHero;
        SinglePickHeroCheck();
    }

    public void SinglePickHeroCheck()
    {
        if (activeTeam == "Player")
        {
            if (myChosenHero == "none")
            {
                StartCoroutine(SinglePickHeroCheckWait());
            }
            else
            {
                cm.PlayerSinglePickHeroChosen();
                cm.ActivateFullMiddlePic();
                cm.DeactivateClassButtons();
                HeroSelectScript.chosenHero = "none";
                StartCoroutine(SinglePickPicPauseAndDeactivate());
            }
        }
        else
        {
            SinglePickEnemyConfirm();
        }
    }

    public void SinglePickEnemyCheck()
    {
        myEnemyCompReady = cm.enemyCompReady;
        Debug.Log("GM.SinglePickEnemyCheck says myEnemyCompReady = " + myEnemyCompReady);
        if (myEnemyCompReady == "no")
        {
            EnemySinglePickText.SetActive(true);
            SinglePickEnemyConfirm();
        }
        if (myEnemyCompReady == "checking")
        {
            EnemySinglePickText.SetActive(true);
            StartCoroutine(SinglePickEnemyCheckWait());
        }
        if (myEnemyCompReady == "yes")
        {
            StartCoroutine(SinglePickEnemyLockWait());
        }
    }

    IEnumerator SinglePickEnemyCheckWait()
    {
        yield return new WaitForSeconds(1.5f);
        SinglePickEnemyCheck();
    }

    private void SinglePickEnemyConfirm()
    {
        cm.ConfirmComp();
        StartCoroutine(SinglePickEnemyCheckWait());
    }

    IEnumerator SinglePickEnemyLockWait()
    {
        yield return new WaitForSeconds(1.5f);
        SinglePickEnemyLock();
    }

    public void SinglePickEnemyLock()
    {
        cm.EnemySinglePickPicPortrait();
        cm.ActivateFullMiddlePic();
        StartCoroutine(SinglePickPicPauseAndDeactivate());
    }

    IEnumerator SinglePickPicPauseAndDeactivate()
    {
        yield return new WaitForSeconds(2.2f);
        fullPicMiddle.SetActive(false);
        cm.DebugTeams();
        MidDraftLandingSite();
    }

    public void DoublePickStart()
    { 
        cm.ActiveTeamCheck();
        myChosenHero = "none";
        HeroSelectScript.chosenHero = "none";
        activeTeam = cm.activeTeam;
        cm.DeactivateClassButtons();
        cm.DoublePickStartText();
        DoublePickHeroCheck();
    }

    IEnumerator DoublePickHeroCheckWait()
    {
        yield return new WaitForSeconds(0.5f);
        myChosenHero = HeroSelectScript.chosenHero;
        DoublePickHeroCheck();
    }

    public void DoublePickHeroCheck()
    {
        if (activeTeam == "Player")
        {
            if (myChosenHero == "none")
            {
                StartCoroutine(DoublePickHeroCheckWait());
            }
            else
            {
                cm.PlayerDoublePickFirstHeroChosen();
                cm.ActivateFullLeftPic();
                myChosenHero = "none";
                HeroSelectScript.chosenHero = "none";
                DoublePickSecondHeroCheck();
            }
        }
        else
        {
            DoublePickEnemyConfirm();
        }
    }

    public void DoublePickEnemyCheck()
    {
        myEnemyCompReady = cm.enemyCompReady;
        Debug.Log("GM.DoublePickEnemyCheck says myEnemyCompReady = " + myEnemyCompReady);
        if (myEnemyCompReady == "no")
        {
            EnemyDoublePickText.SetActive(true);
            DoublePickEnemyConfirm();
        }
        if (myEnemyCompReady == "checking")
        {
            EnemyDoublePickText.SetActive(true);
            StartCoroutine(DoublePickEnemyCheckWait());
        }
        if (myEnemyCompReady == "yes")
        {
            StartCoroutine(DoublePickEnemyLockWait());
        }
    }

    private void DoublePickEnemyConfirm()
    {
        cm.ConfirmComp();
        StartCoroutine(DoublePickEnemyCheckWait());
    }

    IEnumerator DoublePickEnemyCheckWait()
    {
        yield return new WaitForSeconds(1.5f);
        DoublePickEnemyCheck();
    }

    public void DoublePickSecondHeroCheck()
    {
        myChosenHero = HeroSelectScript.chosenHero;
        if (myChosenHero == "none")
        {
            StartCoroutine(DoublePickSecondHeroCheckWait());
        }
        else
        {
            PlayerSecondDoublePickText.SetActive(false);
            cm.PlayerDoublePickSecondHeroChosen();
            cm.ActivateFullRightPic();
            StartCoroutine(DoublePickPicPauseAndDeactivate());
        }

    }

    IEnumerator DoublePickSecondHeroCheckWait()
    {
        yield return new WaitForSeconds(0.5f);
        DoublePickSecondHeroCheck();
    }

    IEnumerator DoublePickEnemyLockWait()
    {
        yield return new WaitForSeconds(1f);
        DoublePickEnemyLock1();
        StartCoroutine(DoublePickEnemySecondLockWait());
    }

    IEnumerator DoublePickEnemySecondLockWait()
    {
        yield return new WaitForSeconds(1.0f);
        DoublePickEnemyLock2();
    }

    public void DoublePickEnemyLock1()
    {
        cm.EnemyFirstDoublePickPicPortrait();
        cm.ActivateFullLeftPic();
    }

    public void DoublePickEnemyLock2()
    {
        cm.EnemySecondDoublePickPicPortrait();
        cm.ActivateFullRightPic();
        StartCoroutine(DoublePickPicPauseAndDeactivate());
    }


    IEnumerator DoublePickPicPauseAndDeactivate()
    {
        yield return new WaitForSeconds(2.2f);
        fullPicLeft.SetActive(false);
        fullPicRight.SetActive(false);
        cm.DebugTeams();
        MidDraftLandingSite();
    }

    public void DraftComplete()
    {

    }
}
