using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 影响成就的数据类型枚举(新增类型根据二进制按位赋值)
/// </summary>
[Serializable]
public enum EmAchievementType
{
	None = 0,
	HP,
	MP,
	Max,
}

[Serializable]
public class CAchievement
{
	public string strName = String.Empty;
	public string Name
	{
		get { return strName; }
		set { strName = value; }
	}

	public int nCheckType = (int)EmAchievementType.None;
	public int CheckType
	{
		get { return nCheckType; }
		set { nCheckType = value; }
	}

	public bool IsFinish { get; set; } = false;

	void AddCheckType(EmAchievementType type)
	{
		nCheckType |= AchievementSystem.achievementPowers[(int)type];
	}

	public int[] CheckValues;
}

public class AchievementSystem : MonoBehaviour
{
	public static readonly int[] achievementPowers = new int[(int)EmAchievementType.Max]
	{
		0,			// None
		1,			// HP
		2,			// MP
	};

	public CAchievement[] achievements;

	void Start()
    {
	    Init();
    }

    void Update()
    {
        
    }

    void Init()
    {
	    if (GameManager.Instance == null)
	    {
			Debug.Log("成就系统初始化失败！");
		    return;
	    }
	    foreach (CAchievement achievement in achievements)
	    {
			int nCheckType = achievement.CheckType;
			if (nCheckType > 0)
			{
				if (IsNeedCheck(nCheckType, EmAchievementType.HP))
				{
					GameManager.Instance.OnHPChanged += CheckAchievementHP;
				}

				if (IsNeedCheck(nCheckType, EmAchievementType.MP))
				{
					GameManager.Instance.OnMPChanged += CheckAchievementMP;
				}
			}
	    }
    }

	/// <summary>
	/// 成就1检查（检查HP是否符合要求）
	/// </summary>
	/// <param name="nIndex"></param>
    void CheckAchievementHP()
    {
	    if (GameManager.Instance == null)
	    {
		    return;
	    }

	    foreach (var achievement in achievements)
	    {
		    if (achievement.IsFinish)
		    {
			    continue;
		    }
		    if (!IsNeedCheck(achievement.CheckType, EmAchievementType.HP))
		    {
			    continue;
		    }

		    if (achievement.CheckValues[(int)EmAchievementType.HP] <= GameManager.Instance.HP)
		    {
			    achievement.IsFinish = true;
				GameManager.Instance.OnFinishAchievement?.Invoke(achievement);
		    }
	    }
    }

	/// <summary>
	/// 成就2检查（检查MP是否符合要求）
	/// </summary>
    void CheckAchievementMP()
    {
	    if (GameManager.Instance == null)
	    {
		    return;
	    }

	    foreach (var achievement in achievements)
	    {
		    if (achievement.IsFinish)
		    {
			    continue;
		    }
		    if (!IsNeedCheck(achievement.CheckType, EmAchievementType.MP))
		    {
			    continue;
		    }

		    if (achievement.CheckValues[(int)EmAchievementType.MP] <= GameManager.Instance.MP)
		    {
			    achievement.IsFinish = true;
			    GameManager.Instance.OnFinishAchievement?.Invoke(achievement);
		    }
	    }
	}

    bool IsNeedCheck(int nType, EmAchievementType emType)
    {
	    return (nType ^ (int)emType) == 0;
    }
}
