using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class DropdownMapSwitcher : MonoBehaviour
{
    public Dropdown mapDropdown;
    public Button mapButtonBoE;
    public Button mapButtonBBay;
    public Button mapButtonBraxxis;
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
    public GameObject mapObjectBraxxis;
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
    public GameObject PanelMapText;
    public GameObject PanelMapZoomImage;
    private int randomMap;
    public string map;

    public void Start()
    {
        map = "none";
        mapDropdown.value = 0;
        mapDropdown.onValueChanged.AddListener(delegate { DropDownSelection(); });
    }

    public void DropDownSelection()
    {
        switch (mapDropdown.value)
        {
            case 0:
                map = "none";
                mapObjectBoE.SetActive(false);
                mapObjectBBay.SetActive(false);
                mapObjectBraxxis.SetActive(false);
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
                mapObjectBraxxis.SetActive(false);
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
                map = "Braxxis";
                mapObjectBraxxis.SetActive(true);
                mapButtonBraxxis.onClick.AddListener(delegate () { ZoomIn(); });
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
        //  Need to get zoomed in map images
        //  Need to create map details text
        //  use variable "map" to call to the appropriate zoom image.
    }
}
