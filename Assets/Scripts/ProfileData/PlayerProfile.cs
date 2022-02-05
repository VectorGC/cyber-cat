using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerProfile
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

    public PlayerProfile(IPlayerProfile playerProfile)
    {
        CopyFromAnother(playerProfile);
    }

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }

    private void CopyFromAnother(IPlayerProfile playerProfile)
    {
        Mail = playerProfile.Mail;
        Name = playerProfile.Name;
        SurName = playerProfile.SurName;
        Tocken = playerProfile.Tocken;
        LastServerChange = playerProfile.LastServerChange;
        LastClientChange = playerProfile.LastClientChange;
        LastIPs = playerProfile.LastIPs;
    }

    public static IPlayerProfile Deserialize(string jsonData)
    {
        return JsonUtility.FromJson<IPlayerProfile>(jsonData);
    }
}
