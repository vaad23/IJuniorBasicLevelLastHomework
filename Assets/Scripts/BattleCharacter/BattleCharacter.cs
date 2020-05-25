using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleCharacter
{
    private int _maxHealth;
    private int _health;
    private int _attack;
    private int _armor;
    private int _energy;
    private Skill _normalSkill;
    private Skill _specialSkill;
    private BattlePlacement _battlePlacement;

    protected int CountTurns;
    protected BattlePlacement BattlePlacement => _battlePlacement;

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
    public int Energy
    {
        get
        {
            return _energy;
        }
        protected set
        {
            _energy = value;
            ChangedCharacteristics();
        }
    }
    public bool IsAlive => Health > 0;    

    public event UnityAction<CharacterActionEvent> ActionEvent;

  /*  protected BattleCharacter(DatabaseCharacterCharacteristics characteristics, BattlePlacement battlePlacement)
    {
        SetCharacteristics(characteristics);
        SetBattlePlacement(battlePlacement);
    }*/


    public BattleCharacter(DatabaseBattleCharacter databaseBattleCharacter, DatabaseCharacterCharacteristics characteristics, BattlePlacement battlePlacement)
    {
        _normalSkill = databaseBattleCharacter.NormalSkill;
        _specialSkill = databaseBattleCharacter.SpecialSkill;

        SetCharacteristics(characteristics);
        SetBattlePlacement(battlePlacement);
    }

    public void SetCharacteristics(DatabaseCharacterCharacteristics characteristics)
    {
        _maxHealth = characteristics.FirstLevel.Health;
        _health = characteristics.FirstLevel.Health;
        _attack = characteristics.FirstLevel.Attack;
        _armor = characteristics.FirstLevel.Armor;
        _energy = characteristics.FirstLevel.Energy;
    }

    public void SetBattlePlacement(BattlePlacement battlePlacement)
    {
        _battlePlacement = battlePlacement;
    }    

    public virtual void ActionSelection()
    {
        if (_battlePlacement == null)
            return;

        if (CountTurns < 1)
        {
            if (Energy < 100)
            {
                if (_normalSkill.Application(this, BattlePlacement))
                {
                    CountTurns++;
                    Energy += 25;
                }
            }
            else
            {
                if (_specialSkill.Application(this, BattlePlacement))
                {
                    CountTurns++;
                    Energy = 0;
                }
            }
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


    protected virtual void TakeDamage(BattleCharacter fromWhom, int attack)
    {
        int damage = DamageCalculation(attack, Armor);
        ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.OnTarget(fromWhom, this, CharacterActionEvent.ActionOnTarget.AttackResult, Health > damage ? damage : Health)));
        ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.Experience(this, CharacterActionEvent.ActionExperience.Health, Health > damage ? damage : Health)));
        Health -= damage;
        Energy += 5;
    }

    protected virtual void TakeHeal(BattleCharacter fromWhom, int heal)
    {
        heal = (heal + Health > MaxHealth) ? MaxHealth - Health : heal;
        Health += heal;
        ActionEvent?.Invoke(new CharacterActionEvent(new CharacterActionEvent.OnTarget(fromWhom, this, CharacterActionEvent.ActionOnTarget.HealResult, heal)));
    }

   /* protected abstract void SpecialAttack();
    protected abstract void NormalAttack();*/

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

    public void CreateAction(CharacterActionEvent action)
    {
        ActionEvent?.Invoke(action);
    }    
}






