
using System.Collections;

public class TypeCsvHeroSkillBase : SuperType
{
	/// 编号
	public int id;
	/// 名字
	public string name;
	/// 描述
	public string info;
	public string info2;
	/// 图标以及释放时候效果
	public int idView;
	/// 技能类型0主动1被动2队长(读被动表就ok)
	public int skillType;
	/// 等级仅显示用
	public int lv;
	/// 这个技能最高等级(显示用)
	public int lvMax;
	/// 消耗id
	public int idConsume;
	/// 技能品级
	public int grade = 1;
	
	/// 图标特效
	public string iconEffect = "iconeff_liuxing_jntx_ui";
	
	/// 相同技能的组
	public int idSame;

	//技能等级限制
	public int limitLv;
}
