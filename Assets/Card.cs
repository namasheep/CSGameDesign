using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    public int handIndex;
    public int selectedIndex;
    
    public int damage;
    public int manaCost;
    public int cardType;
    public TMP_Text cardText;
    public bool isSelected = false;
    public bool selectable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void reset()
    {
        isSelected = false;
        handIndex = -1;
        selectedIndex = -1;

    }
    void Start()
    {
        cardText.text = $"DMG: {damage.ToString()}\nCOST: {manaCost.ToString()}";
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
