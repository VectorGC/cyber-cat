using System;
using System.Linq;
using MonoBehaviours.PropertyFields;
using TMPro;
using UnityEngine;

[Serializable]
public struct TMPSettings
{
    public TMP_FontAsset FontAsset;
    public float CharacterSpacing;
}

[CreateAssetMenu(fileName = "UIDialogs", menuName = "ScriptableObjects/UIDialogs", order = 1)]
public class UIDialogs : ScriptableObject
{
    public LoadWindowView LoadWindowPrefab;

    public TMPSettings TMPSettings;

    public SceneField StartScene;

    private static UIDialogs _instance;

    public static UIDialogs Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.FindObjectsOfTypeAll<UIDialogs>().FirstOrDefault();
            }

            return _instance;
        }
    }
}