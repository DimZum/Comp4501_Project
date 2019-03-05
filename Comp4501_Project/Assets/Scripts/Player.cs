using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int ID;
    int next_ship_id;
    float Iron, ManPower, Exp;
    ShipDesign[] SavedDesign;
    ShipYard ShipYards;
    Ship[] Ships;

    public Player(int I,int d)
    {
        ID = I;
        next_ship_id = 0;
        Iron = Constants.DEF_START_IRON + d * 1000;
        ManPower = Constants.DEF_START_MP + d * 200;
        Exp = Constants.DEF_START_EXP + d * 50;
        SavedDesign = new ShipDesign[Constants.MAX_DESIGN_NUM];
        ShipYards = new ShipYard(this);
        Ships = new Ship[Constants.SHIPARRAYLENGTH_START];
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
    public int getNextShipID()
    {
        return next_ship_id;
    }

    public void AddShip(Ship s)
    {

    }

    public void AddDesign(ShipDesign d)
    {

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
