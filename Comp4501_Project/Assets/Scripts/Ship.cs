using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Ship : MonoBehaviour {

    GameMaster gm;

    // Ship identity
    private int p_id;
    public int ID {
        get { return p_id; }
        set { p_id = value; }
    }
    private Player p_owner;
    public Player Owner {
        get { return p_owner; }
        set { p_owner = value; }
    }

    public Stats stats;

    public int main_gun_caliber;
    public int main_gun_turret_front;
    public float firing_arc_front;
    public int main_gun_turret_side;
    public float firing_arc_side;
    public int main_gun_turret_back;
    public float firing_arc_back;
    public int sub_gun_caliber;
    public int sub_gun_turret;
    public int torpedo;
    public int AntiAir;
    ShipMotor motor;

    ShipDesign design;
    Ship target;
    
    private void Start() {
        gm = GameMaster.instance;
        motor = gameObject.GetComponent<ShipMotor>();
        stats = GetComponent<Stats>();
    }

    // Initialize variables
    public void Init(int mgc, int mgn, int sgc, int sgn, int trp, int A, int arm, int spd, int hitpoints, ShipDesign d)
    {
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

    public void SetMove(Vector3 pos)
    {
        motor.MoveToPoint(pos);
    }

    public void Fire()
    {
        if (main_gun_turret_front + main_gun_turret_side + main_gun_turret_back == 0)
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

        float chance = Hitrate_calc(main_gun_caliber);
        FireFront(angle, chance);
        FireMiddle(angle, chance);
        FireBack(angle, chance);
        //Give player experience each time a ship fires
        p_owner.ExpIncome(0.01f * (int)design.getClass());
    }

    void FireFront(float angle,float chance)
    {
        if (main_gun_turret_front > 0 && Mathf.Abs(angle) <firing_arc_front)
        {
            for(int i = 0; i < main_gun_turret_front; i++)
            {

                if (Random.value * 100 < chance)
                {
                    //It is a hit
                    //Hit effect on p_target
                    target.stats.TakeDamage(main_gun_caliber);
                }
            }
        }
    }

    void FireMiddle(float angle, float chance)
    {
        if (main_gun_turret_side > 0 && Mathf.Abs(angle-90) < firing_arc_side)
        {
            for (int i = 0; i < main_gun_turret_side; i++)
            {
                if (Random.value * 100 < chance)
                {
                    //It is a hit
                    //Hit effect on p_target
                    target.stats.TakeDamage(main_gun_caliber);
                }
            }
        }
    }
    void FireBack(float angle, float chance)
    {
        if (main_gun_turret_back > 0 && angle < firing_arc_back)
        {
            for (int i = 0; i < main_gun_turret_side; i++)
            {
                if (Random.value * 100 < chance)
                {
                    //It is a hit
                    //Hit effect on p_target
                    target.stats.TakeDamage(main_gun_caliber);
                }
            }
        }
    }

    void FiringEffect(Vector3 pos)
    {
        //Add some smoke effect for firing
    }

    float Hitrate_calc(int caliber)
    {
        float hitrate = Constants.BASIC_HIT_RATE;
        hitrate *= getDistModifier(caliber);
        hitrate *= getDirectionModifier();
        hitrate *= getSpeedModifier();
        hitrate *= getSizeModifier();
        return hitrate;
    }

    public float getDistModifier()
    {
        //Maingun is default option.
        return getDistModifier(main_gun_caliber);
    }
    public float getDistModifier(int caliber)
    {
        Vector3 targetDirection = target.gameObject.transform.position - gameObject.transform.position;
        float targetDist = targetDirection.magnitude;
        float distancedifference = (targetDist / Constants.CannonIdealRange[caliber]);
        float Dist_modifier = 1.05f - (distancedifference* distancedifference);
        return Dist_modifier;
    }

    public float getSizeModifier()
    {
        float SizeModifier = 1.0f;
        SizeModifier *= (float)design.getClass()+22.5f/25.0f;
        SizeModifier *= (float)target.design.getClass() + 10.0f / 12.5f;
        return SizeModifier;
    }

    public float getDirectionModifier()
    {
        Vector3 targetHeading = new Vector3(0, target.transform.rotation.y, 0);
        Vector3 targetDirection = target.gameObject.transform.position - gameObject.transform.position;
        targetHeading = targetHeading.normalized;
        targetDirection = targetDirection.normalized;
        float Direction_modifier = 1.125f - Mathf.Abs(Vector3.Dot(targetHeading, targetDirection)) / 4.00f;
        return Direction_modifier;
    }
    
    public float getSpeedModifier()
    {
        float speed_modifier = 1.00f;
        speed_modifier *= 45.00f/target.stats.Speed+20.00f;
        speed_modifier *= 70.00f/stats.Speed+45.00f;
        return speed_modifier;
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
                firing_arc_front = 130;
                main_gun_turret_side = 0;
                firing_arc_side = 0;
                main_gun_turret_back = 2;
                firing_arc_back = 130;
                break;
            case 5:
                main_gun_turret_front = 2;
                firing_arc_front = 120;
                main_gun_turret_side = 1;
                firing_arc_side = 70;
                main_gun_turret_back = 2;
                firing_arc_back = 120;
                break;
            case 6:
                main_gun_turret_front = 2;
                firing_arc_front = 115;
                main_gun_turret_side = 2;
                firing_arc_side = 65;
                main_gun_turret_back = 2;
                firing_arc_back = 1115;
                break;
            case 7:
                main_gun_turret_front = 3;
                firing_arc_front = 105;
                main_gun_turret_side = 2;
                firing_arc_side = 65;
                main_gun_turret_back = 2;
                firing_arc_back = 110;
                break;
        }
    }
}
