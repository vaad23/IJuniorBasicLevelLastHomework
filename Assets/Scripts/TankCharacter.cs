using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCharacter : Character
{
    public TankCharacter(CharacterCharacteristics character, CharacterPlacement myComand, CharacterPlacement enemyComand) : base(character, myComand, enemyComand) { }

    protected override void NormalAttack()
    {
        Character enemy = Search.FirstPosition(EnemyComand);
        if (enemy == null)
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndGame)));
        else
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, enemy, CharacterActionEvent.ActionOnTarget.Attack, Attack)));
    }

    protected override void SpecialAttack()
    {
        CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, this, CharacterActionEvent.ActionOnTarget.Heal, Attack)));
    }
}


public class WariorCharacter : Character
{
    public WariorCharacter(CharacterCharacteristics character, CharacterPlacement myComand, CharacterPlacement enemyComand) : base(character, myComand, enemyComand) { }

    protected override void NormalAttack()
    {
        Character enemy = Search.FirstPosition(EnemyComand);
        if (enemy == null)
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndGame)));
        else
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, enemy, CharacterActionEvent.ActionOnTarget.Attack, Attack)));
    }

    protected override void SpecialAttack()
    {
        Character enemy = Search.FirstPosition(EnemyComand);
        if (enemy == null)
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndGame)));
        else
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, enemy, CharacterActionEvent.ActionOnTarget.Attack, Attack * 2)));
    }
}

public class ShooterCharacter : Character
{
    public ShooterCharacter(CharacterCharacteristics character, CharacterPlacement myComand, CharacterPlacement enemyComand) : base(character, myComand, enemyComand) { }

    protected override void NormalAttack()
    {
        Character enemy = Search.FirstPosition(EnemyComand);
        if (enemy == null)
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndGame)));
        else
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, enemy, CharacterActionEvent.ActionOnTarget.Attack, Attack)));
    }

    protected override void SpecialAttack()
    {
        List<Character> enemies = Search.AllLivingCharacters(EnemyComand);
        foreach (var enemy in enemies)
        {
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, enemy, CharacterActionEvent.ActionOnTarget.Attack, Attack / 2)));
        }
    }
}
