using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToppingType
{
    Cheese = 0,
    Pepperoni,
    Tomato,
}

public class BulletType : MonoBehaviour
{
    public GameObject[] toppingPre = null;
    public ToppingType curToppingType = 0;
}
