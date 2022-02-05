using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerProfile : ICloneable
{
    public string Mail { get; }
    public string Name { get; }
    public string SurName { get; }
    public string Tocken { get; }
    public DateTime LastServerChange { get; }
    public DateTime LastClientChange { get; }
    public string[] LastIPs { get; }

    public string Serialize();
}

public class PlayerProfile : IPlayerProfile
{
    public string Mail
    {
        get;
        private set;
    }

    public string Name
    {
        get;
        private set;
    }

    public string SurName
    {
        get;
        private set;
    }

    public string Tocken
    {
        get;
        private set;
    }

    public DateTime LastServerChange
    {
        get;
        private set;
    }

    public DateTime LastClientChange
    {
        get;
        private set;
    }

    public string[] LastIPs
    {
        get;
        private set;
    }

    public PlayerProfile()
    {

    }

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }
    
    public object Clone()
    {
        return new PlayerProfile
        {
            Mail = Mail,
            Name = Name,
            SurName = SurName,
            Tocken = Tocken,
            LastServerChange = LastServerChange,
            LastClientChange = LastClientChange,
            LastIPs = LastIPs
        };
    }

    public static IPlayerProfile Deserialize(string jsonData)
    {
        return JsonUtility.FromJson<IPlayerProfile>(jsonData);
    }
}
