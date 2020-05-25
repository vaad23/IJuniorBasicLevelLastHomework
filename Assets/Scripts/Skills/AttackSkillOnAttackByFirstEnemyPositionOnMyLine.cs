using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/AttackSkill OnAttack ByFirstEnemyPositionOnMyLine", order = 51)]
public class AttackSkillOnAttackByFirstEnemyPositionOnMyLine : Skill
{
    [SerializeField] private int _percentageOfAttack = 100;

    public override bool Application(BattleCharacter whoUsing, BattlePlacement battlePlacement)
    {
        BattleCharacter enemy = battlePlacement.Search.FirstEnemyPositionOnMyLine(whoUsing);

        if (enemy != null)
            whoUsing.CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(
                whoUsing, 
                enemy, 
                CharacterActionEvent.ActionOnTarget.Attack, 
                whoUsing.Attack * _percentageOfAttack / 100)));
        else
            whoUsing.CreateAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndGame)));

        return true;
    }
}

