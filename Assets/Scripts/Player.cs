using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money;
    public float moneyMultiplier;
    public int damage;
    public int areaOfEffectSize;
    public int pierce;
    public float xpMultiplier;
    public int health;
    public int defence;
    public float movementSpeed;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public static Player CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Player>(jsonString);
    }
}
