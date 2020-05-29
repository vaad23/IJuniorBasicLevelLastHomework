using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FightControler))]
public class FightControlerView : MonoBehaviour
{
    private FightControler _fightControler;
    private List<CharacterActionEvent> _allAction;
    private BattlePlacement _battlePlacement;
    private Dictionary<BattleCharacter, FightCharacterView> _characterViews;
    private float _timer;
    private int _countAction;
    private SaveBattleTeams _battleTeams;

    [SerializeField] private Database _database;
    [SerializeField] private FightCharacterView _prefabCharacter;
    [SerializeField] private List<SpawnPointsList> _teamASpownPoints;
    [SerializeField] private List<SpawnPointsList> _teamBSpownPoints;
    [SerializeField] private FightFinish _panelFightFinish;

    private void Start()
    {
        _battleTeams = (new SaveBattleSystem()).BattleTeams;
        _characterViews = new Dictionary<BattleCharacter, FightCharacterView>();
        List<BattleCharacterPlacement> teamA = new List<BattleCharacterPlacement>();
        List<BattleCharacterPlacement> teamB = new List<BattleCharacterPlacement>();
        List<BattleCharacter> queue = new List<BattleCharacter>();
        Dictionary<BattleCharacter, int> queueTeamA = new Dictionary<BattleCharacter, int>();
        Dictionary<BattleCharacter, int> queueTeamB = new Dictionary<BattleCharacter, int>();

        foreach (var member in _battleTeams.FightTeamA.Characters)
        {
            DatabaseCharacter databaseCharacter = _database.GetDatabaseCharacter(member.Id);
            BattleCharacter character = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);
            PlacementPosition position = new PlacementPosition(member.LinePosition, member.ColumnPosition);

            teamA.Add(new BattleCharacterPlacement(character, position));
            queueTeamA.Add(character,member.PlaceInQueue);
        }

        foreach (var member in _battleTeams.FightTeamB.Characters)
        {
            DatabaseCharacter databaseCharacter = _database.GetDatabaseCharacter(member.Id);
            BattleCharacter character = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);
            PlacementPosition position = new PlacementPosition(member.LinePosition, member.ColumnPosition);

            teamB.Add(new BattleCharacterPlacement(character, position));
            queueTeamB.Add(character, member.PlaceInQueue);
        }

        _battlePlacement = new BattlePlacement(teamA, teamB);

        foreach (var member in _battlePlacement.Search.AllTeam())
        {
            member.SetBattlePlacement(_battlePlacement);
        }

        foreach (var member in teamA)
        {
            FightSpawnPoint spawnPoint =  _teamASpownPoints[member.Position.Line].points[member.Position.Column];
            FightCharacterView fightCharacter = Instantiate(_prefabCharacter, spawnPoint.transform);
            _characterViews.Add(member.Character, fightCharacter);
        }

        foreach (var member in teamB)
        {
            FightSpawnPoint spawnPoint = _teamBSpownPoints[member.Position.Line].points[member.Position.Column];
            FightCharacterView fightCharacter = Instantiate(_prefabCharacter, spawnPoint.transform);
            _characterViews.Add(member.Character, fightCharacter);
        }

        for (int i = 0; queueTeamA.Count>0||queueTeamB.Count>0; i++)
        {
            if (i % 2 == 0)
            {
                if (queueTeamA.Count > 0)
                {
                    BattleCharacter battleCharacter = null;
                    int j = int.MaxValue;

                    foreach (var key in queueTeamA.Keys)
                    {
                        if (queueTeamA[key] < j)
                        {
                            battleCharacter = key;
                            j = queueTeamA[key];
                        }
                    }

                    if (battleCharacter != null)
                    {
                        queue.Add(battleCharacter);
                        queueTeamA.Remove(battleCharacter);
                    }
                }
            }
            else
            {
                if (queueTeamB.Count > 0)
                {
                    BattleCharacter battleCharacter = null;
                    int j = int.MaxValue;

                    foreach (var key in queueTeamB.Keys)
                    {
                        if (queueTeamB[key] < j)
                        {
                            battleCharacter = key;
                            j = queueTeamB[key];
                        }
                    }

                    if (battleCharacter != null)
                    {
                        queue.Add(battleCharacter);
                        queueTeamB.Remove(battleCharacter);
                    }
                }
            }
        }

        _timer = 0;
        _fightControler = GetComponent<FightControler>();

        _allAction = _fightControler.Fight(_battlePlacement, queue);

        for (_countAction = 0; _countAction < _allAction.Count ; _countAction++)
        {
            if (_allAction[_countAction].Act == CharacterActionEvent.Action.System)
                if (_allAction[_countAction].ActSystem.SystemEnum == CharacterActionEvent.ActionSystem.StartFight)
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
                        _characterViews[action.ActOnTarget.FromWhom].SetAnimation(FightCharacterView.State.Attack);
                        _characterViews[action.ActOnTarget.AboutWhom].SetAnimation(FightCharacterView.State.TakeDamage);
                        break;
                    case CharacterActionEvent.ActionOnTarget.Heal:
                        if (action.ActOnTarget.FromWhom != action.ActOnTarget.FromWhom)
                            _characterViews[action.ActOnTarget.FromWhom].SetAnimation(FightCharacterView.State.Attack);
                        _characterViews[action.ActOnTarget.AboutWhom].SetAnimation(FightCharacterView.State.TakeHeal);
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
                        _timer = 1f;
                        break;
                    case CharacterActionEvent.ActionSystem.EndRaund:
                        _timer = 1f;
                        break;
                    case CharacterActionEvent.ActionSystem.EndGame:
                        _panelFightFinish.gameObject.SetActive(true);
                        break;
                }
                break;
        }
    }


    [System.Serializable]
    public class SpawnPointsList
    {
        public List<FightSpawnPoint> points;
    }
}
