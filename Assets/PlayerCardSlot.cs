using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCardSlot : MonoBehaviour, ICardSlot, IPointerClickHandler 
{
    private bool _isOccupied = false;
    private int _slotIndex;
    private Card _card;
    
    // Interface property implementations
    public bool isOccupied => _card != null;
    public GameObject currentCard => _card != null ? _card.gameObject : null;
    public int slotIndex { get; set; }
    public bool isPlayed { get; set; }
    // This line is causing the error
    public RectTransform slotTransform => (RectTransform)transform;

    public Card card => _card;
    
    // Track click count to perform different actions
    private int clickCount = 0;
    
    void Start()
    {
        // Initialize your player card slot
    }
    

    public void addCard(Card card)
    {
        if (card == null) return;
        
        // Set the card reference
        _card = card;
        
        // Set card's position to this slot's position
        card.transform.position = transform.position;
        
        // Set card's parent to this slot
        card.transform.SetParent(transform);
    }

    public Card removeCard()
    {
        if (_card == null) return null;
        
        Card removedCard = _card;
        _card = null;
        
        
        // Unparent the card from this slot
        removedCard.transform.SetParent(null);
        
        return removedCard;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Card slot clicked: " + gameObject.name);
        if (isOccupied){
            GameManager.Instance.selectCard(this);
        }
    }
   

}