
using System.Collections;

public class TypeCsvHeroSkillLeaderLv : SuperType
{
	/// 等级
	public int lv;
	/// 品级
	public int grade;
	/// 物理攻击
	public float atk;
	/// 物理穿透
	public float atkThrough;
	/// 物理防御
	public float atkDef;
	/// 魔法攻击
	public float magic;
	/// 魔法穿透
	public float magicThrough;
	/// 魔法防御
	public float magicDef;
	/// 暴击等级
	public float crit;
	/// 抗暴击等级
	public float critDef;
	/// 闪避等级
	public float duck;
	/// 血量
	public float hp;
	
	public string info = "请策划在 HeroSkillLeaderLv.csv 表中加上一列 info 字段表示描述";
}
