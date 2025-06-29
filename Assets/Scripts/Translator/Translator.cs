using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public static Translator Instance { get; private set; }

    private Dictionary<string, string> translations = new Dictionary<string, string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("translations/ca");
        if (jsonFile == null)
        {
            Debug.LogError("No s'ha trobat el fitxer translations.json dins de Resources.");
            return;
        }

        var rawData = MiniJSON.Deserialize(jsonFile.text) as Dictionary<string, object>;
        translations.Clear();
        FlattenJson(rawData, "", translations);
    }

    private void FlattenJson(Dictionary<string, object> node, string prefix, Dictionary<string, string> dict)
    {
        foreach (var pair in node)
        {
            string key = string.IsNullOrEmpty(prefix) ? pair.Key : $"{prefix}.{pair.Key}";

            if (pair.Value is Dictionary<string, object> childDict)
            {
                FlattenJson(childDict, key, dict);
            }
            else
            {
                dict[key] = pair.Value.ToString();
            }
        }
    }

    public string Translate(string key)
    {
        if (translations.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning($"Traducció no trobada per la clau: {key}");
            return key;
        }
    }
}
