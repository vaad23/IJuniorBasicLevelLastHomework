using System;

[Serializable]
public class Characteristics : CharacteristicsReadOnly
{
    private int _maxHealth;

    public int MaxHealth => _maxHealth;

    public Characteristics(int maxHealth, int health, int attack, int armor, int energy) : base(health, attack, armor, energy)
    {
        _maxHealth = maxHealth;
    }

    public override string ToString()
    {
        return $"MaxHealth: {MaxHealth} | " +
            $"Health: {Health} | " +
            $"Attack: {Attack} | " +
            $"Armor: {Armor} | " +
            $"Energy: {Energy} ";
    }
}
