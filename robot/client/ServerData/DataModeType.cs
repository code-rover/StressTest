using System.Collections;
using System.Collections.Generic;
using net;
using robot.client;

public class EmailInfo
{
	public ulong serverId;
	public int csvId;
	public int isDes;
	public int isLoc; //是否为本地邮件
	public int isSpe; //是否为特殊邮件
	public int isOpen;	
	public int csvMailId;
	public int limiltLv;
	
	public double outTime;
	public double sendTime;
	
	public string text;
	public string title;
	public string nameSend;
		
	//显示小图标类型的附件物品
	//List<RewardItems> small = new List<RewardItems>();

    /**
    public List<RewardItems> getAllSmallIcon()
    {
        return small;
    }
	
    //显示大图标类型的附件物品
    //List<RewardItems> big = new List<RewardItems>();
	
    public List<RewardItems> getAllBigIcon()
    {
        return big;
    }	
	
    //邮件物品奖励
    //List<RewardItems>  reward = new List<RewardItems>();
	
        //此邮件是否有奖励物品，没有只显示文字
    
    public bool isHaveItem()
    {
        bool result = false;
        for(int i=0; i<reward.Count; i++)
        {
            if(reward[i].prop.cnt != 0)
            {
                result = true;
            }
        }
		
        return result;
    }
	


    public void addReward(RewardItems temp)
    {
        if(temp.emailType != 4 && temp.emailType != 5
            && temp.emailType != 7)
            small.Add(temp);
        else 
            big.Add(temp);
		
        reward.Add(temp);
    }
	
    public List<RewardItems> getAllReward()
    {
        return reward;
    }
	
    public void Clear()
    {
        big.Clear();
        small.Clear();	
        reward.Clear();
    }
     * **/
}

public class InfoEmailList
{
	List<InfoEmail> _email = new List<InfoEmail>();
	
	public void addEmail(InfoEmail info)
	{
		if(null != info)
		{
			_email.Add(info);	
		}		
	}
	
	public List<InfoEmail> getList()
	{
		return _email;
	}
	
	public void clear()
	{
		_email.Clear();
	}	
	
	public InfoEmail getEmail(ulong idEmail)
	{
		InfoEmail _temp = null;
		for(int i=0; i<_email.Count; i++)
		{
			if(_email[i].luiIdEmail == idEmail)
			{
				_temp = _email[i];
			}
		}
		return _temp;
	}
}

//邮件
public class InfoEmail
{
	///id server
    public ulong luiIdEmail;
	///csv id
    public int uiIdCsvEmail;
	///主人Id
    public int uiIdMaster;
	///获得时间
    public int uiIdTimeGet;
	/// 保存天数
	public int saveDay;
	///开启时间
    public int uiIdTimeOpen;
	///是否打开
    public int cIsOpen;
	///是否摧毁
    public int cIsDestroy;	
	
	/// 时间模板
	public string formatTime
	{
		get
		{
            return null; //return GUtil.formatTime(Data.serverTime, uiIdTimeGet, saveDay);
		}
	}
}






