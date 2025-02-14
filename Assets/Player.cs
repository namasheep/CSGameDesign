// Player class that extends Entity with additional player-specific features
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour{
    [SerializeField] public string playerName;
    [SerializeField] public int level;
    [SerializeField] public int experience;
    [SerializeField] public Entity entity;
    [SerializeField] public List<Card> deck = new List<Card>();

    public Player(string name, int maxHp, int maxMana, int strength, int defense, int speed) {
        this.playerName = name;
        this.level = 1;
        this.experience = 0;
        entity = new Entity(maxHp, maxMana);
        
    }
    public Player(){
        this.playerName = "Player";
        this.level = 1;
        this.experience = 0;
        entity = new Entity(100, 100);
    }

    public string getName() {
        return name;
    }

    public int getLevel() {
        return level;
    }

    public void gainExperience(int exp) {
        this.experience += exp;
        checkLevelUp();
    }

    private void checkLevelUp() {
        if (experience >= level * 100) {
            levelUp();
        }
    }
    private void levelUp(){
        level++;
        experience = 0;
        entity.setMaxHp(entity.getMaxHp() + 10);
        entity.setMaxMana(entity.getMaxMana() + 5);
    }
}