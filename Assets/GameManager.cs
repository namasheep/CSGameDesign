using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class GameManager : MonoBehaviour
{
    
    public List<PlayerCardSlot> unselectedCardSlots = new List<PlayerCardSlot>();
    public List<PlayerCardSlot> selectedCardSlots = new List<PlayerCardSlot>();
    public List<PlayerCardSlot> enemySelectedCardSlots = new List<PlayerCardSlot>();    
    public Transform discardPile;
    private Card[] selectedCards = new Card[2];
    [SerializeField] private Card[] enemySelectedCards = new Card[2];
    
    
    public TMP_Text deckSizeText;
    public TMP_Text playerHealth;
    public TMP_Text enemyHealth;
    public Player player;
    public Enemy enemy;
    
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DrawCard()
    {
        if (player.deck.Count >= 1)
        {
            Card randCard = player.deck[Random.Range(0, player.deck.Count)];
            for (int i = 0; i < unselectedCardSlots.Count; i++)
            {
                if (unselectedCardSlots[i].isOccupied == false)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    unselectedCardSlots[i].addCard(randCard);
                    
                    player.deck.Remove(randCard);
                    return;
                }
            }
        }
    }
    public void selectCard(PlayerCardSlot slot)
    {
        if(slot.isOccupied == false){
            return;
        }
        Card card = slot.card;
        for(int i = 0; i < selectedCardSlots.Count; i++){
            if(selectedCardSlots[i].isOccupied == false){
                card.gameObject.SetActive(true);
                card.selectedIndex = i;
                selectedCardSlots[i].addCard(card);
                slot.isPlayed = true;
                return;
            }
        }
    }
    private bool validTurn(){
        int cardCount = selectedCardSlots.Count;
        for(int i = 0;i < selectedCardSlots.Count;i++){
            if(selectedCardSlots[i].isOccupied == false){
                cardCount--;
            }

        }
        if(cardCount == 0){
            return false;
        }
        return true;

    }
    public void startTurn(){
        if(!validTurn()){
            return;
        }
        for(int i=0;i<2;i++){
            Card playerCard = selectedCardSlots[i].card;
            Card enemyCard = enemySelectedCards[i];
            duel(playerCard,enemyCard);
        }
        
        /*for(int i = 0; i < selectedCards.Length; i++){
            if(selectedCards[i] != null){
                playerCard = selectedCards[i];
                break;
               
            }
        }
        for(int i = 0; i < enemySelectedCards.Length; i++){
            if(enemySelectedCards[i] != null){
                enemyCard = enemySelectedCards[i];
                break;
               
            }
        }*/
        
        endTurn();
        bool win = checkWin();
        if(win){
            Debug.Log("You Win!");
        }
       
    }
    private void duel(Card pCard, Card eCard){
        if(pCard == null){
            if(eCard!=null){
                player.entity.setCurrentHp(player.entity.getCurrentHp() - eCard.damage);
                enemy.entity.setCurrentMana(enemy.entity.getCurrentMana() - eCard.manaCost);
                
                
            
            }
        }
        else if(eCard == null){
            if(pCard!=null){
                enemy.entity.setCurrentHp(enemy.entity.getCurrentHp() - pCard.damage);
                player.entity.setCurrentMana(player.entity.getCurrentMana() - pCard.manaCost);
            }
        }
        else{
            if(pCard.damage > eCard.damage){
                enemy.entity.setCurrentHp(enemy.entity.getCurrentHp() - pCard.damage);
                player.entity.setCurrentMana(player.entity.getCurrentMana() - pCard.manaCost);
            }
            else if(pCard.damage < eCard.damage){
                player.entity.setCurrentHp(player.entity.getCurrentHp() - eCard.damage);
                enemy.entity.setCurrentMana(enemy.entity.getCurrentMana() - eCard.manaCost);
            }

        }
        

    }
    private void endTurn(){
        for(int i = 0; i < selectedCardSlots.Count; i++){
            if(selectedCardSlots[i].isOccupied){
                selectedCardSlots[i].card.transform.position = discardPile.position;
                selectedCardSlots[i].card.handIndex = -1;
                selectedCardSlots[i].card.gameObject.SetActive(false);
                selectedCardSlots[i].removeCard();
            }
        }
        for(int i = 0; i < unselectedCardSlots.Count; i++){
            if(unselectedCardSlots[i].isOccupied && unselectedCardSlots[i].isPlayed){
                unselectedCardSlots[i].card.handIndex = -1;
                unselectedCardSlots[i].card.gameObject.SetActive(false);
                unselectedCardSlots[i].isPlayed = false;
                unselectedCardSlots[i].removeCard();
            }

        }
        
    }
    private bool checkWin(){
        if(enemy.entity.getCurrentHp() <= 0){
            return true;
        }
        return false;
    }


    void Start()
    {
        for(int i = 0;i<enemySelectedCardSlots.Count;i++){
            enemySelectedCards[i].transform.position = enemySelectedCardSlots[i].slotTransform.position;
        
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        deckSizeText.text = player.deck.Count.ToString();
        playerHealth.text = player.entity.getCurrentHp().ToString();
        enemyHealth.text = enemy.entity.getCurrentHp().ToString();
        
    }
}
