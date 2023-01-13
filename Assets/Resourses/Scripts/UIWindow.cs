using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindow : MonoBehaviour
{ 
	public Transform achievementTrans;
	public Text achievementTxt;

    void Start()
    {
	    if (GameManager.Instance == null)
	    {
		    return;
	    }

	    GameManager.Instance.OnFinishAchievement = ShowAchievementFinish;
    }

    void Update()
    {
        
    }

    public void OnAddHpClicked()
    {
	    if (GameManager.Instance == null)
	    {
		    return;
	    }

	    GameManager.Instance.HP += 5;
    }

    public void OnAddMpClicked()
    {
	    if (GameManager.Instance == null)
	    {
		    return;
	    }

	    GameManager.Instance.MP += 5;
    }

    void ShowAchievementFinish(CAchievement achievement)
    {
	    achievementTxt.text = "¹§Ï²´ï³É" + achievement.Name;
        achievementTrans.Translate(new Vector3(0, -105f, 0));
        Invoke("HideAchievementFinish", 2f);
    }

    void HideAchievementFinish()
    {
	    achievementTrans.Translate(new Vector3(0, 105f, 0));
    }
}
