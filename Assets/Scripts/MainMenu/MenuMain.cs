using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    [SerializeField] private Database _database;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _descriptionMenu;
    [SerializeField] private GameObject _levelSelectionMenu;

    public Database Database => _database;

    public void OpenDescriptionMenu()
    {
        _mainMenu.SetActive(false);
        _descriptionMenu.SetActive(true);
    }

    public void OpenLevelSelectionMenu()
    {
        _mainMenu.SetActive(false);
        _levelSelectionMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        if (_descriptionMenu.activeSelf)
            _descriptionMenu.SetActive(false);
        if (_levelSelectionMenu.activeSelf)
            _levelSelectionMenu.SetActive(false);

        _mainMenu.SetActive(true);
    }

    public void ChangeFightLevel(int level)
    {
        SaveBattleSystem saveBattleSystem = new SaveBattleSystem();
        SaveBattleTeams battleTeams;
        switch (level)
        {
            case 1:
                battleTeams = new SaveBattleTeams(
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 1, 0, 0),
                        new SaveFightCharacter("000002", 1, 1, 1, 1),
                        new SaveFightCharacter("000003", 1, 1, 2, 2),
                    }
                ),
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 1, 0, 0),
                        new SaveFightCharacter("000002", 1, 1, 1, 1),
                        new SaveFightCharacter("000003", 1, 1, 2, 2),
                    }
                ));
                saveBattleSystem.SavingBattleTeams(battleTeams);
                SceneManager.LoadScene("BattLe");
                break;
            case 2:
                battleTeams = new SaveBattleTeams(
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 1, 0, 0),
                        new SaveFightCharacter("000002", 1, 1, 1, 1),
                        new SaveFightCharacter("000003", 1, 1, 2, 2),
                        new SaveFightCharacter("000001", 1, 2, 0, 3),
                        new SaveFightCharacter("000002", 1, 2, 1, 4),
                        new SaveFightCharacter("000003", 1, 2, 2, 5),
                    }
                ),
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 1, 0, 0),
                        new SaveFightCharacter("000002", 1, 1, 1, 1),
                        new SaveFightCharacter("000003", 1, 1, 2, 2),
                        new SaveFightCharacter("000001", 1, 0, 0, 3),
                        new SaveFightCharacter("000002", 1, 0, 1, 4),
                        new SaveFightCharacter("000003", 1, 0, 2, 5),
                    }
                ));
                saveBattleSystem.SavingBattleTeams(battleTeams);
                SceneManager.LoadScene("BattLe");
                break;
            case 3:
                battleTeams = new SaveBattleTeams(
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 1, 0, 4),
                        new SaveFightCharacter("000003", 1, 1, 1, 0),
                        new SaveFightCharacter("000003", 1, 1, 2, 1),
                        new SaveFightCharacter("000001", 1, 2, 0, 5),
                        new SaveFightCharacter("000003", 1, 2, 1, 2),
                        new SaveFightCharacter("000003", 1, 2, 2, 3),
                        new SaveFightCharacter("000001", 1, 0, 2, 6),
                        new SaveFightCharacter("000001", 1, 3, 2, 7),
                    }
                ),
                new SaveFightTeam(
                    new List<SaveFightCharacter>
                    {
                        new SaveFightCharacter("000001", 1, 0, 0, 0),
                        new SaveFightCharacter("000002", 1, 0, 1, 1),
                        new SaveFightCharacter("000002", 1, 0, 2, 2),
                        new SaveFightCharacter("000001", 1, 1, 0, 3),
                        new SaveFightCharacter("000002", 1, 1, 1, 4),
                        new SaveFightCharacter("000002", 1, 1, 2, 5),
                        new SaveFightCharacter("000001", 1, 2, 0, 6),
                        new SaveFightCharacter("000002", 1, 2, 1, 7),
                        new SaveFightCharacter("000002", 1, 2, 2, 8),
                        new SaveFightCharacter("000001", 1, 3, 0, 9),
                        new SaveFightCharacter("000002", 1, 3, 1, 10),
                        new SaveFightCharacter("000002", 1, 3, 2, 11),
                    }
                ));
                saveBattleSystem.SavingBattleTeams(battleTeams);
                SceneManager.LoadScene("BattLe");
                break;
        }
    }
}
