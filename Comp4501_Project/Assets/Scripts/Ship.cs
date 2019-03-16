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
        turret_group(mgn);
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

        fireFront(angle);
        fireMiddle(angle);
        fireBack(angle);
    }

    void fireFront(float angle)
    {

    }
    void fireMiddle(float angle)
    {

    }
    void fireBack(float angle)
    {

    }

    float hitrate_calc(Ship target)
    {

        return 0.0f;
    }



    void turret_group(int num_turret)
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
