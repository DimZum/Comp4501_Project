using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    int ID;
    Player Owner;
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
    int armor;
    int speed;
    int hp, max_hp;
    public Ship(int i, Player p, int mgc, int mgn, int sgc, int sgn, int trp,int A,int arm, int spd, int hitpoints)
    {
        ID = i;
        Owner = p;
        main_gun_caliber = mgc;
        turret_control(mgn);
        sub_gun_caliber = sgc;
        sub_gun_turret = sgn;
        speed = spd;
        torpedo = trp;
        AntiAir = A;
        max_hp = hitpoints;
        hp = max_hp;
    }
    void fire(Ship target)
    {

    }

    float hitrate_calc(Ship target)
    {

        return 0.0f;
    }

    void turret_control(int num_turret)
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
    }
}
