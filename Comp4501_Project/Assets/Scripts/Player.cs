using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    GameMaster gm;
    ResourceManager rm;
    
    List<GameObject> ships;
    List<ShipDesign> shipDesigns;
    public List<ShipDesign> ShipDesigns {
        get { return shipDesigns; }
    }

    public List<GameObject> Ships {
        get { return ships; }
    }
    
    public GameObject baseObj;
    public Vector3 basePos;
    int baseOffset_x = 125; // Used to position the shipyard and factory

    public GameObject factory;
    public Vector3 factoryPos;

    public GameObject shipyard;
    public Vector3 shipyardPos;
    
    public int ID;
    int next_ship_id, next_design_id;
    public float Iron, ManPower, Exp;
    public Color32 PlayerColor;
    ShipYard ShipYards;
    public int diff;
    float timer;

    public Player(int I,int d,Vector3 pos) {
        gm = GameMaster.instance;
        rm = ResourceManager.instance;

        ID = I;
        next_ship_id = 0;
        next_design_id = 0;
        Iron = Constants.DEF_START_IRON + d * 1000;
        ManPower = Constants.DEF_START_MP + d * 200;
        Exp = Constants.DEF_START_EXP + d * 50;

        basePos = pos;
        ships = new List<GameObject>();
        shipDesigns = new List<ShipDesign>();

        diff = d;
        timer = 0;
        ShipYards = new ShipYard(this);


        AddDesign(new ShipDesign("Destroyer", Constants.ShipClass.Destroyer, 5, 3, 0, 0, 4, 0, 2, 34, false, 1800, 13, 100, 50, 15));
        AddDesign(new ShipDesign("Light Cruiser", Constants.ShipClass.LightCruiser, 6, 4, 3, 3, 3, 0, 4, 33, false, 3800, 25, 250, 150, 35));
        AddDesign(new ShipDesign("Heavy Cruiser", Constants.ShipClass.HeavyCruiser, 8, 4, 4, 4, 0, 0, 6, 30, true, 5800, 50, 500, 300, 45));
        

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

        CreateBase();
    }

    // Creates all building gameobjects
    public void CreateBase() {
        baseObj = GameObject.Instantiate<GameObject>(rm.basePrefab);
        baseObj.transform.position = basePos;
        baseObj.GetComponent<Base>().Owner = this;
        
        factory = GameObject.Instantiate<GameObject>(rm.factoryPrefab);
        factory.transform.position = new Vector3(basePos.x + baseOffset_x, factory.transform.position.y, basePos.z);
        factory.GetComponent<Factory>().Owner = this;

        shipyard = GameObject.Instantiate<GameObject>(rm.shipyardPrefab);
        shipyard.transform.position = new Vector3(factory.transform.position.x + baseOffset_x, shipyard.transform.position.y, basePos.z - 20);
        shipyard.GetComponent<ShipyardManager>().Owner = this;

        /*
        if (ID == 1) {
            GameObject humanoid = GameObject.Instantiate<GameObject>(rm.humanoidPrefab);
            humanoid.transform.position = new Vector3(humanoid.transform.position.x, humanoid.transform.position.y, shipyard.transform.position.z + 350);
        }*/
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
        shipDesigns.Add(d);
        Debug.Log("Player " + ID + " received design of " + shipDesigns[next_design_id].getName()+"\nCurrent design: "+ (next_design_id+1));
        next_design_id++;
    }

    // Update is called once per frame
    public void UpdatePlayer()
    {
        ResourceIncome(Time.deltaTime);
        if (timer > 5)
        {
            timer = 0;
            AI_Control();
        }
        timer += Time.deltaTime;
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
            timer = -5;
            return;
        }
        else if(R < 70){
            //Build more ship
            float s = Random.value * 100;
            if (s < 35)
            {
                //Build Destroyer
                buildShip(ShipDesigns[0]);
            }
            else if(s < 55)
            {
                //build Light Cruiser;
                buildShip(ShipDesigns[1]);
            }
            else if (s < 70)
            {
                //build Heavy cruiser
                buildShip(ShipDesigns[2]);
            }
            else if (s < 80)
            {
                //Build BattleCruiser
                buildShip(ShipDesigns[1]);
            }
            else if (s < 90)
            {
                //Build BattleShip
                buildShip(ShipDesigns[0]);
            }
            else
            {
                //Build Dreadnought
                buildShip(ShipDesigns[2]);
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
            foreach (GameObject o in ships)
            {
                o.GetComponent<Ship>().SetMove(gm.player.baseObj.transform.position);
            }
        }
    }
}