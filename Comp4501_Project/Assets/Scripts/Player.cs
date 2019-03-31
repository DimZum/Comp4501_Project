using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    GameMaster gm;

    int ID;
    int next_ship_id,next_design_id;
    float Iron, ManPower, Exp;
    public Color32 PlayerColor;
    ShipYard ShipYards;
    public int diff;
    
    List<GameObject> ships;
    List<ShipDesign> shipDesigns;
    public List<ShipDesign> ShipDesigns {
        get { return shipDesigns; }
    }

    public Player(int I,int d) {
        gm = GameMaster.instance;

        ID = I;
        next_ship_id = 0;
        next_design_id = 0;
        Iron = Constants.DEF_START_IRON + d * 1000;
        ManPower = Constants.DEF_START_MP + d * 200;
        Exp = Constants.DEF_START_EXP + d * 50;

        ships = new List<GameObject>();
        shipDesigns = new List<ShipDesign>();

        ShipYards = new ShipYard(this);
        diff = d;

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
    public void buildShip(ShipDesign d)
    {
        if (Iron >= d.getIronCost() && ManPower >= d.getMPCost())
        {
            //Iron -= d.getIronCost();
            //ManPower -= d.getMPCost();
            ShipYards.BuildShip(d);
            Debug.Log("Add " + d.getName() + " to construction");
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

    public void AddShip(GameObject ship) {
        ship.GetComponent<Ship>().ID = next_ship_id;
        ships.Add(ship);
        next_ship_id++;
    }

    public void AddDesign(ShipDesign d)
    {
        if (next_design_id >= shipDesigns.Count)
        {
            Debug.Log("Error, design full");
            //Should extend the array to store more design, but for now just return;
            return;
        }
        shipDesigns[next_design_id] = d;
        Debug.Log("Player " + ID + " received design of " + shipDesigns[next_design_id].getName()+"\nCurrent design: "+ (next_design_id+1));
        next_design_id++;
    }

    // Update is called once per frame
    public void Update()
    {
        ShipYards.Update();
    }
}