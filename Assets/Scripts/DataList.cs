using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataList
{
    [System.Serializable]
    public class GeneralDataStructure
    {
        public float howManyPeopleToGenerate;
        public float waveNumber;
        public int startMoney;
    }

    public GeneralDataStructure[] levelsArray;
}