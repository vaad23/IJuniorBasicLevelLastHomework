using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Character
{
    private int _maxHealth;
    private int _health;
    private int _attack;
    private int _armor;
    private int _energy;

    private CharacterPlacement _myComand;
    private CharacterPlacement _enemyComand;
    protected int CountTurns;

    public int MaxHealth { get => _maxHealth; protected set => _maxHealth = value; }
    public int Health
    {
        get
        {
            return _health;
        }
        protected set
        {
            _health = value;
            ChangedCharacteristics();
            if (_health <= 0)
                ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.NoTarget(this, CharacterActionEvent.ActionNoTarget.Dead)));
        }
    }
    public int Attack { get => _attack; protected set => _attack = value; }
    public int Armor { get => _armor; protected set => _armor = value; }
    public int Energy { get => _energy; protected set => _energy = value; }
    public CharacterPlacement MyComand => _myComand;
    public CharacterPlacement EnemyComand => _enemyComand;

    public event UnityAction<CharacterActionEvent> ActionEvent;
    protected TargetSearch Search = new TargetSearch();

    protected Character(CharacterCharacteristics character, CharacterPlacement myComand, CharacterPlacement enemyComand)
    {
        _maxHealth = character.FirstLevel.Health;
        _health = character.FirstLevel.Health;
        _attack = character.FirstLevel.Attack;
        _armor = character.FirstLevel.Armor;
        _energy = character.FirstLevel.Energy;
        
        SetComands(myComand, enemyComand);
    }

    public void SetComands(CharacterPlacement myComand, CharacterPlacement enemyComand)
    {
        _myComand = myComand;
        _enemyComand = enemyComand;
    }

    public virtual void ActionSelection()
    {
        if (CountTurns < 1)
        {
            CountTurns++;
            NormalAttack();
        /*  if (Energy >= 100)
          {
              Energy = 0;
              NormalAttack();
          }
          else
          {
              Energy += 25;
              SpecialAttack();
          }*/
        }
        else
        {
            CountTurns = 0;
            ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndRaund)));
        }
    }

    public virtual void ActionResult(CharacterActionEvent action)
    {
        if (action.Act == CharacterActionEvent.Action.OnTarget)
        {
            switch (action.ActOnTarget.OnTargetEnum)
            {
                case CharacterActionEvent.ActionOnTarget.AttackResult:
                    break;
                case CharacterActionEvent.ActionOnTarget.HealResult:
                    break;
            }
        }
    }

    public virtual void TakeAction(CharacterActionEvent action)
    {
        if (action.Act == CharacterActionEvent.Action.OnTarget)
        {
            switch (action.ActOnTarget.OnTargetEnum)
            {
                case CharacterActionEvent.ActionOnTarget.AttackResult:
                    ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.Experience(this, CharacterActionEvent.ActionExperience.Attack, action.ActOnTarget.Value)));
                    break;
                case CharacterActionEvent.ActionOnTarget.HealResult:
                    ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.Experience(this, CharacterActionEvent.ActionExperience.Heal, action.ActOnTarget.Value)));
                    break;
                case CharacterActionEvent.ActionOnTarget.Attack:
                    TakeDamage(action.ActOnTarget.FromWhom, action.ActOnTarget.Value);
                    break;
                case CharacterActionEvent.ActionOnTarget.Heal:
                    TakeHeal(action.ActOnTarget.FromWhom, action.ActOnTarget.Value);
                    break;
            }
        }
    }


    public void ChangedCharacteristics()
    {
        Characteristics characteristics = new Characteristics(MaxHealth, Health, Attack, Armor, Energy);
        ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.ChangedInfo(this, characteristics)));
    }


    protected virtual void TakeDamage(Character fromWhom, int attack)
    {
        int damage = DamageCalculation(attack, Armor);
        ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.OnTarget(fromWhom, this, CharacterActionEvent.ActionOnTarget.AttackResult, Health > damage ? damage : Health)));
        ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.Experience(this, CharacterActionEvent.ActionExperience.Health, Health > damage ? damage : Health)));
        Health -= damage;
    }

    protected virtual void TakeHeal(Character fromWhom, int heal)
    {
        heal = (heal + Health > MaxHealth) ? MaxHealth - Health : heal;
        Health += heal;
        ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.OnTarget(fromWhom, this, CharacterActionEvent.ActionOnTarget.HealResult, heal)));
    }

    protected abstract void SpecialAttack();
    protected abstract void NormalAttack();

    protected int DamageCalculation(int attack, int armor)
    {
        int damage = attack - armor;
        if (attack * 0.3f > damage)
        {
            ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.Experience(this, CharacterActionEvent.ActionExperience.Armor, attack * 7 / 10)));
            return (int)(attack * 0.3f);
        }
        else
        {
            ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.Experience(this, CharacterActionEvent.ActionExperience.Armor, armor)));
            return damage;
        }
    }

    protected void CreateAction(CharacterActionEvent action)
    {
        ActionEvent?.Invoke(action);
    }

    
}


public class TargetSearch
{
    public Character FirstPosition(CharacterPlacement placement)
    {
        for (int i = 0; i < placement.LengthLine; i++)
        {
            for (int j = 0; j < placement.LengthColumn; j++)
            {
                if (placement.TrySearch(i, j, out Character character) && character.Health > 0)
                    return character;
            }
        }

        return null;
    }

    /* public string ToString(CharacterPlacement placement)
     {
         string allString = "";

         for (int i = 0; i < placement.LengthLine; i++)
         {
             for (int j = 0; j < placement.LengthColumn; j++)
             {
                 allString += placement.ReturnCharacter(i, j) + " ";
             }
         }
         return allString;
     }*/
}



