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

        ShipDesigner designer = new ShipDesigner();
        AddDesign(designer.CreateDesignWithValues("Destroyer", Constants.ShipClass.Destroyer, 5, 3, 0, 0, 4, 0, 2, 34, false));
        AddDesign(designer.CreateDesignWithValues("Light Cruiser", Constants.ShipClass.LightCruiser, 6, 4, 3, 3, 3, 0, 4, 33, false));
        AddDesign(designer.CreateDesignWithValues("Heavy Cruiser", Constants.ShipClass.HeavyCruiser, 8, 4, 4, 4, 0, 0, 6, 30, true));
        AddDesign(designer.CreateDesignWithValues("Battle Cruiser", Constants.ShipClass.BattleCruiser, 12, 4, 5, 4, 0, 0, 8, 30, true));
        AddDesign(designer.CreateDesignWithValues("BattleShip", Constants.ShipClass.Battleship, 15, 4, 5, 5, 0, 0, 12, 28, true));
        AddDesign(designer.CreateDesignWithValues("Dreadnought", Constants.ShipClass.Battleship, 19, 4, 8, 5, 0, 0, 18, 25, true));

        if (ID == 0)
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
            Iron -= d.getIronCost();
            ManPower -= d.getMPCost();
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
        ResourceIncome(Time.deltaTime);
        ShipYards.Update();
    }

    void ResourceIncome(float deltaT)
    {
        Iron += 5 * deltaT;
        ManPower += 2 * deltaT;
    }

    public void ExpIncome(float amount)
    {
        Exp += amount;
    }

    public void ExpandShipYard()
    {
        if(Iron >= ShipYards.GetResourceForNextYard())
        {
            Iron -= ShipYards.GetResourceForNextYard();
            ShipYards.UnlockNextYard();
        }
    }

    void AI_Control()
    {
        if (ID == 0)
        {
            //the human player has ID=0
            return;
        }


        /*  Random value for AI
         *  20% do nothing
         *  50% build more ship
         *  10% try build more shipyard
         *  0~20% Attack player with all ships
         */
        float R = Random.value*100;

        if(R < 20){
            //do nothing
            return;
        }
        else if(R < 70){
            //Build more ship
            float s = Random.value * 100;
            if (s < 35)
            {
                //Build Destroyer
            }else if(s < 55)
            {
                //build Light Cruiser;
            }else if (s < 70)
            {
                //build Heavy cruiser
            }else if (s < 80)
            {
                //Build BattleCruiser
            }else if (s < 90)
            {
                //Build BattleShip
            }
            else
            {
                //Build Dreadnought
            }
        }
        else if (R<80)
        {
            //Build shipYard 
            if (Iron >= ShipYards.GetResourceForNextYard())
            {
                ExpandShipYard();
            }
        }
        else
        {
            //Attack

        }
    }
}