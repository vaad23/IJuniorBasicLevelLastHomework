using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterExperience
{
    [SerializeField] private int _attack;
    [SerializeField] private int _heal;
    [SerializeField] private int _armor;
    [SerializeField] private int _health;

    public int Attack  => _attack; 
    public int Heal  => _heal; 
    public int Armor  => _armor; 
    public int Health  => _health;

    public void AddAttack(int value)
    {
        _attack += value;
    }

    public void AddHeal(int value)
    {
        _heal += value;
    }

    public void AddArmor(int value)
    {
        _armor += value;
    }

    public void AddHealth(int value)
    {
        _health += value;
    }
}
