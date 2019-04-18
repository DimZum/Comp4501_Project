using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipDesigner : MonoBehaviour {

    GameMaster gm;
    Constants.ShipClass ShipClass;
    int mgc = 3, mgn = 1, sgc = 2, sgn = 0, trp = 0, AA = 0; 
    int armor = 1, engine = 1;
    int weight_fp, weight_de;
    bool bulge;
    int totalWeight;
    int MaxWeight;
    int HitPoints;
    float IronCost;
    float ManCost;
    float ConstructionTime;
    public Dropdown Sclass;
    public InputField Sname;
    public Text fpvalue, devalue,fpw,dew,totalw,description;
    public Button mgc_inc, mgc_dec;
    public Button mgn_inc, mgn_dec;
    public Button sgc_inc, sgc_dec;
    public Button sgn_inc, sgn_dec;
    public Button trp_inc, trp_dec;
    public Button armor_inc, armor_dec;
    public Button engine_inc, engine_dec;
    public Toggle trp_bulge;
    public Button confirm,cancel,To_Shipyard,ExitButton;
    
    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;

        mgc_inc.onClick.AddListener(add_mgc);
        mgc_dec.onClick.AddListener(red_mgc);
        mgn_inc.onClick.AddListener(add_mgn);
        mgn_dec.onClick.AddListener(red_mgn);
        sgc_inc.onClick.AddListener(add_sgc);
        sgc_dec.onClick.AddListener(red_sgc);
        sgn_inc.onClick.AddListener(add_sgn);
        sgn_dec.onClick.AddListener(red_sgn);
        trp_inc.onClick.AddListener(add_trp);
        trp_dec.onClick.AddListener(red_trp);
        armor_inc.onClick.AddListener(add_armor);
        armor_dec.onClick.AddListener(red_armor);
        engine_inc.onClick.AddListener(add_engine);
        engine_dec.onClick.AddListener(red_engine);
        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
        To_Shipyard.onClick.AddListener(goShipyard);
        ExitButton.onClick.AddListener(CloseWindow);

    }

    void CloseWindow()
    {
        gm.ShipYardUI.SetActive(false);
        gm.ShipDesignerUI.SetActive(false);
    }

    void add_mgc()
    {
        mgc++;
        if (mgc > Constants.MAX_CALIBER)
        {
            mgc = Constants.MAX_CALIBER;
        }
    }
    void red_mgc()//red is short for reduce.
    {
        mgc--;
        if (mgc < Constants.MIN_CALIBER)
        {
            mgc = Constants.MIN_CALIBER;
        }
    }
    void add_mgn()
    {
        mgn++;
        if (mgn > Constants.MAX_TURRET)
        {
            mgn = Constants.MAX_TURRET;
        }
    }
    void red_mgn()
    {
        mgn--;
        if (mgn < Constants.MIN_TURRET)
        {
            mgn = Constants.MIN_TURRET;
        }
    }
    void add_sgc()
    {
        sgc++;
        if (sgc > Constants.MAX_SUB_CALIBER)
        {
            sgc = Constants.MAX_SUB_CALIBER;
        }
        float max = Mathf.Min(mgc / 2, mgc - 3);
        if (sgc > max)
        {
            sgc = Mathf.FloorToInt(max);
        }
        if (sgc < Constants.MIN_SUB_CALIBER)
        {
            sgc = Constants.MIN_SUB_CALIBER;
        }
    }
    void red_sgc()
    {
        sgc--;
        if (sgc < Constants.MIN_SUB_CALIBER)
        {
            sgc = 0;
        }
    }
    void add_sgn()
    {
        sgn++;
        if (sgn > Constants.MAX_TURRET)
        {
            sgn = Constants.MAX_TURRET;
        }
    }
    void red_sgn()
    {
        sgn--;
        if (sgn < 0)
        {
            sgn = 0;
        }
    }
    void add_trp()
    {
        trp++;
        if (trp > Constants.MAX_TORPEDO)
        {
            trp = Constants.MAX_TORPEDO;
        }
    }
    void red_trp()
    {
        trp--;
        if (trp < 0)
        {
            trp = 0;
        }
    }
    void add_armor()
    {
        armor++;
    }
    void red_armor()
    {
        armor--;
        if (armor < Constants.MIN_ARMOR)
        {
            armor = Constants.MIN_ARMOR;
        }
    }
    void add_engine()
    {
        engine++;
    }
    void red_engine()
    {
        engine--;
        if(engine < Constants.MIN_ENGINE)
        {
            engine = Constants.MIN_ENGINE;
        }
    }
    void compute_value()
    {
        MaxWeight = Constants.ClassToCapacity[(int)ShipClass];
        weight_fp = Constants.CannonWeight[mgc] * mgn + Constants.CannonWeight[sgc] * sgn + trp * Constants.trpweight;
        float weightArmor = (armor+31) * (armor+31) *(Constants.ClassToWeight[(int)ShipClass]+mgc*mgn*15+sgc*sgn*15+trp*50) / 1500.0f;
        float weightEngine = (weightArmor + weight_fp) * engine * engine / 900;
        weight_de = (int)(weightEngine+ weightArmor);
        if (bulge)
        {
            weight_de += Constants.ClassToWeight[(int)ShipClass];
        }
        totalWeight = weight_de + weight_fp+Constants.ClassToWeight[(int)ShipClass];
        int weightdif = MaxWeight - totalWeight;
        if (weightdif < 0) weightdif = 1;
        HitPoints = (int)((1+2.0*weightdif/MaxWeight)*((int)ShipClass+1)*((int)ShipClass+1)*15);
        IronCost = armor * armor * 10 * (int)ShipClass + engine * 5 * (int)ShipClass + trp * 20 + mgc * mgc * mgn+sgc*sgc*sgn;
        ManCost = Mathf.Max(totalWeight - 500, 0)/25 + mgc * mgn + sgc * sgn +engine;
        ConstructionTime =(totalWeight / 200+IronCost/100)/5+10;
        if (totalWeight > MaxWeight)
        {
            confirm.interactable=false;
        }
        else
        {
            confirm.interactable = true;
        }
    }

    void getInfo()
    {
        bulge = trp_bulge.isOn;
        ShipClass = (Constants.ShipClass)Sclass.value;
    }

    void Cancel()
    {
        //Testing
        //Debug.Log(gm.player.getNextShipID());
    }

    void Confirm()
    {
        //ShipDesign(int maingc, int maingn, int subgc, int subgn, int trpdo, int A, int arm, int eng
        //              , bool bulge, int tweight, int HP,int Icost,int Mcost, int Tcost)
        //Debug.Log("Trying to save design " + Sname.text);
        gm.player.AddDesign(
            new ShipDesign(Sname.text, (Constants.ShipClass)Sclass.value,mgc, mgn, sgc, sgn, trp, AA, armor, engine, bulge, totalWeight, HitPoints,IronCost, ManCost, ConstructionTime));
        //Debug.Log("Design Saved.\nCurrent design#: " + gm.player.getNextDesignID());
    }

    public ShipDesign CreateDesignWithValues(string n, Constants.ShipClass sc,int maingc, int maingn, int subgc, int subgn, int t, int AntiA, int arm, int speed, bool b)
    {
        ShipClass = sc;
        mgc = maingc;
        mgn = maingn;
        sgc = subgc;
        sgn = subgn;
        trp = t;
        AA = AntiA;
        armor = arm;
        engine = speed;
        bulge = b;
        compute_value();
        Debug.Log("Call CDWV");
        return new ShipDesign(n, (Constants.ShipClass)Sclass.value, mgc, mgn, sgc, sgn, trp, AA, armor, engine, bulge, totalWeight, HitPoints, IronCost, ManCost, ConstructionTime);
    }

    void goShipyard()
    {
        gm.ShipDesignerUI.SetActive(false);
        gm.ShipYardUI.SetActive(true);
    }

    bool is_vowel()
    {
        char[] s = Sname.text.ToLower().ToCharArray();
        if (s.Length == 0)
        {
            return false;
        }
        return (s[0].Equals('a') || s[0].Equals('o') || s[0].Equals('e') || s[0].Equals('i') || s[0].Equals('u'));
    }

    void display_value()
    {
        if (Sname.text.Equals(""))
        {
            Sname.text = "Nameless";
        }
        fpvalue.text = mgc + "\n" + mgn + "\n"+sgc+"\n"+sgn+"\n"+trp;
        devalue.text = armor + "\n" + engine + "\n";
        fpw.text = weight_fp + " tons";
        dew.text = weight_de + " tons";
        totalw.text = "Total weight " + totalWeight + "\nMax Capacity " + MaxWeight;
        int not_used_weight = MaxWeight - totalWeight;
        if (not_used_weight < 0) not_used_weight = 0;
        if (is_vowel())
        {
            description.text = "An ";
        }
        else
        {
            description.text = "A ";
        }

        description.text += Sname.text + " class " + ShipClass.ToString()
            + "\nEquiped with " + mgc
            + " inch Cannon\n" + trp
            + " torpedo tube(s)\nArmor enough to stop " + armor + " inch shell\nMax speed " + engine + " Knots"
            + "\nTotal hitpoints: " + HitPoints
            + "\nIron Cost: " + IronCost
            + "\nOperate Manpower: " + ManCost
            + "\nConstruction Time: " + ConstructionTime;
    }
    // Update is called once per frame
    void Update() {
        if (gameObject.activeSelf) {
            getInfo();
            compute_value();
            display_value();
        }
    }
}
