using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropdownMapSwitcher : MonoBehaviour
{
    public Dropdown mapDropdown;
    public Button mapButtonBoE;
    public Button mapButtonBBay;
    public Button mapButtonBraxis;
    public Button mapButtonCursedH;
    public Button mapButtonDShire;
    public Button mapButtonGarden;
    public Button mapButtonHMines;
    public Button mapButtonInfernalS;
    public Button mapButtonSkyT;
    public Button mapButtonSpiderQueen;
    public Button mapButtonTowersofD;
    public Button mapButtonWarheadJ;
    public GameObject mapObjectBoE;
    public GameObject mapObjectBBay;
    public GameObject mapObjectBraxis;
    public GameObject mapObjectCursedH;
    public GameObject mapObjectDShire;
    public GameObject mapObjectGarden;
    public GameObject mapObjectHMines;
    public GameObject mapObjectInfernalS;
    public GameObject mapObjectSkyT;
    public GameObject mapObjectSpiderQueen;
    public GameObject mapObjectTowersofD;
    public GameObject mapObjectWarheadJ;
    public GameObject HeroSelectionPanel;
    public GameObject PanelMapZoom;
    public GameManager gm;
    public Text PanelMapText1;
    public Text PanelMapText2;
    public Text PanelMapTextTitle;
    public Image PanelMapZoomImage;
    public GameObject PanelFullMiddle;
    public Button StartButton;
    public Button CreditsButton;
    public Button CreditsBlizzard;
    public Button CreditsDreadnaught;
    public Button CreditsStolen;
    public Button CreditsCavalier;
    public Button CreditsSrey;
    public Button CreditsDrew;
    public Button CreditsMe;
    private int randomMap;
    public string map;
    public string draftStage;

    public string mapTextString;
    IDictionary<string, IDictionary<string, string>> mapTextDict;

    public void Start()
    {
        map = "none";
        mapDropdown.value = 0;
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
        //    Following path doesn't work in EXE
        //     mapTextString = File.ReadAllText(Application.dataPath + "/Resources/HotSMapStuff/MapText.json");
        mapTextString = Resources.Load("HotSMapStuff/mapText", typeof(TextAsset)).ToString();

        mapTextDict = JsonConvert.DeserializeObject<IDictionary<string, IDictionary<string, string>>>(mapTextString);
        mapDropdown.onValueChanged.AddListener(delegate { DropDownSelection(); });
        CreditsButton.onClick.AddListener(delegate { ShowCreditsPanel(); });
    }

    public void DropDownSelection()
    {
        PanelMapZoom.SetActive(false);
        switch (mapDropdown.value)
        {
            case 0:
                map = "none";
                mapObjectBoE.SetActive(false);
                mapObjectBBay.SetActive(false);
                mapObjectBraxis.SetActive(false);
                mapObjectCursedH.SetActive(false);
                mapObjectDShire.SetActive(false);
                mapObjectGarden.SetActive(false);
                mapObjectHMines.SetActive(false);
                mapObjectInfernalS.SetActive(false);
                mapObjectSkyT.SetActive(false);
                mapObjectSpiderQueen.SetActive(false);
                mapObjectTowersofD.SetActive(false);
                mapObjectWarheadJ.SetActive(false);
                break;
            case 1:
                mapObjectBoE.SetActive(false);
                mapObjectBBay.SetActive(false);
                mapObjectBraxis.SetActive(false);
                mapObjectCursedH.SetActive(false);
                mapObjectDShire.SetActive(false);
                mapObjectGarden.SetActive(false);
                mapObjectHMines.SetActive(false);
                mapObjectInfernalS.SetActive(false);
                mapObjectSkyT.SetActive(false);
                mapObjectSpiderQueen.SetActive(false);
                mapObjectTowersofD.SetActive(false);
                mapObjectWarheadJ.SetActive(false);
                randomMap = Random.Range(2, 14);
                mapDropdown.value = randomMap;
                map = "none";
                DropDownSelection();
                break;
            case 2:
                map = "BoE";
                mapObjectBoE.SetActive(true);
                mapButtonBoE.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 3:
                map = "BBay";
                mapObjectBBay.SetActive(true);
                mapButtonBBay.onClick.AddListener(delegate () { ZoomIn(); });
                break;

            case 4:
                map = "Braxis";
                mapObjectBraxis.SetActive(true);
                mapButtonBraxis.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 5:
                map = "CursedH";
                mapObjectCursedH.SetActive(true);
                mapButtonCursedH.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 6:
                map = "DShire";
                mapObjectDShire.SetActive(true);
                mapButtonDShire.onClick.AddListener(delegate () { ZoomIn(); });
                break;

            case 7:
                map = "Garden";
                mapObjectGarden.SetActive(true);
                mapButtonGarden.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 8:
                map = "HMines";
                mapObjectHMines.SetActive(true);
                mapButtonHMines.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 9:
                map = "InfernalS";
                mapObjectInfernalS.SetActive(true);
                mapButtonInfernalS.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 10:
                map = "SkyT";
                mapObjectSkyT.SetActive(true);
                mapButtonSkyT.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 11:
                map = "SpiderQueen";
                mapObjectSpiderQueen.SetActive(true);
                mapButtonSpiderQueen.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 12:
                map = "TowersofD";
                mapObjectTowersofD.SetActive(true);
                mapButtonTowersofD.onClick.AddListener(delegate () { ZoomIn(); });
                break;
            case 13:
                map = "WarheadJ";
                mapObjectWarheadJ.SetActive(true);
                mapButtonWarheadJ.onClick.AddListener(delegate () { ZoomIn(); });
                break;
        }
    }

    public void ZoomIn()
    {
        Debug.Log(mapTextDict[map]["Name"]);
        draftStage = gm.draftStage;
        if (draftStage == "none")
        {
            mapDropdown.gameObject.SetActive(false);
            StartButton.gameObject.SetActive(false);
        }
        PanelMapZoom.SetActive(true);
        PanelMapZoomImage.sprite = Resources.Load<Sprite>("HotSMapStuff/ZoomMap" + map);
        HeroSelectionPanel.SetActive(false);
        PanelFullMiddle.SetActive(false);

        string Text1 = mapTextDict[map]["Facts"];
        string Text2 = mapTextDict[map]["Advice"];
        string TextTitle = mapTextDict[map]["Name"];

        PanelMapText1.text = Text1;
        PanelMapText2.text = Text2;
        PanelMapTextTitle.text = TextTitle;
    }

    public void ShowCreditsPanel()
    {
        PanelMapZoom.SetActive(true);
        PanelMapTextTitle.text = "Source Links";
        PanelMapText1.text = "";
        PanelMapText2.text = "";
        PanelMapZoomImage.sprite = Resources.Load<Sprite>("HotSPics/Credits");
        PanelFullMiddle.SetActive(false);
        HeroSelectionPanel.SetActive(false);
        mapDropdown.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
        CreditsBlizzard.gameObject.SetActive(true);
        CreditsDreadnaught.gameObject.SetActive(true);
        CreditsStolen.gameObject.SetActive(true);
        CreditsCavalier.gameObject.SetActive(true);
        CreditsSrey.gameObject.SetActive(true);
        CreditsDrew.gameObject.SetActive(true);
        CreditsMe.gameObject.SetActive(true);
    }
}
