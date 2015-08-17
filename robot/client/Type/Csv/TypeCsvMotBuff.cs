
using System.Collections;

public class TypeCsvMotBuff : SuperType
{
	/// 取值 1为不+属性 > 1为降低属性 < 1 为属性增长
	
	/// id引用
	public int id;
	public int idView;
	/// 名字描述
	public string name;
	public string info;
	/// 属性系数
	public float strMul;
	public float agiMul;
	public float intellMul;
	public float vitMul;
	public float hpMul;
	public float atkMul;
	public float atkDefMul;
	public float magicMul;
	public float magicDefMul;
	public int motFightMoney;
//	buffid	视图	名字	描述	力量	敏捷	智力	体力	血量	物理攻击	物理防御	魔法攻击	魔法防御

}
