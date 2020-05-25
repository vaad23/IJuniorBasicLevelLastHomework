using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacteristicsReadOnly
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _attack = 20;
    [SerializeField] private int _armor = 5;
    [SerializeField] private int _energy = 50;

    public CharacteristicsReadOnly(int health, int attack, int armor, int energy)
    {
        _health = health;
        _attack = attack;
        _armor = armor;
        _energy = energy;
    }

    public int Health => _health;
    public int Attack => _attack;
    public int Armor => _armor;
    public int Energy => _energy;
}
