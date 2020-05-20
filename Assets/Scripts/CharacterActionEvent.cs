using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionEvent
{
    public Character FromWhom { get; private set; }
    public Character AboutWhom { get; private set; }
    public Action Act { get; private set; }
    public ActionOnTarget ActOnTarget { get; private set; }
    public ActionNoTarget ActNoTarget { get; private set; }
    public ActionExperiense ActExperiense { get; private set; }
    public ActionSystem ActSystem { get; private set; }
    public int Value { get; private set; }
    public Characteristics ChangeCharacteristics { get; private set; }

    public CharacterActionEvent(Character fromWhom, Character aboutWhom, ActionOnTarget actOnTarget, int value)
    {
        Create(fromWhom: fromWhom, act: Action.OnTarget, aboutWhom: aboutWhom, actOnTarget: actOnTarget, value: value);
    }

    public CharacterActionEvent(Character fromWhom, ActionNoTarget actNoTarget)
    {
        Create(fromWhom: fromWhom, act: Action.NoTarget, actNoTarget: actNoTarget);
    }

    public CharacterActionEvent(Character fromWhom, ActionNoTarget actNoTarget, Characteristics characteristics)
    {
        Create(fromWhom: fromWhom, act: Action.NoTarget, actNoTarget: actNoTarget, characteristics: characteristics);
    }

    public CharacterActionEvent(ActionSystem actSystem)
    {
        Create(act: Action.System, actSystem: actSystem);
    }

    public CharacterActionEvent(Character fromWhom, ActionExperiense actExperience, int value)
    {
        Create(fromWhom: fromWhom, act: Action.Experience, actExperience: actExperience, value: value);
    }

    private void Create(Character fromWhom = null,
                        Character aboutWhom = null,
                        Action act = 0,
                        ActionOnTarget actOnTarget = 0,
                        ActionNoTarget actNoTarget = 0,
                        ActionExperiense actExperience = 0,
                        ActionSystem actSystem = 0,
                        int value = 0,
                        Characteristics characteristics = null)
    {
        FromWhom = fromWhom;
        AboutWhom = aboutWhom;
        Act = act;
        ActOnTarget = actOnTarget;
        ActNoTarget = actNoTarget;
        ActExperiense = actExperience;
        ActSystem = actSystem;
        Value = value;
        ChangeCharacteristics = characteristics;
    }

    public override string ToString()
    {
        string characteristic = ChangeCharacteristics == null ? "" : ChangeCharacteristics.ToString();
        return $"FromWhom: {FromWhom} / " +
            $"AboutWhom: {AboutWhom} / " +
            $"Act: {Act} / " +
            $"ActOnTarget: {ActOnTarget} / " +
            $"ActNoTarget: {ActNoTarget} / " +
            $"ActExperiense: {ActExperiense} / " +
            $"ActSystem: {ActSystem} / " +
            $"Value: {Value} / " +
            $"ChanceCharacteristics: {characteristic}";
    }
    
    public enum Action
    {
        OnTarget = 0,
        NoTarget = 1,
        Experience = 2,
        System = 3
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
        Dead = 2,
        ChancgedInfo,
    }

    public enum ActionExperiense
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
}
