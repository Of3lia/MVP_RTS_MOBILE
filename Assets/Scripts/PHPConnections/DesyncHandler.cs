using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesyncHandler : MonoBehaviour
{
    /*
    public Dictionary<int, Unit> AllUnitsInGame;
    public Unit[] allUnits;

    Transform unitsContainer;
    Transform p1Units;
    Transform p2Units;
    Transform goldMines;

    private string syncDataString;

    private void Awake()
    {
        unitsContainer = GetComponent<SharedFields>().unitsContainer;
        p1Units = GetComponent<SharedFields>().p1Units;
        p2Units = GetComponent<SharedFields>().p2Units;
        goldMines = GetComponent<SharedFields>().goldMines;

        AllUnitsInGame = new Dictionary<int, Unit>();

        // Populate AllUnitsInGame
        allUnits = FindObjectsOfType<Unit>();
        for(int i = 0; i < allUnits.Length; i++)
        {
            allUnits[i].UnitId = i;

            //AllUnitsInGame.Add(allUnits[i].UnitId, allUnits[i]);
            //Debug.Log(allUnits[i].UnitId);
        }

        //StartCoroutine(WriteSyncData());
    }

    private IEnumerator WriteSyncData()
    {
        while (true)
        {
            if (StepCounter.currentStep % 10 == 0)
            {
                syncDataString = "_SyncDataString:_";
                for (int i = 0; i < allUnits.Length; i++)
                {
                    syncDataString += allUnits[i].GetSyncData();
                    //Debug.Log(syncDataString);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    */
}
