using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipYardUI : MonoBehaviour
{
    Button Design1, Design2, Design3, Design4,PrevPage,NextPage;
    Button ExitButton;
    Image[] YardIcons;
    Text YardName;
    Text YardStat;
    Text YardTime;
    Text YardShip;
    Text YardPage;
    int CurPage;
    int MaxPage;
    byte t = 0;

    // Start is called before the first frame update
    void Start()
    {
        Design1 = GameObject.Find("DesignButton_1").GetComponent<Button>();
        Design2 = GameObject.Find("DesignButton_2").GetComponent<Button>();
        Design3 = GameObject.Find("DesignButton_3").GetComponent<Button>();
        Design4 = GameObject.Find("DesignButton_4").GetComponent<Button>();
        PrevPage = GameObject.Find("Prev_Page").GetComponent<Button>();
        NextPage = GameObject.Find("Next_Page").GetComponent<Button>();
        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        YardName = GameObject.Find("ShipYardName").GetComponent<Text>();
        YardName = GameObject.Find("ShipYardName").GetComponent<Text>();
        YardStat = GameObject.Find("ShipYardStatus").GetComponent<Text>();
        YardTime = GameObject.Find("ShipYardTime").GetComponent<Text>();
        YardShip = GameObject.Find("ConstructionName").GetComponent<Text>();
        YardPage = GameObject.Find("Page_Text").GetComponent<Text>();
        YardIcons = new Image[10];
        for (int i = 0; i < 10; i++)
        {
            YardIcons[i]= GameObject.Find("Type_Icon_"+(i+1).ToString()).GetComponent<Image>();
        }
        CurPage = 1;
        MaxPage = 2;

        PrevPage.onClick.AddListener(Ppage);
        NextPage.onClick.AddListener(Npage);
    }

    void Ppage()
    {
        CurPage--;
        if (CurPage < 1)
        {
            CurPage = 1;
        }
    }
    void Npage()
    {
        CurPage++;
        if (CurPage > MaxPage)
        {
            CurPage = MaxPage;
        }
    }

    void TextUpdate()
    {
        YardPage.text = CurPage + "/" + MaxPage;
    }

    // Update is called once per frame
    void Update()
    {
        // YardIcons[1].color = new Color32(255, 255, 255, t);
        TextUpdate();
    }
}
