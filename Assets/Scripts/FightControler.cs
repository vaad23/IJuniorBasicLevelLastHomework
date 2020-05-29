using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControler : MonoBehaviour
{
    [SerializeField] private List<CharacterExperience> _experienceArray;
    [SerializeField] private bool _isFinishGame;

    private List<BattleCharacter> _characters = new List<BattleCharacter>();
    private List<BattleCharacter> _queue = new List<BattleCharacter>();
    private List<CharacterActionEvent> _allAction = new List<CharacterActionEvent>();
    private List<CharacterActionEvent> _actionQueue = new List<CharacterActionEvent>();
    private Dictionary<BattleCharacter, CharacterExperience> _experiences;
    private int _queueNumber;

    public List<CharacterActionEvent> Fight(BattlePlacement battlePlacement, List<BattleCharacter> queue)
    {
        _experiences = new Dictionary<BattleCharacter, CharacterExperience>();
        _queue = queue;

        foreach (var member in battlePlacement.Search.AllTeam())
        {
            member.ActionEvent += OnAction;
            member.ChangedCharacteristics();
            _characters.Add(member);
           // _queue.Add(member);
            _experiences.Add(member, new CharacterExperience());
        }

        OnAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.StartFight)));

        _isFinishGame = false;
        _queueNumber = 0;
        while (!_isFinishGame)
        {
            _queue[_queueNumber].ActionSelection();

            while (_actionQueue.Count > 0)
            {
                ActionHandler(_actionQueue[0]);
                _actionQueue.Remove(_actionQueue[0]);
            }
        }

        Debug.Log("All Action:");
        foreach (var action in _allAction)
        {
            Debug.Log(action.ToString());
        }

        ChangeExperienceInInspector();
        foreach (var member in battlePlacement.Search.AllTeam())
        {
            member.ActionEvent -= OnAction;
        }

        return _allAction;
    }

    private void AddQueueNumber()
    {
        _queueNumber++;
        if (_queueNumber >= _queue.Count)
            _queueNumber = 0;
    }

    private void RemoveQueue(BattleCharacter character)
    {
        if (_queue.IndexOf(character) < _queueNumber)
            _queueNumber--;
        _queue.Remove(character);
    }

    private void ActionHandler(CharacterActionEvent action)
    {
        switch (action.Act)
        {
            case CharacterActionEvent.Action.Experience:
                switch (action.ActExperience.ExperienceEnum)
                {
                    case CharacterActionEvent.ActionExperience.Attack:
                        _experiences[action.ActExperience.FromWhom].AddAttack(action.ActExperience.Value);
                        break;
                    case CharacterActionEvent.ActionExperience.Armor:
                        _experiences[action.ActExperience.FromWhom].AddArmor(action.ActExperience.Value);
                        break;
                    case CharacterActionEvent.ActionExperience.Heal:
                        _experiences[action.ActExperience.FromWhom].AddHeal(action.ActExperience.Value);
                        break;
                    case CharacterActionEvent.ActionExperience.Health:
                        _experiences[action.ActExperience.FromWhom].AddHealth(action.ActExperience.Value);
                        break;
                }
                break;
            case CharacterActionEvent.Action.NoTarget:
                switch (action.ActNoTarget.NoTargetEnum)
                {
                    case CharacterActionEvent.ActionNoTarget.Create:
                        break;
                    case CharacterActionEvent.ActionNoTarget.Dead:
                        RemoveQueue(action.ActNoTarget.FromWhom);
                        break;
                }
                break;
            case CharacterActionEvent.Action.OnTarget:
                switch (action.ActOnTarget.OnTargetEnum)
                {
                    case CharacterActionEvent.ActionOnTarget.Attack:
                        action.ActOnTarget.AboutWhom.TakeAction(action);
                        break;
                    case CharacterActionEvent.ActionOnTarget.Heal:
                        action.ActOnTarget.AboutWhom.TakeAction(action);
                        break;
                    case CharacterActionEvent.ActionOnTarget.AttackResult:
                        action.ActOnTarget.FromWhom.TakeAction(action);
                        break;
                    case CharacterActionEvent.ActionOnTarget.HealResult:
                        action.ActOnTarget.FromWhom.TakeAction(action);
                        break;
                }
                break;
            case CharacterActionEvent.Action.System:
                switch (action.ActSystem.SystemEnum)
                {
                    case CharacterActionEvent.ActionSystem.StartFight:
                        break;
                    case CharacterActionEvent.ActionSystem.EndRaund:
                        AddQueueNumber();
                        break;
                    case CharacterActionEvent.ActionSystem.EndGame:
                        _isFinishGame = true;
                        break;
                }
                break;
        }
    }


    private void ChangeExperienceInInspector()
    {
        _experienceArray = new List<CharacterExperience>();
        foreach (var key in _experiences.Keys)
        {
            _experienceArray.Add(_experiences[key]);
        }
    }

    private void OnAction(CharacterActionEvent action)
    {
        _allAction.Add(action);
        _actionQueue.Add(action);
    }
}
