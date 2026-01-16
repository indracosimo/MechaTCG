using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Leader,
    Facility,
    Staff,
    Mech,
    Tactics
}

public enum CoreType 
{
    EnergyCore,
    TitaniumCore,
    SteelCore,
    CopperCore,
    BismuthCore,
    OmniCore
}

[System.Serializable]
public struct CardCost 
{
    public int energyCost;
    public int titaniumCost;
    public int steelCost;
    public int copperCost;
    public int bismuthCost;
    public int omniCost;
}

[CreateAssetMenu(fileName = "New Card", menuName = "CardGenerator")]
public abstract class BlankCard : ScriptableObject
{
    [Header("Card Type")]
    public string cardName;
    public CardType cardType;
    public Sprite cardArt;

    [Header("Fame & Fortune")]
    public CardCost cardCost;
    public int lumenars;
    public int reputation;

    [Header("Tags")]
    public List<string> tags;

    [Header("Effects")]
    [TextArea] public string activeEffectDescription;
    [TextArea] public string passiveEffectDescription;

}
