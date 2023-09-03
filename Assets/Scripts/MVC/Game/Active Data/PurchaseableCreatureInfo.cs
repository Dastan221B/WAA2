using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class PurchaseableCreatureInfo
{
    public int amount { get; set; }
    public List<int> creatureIds { get; set; }

    public static implicit operator int(PurchaseableCreatureInfo creatureInfo) => creatureInfo.amount;
}

