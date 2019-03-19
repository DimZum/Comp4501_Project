using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static float PI = 3.1415926f;
    public enum ShipClass { Destroyer, LightCruiser, HeavyCruiser, BattleCruiser, Battleship, Dreadnought };
    public static Sprite[] SHIP_ICONS = new Sprite[6];

    public static int MAX_CALIBER = 20;
    public static int MIN_CALIBER = 3;
    public static int MAX_SUB_CALIBER = 9;
    public static int MIN_SUB_CALIBER = 2;
    public static int MAX_TURRET = 7;
    public static int MIN_TURRET = 1;
    public static int MAX_TORPEDO = 6;
    public static int MIN_ARMOR = 1;
    public static int MIN_ENGINE = 1;
    public static int[] ClassToCapacity = { 2000, 5000, 8000, 20000, 35000, 50000 };
    public static int[] ClassToWeight = { 100, 300, 600, 1500, 2000, 4000 };
    public static float[] ClassToDrag = { 0.55f, 0.8f, 1.2f, 1.8f, 2.3f, 3.5f};
    public static int[] CannonWeight = { 0,0,10,20,35,
                                         50,80,120,200,350,
                                         500,700,950,1200,1500,
                                         1800,2100,2400,2700,3100,3500};
    public static int trpweight = 50;
    public static int MAX_BUILD_QUEUE = 10;
    public static int DEF_START_IRON = 2500;
    public static int DEF_START_MP = 500;
    public static int DEF_START_EXP = 100;
    public static int MAX_DESIGN_NUM = 64;
    public static int SHIPARRAYCount_START = 32;
    
    void Start()
    {
        SHIP_ICONS[0] = Resources.Load<Sprite>("Sprites/Icon_Destroyer");
        SHIP_ICONS[1] = Resources.Load<Sprite>("Sprites/Icon_LCruiser");
        SHIP_ICONS[2] = Resources.Load<Sprite>("Sprites/Icon_HCruiser");
        SHIP_ICONS[3] = Resources.Load<Sprite>("Sprites/Icon_BattleCruiser");
        SHIP_ICONS[4] = Resources.Load<Sprite>("Sprites/Icon_BattleShip");
        SHIP_ICONS[5] = Resources.Load<Sprite>("Sprites/Icon_Dreadnought");
        
    }
}
