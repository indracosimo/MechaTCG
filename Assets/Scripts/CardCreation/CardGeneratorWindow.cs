using System.IO;
using UnityEditor;
using UnityEngine;

public class CardGeneratorWindow : EditorWindow
{
    private CardType selectedType;
    private BlankCard tempCard;

    [MenuItem("Indra's Cool Toolkit/Card Generator")]
    static void Open()
    {
        GetWindow<CardGeneratorWindow>("Card Generator");
    }

    string GetFolderPathForType(CardType type)
    {
        switch (type)
        {
            case CardType.Facility:
                return "Assets/ScriptableObjects/Facility";

            case CardType.Staff:
                return "Assets/ScriptableObjects/Staff";

            case CardType.Mech:
                return "Assets/ScriptableObjects/Mech";

            case CardType.Leader:
                return "Assets/ScriptableObjects/Leader";

            case CardType.Tactics:
                return "Assets/ScriptableObjects/Tactics";

            default:
                return "Assets/ScriptableObjects/Other";
        }
    }
    void CreateFolderRecursively(string fullPath)
    {
        string[] parts = fullPath.Split('/');
        string currentPath = parts[0];

        for (int i = 1; i < parts.Length; i++)
        {
            string nextPath = $"{currentPath}/{parts[i]}";

            if (!AssetDatabase.IsValidFolder(nextPath))
            {
                AssetDatabase.CreateFolder(currentPath, parts[i]);
            }

            currentPath = nextPath;
        }
    }


    void OnGUI()
    {
        selectedType = (CardType)EditorGUILayout.EnumPopup("Card Type", selectedType);

        if (GUILayout.Button("Create New Card"))
        {
            CreateTempCard();
        }
        EditorGUILayout.LabelField("Card Identity", EditorStyles.boldLabel);
        if (tempCard != null)
        {
            SerializedObject so = new SerializedObject(tempCard);
            SerializedProperty prop = so.GetIterator();

            prop.NextVisible(true);
            while (prop.NextVisible(false))
            {
                EditorGUILayout.PropertyField(prop, true);
            }

            so.ApplyModifiedProperties();

            if (GUILayout.Button("Generate"))
            {
                SaveCard();
            }
        }
    }
    void CreateTempCard()
    {
        if (tempCard != null)
        {
            DestroyImmediate(tempCard);
        }

        switch (selectedType)
        {
            case CardType.Staff:
                tempCard = CreateInstance<StaffCard>();
                tempCard.cardType = CardType.Staff;
                break;

            case CardType.Mech:
                tempCard = CreateInstance<MechCard>();
                tempCard.cardType = CardType.Mech;
                break;

            case CardType.Leader:
                tempCard = CreateInstance<LeaderCard>();
                tempCard.cardType = CardType.Leader;
                break;

            case CardType.Tactics:
                tempCard = CreateInstance<TacticCard>();
                tempCard.cardType = CardType.Tactics;
                break;

            case CardType.Facility:
                tempCard = CreateInstance<FacilityCard>();
                tempCard.cardType = CardType.Facility;
                break;

        }
    }
    void SaveCard()
    {
        if (tempCard == null)
            return;

        if(string.IsNullOrEmpty(tempCard.cardName))
        {
            Debug.LogError("Card must have a name before saving.");
            return;
        }   

        string folderPath = GetFolderPathForType(tempCard.cardType);

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            CreateFolderRecursively(folderPath);
        }

        //check for existing asset with same name
        string safeName = string.Concat(tempCard.cardName.Split(Path.GetInvalidFileNameChars()));
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{safeName}.asset");
        tempCard.name = safeName;


        string assetName = string.IsNullOrEmpty(tempCard.cardName) ? "New Card" : tempCard.cardName;

        string fullPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{assetName}.asset");

        AssetDatabase.CreateAsset(tempCard, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        tempCard = null;

    }
}
