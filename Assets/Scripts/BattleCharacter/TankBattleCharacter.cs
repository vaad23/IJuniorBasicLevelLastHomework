using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class TankBattleCharacter : BattleCharacter
{
    public TankBattleCharacter(DatabaseCharacterCharacteristics character, BattlePlacement battlePlacement) : base(character, battlePlacement) { }

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
        CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(this, this, CharacterActionEvent.ActionOnTarget.Heal, Attack)));
    }
}*/

