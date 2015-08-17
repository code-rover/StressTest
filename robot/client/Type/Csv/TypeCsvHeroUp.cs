
using System.Collections;

public class TypeCsvHeroUp : SuperType
{
	
	/// 描述
	public int id;
	
	/// 名字
	public string name;
	public string info;
	
	/// 道具
	public int upStone0;
	public int upStone1;
	public int upStone2;
	public int upStone3;
	public int upStone4;
	public int upStone5;
	
	/// 限制等级
	public int upLevel0;
	public int upLevel1;
	public int upLevel2;
	public int upLevel3;
	public int upLevel4;
	public int upLevel5;
	
	/// 属性类型
	public string[] proType0;
	public string[] proType1;
	public string[] proType2;
	public string[] proType3;
	public string[] proType4;
	public string[] proType5;
	
	/// 属性
	public string[] proValue0;
	public string[] proValue1;
	public string[] proValue2;
	public string[] proValue3;
	public string[] proValue4;
	public string[] proValue5;
	
	public int GetLv(int index)
	{
		switch(index)
		{
		case 0:
			return upLevel0;
		case 1:
			return upLevel1;
		case 2:
			return upLevel2;
		case 3:
			return upLevel3;
		case 4:
			return upLevel4;
		case 5:
			return upLevel5;
		default:
			utils.Logger.Error("错误的 id");
			return -1;
		}
	}
	
	public int GetStoneId(int index)
	{
		switch(index)
		{
		case 0:
			return upStone0;
		case 1:
			return upStone1;
		case 2:
			return upStone2;
		case 3:
			return upStone3;
		case 4:
			return upStone4;
		case 5:
			return upStone5;
		default:
			return -1;
		}
	}
	
	public string[] GetType(int index)
	{
		switch(index)
		{
		case 0:
			return proType0;
		case 1:
			return proType1;
		case 2:
			return proType2;
		case 3:
			return proType3;
		case 4:
			return proType4;
		case 5:
			return proType5;
		default:
			utils.Logger.Error("错误的 id");
			return null;
		}
	}
	
	public string[] GetValue(int index)
	{
		switch(index)
		{
		case 0:
			return proValue0;
		case 1:
			return proValue1;
		case 2:
			return proValue2;
		case 3:
			return proValue3;
		case 4:
			return proValue4;
		case 5:
			return proValue5;
		default:
			utils.Logger.Error("错误的 id");
			return null;
		}
	}
	
//	EM_PET_PRO_TYPE_STR = 1,			///力量
//	EM_PET_PRO_TYPE_AGI = 2,			///敏捷
//	EM_PET_PRO_TYPE_INTELL = 3,			///智力
//	EM_PET_PRO_TYPE_VIT = 4,			///体力
//	EM_PET_PRO_TYPE_ATK = 5,			///物理攻击
//	EM_PET_PRO_TYPE_ATKTHROUGH = 6,		///无聊穿透
//	EM_PET_PRO_TYPE_ATKDEF = 7,			///物理防御
//	EM_PET_PRO_TYPE_MAGIC = 8,			///魔法攻击
//	EM_PET_PRO_TYPE_MAGICTHROUGH = 9,	///魔法穿透
//	EM_PET_PRO_TYPE_MAGICDEF = 10,		///魔法防御
//	EM_PET_PRO_TYPE_CRIT = 11,			///暴击
//	EM_PET_PRO_TYPE_CRITDEF = 12,		///暴击防御
//	EM_PET_PRO_TYPE_DUCK = 13,			///闪避
//	EM_PET_PRO_TYPE_HP = 14,			///血量

}
