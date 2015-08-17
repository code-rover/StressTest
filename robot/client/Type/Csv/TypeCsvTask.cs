
using System.Collections;

public class TypeCsvTask : SuperType
{
		/// 编号id
	public int id;
		///奖励id
	public int idReward;
	/// 类型(1:新手任务 2每日任务 3:等级任务 4：主线副本任务 5:支线精英副本任务 6:支线挑战副本任务)
	public int type;
	///完成条件(副本id | 等级)
	public int csvId;
	/// 对应的viewId
	public int viewId;
	///任务题目title
	public string title;
	///任务描述
	public string content;
	///任务限制等级
	public int limitLv;
	///任务对应副本的heroId
	public int heroId;
	
	public int idNext;

}
