using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacement 
{
    private Character[,] _placements;

    public int LengthLine => _placements.GetLength(0);
    public int LengthColumn => _placements.GetLength(1);
    
    public CharacterPlacement(int placeLine, int placeСolumn)
    {
        _placements = new Character[placeLine, placeСolumn];
    }

    public CharacterPlacement(CharacterPlacement newPlacement)
    {
        _placements = new Character[newPlacement.LengthLine, newPlacement.LengthColumn];
        for (int i = 0; i < newPlacement.LengthLine; i++)
        {
            for (int j = 0; j < newPlacement.LengthColumn; j++)
            {
                _placements[i, j] = newPlacement.ReturnCharacter(i, j);
            }
        }
    }

    public Character ReturnCharacter(int placeLine, int placeСolumn)
    {
        if (PlaceNotExists(placeLine, placeСolumn))
            return null;

        return _placements[placeLine, placeСolumn];
    }

    private bool PlaceNotExists(int placeLine, int placeСolumn)
    {
        return LengthLine < placeLine || LengthColumn < placeСolumn;
    }

    public bool TryAddCharacter(int placeLine, int placeСolumn, Character character)
    {
        if (PlaceNotExists(placeLine, placeСolumn))
            return false;

        _placements[placeLine, placeСolumn] = character;
        return true;
    }

    public bool TrySearch(int placeLine, int placeСolumn, out Character character)
    {
        character = null;
        if (PlaceNotExists(placeLine, placeСolumn))
            return false;

        if (_placements[placeLine, placeСolumn] == null)
            return false;

        character = _placements[placeLine, placeСolumn];
        return true;
    }
}
