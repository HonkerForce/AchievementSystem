using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int nHP;
    public int HP
    {
	    get { return nHP;}
	    set
	    {
		    nHP = value;
            OnHPChanged?.Invoke();
	    }
    }

    public int nMP;

    public int MP
    {
	    get { return nMP;}
	    set
	    {
		    nMP = value;
            OnMPChanged?.Invoke();
	    }
    }

    public Action OnHPChanged = null;
    public Action OnMPChanged = null;

    public Action<CAchievement> OnFinishAchievement = null;

    void Awake()
    {
	    Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
