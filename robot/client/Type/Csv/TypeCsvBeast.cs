
using System.Collections;

public class TypeCsvBeast : SuperType
{
	//	编号	等级	视图	名字	描述	魂兽(HeroSkill)	魂兽技能(全是光环)	攻击	初始怒气	怒气上限	怒气回复等级/100 * 需要回复的怒气	释放剩余怒气	类型 装备位置
	public int id;
	public int lv;
	public int idView;
	public float scale;
	public float scaleUI;
	public string name;
	public string info;
	public int idSkill;
	/// id|0己方#id|1敌方
	public string[][] idBeastBuff;
	/// 解锁几个光环 0为 没有解锁的解锁
	public int beastBuffLimitLen;

	public float atk;
	public float angerInit;
	public float angerTotal;
	public float angerRecLv;
	public float angerRelease;
	public int type;
	public int local;

	public string effectShow = "effect_common_hunshou_born";
	public string effectHide = "effect_common_hunshou_end";

	/// x|y|z 根据角色方向的偏移。敌人镜像 摄像机中心点
	public string[] standPosition;
	/// x|y|z朝向
	public string[] standRotaion;

	public bool isHasReast = true;


	/// 分解后的碎片id
	public int chipId;
	//分解后的碎片数量
	public int chipCnt;

	// add by ssy 15-03-18
	// beast last effect
	/// <summary>
	/// 魂兽身上穿装备的持久特效
	/// </summary>
	public string effLasting0;
	public string effLasting1;
	public string effLasting2;
	public string effRelax;
	public string audioRelax;
	// add end
}
