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

    Transform unitTocreate;

    [SerializeField]
    Transform p1UnitsContainer;
    [SerializeField]
    Transform p2UnitsContainer;

    Transform unitParent;

    public enum UnitsPool { Miner, Swordsman }

    public void ActivateUnitFromPool(int player, int posx, int posy, UnitsPool kind)
    {
        unitParent = p1UnitsContainer;
        if(player == 2)
        {
            unitParent = p2UnitsContainer;
        }
        switch (kind)
        {
            case UnitsPool.Miner:
                if (player == 1)
                {
                    unitTocreate = p1MinersPool.GetChild(0);
                }
                else
                {
                    unitTocreate = p2MinersPool.GetChild(0);
                }
                break;

            case UnitsPool.Swordsman:
                if (player == 1)
                {
                    unitTocreate = p1SwordsmanPool.GetChild(0);
                }
                else
                {
                    unitTocreate = p2SwordsmanPool.GetChild(0);
                }
                break;
        }
        if(unitTocreate != null)
        {
            // Create Unit

            unitTocreate.transform.position = new Vector2(posx, posy);
            unitTocreate.transform.parent = unitParent;
            unitTocreate.gameObject.SetActive(true);
        }
        else
        {
            // "You cant create more units"

            Debug.Log("Cant create this unit");
        }
    }

}
