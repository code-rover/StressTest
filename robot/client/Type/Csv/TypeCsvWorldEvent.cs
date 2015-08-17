
using System.Collections;

public class TypeCsvWorldEvent : SuperType
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
	
	public string cameraAnim;
	/// 相机点
	public string[] initCameraTargetPost;
	/// 活动类型1世界boss,2菊爆大队
	public int type;
	public int powerCost;
	public int cntBuy;
	public int qmoenyCost;
	public string[] idCombatEnemy;
	public int cntRandom;
	public string[][] standInfo;
	public string musicSence;
	public string musicCombatLast;

	public string[] betterHeros;

	public string[] rewardIDs;
}
