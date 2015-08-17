
using System.Collections;

public class TypeCsvProp : SuperType
{
	/// 编号id
	public int id;
	/// 名字
	public string name;
	/// 描述
	public string info;
	/// 视图
	public int idView;
	/// 类型1装备2材料3初级卷轴4中级卷轴5高级卷轴6小经验7中经验8大经验9超大经验10进阶石碎片11魂兽装备12魂兽材料(食量算材料)13魂兽碎片14极品魂兽碎片15宝箱
	public int type;
	/// 品阶(灰白绿蓝紫橙)
	public int grade;
	/// 卖出金币
	public int sell;
	/// 最大堆叠
	public int maxCount;
	/// 寻路id
	public int fdId;
	/// 强化后的装备id
	public int idNext;
	/// 一键强化消耗的钱(RMB)
	public int qmoney;
	/// 强化消耗id
	public int idCom;
	
	/// 是否自动出售
	public int autoSell;

	/// 是不是魂兽装备
	public bool isPropBeast()
	{
		if(type >= 11 && type <= 14)
		{
			return true;
		}
		else if(type == 15)
		{
            /**
			TypeCsvGiftBox gift = utils.ManagerCsv.getGiftBox(id);
			if(gift != null)
			{
				if(gift.type == 1)
				{
					return true;
				}
			}
             **/
		}
		return false;
	}

	/// 是不是魂兽碎片(魂兽碎片和极品魂兽碎片)
	public bool isBeastChip()
	{
		if(type == 13 || type == 14)
		{
			return true;
		}
		return false;
	}

	/// 是不是碎片 (用于区分是否显示碎片的遮罩 hero_touxiang 里面那个)
	public bool isChip()
	{
		if(type == 10 || type == 13 || type == 14)
		{
			return true;
		}
		return false;
	}
}
