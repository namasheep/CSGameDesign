using UnityEngine;

public interface ICardSlot
{
    bool isOccupied { get; }
    GameObject currentCard { get; }
    int slotIndex { get; set; }
    RectTransform slotTransform { get; } // Added transform property
    void addCard(Card card);
    Card card { get; }        
    Card removeCard();
}

