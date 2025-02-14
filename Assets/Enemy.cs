// Player class that extends Entity with additional player-specific features
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy : MonoBehaviour{
    public string enemyName;
    public Entity entity;
    public List<Card> deck = new List<Card>();

    public Enemy(string name, int maxHp, int maxMana, int strength, int defense, int speed) {
        this.enemyName = name;
        
        
        entity = new Entity(maxHp, maxMana);
        
    }
    public Enemy(){
        this.enemyName = "Enemy";
        
        entity = new Entity(100, 100);
    }

    public string getName() {
        return name;
    }

   
}
    
