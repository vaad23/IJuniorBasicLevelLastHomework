using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/HealSkill OnAttack ByOneself", order = 52)]
public class HealSkillOnAttackByOneself : Skill
{
    [SerializeField] private int _percentageOfAttack = 100;

    public override bool Application(BattleCharacter whoUsing, BattlePlacement battlePlacement)
    {
        whoUsing.CreateAction(new CharacterActionEvent(new CharacterActionEvent.OnTarget(
            whoUsing,
            whoUsing,
            CharacterActionEvent.ActionOnTarget.Heal,
            whoUsing.Attack * _percentageOfAttack / 100)));

        return true;
    }
}
