using UnityEngine;

public class Dialogue_Tell : DiaLogue
{
    protected override string[] GetDialogue()
    {
        return new string[]
        {
            "Deplete the monster's Sanity first, then it can take health damage!",
            "Defeat the monster to collect the item to return to the real world!",
        };
    }
}
