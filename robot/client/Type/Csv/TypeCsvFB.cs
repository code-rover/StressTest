
using System.Collections;
using System.Collections.Generic;

public class TypeCsvFB : SuperType
{
	public int id;
	
	public string name;
	public string info;
	public int idView;
	public int limitLv;
	public int idCombatStand;
	public string urls;
	public string[] initXYZ;
	public string[] initCamera;
	public string[] idCombatEnemy;
	public string[][] standInfo;
	
	public string[] weather;
	/// 获得数据
	public string musicSence;
	public string musicCombatLast;
	
	/// add by ssy 副本界面

	/// <summary>
	/// 副本奖励
	/// </summary>
	public string[] rewardFb;
	/// <summary>
	/// 怪物掉落
	/// </summary>
	public string[] rewardGuai;
	/// <summary>
	/// 副本标志 0：小旗子 ； heroID：boss 模型
	/// </summary>
	public string flag;
	/// <summary>
	///  小旗子或者boss模型的位置
	/// </summary>
	public string[] flagPos;
	/// <summary>
	/// 小旗子或者boss 模型的大小
	/// </summary>
	public string[] flagScale;
	/// <summary>
	/// 0: other
	/// 1: normal
	/// 2: elit
	/// 3: challenge
	/// </summary>
	public int type;

	/// <summary>
	/// complete times
	/// # : no times limit
	/// </summary>
	public string comTimes;

	/// <summary>
	/// this fb cost power
	/// </summary>
	public int powerCost;
	
	/// 进入副本的剧情？
	public int storyIn;
	/// 波次前剧情 1|#|1
	public string[] storyCombatBefor;
	/// 胜利剧情
	public int storyWin;
		
	
	

	
	
	/// add end

	/// get reward guai csv ids
	public int[] getRewardGuai()
	{
		List<int> ret = new List<int> ();
		foreach(string str in rewardGuai)
		{
			//ret.Add(GMath.toInt(str));
		}

		return ret.ToArray();
	}
	
}
