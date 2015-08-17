
using System.Collections;

public class TypeCsvCharge : SuperType
{
	/// 编号id
	public int id;
	///  有效天数
	public int time;
	///  花费rmb
	public int rmb;
	///  第一次赠送的砖石
	public int f_qmoney;
	/// 不是第一次充值送的钻石
	public int s_qmoney;
	/// 充值获得的钻石
	public int qmoney;
	///每天送的钻石
	public int day_qmoney;	
	///图标id
	public int viewId;	
	///是否是推荐的，1：推荐，0：不推荐，首次推荐不算在这里
	public int isRecommend;
	///描述信息
	public string info;
	///充值名称
	public string name;
}
