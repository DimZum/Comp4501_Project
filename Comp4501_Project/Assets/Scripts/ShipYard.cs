using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipYard : MonoBehaviour
{
    Player Owner;
    Queue<ShipDesign>BuildingQueue;
    int YardAvaliable;
    Ship[] ShipInConstruction;
    float[] ShipConstructionTime;
    ShipYard(Player p)
    {
        Owner = p;
        BuildingQueue = new Queue<ShipDesign>();
        ShipInConstruction = new Ship[Constants.MAX_BUILD_QUEUE];
        ShipConstructionTime = new float[Constants.MAX_BUILD_QUEUE];
        //
    }
    void BuildShip(ShipDesign d)
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
            }
            else
            {
                if (ShipInConstruction[i] == null)
                {
                    ShipDesign d = BuildingQueue.Dequeue();
                    ShipInConstruction[i] = d.ToShip(Owner);
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
                    Owner.AddShip(ShipInConstruction[i]);
                    ShipInConstruction[i] = null;
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
        
    }
}
