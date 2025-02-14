using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class GameManager : MonoBehaviour
{
    
    public Transform[] unselectedCardSlots;
    public Transform[] selectedCardSlots;
    public Transform[] enemySelectedCardSlots;
    public Transform discardPile;
    private Card[] selectedCards = new Card[2];
    [SerializeField] private Card[] enemySelectedCards = new Card[2];
    public bool[] availableCardSlots;
    public bool[] availableSelectedCardSlots;
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
            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    
                    randCard.transform.position = unselectedCardSlots[i].position;
                    availableCardSlots[i] = false;
                    player.deck.Remove(randCard);
                    return;
                }
            }
        }
    }
    public void selectCard(Card card)
    {
        for(int i = 0; i < availableSelectedCardSlots.Length; i++){
            if(availableSelectedCardSlots[i] == true){
                card.gameObject.SetActive(true);
                card.selectedIndex = i;
                selectedCards[i] = card;
                card.transform.position = selectedCardSlots[i].position;
                availableSelectedCardSlots[i] = false;
                return;
            }
        }
    }
    private bool validTurn(){
        int cardCount = availableCardSlots.Length;
        for(int i = 0;i < availableSelectedCardSlots.Length;i++){
            if(availableSelectedCardSlots[i]){
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
            Card playerCard = selectedCards[i];
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
        for(int i = 0; i < selectedCards.Length; i++){
            if(selectedCards[i] != null){
                selectedCards[i].transform.position = discardPile.position;
                availableSelectedCardSlots[i] = true;
                availableCardSlots[selectedCards[i].handIndex] = true;
                selectedCards[i].handIndex = -1;
                selectedCards[i] = null;
            }
        }
        
    }


    void Start()
    {
        for(int i = 0;i<enemySelectedCardSlots.Length;i++){
            enemySelectedCards[i].transform.position = enemySelectedCardSlots[i].position;
        
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
