using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    // Ship identity
    int ID;
    Player Owner;

    ShipStats stats;

    int main_gun_caliber;
    int main_gun_turret_front;
    float firing_arc_front;
    int main_gun_turret_side;
    float firing_arc_side;
    int main_gun_turret_back;
    float firing_arc_back;
    int sub_gun_caliber;
    int sub_gun_turret;
    int torpedo;
    int AntiAir;

    ShipDesign design;
    Ship target;

    public Ship(int i, Player p, int mgc, int mgn, int sgc, int sgn, int trp,int A,int arm, int spd, int hitpoints,ShipDesign d)
    {
        ID = i;
        Owner = p;
        main_gun_caliber = mgc;
        Turret_group(mgn);
        sub_gun_caliber = sgc;
        sub_gun_turret = sgn;
        torpedo = trp;
        AntiAir = A;
        design = d;

        stats.MaxHealth = hitpoints;
        stats.CurrentHealth = hitpoints;
        stats.Speed = spd;
    }

    public void Fire()
    {
        if (main_gun_turret_front == 0)
        {
            return; //This means the ship is not armed so it cant fire
        }
        Vector3 forward = gameObject.transform.forward;
        Vector3 enemyV = target.gameObject.transform.position - gameObject.transform.position;
        float distance = enemyV.magnitude;
        forward = forward.normalized;
        enemyV = enemyV.normalized;
        float angle = Mathf.Acos(Vector3.Dot(forward, enemyV))*180.00f/Constants.PI;
        Debug.Log("Ship forward: "+forward+", Ship position:"+ gameObject.transform.position+ ", Target position: "+ target.gameObject.transform.position + ", Calculated Angle: " + angle);

        FireFront(angle);
        FireMiddle(angle);
        FireBack(angle);
    }

    void FireFront(float angle)
    {
        float chance = Hitrate_calc();
        if (main_gun_turret_front > 0 && Mathf.Abs(angle-180) <firing_arc_front)
        {
            for(int i = 0; i < main_gun_turret_front; i++)
            {
                if (Random.value * 100 < chance)
                {
                    //It is a hit
                    //Hit effect on target
                    target.stats.TakeDamage(main_gun_caliber);
                }
            }
        }
    }

    void FireMiddle(float angle)
    {

    }
    void FireBack(float angle)
    {

    }

    void FiringEffect(Vector3 pos)
    {
        //Add some smoke effect for firing
    }

    float Hitrate_calc()
    {
        return 0.0f;
    }


    void Turret_group(int num_turret)
    {
        /* Compute where the turret should go.
         * Main turrets
         * 1 turret: front
         * 2 turret: one front and one back.
         * 3 turret: 2 front and 1 back,
         * 4 turret: 2 front and 2 back
         * 5 turret: 2 front and 2 back, plus one in middle
         * 6 turret: 2 front and 2 back and 2 middle
         * 7 turret: 3 front and 2 back and 2 middle
         */
        switch (num_turret)
        {
            case 0:
                main_gun_turret_front = 0;
                firing_arc_front = 0;
                main_gun_turret_side = 0;
                firing_arc_side = 0;
                main_gun_turret_back = 0;
                firing_arc_back = 0;
                break;
            case 1:
                main_gun_turret_front =1;
                firing_arc_front = 160;
                main_gun_turret_side =0;
                firing_arc_side=0;
                main_gun_turret_back=0;
                firing_arc_back=0;
                break;
            case 2:
                main_gun_turret_front = 1;
                firing_arc_front = 150;
                main_gun_turret_side = 0;
                firing_arc_side = 0;
                main_gun_turret_back = 1;
                firing_arc_back = 150;
                break;
            case 3:
                main_gun_turret_front = 2;
                firing_arc_front = 140;
                main_gun_turret_side = 0;
                firing_arc_side = 0;
                main_gun_turret_back = 1;
                firing_arc_back = 145;
                break;
            case 4:
                main_gun_turret_front = 2;
                firing_arc_front = 135;
                main_gun_turret_side = 0;
                firing_arc_side = 0;
                main_gun_turret_back = 2;
                firing_arc_back = 135;
                break;
            case 5:
                main_gun_turret_front = 2;
                firing_arc_front = 130;
                main_gun_turret_side = 1;
                firing_arc_side = 70;
                main_gun_turret_back = 2;
                firing_arc_back = 130;
                break;
            case 6:
                main_gun_turret_front = 2;
                firing_arc_front = 125;
                main_gun_turret_side = 2;
                firing_arc_side = 65;
                main_gun_turret_back = 2;
                firing_arc_back = 125;
                break;
            case 7:
                main_gun_turret_front = 3;
                firing_arc_front = 120;
                main_gun_turret_side = 2;
                firing_arc_side = 65;
                main_gun_turret_back = 2;
                firing_arc_back = 120;
                break;
        }
    }
}
