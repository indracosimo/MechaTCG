using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public BlankCard card;

    public TMP_Text nameText;
    public TMP_Text activeEffectText;
    public TMP_Text passiveEffectText;

    public Sprite artwork;
    
    //to do
    //all the other fields

    void Start()
    {
        card.Print();

        nameText.text = card.cardName;
        activeEffectText.text = card.activeEffectDescription;
        passiveEffectText.text = card.passiveEffectDescription;
        artwork = card.cardArt;
    }
}
