using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDesigner : MonoBehaviour
{
    Constants.ShipClass ShipClass;
    public int mgc = 3, mgn = 1, sgc = 2, sgn = 0, trp = 0, AA;
    public int armor = 1, engine = 1;
    public int weight_fp, weight_de;
    public bool bulge;
    int totalWeight;
    int MaxWeight;
    int HitPoints;
    float IronCost;
    float ManCost;
    float ConstructionTime;
    Dropdown Sclass;
    InputField Sname;
    Text fpvalue, devalue,fpw,dew,totalw,description;
    Button mgc_inc, mgc_dec;
    Button mgn_inc, mgn_dec;
    Button sgc_inc, sgc_dec;
    Button sgn_inc, sgn_dec;
    Button trp_inc, trp_dec;
    Button armor_inc, armor_dec;
    Button engine_inc, engine_dec;
    Toggle trp_bulge;
    Button confirm;
    Button cancel;
    // Start is called before the first frame update
    void Start()
    {
        Sclass = GameObject.Find("ShipClass").GetComponent<Dropdown>();
        Sname = GameObject.Find("ShipName").GetComponent<InputField>();
        mgc_inc = GameObject.Find("Add_caliber_main").GetComponent<Button>();
        mgc_dec = GameObject.Find("Reduce_caliber_main").GetComponent<Button>();
        mgn_inc = GameObject.Find("Add_turret_main").GetComponent<Button>();
        mgn_dec = GameObject.Find("Reduce_turret_main").GetComponent<Button>();
        sgc_inc = GameObject.Find("Add_caliber_sub").GetComponent<Button>();
        sgc_dec = GameObject.Find("Reduce_caliber_sub").GetComponent<Button>();
        sgn_inc = GameObject.Find("Add_turret_sub").GetComponent<Button>();
        sgn_dec = GameObject.Find("Reduce_turret_sub").GetComponent<Button>();
        trp_inc = GameObject.Find("Add_torpedo").GetComponent<Button>();
        trp_dec = GameObject.Find("Reduce_torpedo").GetComponent<Button>();
        armor_inc = GameObject.Find("Add_armor").GetComponent<Button>();
        armor_dec = GameObject.Find("Reduce_armor").GetComponent<Button>();
        engine_inc = GameObject.Find("Add_engine").GetComponent<Button>();
        engine_dec = GameObject.Find("Reduce_engine").GetComponent<Button>();
        trp_bulge = GameObject.Find("Torpedo_bulge").GetComponent<Toggle>();
        confirm = GameObject.Find("Confirm").GetComponent<Button>();
        cancel = GameObject.Find("Cancel").GetComponent<Button>();
        fpvalue = GameObject.Find("FP_Values").GetComponent<Text>();
        devalue = GameObject.Find("DE_Values").GetComponent<Text>();
        fpw = GameObject.Find("Weapon_weight").GetComponent<Text>();
        dew = GameObject.Find("Defense_weight").GetComponent<Text>();
        totalw = GameObject.Find("Weight and Capacity").GetComponent<Text>();
        description = GameObject.Find("Description").GetComponent<Text>();
        ShipClass = (Constants.ShipClass)Sclass.value;
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
    }

    void add_mgc()
    {
        mgc++;
        if (mgc > Constants.MAX_CALIBER)
        {
            mgc = Constants.MAX_CALIBER;
        }
    }
    void red_mgc()
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
        ShipClass = (Constants.ShipClass)Sclass.value;
        MaxWeight = Constants.ClassToCapacity[(int)ShipClass];
        weight_fp = Constants.CannonWeight[mgc] * mgn + Constants.CannonWeight[sgc] * sgn + trp * Constants.trpweight;
        weight_de = (int)(armor*armor * (mgn * mgc + sgn * sgc + trp + engine + (int)ShipClass)/2.5f) + Mathf.RoundToInt((engine*engine/2.0f)*Mathf.Log((armor*armor+weight_fp+1),3.65f)*Constants.ClassToDrag[(int)ShipClass]);
        totalWeight = weight_de + weight_fp+Constants.ClassToWeight[(int)ShipClass];
        if (trp_bulge.isOn)
        {
            totalWeight += Constants.ClassToWeight[(int)ShipClass];
        }
        int weightdif = MaxWeight - totalWeight;
        if (weightdif < 0) weightdif = 1;
        HitPoints = (int)((1+4.0*weightdif/MaxWeight)*((int)ShipClass+1)*((int)ShipClass+1)*10);
        IronCost = armor * armor * 10 * (int)ShipClass + engine * 5 * (int)ShipClass + trp * 20 + mgc * mgc * mgn+sgc*sgc*sgn;
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
    void display_value()
    {
        fpvalue.text = mgc + "\n" + mgn + "\n"+sgc+"\n"+sgn+"\n"+trp;
        devalue.text = armor + "\n" + engine + "\n";
        fpw.text = weight_fp + " tons";
        dew.text = weight_de + " tons";
        totalw.text = "Total weight " + totalWeight + "\nMax Capacity " + MaxWeight;
        int not_used_weight = MaxWeight - totalWeight;
        if (not_used_weight < 0) not_used_weight = 0;
        description.text = "A " + Sname.text + " class " + ShipClass.ToString()
            + "\nEquiped with " + mgc
            + " inch Cannon\n" + trp
            + " torpedo tube(s)\nArmor enough to stop " + armor + " inch shell\nMax speed " + engine + " Knots"
            + "\nTotal hitpoints: " + HitPoints
            + "\nIron Cost:" + IronCost
            + "\nConstruction Time: " + ConstructionTime;
    }
    // Update is called once per frame
    void Update()
    {
        compute_value();
        display_value();
    }
}
