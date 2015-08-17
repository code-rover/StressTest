
using System.Collections;
/// 角色等级数据
public class TypeCsvHeroLv : SuperType
{
	public int lv;
	
	/// 如果卡牌的经验值 大于等于 0到lv对应 exp的累加时升级到下一级
	/// 例如 当前经验为0 表中1级对应exp 为 1 则卡牌为1级 (0 < 1)
	/// 当前经验为 16 表中1级对应exp 为1 2级exp对应为 16 则 卡牌等级为2级(1 <= 16(当前) < 1+ 16)
	/// 当前经验为 17 表中1级对应exp 为1 2级exp对应为 16 则 卡牌等级为3级(17 >= 1+ 16)
	public ulong exp;
	
	public ulong expPlayer;
	//魂兽等级
	public ulong expBeast;
	public int leaderShip;
	///魂兽装备升品限制等级
	public int BeastGrade;
	/// 体力上限
	public int powerMax;
	/// 背包上限
	public int cntPetBag;
	///仓库上限
	public int cntPetBar;
	/// 材料背包上限
	public int cntPropBag;
	/// 装备背包上限
	public int cntEquipBag;
	/// 好友上限
	public int friendMax;
	///点金石
	public int stoneLv;
	///没级增加体力
	public int powerAdd;
	
	/// 限制卡牌最大等级
	public int maxHeroLv;

	//魂兽最大等级
	public int maxBeastLv;
	/// 远征开箱奖励的金币系数
	public float motSMoney;
}
