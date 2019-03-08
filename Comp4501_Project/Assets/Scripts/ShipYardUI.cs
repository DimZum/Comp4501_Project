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
        MaxPage = 2;

        PrevPage.onClick.AddListener(Ppage);
        NextPage.onClick.AddListener(Npage);
        ToDesign.onClick.AddListener(Tdesign);
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
        SceneManager.LoadSceneAsync("ShipDesign");
    }

    string getShipName(int index)
    {
        return GameMaster.player.GetShipDesigns()[index].getName();
    }

    Sprite getShipIcon(int index)
    {
        int c = (int)GameMaster.player.GetShipDesigns()[index].getClass();
        Debug.Log("Loading Sprite: " + Constants.SHIP_ICONS[c]);
        return Resources.Load<Sprite>(Constants.SHIP_ICONS[c]);
    }

    bool DesignExist(int index)
    {
        return GameMaster.player.getNextDesignID() > index;
    }

    void DesignButtonUpdate()
    {
        int pageoffset = (CurPage - 1) * 4;
        if (DesignExist(pageoffset))
        {
            D1t.text = getShipName(pageoffset);
            D1i.color = new Color32(255, 255, 255, 255);
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
            D2t.text = getShipName(pageoffset + 1);
            D2i.sprite = getShipIcon(pageoffset + 1);
            D2i.color = new Color32(255, 255, 255, 255);
            Design2.interactable = true;
        }
        else{
            D2t.text = "Empty";
            D2i.color = new Color32(255, 255, 255, 0);
            Design2.interactable = false;
        }
        if (DesignExist(pageoffset+2))
        {
            D3t.text = getShipName(pageoffset + 2);
            D3i.sprite = getShipIcon(pageoffset + 2);
            D3i.color = new Color32(255, 255, 255, 255);
            Design3.interactable = true;
        }
        else{
            D3t.text = "Empty";
            D3i.color = new Color32(255, 255, 255, 0);
            Design3.interactable = false;
        }
        if (DesignExist(pageoffset+3))
        {
            D4t.text = getShipName(pageoffset + 3);
            D4i.sprite = getShipIcon(pageoffset + 3);
            D4i.color = new Color32(255, 255, 255, 255);
            Design4.interactable = true;
        }
        else{
            D4t.text = "Empty";
            D4i.color = new Color32(255, 255, 255, 0);
            Design4.interactable = false;
        }
        YardPage.text = CurPage + "/" + MaxPage;
    }

    // Update is called once per frame
    void Update()
    {
        // YardIcons[1].color = new Color32(255, 255, 255, t);
        DesignButtonUpdate();
    }
}
