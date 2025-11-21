using UnityEngine;

public class Gloob : Enemy
{
    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializeGloob()
    {
        base.Intialize(30, 30);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntializeGloob();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
