
using System.Collections;

/// 装备属性
public class TypeCsvPropEquip : TypeCsvProp
{
	public string[] limitJob;
	///0武器1头盔2胸甲3腿甲4披风5戒指
	public int local;
	/// 属性

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
	/// 抗暴等级
	public float critDef;
	/// 闪避等级
	public float duck;
	/// 生命上限
	public float hp;
	/// 领导力
	public int leadership;
	/// 限制等级
	public int limitLv;
	/// +几的装备
	public int addNumber;
}
