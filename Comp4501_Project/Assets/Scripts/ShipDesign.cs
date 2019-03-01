using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDesign : MonoBehaviour
{
    Constants.ShipClass ShipClass;
    int ID;//The id of ship design, not the ship.
    int mgc, mgn, sgc, sgn, trp, AA;
    int armor, speed;
    bool hasbulge;
    int totalWeight;
    int HitPoints;
    float IronCost;
    float ManCost;
    float ConstructionTime;
    
    ShipDesign(int i, int maingc, int maingn, int subgc, int subgn, int trpdo, int A, int arm, int eng
                , bool bulge, int tweight, int HP ,int Icost,int Mcost, int Tcost)
    {
        ID = i;
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

    public Ship ToShip(Player P, int i){
       Ship s = new Ship(i, P, mgc,mgn,sgc,sgn,trp,AA,armor,speed,HitPoints);
        return s;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
