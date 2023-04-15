using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum Type
{
    Geo,
    Reg,
    Danger
}

public class Waypoint
{
    private Vector3 position;
    private string title;
    private string description;
    private Type type;
    private string json; // json property to store the serialized JSON data

    public Waypoint()
    {
        position = new Vector3(0, 0, 0);
        title = "DEFAULT";
        description = "DEFAULT";
        type = Type.Reg;
        json = string.Empty;
    }

    public Waypoint(Vector3 pos, string ti, string desc, Type ty)
    {
        position = pos;
        title = ti;
        description = desc;
        type = ty;
        json = JsonConvert.SerializeObject(this);
    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
        json = JsonConvert.SerializeObject(this);
    }

    public void SetTitle(string ti)
    {
        title = ti;
        json = JsonConvert.SerializeObject(this);
    }

    public void SetDescription(string desc)
    {
        description = desc;
        json = JsonConvert.SerializeObject(this);
    }

    public void SetType(Type ty)
    {
        type = ty;
        json = JsonConvert.SerializeObject(this);
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }

    public Type GetType()
    {
        return type;
    }

    public string GetJson()
    {
        return json;
    }
}