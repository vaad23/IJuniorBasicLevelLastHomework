using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionEvent
{
    public Action Act { get; private set; }
    public OnTarget ActOnTarget { get; private set; }
    public NoTarget ActNoTarget { get; private set; }
    public Experience ActExperience { get; private set; }
    public ChangedInfo ActChengedInfo { get; private set; }
    public System ActSystem { get; private set; }
    
    public CharacterActionEvent(OnTarget actionOnTarget)
    {
        Act = Action.OnTarget;
        ActOnTarget = actionOnTarget;
    }
    
    public CharacterActionEvent(NoTarget actionNoTarget)
    {
        Act = Action.NoTarget;
        ActNoTarget = actionNoTarget;
    }

    public CharacterActionEvent(Experience experience)
    {
        Act = Action.Experience;
        ActExperience = experience;
    }

    public CharacterActionEvent(ChangedInfo changedInfo)
    {
        Act = Action.ChancgedInfo;
        ActChengedInfo = changedInfo;
    }

    public CharacterActionEvent(System system)
    {
        Act = Action.System;
        ActSystem = system;
    }    

     public override string ToString()
     {
        string text = $"{Act} / ";

        switch(Act)
        {
            case Action.OnTarget:
                text += ActOnTarget.ToString();
                break;
            case Action.NoTarget:
                text += ActNoTarget.ToString();
                break;
            case Action.Experience:
                text += ActExperience.ToString();
                break;
            case Action.ChancgedInfo:
                text += ActChengedInfo.ToString();
                break;
            case Action.System:
                text += ActSystem.ToString();
                break;
        }

        return text;
     }

    public enum Action
    {
        OnTarget = 0,
        NoTarget = 1,
        Experience = 2,
        ChancgedInfo = 3,
        System = 4
    }

    public enum ActionOnTarget
    {
        None = 0,
        Attack = 1,
        AttackResult = 2,
        Heal = 3,
        HealResult = 4
    }

    public enum ActionNoTarget
    {
        None = 0,
        Create = 1,
        Dead = 2
    }

    public enum ActionExperience
    {
        None = 0,
        Attack = 1,
        Heal = 2,
        Armor = 3,
        Health = 4
    }

    public enum ActionSystem
    {
        None = 0,
        StartFight = 1,
        EndRaund = 2,
        EndGame = 3
    }

    public class OnTarget
    {
        public BattleCharacter FromWhom { get; private set; }
        public BattleCharacter AboutWhom { get; private set; }
        public ActionOnTarget OnTargetEnum { get; private set; }
        public int Value { get; private set; }

        public OnTarget(BattleCharacter fromWhom, BattleCharacter aboutWhom, ActionOnTarget onTargetEnum, int value)
        {
            FromWhom = fromWhom;
            AboutWhom = aboutWhom;
            OnTargetEnum = onTargetEnum;
            Value = value;
        }

        public override string ToString()
        {
            return $"FromWhom: {FromWhom} / " +
                $"AboutWhom: {AboutWhom} / " +
                $"OnTargetEnum: {OnTargetEnum} / " +
                $"Value: {Value} / ";
        }
    }

    public class NoTarget
    {
        public BattleCharacter FromWhom { get; private set; }
        public ActionNoTarget NoTargetEnum { get; private set; }

        public NoTarget(BattleCharacter fromWhom, ActionNoTarget noTargetEnum)
        {
            FromWhom = fromWhom;
            NoTargetEnum = noTargetEnum;
        }

        public override string ToString()
        {
            return $"FromWhom: {FromWhom} / " +
                $"NoTargetEnum: {NoTargetEnum}";
        }
    }

    public class Experience
    {
        public BattleCharacter FromWhom { get; private set; }
        public ActionExperience ExperienceEnum { get; private set; }
        public int Value { get; private set; }

        public Experience(BattleCharacter fromWhom, ActionExperience experienceEnum, int value)
        {
            FromWhom = fromWhom;
            ExperienceEnum = experienceEnum;
            Value = value;
        }

        public override string ToString()
        {
            return $"FromWhom: {FromWhom} / " +
                $"ExperienceEnum: {ExperienceEnum} / " +
                $"Value: {Value} / ";
        }
    }

    public class ChangedInfo
    {
        public BattleCharacter FromWhom { get; private set; }
        public Characteristics Characteristic { get; private set; }

        public ChangedInfo(BattleCharacter fromWhom, Characteristics characteristic)
        {
            FromWhom = fromWhom;
            Characteristic = characteristic;
        }

        public override string ToString()
        {
            string characteristic = Characteristic == null ? "" : Characteristic.ToString();
            return $"FromWhom: {FromWhom} / " +
                $"Characteristic: {characteristic}";
        }
    }

    public class System
    {
        public ActionSystem SystemEnum { get; private set; }

        public System(ActionSystem systemEnum)
        {
            SystemEnum = systemEnum;
        }

        public override string ToString()
        {
            return $"SystemEnum: {SystemEnum}";
        }
    }
}
