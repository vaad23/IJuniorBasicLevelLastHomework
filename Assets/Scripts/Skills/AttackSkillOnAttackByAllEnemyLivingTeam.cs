using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/AttackSkill OnAttack ByAllEnemyLivingTeam", order = 51)]
public class AttackSkillOnAttackByAllEnemyLivingTeam : Skill
{
    [SerializeField] private int _percentageOfAttack = 100;

    public override bool Application(BattleCharacter whoUsing, BattlePlacement battlePlacement)
    {
        List<BattleCharacter> enemies = battlePlacement.Search.AllEnemyLivingTeam(whoUsing);
        foreach (var enemy in enemies)
        {
            whoUsing.CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(
                whoUsing,
                enemy,
                CharacterActionEvent.ActionOnTarget.Attack,
                whoUsing.Attack * _percentageOfAttack / 100)));
        }

        if (enemies.Count == 0)
            whoUsing.CreateAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.EndGame)));

        return true;
    }
}
