using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FightControler))]
public class FightControlerView : MonoBehaviour
{
    private FightControler _fightControler;
    private List<CharacterActionEvent> _allAction;
    private CharacterPlacement _comandA;
    private CharacterPlacement _comandB;
    private Dictionary<Character, FightCharacterView> _characterViews;
    private float _timer;
    private int _countAction;

    [SerializeField] private FightCharacterView _prefabCharacter;
    [SerializeField] private Transform parentPrefab;

    private void Start()
    {
        _timer = 0;
        _fightControler = GetComponent<FightControler>();
        _allAction = _fightControler.Fight(out _comandA, out _comandB);
        _characterViews = new Dictionary<Character, FightCharacterView>();

        for (int i = 0; i < _comandA.LengthLine; i++)
        {
            for (int j = 0; j < _comandA.LengthColumn; j++)
            {
                if (_comandA.TrySearch(i, j, out Character character))
                {
                    FightCharacterView fightCharacter = Instantiate(_prefabCharacter, parentPrefab);
                    fightCharacter.transform.localPosition = new Vector3(-250 * (j + 1), 0, 0);
                    _characterViews.Add(character, fightCharacter);
                }                    
            }
        }

        for (int i = 0; i < _comandB.LengthLine; i++)
        {
            for (int j = 0; j < _comandB.LengthColumn; j++)
            {
                if (_comandB.TrySearch(i, j, out Character character))
                {
                    FightCharacterView fightCharacter = Instantiate(_prefabCharacter, parentPrefab);
                    fightCharacter.transform.localPosition = new Vector3(250 * (j + 1), 0, 0);
                    _characterViews.Add(character, fightCharacter);
                }
            }
        }

        for (_countAction = 0; _countAction < _allAction.Count ; _countAction++)
        {
            if (_allAction[_countAction].Act == CharacterActionEvent.Action.System)
                if (_allAction[_countAction].ActSystem.SystemEnum != CharacterActionEvent.ActionSystem.StartFight)
                    return;

            ActionHandler(_allAction[_countAction]);
        }
    }

    private void Update()
    {
        while (_timer <= 0 && _countAction < _allAction.Count)
        {
            ActionHandler(_allAction[_countAction]);
            _countAction++;
        }

        _timer -= Time.deltaTime;
    }



    private void ActionHandler(CharacterActionEvent action)
    {
        switch (action.Act)
        {
            case CharacterActionEvent.Action.Experience:
                switch (action.ActExperience.ExperienceEnum)
                {
                    case CharacterActionEvent.ActionExperience.Attack:
                        break;
                    case CharacterActionEvent.ActionExperience.Armor:
                        break;
                    case CharacterActionEvent.ActionExperience.Heal:
                        break;
                    case CharacterActionEvent.ActionExperience.Health:
                        break;
                }
                break;
            case CharacterActionEvent.Action.NoTarget:
                switch (action.ActNoTarget.NoTargetEnum)
                {
                    case CharacterActionEvent.ActionNoTarget.Create:
                        break;
                    case CharacterActionEvent.ActionNoTarget.Dead:
                        break;
                }
                break;
            case CharacterActionEvent.Action.OnTarget:
                switch (action.ActOnTarget.OnTargetEnum)
                {
                    case CharacterActionEvent.ActionOnTarget.Attack:
                        break;
                    case CharacterActionEvent.ActionOnTarget.Heal:
                        break;
                    case CharacterActionEvent.ActionOnTarget.AttackResult:
                        break;
                    case CharacterActionEvent.ActionOnTarget.HealResult:
                        break;
                }
                break;
            case CharacterActionEvent.Action.ChancgedInfo:
                _characterViews[action.ActChengedInfo.FromWhom].ChangeCharacteristic(action.ActChengedInfo.Characteristic);
                break;
            case CharacterActionEvent.Action.System:
                switch (action.ActSystem.SystemEnum)
                {
                    case CharacterActionEvent.ActionSystem.StartFight:
                        _timer = 1;
                        break;
                    case CharacterActionEvent.ActionSystem.EndRaund:
                        _timer = 1f;
                        break;
                    case CharacterActionEvent.ActionSystem.EndGame:
                        Debug.Log("EndGame");
                        break;
                }
                break;
        }
    }
}
