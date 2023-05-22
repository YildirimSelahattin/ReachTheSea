using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataList
{
    [System.Serializable]
    public class GeneralDataStructure
    {
        public int gridHeight;
        public int gridWidth;
        public List<int> roadIndexes;
        public List<int> machineSpotIndexes;
        public int howManyPeopleToGenerate;
    }

    public GeneralDataStructure[] levelsArray;
}