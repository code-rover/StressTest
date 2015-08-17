
using System.Collections;

public class TypeCsvActivEvent : SuperType
{
	public int id;
	public string name;
	public string info;
	public string infoReward;
	public int idView;
	public int idCombatStand;
	public string urls;
	public string[] initXYZ;
	public string[] initCamera;
	public string[] rewardFb;
	public string[] rewardGuai;


	public string[] weather;
	/// 相机点
	public string[] initCameraTargetPost;
	/// 活动类型1世界boss,2菊爆大队
	public int type;
	/// 0普通1太阳井
	public int typeEvent;



	public int powerCost;
	public int cntBuy;
	public int qmoenyCost;
	public string[][] idCombatEnemy1;
	public string[][] idCombatEnemy2;
	public string[][] idCombatEnemy3;
	
	
	public int cntRandom;
	public string[][] standInfo;
	public string musicSence;
	public string musicCombatLast;
	public string[] betterHeros;

	
	public int lv;
	
	/// 推荐战斗力
	public int fighting;
	
}
