using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDesign
{
    string Classname;
    Constants.ShipClass ShipClass;
 //   int ID;//The id of ship design, not the ship.
    int mgc, mgn, sgc, sgn, trp, AA;
    int armor, speed;
    bool hasbulge;
    int totalWeight;
    int HitPoints;
    float IronCost;
    float ManCost;
    float ConstructionTime;
    
    public ShipDesign(string name, Constants.ShipClass c, int maingc, int maingn, int subgc, int subgn, int trpdo, int A, int arm, int eng
                , bool bulge, int tweight, int HP , float Icost, float Mcost, float Tcost)
    {
        Classname = name;
        ShipClass = c;
        mgc = maingc;
        mgn = maingn;
        sgc = subgc;
        sgn = subgn;
        trp = trpdo;
        AA = A;
        armor = arm;
        speed = eng;
        hasbulge = bulge;
        totalWeight = tweight;
        HitPoints = HP;
        IronCost = Icost;
        ManCost = Mcost;
        ConstructionTime = Tcost;
    }

    public Ship ToShip(Player P){
       Ship s = new Ship(P.getNextShipID(), P, mgc,mgn,sgc,sgn,trp,AA,armor,speed,HitPoints);
        return s;
    }

    public string getName()
    {
        return Classname;
    }

    public Constants.ShipClass getClass()
    {
        return ShipClass;
    }
    
    public float getIronCost()
    {
        return IronCost;
    }
    public float getMPCost()
    {
        return ManCost;
    }
    public float getTimeCost()
    {
        return ConstructionTime;
    }
    
    public 

    void Update()
    {
        
    }
}
