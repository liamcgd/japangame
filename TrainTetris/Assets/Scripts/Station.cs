using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Container class for Station info
public class Station
{
    private string entryDirection;
    private string stationName;

    public string EntryDirection { get { return entryDirection; } }
    public string StationName { get { return stationName; } }

    public Station(string name, string direction)
    {
        stationName = name;
        entryDirection = direction;
    }
}
