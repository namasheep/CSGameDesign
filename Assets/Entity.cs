// Base entity class with common stats
using System;
using Unity.Mathematics;
public class Entity {
    protected int maxHp;
    protected int currentHp;
    protected int maxMana;
    protected int currentMana;

    public Entity(int maxHp, int maxMana) {
        this.maxHp = maxHp;
        this.currentHp = maxHp;
        this.maxMana = maxMana;
        this.currentMana = maxMana;
    }

    public int getCurrentHp() {
        return currentHp;
        
    }

    public void setCurrentHp(int hp) {
        this.currentHp = math.min(hp, maxHp);
    }

    public int getCurrentMana() {
        return currentMana;
    }

    public void setCurrentMana(int mana) {
        this.currentMana = math.min(mana, maxMana);
    }
    public void setMaxHp(int maxHp){
        this.maxHp = maxHp;
    
    }
    public void setMaxMana(int maxMana){
        this.maxMana = maxMana;

    }
    public int getMaxMana(){
        return maxMana;
    
    }
    public int getMaxHp(){
        return maxHp;

    }
}