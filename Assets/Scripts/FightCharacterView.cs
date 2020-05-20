using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightCharacterView : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private int _attack;
    [SerializeField] private int _armor;
    [SerializeField] private int _energy;

    [SerializeField] private Text _maxHealthText;
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _attackText;
    [SerializeField] private Text _armorText;
    [SerializeField] private Text _energyText;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        private set
        {
            _maxHealth = value;
            _maxHealthText.text = _maxHealth.ToString();
        }
    }
    public int Health
    {
        get
        {
            return _health;
        }
        private set
        {
            _health = value;
            _healthText.text = _health.ToString();
        }
    }
    public int Attack
    {
        get
        {
            return _attack;
        }
        private set
        {
            _attack = value;
            _attackText.text = _attack.ToString();
        }
    }
    public int Armor
    {
        get
        {
            return _armor;
        }
        private set
        {
            _armor = value;
            _armorText.text = _armor.ToString();
        }
    }
    public int Energy
    {
        get
        {
            return _energy;
        }
        private set
        {
            _energy = value;
            _energyText.text = _energy.ToString();
        }
    }



    public void ChangeCharacteristic(Characteristics characteristics)
    {
        MaxHealth = characteristics.MaxHealth;
        Health = characteristics.Health;
        Attack = characteristics.Attack;
        Armor = characteristics.Armor;
        Energy = characteristics.Energy;
    }
}
