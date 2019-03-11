using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipYardUI : MonoBehaviour
{
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
    void Start()
    {
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

    void CloseWindow()
    {
        GameMaster.ShipYardUI.SetActive(false);
        GameMaster.ShipDesignerUI.SetActive(false);
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
        GameMaster.ShipDesignerUI.SetActive(true);
        GameMaster.ShipYardUI.SetActive(false);
    }

    string getShipInfo(int index)
    {
        string s = GameMaster.player.GetShipDesigns()[index].getName();
        s += "\nIron: " + GameMaster.player.GetShipDesigns()[index].getIronCost();
        s += "\nMP: " + GameMaster.player.GetShipDesigns()[index].getMPCost();
        return s;
    }

    Sprite getShipIcon(int index)
    {
        return Constants.SHIP_ICONS[(int)GameMaster.player.GetShipDesigns()[index].getClass()];
    }

    bool DesignExist(int index)
    {
        return GameMaster.player.getNextDesignID() > index;
    }

    void DB1_handle()
    {
        GameMaster.player.buildShip(GameMaster.player.GetShipDesigns()[(CurPage - 1) * 4]);
    }
    void DB2_handle()
    {
        GameMaster.player.buildShip(GameMaster.player.GetShipDesigns()[(CurPage - 1) * 4 + 1]);
    }
    void DB3_handle()
    {
        GameMaster.player.buildShip(GameMaster.player.GetShipDesigns()[(CurPage - 1) * 4 + 2]);
    }
    void DB4_handle()
    {
        GameMaster.player.buildShip(GameMaster.player.GetShipDesigns()[(CurPage - 1) * 4 + 3]);
    }


    void DesignButtonUpdate()
    {
        MaxPage = Mathf.CeilToInt(GameMaster.player.getNextDesignID()/4);
        int pageoffset = (CurPage - 1) * 4;
        if (DesignExist(pageoffset))
        {
            D1t.text = getShipInfo(pageoffset);
            D1i.color = GameMaster.player.getPlayerColor();
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
            D2i.color = GameMaster.player.getPlayerColor();
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
            D3i.color = GameMaster.player.getPlayerColor();
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
            D4i.color = GameMaster.player.getPlayerColor();
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
        YardShip.text = GameMaster.player.GetShipYard().ConstructionNameString();
        YardTime.text = GameMaster.player.GetShipYard().ConstructionTimeString();
        YardStat.text = "";
        string[] s = { "Not avaliable", "Idle", "Working" };
        for (int i = 0; i < Constants.MAX_BUILD_QUEUE; i++)
        {
            ShipDesign d = GameMaster.player.GetShipYard().getDesignInConstruction(i);
            if (d == null)
            {
                YardIcons[i].color = new Color32(255, 255, 255, 0);
            }
            else
            {
                YardIcons[i].sprite = Constants.SHIP_ICONS[(int)d.getClass()];
                YardIcons[i].color = GameMaster.player.getPlayerColor();
            }
            YardStat.text += s[GameMaster.player.GetShipYard().getYardStat(i)]+"\n";
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
