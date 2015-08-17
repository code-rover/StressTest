
using System.Collections;

public class TypeCsvCartoon : SuperType
{
	public int id;
	public int idNext;
	public float timeNext;
	public float timeOver;
	
	/// 类型 1正常图片 2左看板娘 3右看板娘  4特效
	public int type;
	public int idView;
	
	public float showTime;
	public string[][] showPostBezier;
	public bool showIsAlpha;
	public bool showIsPopup;
	
	public float hideTime;
	public string[][] hidePostBezier;
	public bool hideIsAlpha;
	public bool hideIsPopup;
	
	public string name;
	public string info;
	
	public int width;
	public int height;
	
	
	/// 事件类型 0 一般 1 结束引导技能 2 奖励卡牌
	public int typeAction = 1;
}
