using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trade
{
    public string take;
    public int count;
    int initCount;

    public void Init() {
        initCount = count;
    }

    public int GetInitialCount() {
        return initCount;
    }
}
