using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum Type
{
    geosample,
    regular,
    danger
}

public class Waypoint
{
    public static List<Waypoint> waypointsList = new List<Waypoint>();

    static string nextLetter = "A";
    private string letter;

    private Transform position;
    private string title;
    private Type type;
    private string json; // json property to store the serialized JSON data

    public Waypoint(Transform pos, string ti, Type ty)
    {
        position = pos;
        title = ti;
        type = ty;
        letter = nextLetter;
        json = JsonConvert.SerializeObject(this);

        nextLetter = ((char)(nextLetter[0] + 1)).ToString();
        waypointsList.Add(this);

    }

    public void SetPosition(Transform pos)
    {
        position = pos;
        json = JsonConvert.SerializeObject(this);
    }

    public void SetTitle(string ti)
    {
        title = ti;
        json = JsonConvert.SerializeObject(this);
    }


    public void SetType(Type ty)
    {
        type = ty;
        json = JsonConvert.SerializeObject(this);
    }

    public Transform GetPosition()
    {
        return position;
    }

    public string GetTitle()
    {
        return title;
    }


    public Type GetType()
    {
        return type;
    }

    public string GetJson()
    {
        return json;
    }

    public string GetLetter()
    {
        return letter;
    }
}