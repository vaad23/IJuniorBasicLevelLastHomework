using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightControler : MonoBehaviour
{
    [SerializeField] private FightControlerView _view;
    [SerializeField] private CharacterCharacteristics _tank;
    [SerializeField] private CharacterCharacteristics _warior;
    [SerializeField] private CharacterCharacteristics _shooter;
    [SerializeField] private List<CharacterExperience> _experienceArray;
    [SerializeField] private bool _isFinishGame;

    private CharacterPlacement _comandA;
    private CharacterPlacement _comandB;
    private List<Character> _characters = new List<Character>();
    private List<Character> _queue = new List<Character>();
    private List<CharacterActionEvent> _allAction = new List<CharacterActionEvent>();
    private List<CharacterActionEvent> _actionQueue = new List<CharacterActionEvent>();
    private Dictionary<Character, CharacterExperience> _experiences;

    public List<CharacterActionEvent> Fight(out CharacterPlacement comandA,out CharacterPlacement comandB)
    {
        _comandA = new CharacterPlacement(1, 3);
        _comandB = new CharacterPlacement(1, 3);
        _experiences = new Dictionary<Character, CharacterExperience>();

        AddTankCharacter(_comandA, _comandB, _tank, 0, 0);
        AddWariorCharacter(_comandA, _comandB, _warior, 0, 1);
        AddWariorCharacter(_comandA, _comandB, _warior, 0, 2);
        AddTankCharacter(_comandB, _comandA, _tank, 0, 0);
        AddShooterCharacter(_comandB, _comandA, _shooter, 0, 1);
        AddShooterCharacter(_comandB, _comandA, _shooter, 0, 2);

        comandA = new CharacterPlacement(_comandA);
        comandB = new CharacterPlacement(_comandB);

        OnAction(new CharacterActionEvent(new CharacterActionEvent.System(CharacterActionEvent.ActionSystem.StartFight)));

        _isFinishGame = false;
        _queueNumber = 0;
        while (!_isFinishGame)
        {
            _queue[_queueNumber].ActionSelection();

            while (_actionQueue.Count > 0)
            {
              //  Debug.Log(_actionQueue[0].ToString());
                ActionHandler(_actionQueue[0]);
                _actionQueue.Remove(_actionQueue[0]);
            }
        }

        Debug.Log("All Action:");
        foreach (var action in _allAction)
        {
            Debug.Log(action.ToString());
        }

        ChanceEcperienceInInspector();

        return _allAction;
    }

    private int _queueNumber;

    private void AddQueueNumber()
    {
        _queueNumber++;
        if (_queueNumber >= _queue.Count)
            _queueNumber = 0;
    }

    private void RemoveQueue(Character character)
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

    private void AddCharacter(CharacterPlacement fromPlacement, CharacterPlacement aboutPlacement, Character character, int placeLine, int placeСolumn)
    {
        character.ActionEvent += OnAction;
        character.ChangedCharacteristics();
        _characters.Add(character);
        _queue.Add(character);
        _experiences.Add(character, new CharacterExperience());
        fromPlacement.TryAddCharacter(placeLine, placeСolumn, character);
    }

    private void AddTankCharacter(CharacterPlacement fromPlacement, CharacterPlacement aboutPlacement, CharacterCharacteristics characterCharacteristics, int placeLine, int placeСolumn)
    {
        Character character = new TankCharacter(characterCharacteristics, fromPlacement, aboutPlacement);
        AddCharacter(fromPlacement, aboutPlacement, character, placeLine, placeСolumn);
    }

    private void AddWariorCharacter(CharacterPlacement fromPlacement, CharacterPlacement aboutPlacement, CharacterCharacteristics characterCharacteristics, int placeLine, int placeСolumn)
    {
        Character character = new WariorCharacter(characterCharacteristics, fromPlacement, aboutPlacement);
        AddCharacter(fromPlacement, aboutPlacement, character, placeLine, placeСolumn);
    }

    private void AddShooterCharacter(CharacterPlacement fromPlacement, CharacterPlacement aboutPlacement, CharacterCharacteristics characterCharacteristics, int placeLine, int placeСolumn)
    {
        Character character = new ShooterCharacter(characterCharacteristics, fromPlacement, aboutPlacement);
        AddCharacter(fromPlacement, aboutPlacement, character, placeLine, placeСolumn);
    }

    private void ChanceEcperienceInInspector()
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
