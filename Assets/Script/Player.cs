using UnityEngine;

public class Player : Character
{
    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializePlayer()
    {
        base.Intialize(100, 100);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntializePlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
