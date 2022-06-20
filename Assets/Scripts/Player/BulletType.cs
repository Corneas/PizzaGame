using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToppingType
{
    Cheese = 0,
    Onion,
    Tomato,
}

public class BulletType : MonoBehaviour
{
    public GameObject cheesePre = null;
    public GameObject onionPre = null;
    public GameObject tomatoPre = null;
    public ToppingType curToppingType = 0;
}
