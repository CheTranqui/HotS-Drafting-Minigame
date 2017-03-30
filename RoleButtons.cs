using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleButtons : MonoBehaviour {
private Text buttonText;
private Button classButton;

private static string classText;
public GameObject WPanel;
public GameObject SupPanel;
public GameObject APanel;
public GameObject SpecPanel;

void Start()
{
		classButton = gameObject.GetComponent<Button>();
        classButton.onClick.AddListener(delegate() { OnClick(); });
}
void OnClick()
{
    Text buttonText = GetComponentInChildren<Text>();
    classText = buttonText.text;
    ActivatePanel();
}

void ActivatePanel()
{   
    if (classText == "Warrior")
    {
        if (WPanel.activeSelf == true)
        {
            WPanel.SetActive(false);
        }
        else
        {
        WPanel.SetActive(true);
        SupPanel.SetActive(false);
        APanel.SetActive(false);
        SpecPanel.SetActive(false);
        }
    }
    if (classText == "Support")
    {
        if (SupPanel.activeSelf == true)
        {
            SupPanel.SetActive(false);
        }
        else
        {
        WPanel.SetActive(false);
        SupPanel.SetActive(true);
        APanel.SetActive(false);
        SpecPanel.SetActive(false);
        }
    }
    if (classText == "Assassin")
    {
        if (APanel.activeSelf == true)
        {
            APanel.SetActive(false);
        }
        else
        {
        WPanel.SetActive(false);
        SupPanel.SetActive(false);
        APanel.SetActive(true);
        SpecPanel.SetActive(false);
        }
    }
    if (classText == "Specialist")
    {
        if (SpecPanel.activeSelf == true)
        {
            SpecPanel.SetActive(false);
        }
        else
        {
        WPanel.SetActive(false);
        SupPanel.SetActive(false);
        APanel.SetActive(false);
        SpecPanel.SetActive(true);
        }
    }
    else
    {
    // purposefully left empty. this script is solely used
    // to permit clean transitions between those four buttons.
    }
}
}
