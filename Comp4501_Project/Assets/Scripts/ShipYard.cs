using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipYard
{
    ResourceManager rm = ResourceManager.instance;

    Player Owner;
    Queue<ShipDesign>BuildingQueue;
    int YardAvaliable;
    ShipDesign[] ShipInConstruction;
    float[] ShipConstructionTime;

    public ShipYard(Player p)
    {
        Owner = p;
        BuildingQueue = new Queue<ShipDesign>();
        ShipInConstruction = new ShipDesign[Constants.MAX_BUILD_QUEUE];

        for (int i = 0; i < ShipInConstruction.Length; i++)
        {
            ShipInConstruction[i] = null;
        }
        //Debug.Log("Player " + p.ID + " dif" + p.diff);
        ShipConstructionTime = new float[Constants.MAX_BUILD_QUEUE];
        YardAvaliable = Mathf.CeilToInt((p.diff+1)/2);
    }
    public void BuildShip(ShipDesign d)
    {
        Debug.Log("added " + d.getName());
        BuildingQueue.Enqueue(d);
    }

    public string ConstructionTimeString()
    {
        string s="";
        for (int i = 0; i < Constants.MAX_BUILD_QUEUE; i++)
        {
            if (i >= YardAvaliable)
            {
                s += "--:--:--\n";
            }
            else
            {
                if (ShipInConstruction[i] == null)
                {
                    s += "--:--:--\n";
                }
                else
                {
                    float t = ShipConstructionTime[i];
                    s += Mathf.FloorToInt(t / 3600) + ":" + Mathf.FloorToInt((t % 3600) / 60) + ":" + Mathf.FloorToInt((t % 60))+"\n";
                }
            }
        }
        return s;
    }
    public string ConstructionNameString()
    {
        string s = "";
        for (int i = 0; i < Constants.MAX_BUILD_QUEUE; i++)
        {
            if (i >= YardAvaliable)
            {
                s += "------\n";
            }
            else
            {
                if (ShipInConstruction[i] == null)
                {
                    s += "------\n";
                }
                else
                {
                    s += ShipInConstruction[i].getName()+"\n";
                }
            }
        }
        return s;
    }

    public ShipDesign getDesignInConstruction(int i)
    {
        if(i<0||i >= Constants.MAX_BUILD_QUEUE)
        {
            return null;
        }
        else
        {
            return ShipInConstruction[i];
        }
    }

    public int getYardStat(int i)
    {
        //Return yard status by integer
        //0 for not avaliable
        //1 for idle
        //2 for busy
        if (i < 0 || i >= Constants.MAX_BUILD_QUEUE)
        {
            return 0;
        }
        else
        {
            if (i>=YardAvaliable)
            {
                return 0;
            }
            else if (ShipInConstruction[i] == null)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }

    public int getAvaliableYard()
    {
        return YardAvaliable;
    }

    void UpdateConstruction()
    {
        //Debug.Log("Called Update Construction");
        for (int i = 0; i < YardAvaliable; i++)
        {
            //Debug.Log("In the for loop "+i);
            if (BuildingQueue.Count == 0)
            {
                //There is no ship waiting for construction;
                //Debug.Log("Build queue is empty");
                return;
            }else{
                //Debug.Log("Currently updateing Shipyard " + i + " empty is " + (ShipInConstruction[i] == null));
                if (ShipInConstruction[i] == null) {
                    //If there is free shipyard avaliable
                    ShipDesign d = BuildingQueue.Dequeue();
                    //Debug.Log("Dequeueing " + d.getName());
                    ShipInConstruction[i] = d;
                    ShipConstructionTime[i] = d.getTimeCost();
                }
            }
        }
    }

    public ShipDesign[] getConstructionList()
    {
        return ShipInConstruction;
    }

    public int getQueueLength()
    {
        return BuildingQueue.Count;
    }

    void UpdateProgress(float deltaT) {
        for (int i = 0; i < YardAvaliable; i++) {
            if (ShipConstructionTime[i]>0) {
                ShipConstructionTime[i] -= deltaT;
            } else {
                if (ShipInConstruction[i] != null) {
                    // Create gameobject from prefab
                    GameObject prefab = rm.GetShipPrefab(ShipInConstruction[i].getClass());
                    GameObject ship = GameObject.Instantiate(prefab,Owner.basePos, Quaternion.identity) as GameObject;

                    // Initialization of ship variables
                    ShipInConstruction[i].InitializeShip(ship);
                    ship.GetComponent<Ship>().Owner = Owner;
                    Debug.Log("Owner: " + Owner.ID + ", Base Pos:" + Owner.basePos);
                    Debug.Log("Ship: " + ship.name + ", ShipPos :" + ship.transform.position);
                    //ship.transform.position = Owner.basePos;

                    //Construction is finished and the ship should be added to player
                    Owner.AddShip(ship);
                    ShipInConstruction[i] = null;
                    ShipConstructionTime[i] = 0;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetResourceForNextYard()
    {
        return 300+YardAvaliable * 250;
    }

    public void UnlockNextYard()
    {
        if(YardAvaliable< Constants.MAX_BUILD_QUEUE-1)
        {
            YardAvaliable++;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateConstruction();
        UpdateProgress(Time.deltaTime);
    }
}
