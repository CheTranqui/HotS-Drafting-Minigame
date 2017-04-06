using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

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
    public Button StartButton;
    public Button CreditsButton;
    public string myChosenHero;
    public string draftMap;
    private string myEnemyCompReady;
    public string draftStage;
    public string activeTeam;

    private void Start()
    {
        draftStage = "none";
        draftMap = "none";
    }

    public void StartTheDraft()
    {
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
        yield return new WaitForSeconds(0.5f);
        CheckForMap();
    }

    IEnumerator PauseForWhoGoesFirst()
    {
        yield return new WaitForSeconds(1);
        PlayerPicksBansPanel.SetActive(true);
        EnemyPicksBansPanel.SetActive(true);
        draftStage = "new";
        MidDraftLandingSite();
    }

    public void MidDraftLandingSite()
    {
        Debug.Log("MidDraftLandingSite says:  Current draftStage = " + draftStage);
        myChosenHero = "none";
        HeroSelectScript.chosenHero = "none";
        cm.DeactivateClassButtons();
        cm.NewStageVariableClear();

        if (draftStage == "Pick910")
        {
            draftStage = "Complete";
            DraftComplete();
        }
        if (draftStage == "Pick78")
        {
            draftStage = "Pick910";
            DoublePickStart();
        }
        if (draftStage == "Pick6")
        {
            draftStage = "Pick78";
            DoublePickStart();
        }
        if (draftStage == "Ban4")
        {
            draftStage = "Pick6";
            SinglePickStart();
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
        else { }
        Debug.Log("MidDraftLandingSite sending us to:  " + draftStage);
    }

    private void FirstBan()
    {
        draftStage = "Ban1";
        cm.GetMap();
        cm.OpeningBan();
        BanStart();
    }

    private void BanStart()
    {
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
        Debug.Log("GM activeTeam = " + activeTeam);
        if (activeTeam == "Player")
        {
            Debug.Log("GM = FirstBanHeroCheck() + activeTeam = Player");
            if (myChosenHero == "none")
            {
                StartCoroutine(BanHeroCheckWait());
            }
            else
            {
                cm.PlayerBanChosen();
                cm.ActivateFullMiddlePic();
                cm.DeactivateClassButtons();
                StartCoroutine(BanPicPauseAndDeactivate());
            }
        }
        else
        {
            Debug.Log("GM = FirstBanHeroCheck() + activeTeam = Enemy");
            EnemyBanCheck();
        }
    }
    public void EnemyBanCheck()
    {
        myEnemyCompReady = cm.enemyCompReady;
        Debug.Log("GM says:   myEnemyCompReady = " + myEnemyCompReady);
        if (myEnemyCompReady == "no")
        {
            EnemyBanText.SetActive(true);
            StartCoroutine(EnemyBanWait());
        }
        else
        {
            StartCoroutine(EnemyBanLockWait());
        }
    }

    IEnumerator EnemyBanWait()
    {
        cm.GetEnemyComp();
        yield return new WaitForSeconds(0.6f);
        cm.ConfirmComp();
        EnemyBanCheck();
    }

    IEnumerator EnemyBanLockWait()
    {
        yield return new WaitForSeconds(1f);
        EnemyBanLock();
    }

    public void EnemyBanLock()
    {
        cm.EnemyBanPicPortrait();
        cm.ActivateFullMiddlePic();
        StartCoroutine(BanPicPauseAndDeactivate());
    }

    IEnumerator BanPicPauseAndDeactivate()
    {
        yield return new WaitForSeconds(2.2f);
        fullPicMiddle.SetActive(false);
        MidDraftLandingSite();
    }

    /* private void SecondBan()
    {
        cm.DebugTeams();
        draftStage = "Ban2";
        cm.ActiveTeamCheck();
        myChosenHero = "none";
        HeroSelectScript.chosenHero = "none";
        activeTeam = cm.activeTeam;
        cm.BanStart();
        cm.DeactivateClassButtons();
        SecondBanHeroCheck();
    }

    IEnumerator SecondBanHeroCheckWait()
    {
        yield return new WaitForSeconds(0.5f);
        myChosenHero = HeroSelectScript.chosenHero;
        SecondBanHeroCheck();
    }

    public void SecondBanHeroCheck()
    {
        cm.ConfirmComp();
        if (activeTeam == "Player")
        {
            Debug.Log("GM = SecondBanHeroCheck() + activeTeam = Player");
            if (myChosenHero == "none")
            {
                StartCoroutine(SecondBanHeroCheckWait());
            }
            else
            {
                cm.PlayerBanChosen();
                cm.ActivateFullMiddlePic();
                cm.DeactivateClassButtons();
                StartCoroutine(SecondBanPicPauseAndDeactivate());
            }
        }
        else
        {
            Debug.Log("GM = SecondBanHeroCheck() + activeTeam = Enemy");
            myEnemyCompReady = cm.enemyCompReady;
            if (myEnemyCompReady == "no")
            {
                EnemyBanText.SetActive(true);
                cm.GetEnemyComp();
                StartCoroutine(SecondBanEnemyWait());
            }
            else
            {
                StartCoroutine(SecondBanEnemyLockWait());
            }
        }
    }

    IEnumerator SecondBanEnemyWait()
    {
        yield return new WaitForSeconds(0.3f);
        cm.ConfirmComp();
        yield return new WaitForSeconds(0.3f);
        SecondBanHeroCheck();
    }

    IEnumerator SecondBanEnemyLockWait()
    {
        yield return new WaitForSeconds(1.5f);
        SecondBanEnemyLock();
    }

    public void SecondBanEnemyLock()
    {
        cm.EnemyBanPicPortrait();
        cm.ActivateFullMiddlePic();
        StartCoroutine(SecondBanPicPauseAndDeactivate());
    }

    IEnumerator SecondBanPicPauseAndDeactivate()
    {
        yield return new WaitForSeconds(2.2f);
        fullPicMiddle.SetActive(false);
        FirstPick();
    }
    
    private void FirstPick()
    {
        draftStage = "Pick1";
        SinglePickStart();
    }
    
    */
    
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
            Debug.Log("GM = FirstPickHeroCheck() + activeTeam = Player");
            if (myChosenHero == "none")
            {
                StartCoroutine(SinglePickHeroCheckWait());
            }
            else
            {
                cm.PlayerSinglePickHeroChosen();
                cm.ActivateFullMiddlePic();
                cm.DeactivateClassButtons();
                StartCoroutine(SinglePickPicPauseAndDeactivate());
            }
        }
        else
        {
            SinglePickEnemyCheck();
        }
    }

    public void SinglePickEnemyCheck()
    {
        cm.ConfirmComp();
        Debug.Log("GM = FirstPickHeroCheck() + activeTeam = Enemy");
        myEnemyCompReady = cm.enemyCompReady;
        if (myEnemyCompReady == "no")
        {
            EnemySinglePickText.SetActive(true);
            cm.GetEnemyComp();
            StartCoroutine(SinglePickEnemyWait());
        }
        else
        {
            StartCoroutine(SinglePickEnemyLockWait());
        }

    }

    IEnumerator SinglePickEnemyWait()
    {
        yield return new WaitForSeconds(0.6f);
        cm.ConfirmComp();
        SinglePickEnemyCheck();
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
            Debug.Log("GM = DoublePickHeroCheck() + activeTeam = Player");
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
            StartCoroutine(DoublePickEnemyWait());
        }
    }

    public void DoublePickEnemyCheck()
    {
        Debug.Log("GM = DoublePickHeroCheck() + activeTeam = Enemy");
        myEnemyCompReady = cm.enemyCompReady;
        if (myEnemyCompReady == "no")
        {
            EnemyDoublePickText.SetActive(true);
            cm.GetEnemyComp();
            StartCoroutine(DoublePickEnemyWait());
        }
        else
        {
            StartCoroutine(DoublePickEnemyLockWait());
        }

    }

    public void DoublePickSecondHeroCheck()
    {
        myChosenHero = HeroSelectScript.chosenHero;
        Debug.Log("GM = DoublePickSecondHeroCheck()");
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

    IEnumerator DoublePickEnemyWait()
    {
        yield return new WaitForSeconds(0.6f);
        cm.ConfirmComp();
        DoublePickEnemyCheck();
    }

    IEnumerator DoublePickEnemyLockWait()
    {
        yield return new WaitForSeconds(1f);
        DoublePickEnemyLock1();
        StartCoroutine(DoublePickEnemySecondLockWait());
    }

    IEnumerator DoublePickEnemySecondLockWait()
    {
        yield return new WaitForSeconds(1f);
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
