using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : Tower
{
    protected override void CalculateDamage()
    {
        base.CalculateDamage();
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            
            if (obj.GetComponent<SaveManager>().GetSkulls("Blockade") >= 5)
            {
                fullValue = false;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if(player)
                {
                    player.GetComponent<Player>().AddMoney(value / 2);
                }

            }
        }
    }
}
