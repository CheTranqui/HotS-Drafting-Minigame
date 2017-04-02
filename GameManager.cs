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
    public Button StartButton;
    public string myChosenHero;
    private bool playerGoesFirst;
    public string draftMap;
    private bool myEnemyCompReady;
    public string draftStage;
    public string activeTeam;


    public void StartTheDraft()
    {
        draftMap = "none";
        draftStage = "none";
        cm = gameObject.GetComponent<CoreMethods>();
        ddm = DropDownMap.GetComponent<DropdownMapSwitcher>();
        ddm.Start();
        StartButton.gameObject.SetActive(false);
        CheckForMap();
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
            cm.StartDraft();
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
        yield return new WaitForSeconds(0.5f);
        FirstBan1();
    }

    private void FirstBan1()
    {
        draftStage = "FirstBan1";
        cm.ActiveTeamCheck();
        myChosenHero = "none";
        activeTeam = cm.activeTeam;
        cm.FirstBanStart();
        FirstBanHeroCheck();
    }

    IEnumerator FirstBanHeroCheckWait()
    {
        yield return new WaitForSeconds(0.5f);
        myChosenHero = HeroSelectScript.chosenHero;
        FirstBanHeroCheck();
    }

    public void FirstBanHeroCheck()
    {
        if (activeTeam == "Player")
        {
            Debug.Log("GM = FirstBanHeroCheck() + activeTeam = Player");
            if (myChosenHero == "none")
            {
                StartCoroutine(FirstBanHeroCheckWait());
            }
            else
            {
                cm.Ban1HeroChosen();
                cm.ActivateFullMiddlePic();
                StartCoroutine(FirstBanPicPauseAndDeactivate());
            }
        }
        else
        {
            Debug.Log("GM = FirstBanHeroCheck() + activeTeam = Enemy");
            cm.GetEnemyComp();
            cm.ConfirmComp();
            myEnemyCompReady = cm.enemyCompReady;
            if (!myEnemyCompReady)
            {
                StartCoroutine(FirstBanEnemyWait());
            }
            else
            {
                EnemyBanText.SetActive(true);
                StartCoroutine(EnemyBanLockWait());
            }
        }
    }

    IEnumerator FirstBanEnemyWait()
    {
        yield return new WaitForSeconds(0.5f);
        FirstBanHeroCheck();
    }

    IEnumerator FirstBanPicPauseAndDeactivate()
    {
        yield return new WaitForSeconds(2.2f);
        fullPicMiddle.SetActive(false);
    }

    IEnumerator EnemyBanLockWait()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyFirstBanLock();
    }

    public void EnemyFirstBanLock()
    {
        cm.EnemyFirstBanPicPortrait();
        cm.ActivateFullMiddlePic();
        StartCoroutine(EnemyFirstBanPicPauseAndDeactivate());
    }

    IEnumerator EnemyFirstBanPicPauseAndDeactivate()
    {
        yield return new WaitForSeconds(2.2f);
        fullPicMiddle.SetActive(false);
    }

}
