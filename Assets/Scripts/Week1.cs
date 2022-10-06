using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Week1 : MonoBehaviour //Basics
{
    public int num1;
    public int num2;
    public int num3;
    public int health = 100;
    public int bonus = 50;
    public GameObject go;
    Light ourLight;

    void Start()
    {
        //diferent ways to print the sum of num1 and num2
        //Debug.Log("Result is " + (num1 + num2));

        AddNumbers(num2, num3);
        AddNumbers(num1, num3);
        AddNumbers(num1, num2);

        health = AddValues(health, bonus);
        Debug.Log(health);

        ourLight = go.GetComponent<Light>();
        ourLight.color = Color.blue;
    }

    //if you tripple / before a function it will give you the follwing; then when u hover over the function it will show what u typed

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_1"></param>
    /// <param name="_2"></param>
    void AddNumbers(int _1, int _2) //void means the functions doesnt return a value
    {
        int sum = _1 + _2;
        Debug.Log("Result is " + (sum));
    }

    int AddValues(int _1, int _2) //a value on a function means they return that value
    {
        return _1 + _2;
    }

}
     

