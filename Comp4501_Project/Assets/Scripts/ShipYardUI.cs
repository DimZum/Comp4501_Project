﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipYardUI : MonoBehaviour {

    GameMaster gm;

    public Button Design1, Design2, Design3, Design4,PrevPage,NextPage;
    public Text D1t, D2t, D3t, D4t;
    public Image D1i, D2i, D3i, D4i;
    public Button ExitButton, ToDesign;
    Image[] YardIcons;
    public Text YardName;
    public Text YardStat;
    public Text YardTime;
    public Text YardShip;
    public Text YardPage;
    int CurPage;
    int MaxPage;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;

        YardIcons = new Image[10];
        for (int i = 0; i < 10; i++)
        {
            YardIcons[i]= GameObject.Find("Type_Icon_"+(i+1).ToString()).GetComponent<Image>();
        }
        CurPage = 1;
        MaxPage = 1;

        PrevPage.onClick.AddListener(Ppage);
        NextPage.onClick.AddListener(Npage);
        ToDesign.onClick.AddListener(Tdesign);
        Design1.onClick.AddListener(DB1_handle);
        Design2.onClick.AddListener(DB2_handle);
        Design3.onClick.AddListener(DB3_handle);
        Design4.onClick.AddListener(DB4_handle);
        ExitButton.onClick.AddListener(CloseWindow);
    }

    void CloseWindow() {
        gm.ShipYardUI.SetActive(false);
        gm.ShipDesignerUI.SetActive(false);
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

    void Tdesign()
    {
        gm.ShipDesignerUI.SetActive(true);
        gm.ShipYardUI.SetActive(false);
    }

    string getShipInfo(int index)
    {
        string s = gm.player.ShipDesigns[index].getName();
        s += "\nIron: " + gm.player.ShipDesigns[index].getIronCost();
        s += "\nMP: " + gm.player.ShipDesigns[index].getMPCost();
        return s;
    }

    Sprite getShipIcon(int index)
    {
        return Constants.SHIP_ICONS[(int)gm.player.ShipDesigns[index].getClass()];
    }

    bool DesignExist(int index)
    {
        return gm.player.getNextDesignID() > index;
    }

    void DB1_handle()
    {
        gm.player.buildShip(gm.player.ShipDesigns[(CurPage - 1) * 4]);
    }
    void DB2_handle()
    {
        gm.player.buildShip(gm.player.ShipDesigns[(CurPage - 1) * 4 + 1]);
    }
    void DB3_handle()
    {
        gm.player.buildShip(gm.player.ShipDesigns[(CurPage - 1) * 4 + 2]);
    }
    void DB4_handle()
    {
        gm.player.buildShip(gm.player.ShipDesigns[(CurPage - 1) * 4 + 3]);
    }


    void DesignButtonUpdate()
    {
        MaxPage = Mathf.CeilToInt(gm.player.getNextDesignID()/4);
        int pageoffset = (CurPage - 1) * 4;
        if (DesignExist(pageoffset))
        {
            D1t.text = getShipInfo(pageoffset);
            D1i.color = gm.player.getPlayerColor();
            D1i.sprite = getShipIcon(pageoffset);
            Design1.interactable = true;
        }
        else{
            D1t.text = "Empty";
            D1i.color = new Color32 (255,255,255,0);
            Design1.interactable = false;
        }
        if (DesignExist(pageoffset+1))
        {
            D2t.text = getShipInfo(pageoffset + 1);
            D2i.sprite = getShipIcon(pageoffset + 1);
            D2i.color = gm.player.getPlayerColor();
            Design2.interactable = true;
        }
        else{
            D2t.text = "Empty";
            D2i.color = new Color32(255, 255, 255, 0);
            Design2.interactable = false;
        }
        if (DesignExist(pageoffset+2))
        {
            D3t.text = getShipInfo(pageoffset + 2);
            D3i.sprite = getShipIcon(pageoffset + 2);
            D3i.color = gm.player.getPlayerColor();
            Design3.interactable = true;
        }
        else{
            D3t.text = "Empty";
            D3i.color = new Color32(255, 255, 255, 0);
            Design3.interactable = false;
        }
        if (DesignExist(pageoffset+3))
        {
            D4t.text = getShipInfo(pageoffset + 3);
            D4i.sprite = getShipIcon(pageoffset + 3);
            D4i.color = gm.player.getPlayerColor();
            Design4.interactable = true;
        }
        else{
            D4t.text = "Empty";
            D4i.color = new Color32(255, 255, 255, 0);
            Design4.interactable = false;
        }
        YardPage.text = CurPage + "/" + MaxPage;
    }

    void BuildQueueUpdate()
    {
        YardShip.text = gm.player.GetShipYard().ConstructionNameString();
        YardTime.text = gm.player.GetShipYard().ConstructionTimeString();
        YardStat.text = "";
        string[] s = { "Not avaliable", "Idle", "Working" };
        for (int i = 0; i < Constants.MAX_BUILD_QUEUE; i++)
        {
            ShipDesign d = gm.player.GetShipYard().getDesignInConstruction(i);
            if (d == null)
            {
                YardIcons[i].color = new Color32(255, 255, 255, 0);
            }
            else
            {
                YardIcons[i].sprite = Constants.SHIP_ICONS[(int)d.getClass()];
                YardIcons[i].color = gm.player.getPlayerColor();
            }
            YardStat.text += s[gm.player.GetShipYard().getYardStat(i)]+"\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // YardIcons[1].color = new Color32(255, 255, 255, t);
        DesignButtonUpdate();
        BuildQueueUpdate();
    }
}
