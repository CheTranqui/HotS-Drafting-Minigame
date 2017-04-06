using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour {

    private Button CreditsButton;
    private Text person;
    private string personString;
    public string link;

    void Start()
    {
        link = "none";
        CreditsButton = this.gameObject.GetComponent<Button>();
        CreditsButton.onClick.AddListener(delegate () { ShowCredits(); });
    }

    public void ShowCredits()
    {
        person = this.gameObject.GetComponentInChildren<Text>();
        personString = person.text.ToString();

        if (personString == "Blizzard")
        {
            link = "http://us.battle.net/heroes/en/";
        }
        if (personString == "Dreadnaught")
        {
            link = "https://www.youtube.com/watch?v=Hdzdksu20XM&t";
        }
        if (personString == "Cavalier Guest")
        {
            link = "https://www.youtube.com/watch?v=fHZ01eYLEPc";
        }
        if (personString == "Stolen")
        {
            link = "https://www.reddit.com/r/heroesofthestorm/comments/47m8qz/tips_for_every_map/";
        }
        if (personString == "Srey")
        {
            link = "https://www.youtube.com/watch?v=-ILfQUaTuv4";
        }
        if (personString == "Renewed Hope")
        {
            link = "https://renewedhope.guildlaunch.com";
        }
        if (personString == "CheTranqui")
        {
            link = "https://chetranqui.weebly.com";
        }
        Application.OpenURL(link);
    }

}
