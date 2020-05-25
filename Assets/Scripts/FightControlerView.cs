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

    [SerializeField] private Database _database;
    [SerializeField] private DatabaseCharacterCharacteristics _tank;
    [SerializeField] private DatabaseCharacterCharacteristics _warior;
    [SerializeField] private DatabaseCharacterCharacteristics _shooter;
    [SerializeField] private FightCharacterView _prefabCharacter;
    [SerializeField] private List<SpawnPointsList> _teamASpownPoints;
    [SerializeField] private List<SpawnPointsList> _teamBSpownPoints;

    private void Start()
    {
        /*   BattleCharacter character1 = null;// new TankBattleCharacter(_tank, null);
           BattleCharacter character2 = null;// new WariorBattleCharacter(_warior, null);
           BattleCharacter character3 = null;// new WariorBattleCharacter(_warior, null);
           BattleCharacter character4 = null;// new TankBattleCharacter(_tank, null);
           BattleCharacter character5 = null;// new ShooterBattleCharacter(_shooter, null);
           BattleCharacter character6 = null;// new ShooterBattleCharacter(_shooter, null);
           */
        DatabaseCharacter databaseCharacter = _database.GetDatabaseCharacter("000001");
        BattleCharacter character1 = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);

        databaseCharacter = _database.GetDatabaseCharacter("000002");
        BattleCharacter character2 = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);

        databaseCharacter = _database.GetDatabaseCharacter("000002");
        BattleCharacter character3 = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);

        databaseCharacter = _database.GetDatabaseCharacter("000001");
        BattleCharacter character4 = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);

        databaseCharacter = _database.GetDatabaseCharacter("000003");
        BattleCharacter character5 = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);

        databaseCharacter = _database.GetDatabaseCharacter("000003");
        BattleCharacter character6 = new BattleCharacter(databaseCharacter.BattleCharacter, databaseCharacter.Characteristics, null);

        PlacementPosition place1 = new PlacementPosition(1, 0);
        PlacementPosition place2 = new PlacementPosition(1, 1);
        PlacementPosition place3 = new PlacementPosition(1, 2);
        PlacementPosition place4 = new PlacementPosition(2, 0);
        PlacementPosition place5 = new PlacementPosition(0, 1);
        PlacementPosition place6 = new PlacementPosition(3, 2);

        _battlePlacement = new BattlePlacement(new List<BattleCharacterPlacement>()
                                              {
                                                  new BattleCharacterPlacement(character1, place1),
                                                  new BattleCharacterPlacement(character2, place2),
                                                  new BattleCharacterPlacement(character3, place3)
                                              },
                                              new List<BattleCharacterPlacement>()
                                              {
                                                  new BattleCharacterPlacement(character4, place4),
                                                  new BattleCharacterPlacement(character5, place5),
                                                  new BattleCharacterPlacement(character6, place6)
                                              });

        foreach (var member in _battlePlacement.Search.AllTeam())
        {
            member.SetBattlePlacement(_battlePlacement);
        }

        _characterViews = new Dictionary<BattleCharacter, FightCharacterView>();

        foreach (var member in _battlePlacement.Search.AllTeam())
        {
            //ужас
            //надо исправить!!!!!!!!
            PlacementPosition place = null;
            if (member == character1)
                place = place1;
            else if (member == character2)
                place = place2;
            else if(member == character3)
                place = place3;
            else if (member == character4)
                place = place4;
            else if(member == character5)
                place = place5;
            else
                place = place6;

            FightSpawnPoint spawnPoint = null;
            if (place == place1 || place == place2 || place == place3)
                spawnPoint = _teamASpownPoints[place.Line].points[place.Column];
            else
                spawnPoint = _teamBSpownPoints[place.Line].points[place.Column];


            FightCharacterView fightCharacter = Instantiate(_prefabCharacter, spawnPoint.transform);
            _characterViews.Add(member, fightCharacter);
        }

        _timer = 0;
        _fightControler = GetComponent<FightControler>();

        _allAction = _fightControler.Fight(_battlePlacement);
        

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
                        Debug.Log("EndGame");
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
