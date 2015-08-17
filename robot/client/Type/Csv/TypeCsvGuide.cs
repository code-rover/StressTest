
using System.Collections;
using System.Collections.Generic;

public class TypeCsvGuide : SuperType {
	//编号
	public int id;
	//引导的id
	public int guide_id;
	//引导到哪步
	public int step;
	/// 触发开始的类型 0 范类型 1.任务完成触发 2任务领取触发 3。引导触发 4第一次通关某个副本 5系统开启()
	public int start_case_type;
	public string[] start_case;
	
	public int is_over_step;
	
	/// 数据 看情况而定 通常用于存储触发引导需要的条件 如通关某个副本 完成某些任务等
	public string[] obj;
	
	/// 对话框的文字说明
	public string info;
	/// 对话框对于小手的偏移量
	public string[] dialog_offset;
	public int dialog_left;
	public int need_follow;
	
	
	
	public string[] hand_offset;
	public int hang_left;
	public int need_follow_hand;
	
	public int window_id;
	public string obj_name;
	public string[] obj_id;
	
	/// 操作类型 -2开始步骤函数 不处理就好了 -1是特殊的操作 需要下发事件 0是可以跳过的步骤 不需要操作的那种 1是选择一个英雄 2界面关闭事件
	public int operate_type;

	public string layer;
}
