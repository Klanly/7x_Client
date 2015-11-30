using UnityEngine;
using System.Collections;

public class BattlePauseControllor : MonoBehaviour 
{
	public UILabel label;

	public UILabel labelDesc;


	public void refreshData()
	{
		label.text = LanguageTemplate.GetText ((LanguageTemplate.Text)543);

		int descLanguageId = 0;

		string strNum = "";

		BattleWinTemplate winDescTemplate = BattleUIControlor.Instance ().winDescTemplate;

		if(winDescTemplate == null)
		{
			winDescTemplate = BattleWinTemplate.templates[0];
		}

		if(winDescTemplate.winType == BattleWinFlag.WinType.Kill_All)
		{
			descLanguageId = 1082;
			
			strNum = "";
		}
		else if(winDescTemplate.winType == BattleWinFlag.WinType.Kill_Boss)
		{
			descLanguageId = 1083;
			
			strNum = BattleControlor.Instance().bossKilled + "/" + winDescTemplate.killNum;
		}
		else if(winDescTemplate.winType == BattleWinFlag.WinType.Kill_Hero)
		{
			descLanguageId = 1092;
			
			strNum = BattleControlor.Instance().heroKilled + "/" + winDescTemplate.killNum;
		}
		else if(winDescTemplate.winType == BattleWinFlag.WinType.Kill_Soldier)
		{
			descLanguageId = 1090;
			
			strNum = BattleControlor.Instance().soldierKilled + "/" + winDescTemplate.killNum;
		}
		else if(winDescTemplate.winType == BattleWinFlag.WinType.Kill_Gear)
		{
			descLanguageId = 1088;
			
			strNum = BattleControlor.Instance().gearKilled + "/" + winDescTemplate.killNum;
		}
		else if(winDescTemplate.winType == BattleWinFlag.WinType.Reach_Destination)
		{
			descLanguageId = 1085;
			
			strNum = ((int)Vector3.Distance(BattleControlor.Instance().getKing().transform.position, winDescTemplate.destination) - winDescTemplate.destinationRadius) + "m";
		}
		else if(winDescTemplate.winType == BattleWinFlag.WinType.Reach_Time)
		{
			descLanguageId = 1086;
			
			strNum = BattleControlor.Instance().timeLast + "s";
		}
		
		labelDesc.text = LanguageTemplate.GetText (descLanguageId) + " " + strNum;
	}

	public void close()
	{
		Time.timeScale = 1.0f;

		gameObject.SetActive(false);
	}

	public void Lose()
	{
		close ();

		BattleUIControlor.Instance ().devolopmentLose ();
	}

	public void runaway()
	{
		Time.timeScale = 1.0f;

		GameObject root3d = GameObject.Find ("BattleField_V4_3D");
		
		GameObject root2d = GameObject.Find ("BattleField_V4_2D");

		Destroy (root3d);

		Destroy (root2d);

//		BattleNet bn = (BattleNet)BattleControlor.Instance ().gameObject.GetComponent ("BattleNet");

		//SceneManager.EnterMainCity();

        //if (JunZhuData.Instance().m_junzhuInfo.lianMengId <= 0)
        //{
            SceneManager.EnterMainCity();
        //}
        //else
        //{
        //    SceneManager.EnterAllianceCity();
        //}

//		Application.LoadLevel( ConstInGame.CONST_SCENE_NAME_LOADING___FOR_COMMON_SCENE );
	}

}
