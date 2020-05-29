using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/AttackSkill OnAttack ByFirstEnemyPositionOnMyLine", order = 51)]
public class AttackSkillOnAttackByFirstEnemyPositionOnMyLineDatabase : SkillDatabase
{
    [SerializeField] private int _percentageOfAttack = 100;

    public override Skill Skill() => new AttackSkillOnAttackByFirstEnemyPositionOnMyLine(_percentageOfAttack);
}

public class AttackSkillOnAttackByFirstEnemyPositionOnMyLine : Skill
{
    private int _percentageOfAttack;

    public AttackSkillOnAttackByFirstEnemyPositionOnMyLine(int percentageOfAttack)
    {
        _percentageOfAttack = percentageOfAttack;
    }

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

