using System;

[Serializable]
public class Resource
{
    private int _amount;
    private int _dicResourceId;
    public int Amount
    {
        get => _amount;
        set
        {
            if (value > 0)
                _amount = value;
        }
    }
    public int DicResourceId
    {
        get => _dicResourceId;
        set
        {
            if (value > 0)
                _dicResourceId = value;
        }
    }
}
