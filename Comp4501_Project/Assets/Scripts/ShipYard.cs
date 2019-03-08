using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipYard
{
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
        ShipConstructionTime = new float[Constants.MAX_BUILD_QUEUE];
        YardAvaliable = 2;
    }
    public void BuildShip(ShipDesign d)
    {
        BuildingQueue.Enqueue(d);
    }

    void UpdateConstruction()
    {
        for(int i = 0; i < YardAvaliable; i++)
        {
            if (BuildingQueue.Count == 0)
            {
                //There is no ship waiting for construction;
                return;
            }else{
                if (ShipInConstruction[i] == null)
                {
                    //If there is free shipyard avaliable
                    ShipDesign d = BuildingQueue.Dequeue();
                    ShipInConstruction[i] = d;
                    ShipConstructionTime[i] = d.getTimeCost();
                }
            }
        }
    }

    void UpdateProgress(float deltaT)
    {
        for (int i = 0; i < YardAvaliable; i++)
        {
            if (ShipConstructionTime[i]>0)
            {
                ShipConstructionTime[i] -= deltaT;
            }else{
                if (ShipInConstruction[i] != null)
                {
                    //Construction is finished and the ship should be added to player
                    Owner.AddShip(ShipInConstruction[i].ToShip(Owner));
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

    // Update is called once per frame
    void Update()
    {
        UpdateProgress(Time.deltaTime);
    }
}
