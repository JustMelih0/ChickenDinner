using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IA_Claimable 
{
    bool Claim(float count,ItemBase.ItemType itemType);
}
