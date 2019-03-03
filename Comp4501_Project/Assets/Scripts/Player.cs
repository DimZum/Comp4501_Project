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
