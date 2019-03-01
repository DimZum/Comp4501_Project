using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public enum ShipClass { Destroyer, LightCruiser, HeavyCruiser, BattleCruiser, Battleship, Dreadnought };
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
    public static int[] ClassToWeight = { 200, 500, 800, 2000, 3500, 5000 };
    public static float[] ClassToDrag = { 0.55f, 0.8f, 1.2f, 1.8f, 2.3f, 3.5f};
    public static int[] CannonWeight = { 0,0,10,20,30,
                                         40,75,100,200,300,
                                         400,600,800,1000,1200,
                                         1500,1800,2000,2500,3000,3500};
    public static int trpweight = 50;
}
