using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private int _currentMoney = 50;
    public int currentReachedPeople = 0;
    public int currentMoney
    {
        get {
            return _currentMoney;
        }
        set {
        }
    }
    public static GameManager Instance;
    void Start()
    {
        if(Instance = null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
