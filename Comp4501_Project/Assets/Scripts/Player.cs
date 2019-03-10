using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int ID;
    int next_ship_id,next_design_id;
    float Iron, ManPower, Exp;
    public Color32 PlayerColor;
    ShipDesign[] SavedDesign;
    ShipYard ShipYards;
    Ship[] Ships;

    public Player(int I,int d)
    {
        ID = I;
        next_ship_id = 0;
        next_design_id = 0;
        Iron = Constants.DEF_START_IRON + d * 1000;
        ManPower = Constants.DEF_START_MP + d * 200;
        Exp = Constants.DEF_START_EXP + d * 50;
        Ships = new Ship[Constants.SHIPARRAYLENGTH_START];
        SavedDesign = new ShipDesign[Constants.MAX_DESIGN_NUM];
        ShipYards = new ShipYard(this);
        if(ID == 0)
        {
            PlayerColor = new Color32(5, 5, 180,255);
        }else if (ID == 1)
        {
            PlayerColor = new Color32(180, 5, 5,255);
        }
        else
        {
            PlayerColor = new Color32(5, 180, 180, 255);
        }
    }

    // Start is called before the first frame update
    void buildShip(ShipDesign d)
    {
        if (Iron >= d.getIronCost() && ManPower >= d.getMPCost())
        {
            Iron -= d.getIronCost();
            ManPower -= d.getMPCost();
            ShipYards.BuildShip(d);
        }
    }

    public Color32 getPlayerColor()
    {
        return PlayerColor;
    }

    public ShipYard GetShipYard()
    {
        return ShipYards;
    }

    public int getNextShipID()
    {
        return next_ship_id;
    }

    public int getNextDesignID()
    {
        return next_design_id;
    }

    public void AddShip(Ship s)
    {
        if (next_ship_id >= Ships.Length)
        {
            extend_ship_array();
        }
        Ships[next_ship_id] = s;
        next_ship_id++;
    }

    public void AddDesign(ShipDesign d)
    {
        if (next_design_id >= SavedDesign.Length)
        {
            Debug.Log("Error, design full");
            //Should extend the array to store more design, but for now just return;
            return;
        }
        SavedDesign[next_design_id] = d;
        Debug.Log("Player " + ID + " received design of " + SavedDesign[next_design_id].getName()+"\nCurrent design: "+ (next_design_id+1));
        next_design_id++;
    }

    public ShipDesign[] GetShipDesigns()
    {
        return SavedDesign;
    }

    void extend_ship_array()
    {
        Ship[] newarray = new Ship[Ships.Length*2];
        for(int i = 0;i<Ships.Length;i++)
        {
            newarray[i]=Ships[i];
        }
        Ships = newarray;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
