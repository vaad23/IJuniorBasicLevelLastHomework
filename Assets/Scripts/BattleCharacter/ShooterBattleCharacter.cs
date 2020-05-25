using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class ShooterBattleCharacter : BattleCharacter
{
    public ShooterBattleCharacter(DatabaseCharacterCharacteristics character, BattlePlacement battlePlacement) : base(character, battlePlacement) { }

    protected override void NormalAttack()
    {
        BattleCharacter enemy = BattlePlacement.Search.FirstEnemyPositionOnMyLine(this);
        if (enemy == null)
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndGame)));
        else
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, enemy, CharacterActionEvent.ActionOnTarget.Attack, Attack)));
    }

    protected override void SpecialAttack()
    {
        List<BattleCharacter> enemies = BattlePlacement.Search.AllEnemyLivingTeam(this);
        foreach (var enemy in enemies)
        {
            CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, enemy, CharacterActionEvent.ActionOnTarget.Attack, Attack / 2)));
        }
    }
}*/

