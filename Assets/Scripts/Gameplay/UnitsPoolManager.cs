using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsPoolManager : MonoBehaviour
{
    [SerializeField]
    Transform p1MinersPool;
    [SerializeField]
    Transform p2MinersPool;
    [SerializeField]
    Transform p1SwordsmanPool;
    [SerializeField]
    Transform p2SwordsmanPool;
    [SerializeField]
    Transform p1KnightPool;
    [SerializeField]
    Transform p2KnightPool;

    Transform unitToActivate;
    Transform unitPool;

    [SerializeField]
    Transform p1UnitsContainer;
    [SerializeField]
    Transform p2UnitsContainer;

    //Transform unitParent;
    public enum UnitsPool { Miner, Swordsman, Knight }

    public void ActivateUnitFromPool(int player, int posx, int posy, UnitsPool kind)
    {
        //unitParent = p1UnitsContainer;
        if(player == 2)
        {
            //unitParent = p2UnitsContainer;
        }
        switch (kind)
        {
            case UnitsPool.Miner:
                if (player == 1)
                {
                    unitPool = p1MinersPool;
                }
                else
                {
                    unitPool = p2MinersPool;
                }
                break;

            case UnitsPool.Swordsman:
                if (player == 1)
                {
                    unitPool = p1SwordsmanPool;
                }
                else
                {
                    unitPool = p2SwordsmanPool;
                }
                break;

            case UnitsPool.Knight:
                if(player == 1)
                {
                    unitPool = p1KnightPool;
                }
                else
                {
                    unitPool = p2KnightPool;
                }
                break;
        }

        if (unitPool.childCount > 0)
        {
            unitToActivate = unitPool.GetChild(0);
        }
        else
        {
            // "You cant create more units"

            Debug.Log("Cant create this unit");

            return;
        }

        if(unitToActivate != null)
        {
            // Create Unit

            unitToActivate.GetComponent<Unit>().ActivateUnit(posx, posy);

            //unitTocreate.transform.parent = unitParent;
            //unitTocreate.gameObject.SetActive(true);
        }

    }

}
