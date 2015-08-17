using System;
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
	List<RewardItems> small = new List<RewardItems>();

    
    public List<RewardItems> getAllSmallIcon()
    {
        return small;
    }
	
    //显示大图标类型的附件物品
    List<RewardItems> big = new List<RewardItems>();
	
    public List<RewardItems> getAllBigIcon()
    {
        return big;
    }	
	
    //邮件物品奖励
    List<RewardItems>  reward = new List<RewardItems>();
	
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

/// /// 卖的类型12经验3碎片4卡牌5装备6灵石7竞分
public enum EFBRewardType
{
    EINVALID = 0,
    /// <summary>
    /// 货币
    /// </summary>
    EMoney = 1,
    /// <summary>
    /// 经验
    /// </summary>
    EExp = 2,
    /// <summary>
    /// 卡牌碎片
    /// </summary>
    EFragment = 3,
    /// <summary>
    /// 卡牌
    /// </summary>
    ECard = 4,
    /// <summary>
    /// 道具和装备 
    /// </summary>
    EProp = 5,
    /// <summary>
    /// 灵石
    /// </summary>
    EMoenyTower = 6,
    /// <summary>
    /// 竞技积分.
    /// </summary>
    EPKScore = 7,
    /// <summary>
    /// 助战币
    /// </summary>
    EMotMoney,
}

//奖励物品
public class RewardItems
{
    public InfoProp prop;
    public EFBRewardType type;
    public int emailType; //邮件类型
}

/// 道具
public class InfoProp
{
    public int idCsv;
    public int cnt;
    // add by ssy
    public InfoProp( int id_csv, int cnt )
    {
        idCsv = id_csv;
        this.cnt = cnt;
    }

    public InfoProp()
    {
        ;
    }
    // add end
}

///各种碎片
public class InfoHeroChip
{
    /// 每张卡片多少碎片(idSame, cnt) hero表中的sameid
    public Dictionary<int, long> chipHero = new Dictionary<int, long>();
    /// k值
    public List<int> chipHeroKey = new List<int>();

    public bool isChange = false;

    private bool _needResetRed = false;
    public bool needResetRed
    {
        get
        {
            return _needResetRed;
        }
    }

    public void ResetRed()
    {
        _needResetRed = false;
    }

    public void Clear()
    {
        chipHeroKey.Clear();
        chipHero.Clear();
    }

    /// 添加英雄碎片 
    public void addHeroChip( int idSame, int cnt )
    {
        if( idSame <= 0 )
        {
            return;
        }
        isChange = true;
        _needResetRed = true;
        if( chipHero.ContainsKey( idSame ) )
        {
            chipHero[ idSame ] += cnt;
            if( chipHero[ idSame ] <= 0 )
            {
                chipHero.Remove( idSame );
                chipHeroKey.Remove( idSame );
            }
            return;
        }
        chipHero.Add( idSame, cnt );
        chipHeroKey.Add( idSame );
    }
    /// 直接设置碎片
    public void setHeroChip( int idSame, int cnt )
    {
        if( idSame <= 0 )
        {
            return;
        }
        isChange = true;
        _needResetRed = true;
        if( chipHero.ContainsKey( idSame ) && cnt <= 0 )
        {
            chipHero.Remove( idSame );
            chipHeroKey.Remove( idSame );
            return;
        }

        if( chipHero.ContainsKey( idSame ) )
        {
            chipHero[ idSame ] = cnt;
            return;
        }
        chipHero.Add( idSame, cnt );
        chipHeroKey.Add( idSame );
    }
    /// 获得碎片数量 通过sameId
    public long getHeroChipCount( int idSame )
    {
        if( chipHero.ContainsKey( idSame ) )
            return chipHero[ idSame ];
        return 0;
    }
}



/// 主角的信息
public class InfoPlayer
{
	public double timeTeampUpdate;
	
	/// serverID
	public uint idServer;
	/// 名字
	public string name
	{
		set
		{
			if(!string.IsNullOrEmpty(value))
			{
				_name = value;
			}
		}
		get
		{
			if(string.IsNullOrEmpty(_name))
			{
//				return ConfigLabel.HEOR_NAME;
				return idServer.ToString();
			}
			else
			{
				return _name;
			}
		}
	}
	/// 名字
	private string _name;
	public string getName()
	{
		return _name;
	}
	/// 体力
	public ushort power;
	public ushort powerMax
	{
		get
		{
            /**
			TypeCsvNobility csvNobility = ManagerCsv.getNobility(infoNobility.lv);
			ushort result = 0;
			if(null != csvNobility)
				result += (ushort)csvNobility.power;
			TypeCsvHeroLv csvHeroLv = ManagerCsv.getHeroLv(lv);
			if(null != csvHeroLv)
				result += (ushort)csvHeroLv.powerMax;
			return result;
             **/
            return 0;
		}
	}
	/// 领取体力次数
	public int powerFriendCnt;
	public int powerBuyCnt;	
	// 金币购买次数
	public int stoneBuyCnt;

	/// 声望
	public long honer;
	
	/// 我的产生技能点的技能CD
	//public InfoCD skillCD = new InfoCD();
	public int skillPoint = 0;
	public int BuySkillPointCnt = 0;
	public int maxSkillPoint = 10;  //读表
	
	/// 今天副本扫荡次数
	private int sweepCnt;
	public int SweepTodayCnt
	{
		set{ sweepCnt = value; }
		get{ return sweepCnt; }
	}

	///太阳井活动次数
	private int sunWellCnt;
	public int sunWellTodayCnt
	{
		set{ sunWellCnt = value; }
		get{ return sunWellCnt; }
	}
	/// 角色VIP等级
//	private int vip;
	public int vipLv
	{
//		set{ vip = value; }
		get{ return 1; }
	}
	private int _vipMax = 0;
	public int vipLvMax
	{
		get
		{
			if(_vipMax > 0)
				return _vipMax;
			/// 换算vip的最高级
			//while(null != ManagerCsv.getVIP(_vipMax + 1))
			//{
			//	_vipMax++;
			//}
			return _vipMax;
		}
	}

	public ulong vipExp;
	//今日充值钻石数
	public int chargeMoneyToday = 0;

	/// 我体力的CD
	//public InfoCD powerCD = new InfoCD();
	/// 卡牌背包上限
	public int maxHeroList = 0;
	/// 卡牌仓库上限
	public int maxHeroListWarehouse = 0;
	/// 背包角色信息
	//public InfoHeroList infoHeroList = new InfoHeroList();
	/// 魂兽背包信息
	//public InfoBeastList infoBeastList = new InfoBeastList();
	/// 好友
	//public InfoFriend infoFriendList = new InfoFriend();
	/// 道具背包
	//public InfoPropList infoPropList = new InfoPropList();	
	/// 魂兽道具背包
	//public InfoPropList infoPropBeastList = new InfoPropList();
	/// 购买的材料背包上限
	public int maxPropBagList = 0;
	/// 购买的装备背包上限
	public int maxEquipBagList = 0;
	/// 购买的好友上限
	public int friendBuy = 0;
	/// 友情点数
	public int friendShip = 0;
	/// 升级前的体力
	public int powerLv = 0;
	/// 酒足饭饱开启时间
	public double PowerMealBeginTime;
	/// 酒足饭饱结束时间
	public double PowerMealEndTime;
	/// 酒足饭饱更新下一次时间
	//public double PowerMealUpdateTime = Random.Range(0f, 600f);
	/// 酒足饭饱次数
	public int PowerMealCnt;


    /**

	//远征商店
	public InfoShop infoEDShop = new InfoShop();
	//金币商店
	public InfoShop infoMoneyShop = new InfoShop();


	/// 引导的系统 
	public string guide = "";
	//设置最大体力上限
//	public void setPowerMax()
//	{
//		powerMax = (ushort)(lv + infoNobility.lv);
//	}
	//最大好友上限
	public int getMaxFriendList()
	{
		TypeCsvHeroLv csvhero = ManagerCsv.getHeroLv(lv);
		return csvhero.friendMax + friendBuy;
	}
	/// 材料背包上限
	public int GetPropBagListCount()
	{
		TypeCsvHeroLv csvhero = ManagerCsv.getHeroLv(lv);
		return csvhero.cntPropBag + maxPropBagList;
	}
	/// 装备背包上限
	public int GetEquipBagListCount()
	{
		TypeCsvHeroLv csvhero = ManagerCsv.getHeroLv(lv);
		return csvhero.cntEquipBag + maxEquipBagList;
	}
	/// 卡牌背包上限
	public int GetCardBagListCount()
	{
		TypeCsvHeroLv csvhero = ManagerCsv.getHeroLv(lv);
		return csvhero.cntPetBag + maxHeroList;
	}
	/// 仓库上限
	public int GetWardHouseListCount()
	{
		TypeCsvHeroLv csvhero = ManagerCsv.getHeroLv(lv);
		return csvhero.cntPetBar + maxHeroListWarehouse;
	}
	/// 卡牌背包是否已满
	public bool isHeroListIsFull()
	{
		TypeCsvHeroLv csvhero = ManagerCsv.getHeroLv(lv);
		if(csvhero.cntPetBag + maxHeroList > infoHeroList.getHeros().Count)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	/// 装备和材料背包是否已满
	public bool isPropBagIsFull()
	{
		TypeCsvHeroLv csvhero = ManagerCsv.getHeroLv(lv);
		if(csvhero.cntPropBag + csvhero.cntEquipBag + maxPropBagList + maxEquipBagList 
			> infoPropList.getProps().Count)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	//此系统是否开启
	public bool isOpenSystem(InfoSystem _idSystem)
	{
		bool _isOpen = false;
		TypeCsvSystem _system = ManagerCsv.getSystem(_idSystem);
		
		if(_system == null)
		{
			return true;
		}
		
		if(DataMode.myPlayer.lv >= _system.lv)
		{
			_isOpen = true;
		}
		
		return _isOpen;
	}
	
	/// 获得副本信息
	public InfoFBList infoFBList = new InfoFBList();
	public InfoFBList infoFBListElit = new InfoFBList ();
	public InfoFBList infoFBListChallenge = new InfoFBList ();
	public bool isOpenNewFB = false;
	public bool isOpenNewFBElit = false;
	public bool isOpenNewFBChallenge = false;
	
	public bool isShowIcon = true;
	
	// add by ssy
	/// fb star reward
	public InfoFBStarRewardList infoFBStarRewardList = new InfoFBStarRewardList ();
	// add end
	
	/// 各种系统出产的货币
	//public InfoPoint infoPoint = new InfoPoint();
     * 
     */ 
	/// 经验 (累加)
	public ulong exp;	
	
	/// 这个人的军衔信息
	//public InfoNobility infoNobility = new InfoNobility();
	/// 这个人的竞技信息
	//public InfoPK infoPK = new InfoPK();
	/// 邮件数量
	public int emailCnt
	{
		get
		{
			return _emailCnt;
		}
		set
		{
			if(_emailCnt <= 0)
			{
				_emailCnt = 0;
			}
			_emailCnt = value;
		}
	}
	private int _emailCnt;
	/// 人民币充值的钱
    
	public long money
	{
		get
		{
			return _money;
		}
		set
		{
			if(value < 0)
			{
				//moneyChange();
			}
			_money = value;
		}
	}
     
	private long _money;
	/// 游戏里面用的钱
	public long money_game
	{
		get
		{
			return _money_game;
		}
		set
		{
			if(value < 0)
			{
				//moneyChange();
			}
			_money_game = value;
		}
	}
	
	private long _money_game;
	
    /**
	/// 金钱减少播放音效
	private void moneyChange()
	{
		
	}
	
	// add by ssy
	/// which land show animation have looked
	public int fbLandAniFlag = 0;
	// add end
	
	/// 经验缓存
	public static Dictionary<ulong, int> memoryLv = new Dictionary<ulong, int>();
	/// 返回等级
	public int lv
	{
		get
		{
			if(memoryLv.ContainsKey(exp))
				return memoryLv[exp];
			int resultLv = 1;
			ulong expAdd = 0;
			while(true)
			{
				if(null == ManagerCsv.getHeroLv(resultLv + 1))
					break;
				expAdd += ManagerCsv.getHeroLv(resultLv).expPlayer;
				if(exp < expAdd)
					break;
				resultLv += 1;
			}
			memoryLv.Add(exp, resultLv);
			return resultLv;
		}
	}
	
	/// 领导力
	public int leadership
	{
		get
		{
			TypeCsvHeroLv csvHeroLv = ManagerCsv.getHeroLv(lv);
			return csvHeroLv.leaderShip + ManagerCsv.getNobility(infoNobility.lv).leadership;
		}
	}
	
	
	/// 我的位置类型
	public byte inType;
	public int inIDCsv;
	public bool inIsDie;
	public bool isInFB
	{
		get{return inType == 1;}
		set{inType = (value ? (byte)1 : inType);}
	}
	public bool isInTown
	{
		get{return inType == 2;}
		set{inType = (value ? (byte)2 : inType);}
	}
	public bool isInPK
	{
		get{return inType == 3;}
		set{inType = (value ? (byte)3 : inType);}
	}
	public bool isInFBWorldBoss
	{
		get{return inType == 4;}
		set{inType = (value ? (byte)4 : inType);}
	}
	public bool isInPKFriend
	{
		get{return inType == 5;}
		set{inType = (value ? (byte)5 : inType);}
	}
	public bool isInActive
	{
		get{return inType == 6;}
		set{inType = (value ? (byte)6 : inType);}
	}
	public bool isInTower
	{
		get{return inType == 7;}
		set{inType = (value ? (byte)7 : inType);}
	}
	public bool isInAutoCombat
	{
		get{return inType == 8;}
		set{inType = (value ? (byte)8 : inType);}
	}
	public bool isInTBC
	{
		get{return inType == 9;}
		set{inType = (value ? (byte)9 : inType);}
	}
	public bool isInSunWell
	{
		get{return inType == 10;}
		set{inType = (value ? (byte)10 : inType);}
	}
	/// 是否在抢矿
	public bool isInEscortRob
	{
		get{return inType == 11;}
		set{inType = (value ? (byte)11 : inType);}
	}
	/// 是否海山 奖励关
	public bool isInTBCReward
	{
		get{return inType == 12;}
		set{inType = (value ? (byte)12 : inType);}
	}
	
	
	
	public void addOpenNewFB(int csv_id, ulong server_id)
	{
		TypeCsvFB csv_fb = ManagerCsv.getFB(csv_id);
		switch(csv_fb.type)
		{
			case 1:
			break;
			case 2:
			if(infoFBListElit.fbs.Count == 0)
			{
				isOpenNewFBElit = true;
				PlayerPrefs.SetInt("open_newfb_elit", 1);
				PlayerPrefs.Save();
			}
			break;
			case 3:
			if(infoFBListChallenge.fbs.Count == 0)
			{
				isOpenNewFBElit = true;
				PlayerPrefs.SetInt("open_newfb_challenge", 1);
				PlayerPrefs.Save();		
			}
			break;
			default:
			UtilLog.LogError("fb type is not exist!! type = "  + csv_fb.type);
			break;
		}
	}
	
	// add by ssy
	/// <summary>
	/// open a new fb add depends on fb type
	/// </summary>
	/// <param name="csv_id">Csv_id.</param>
	/// <param name="server_id">Server_id.</param>
	public void addFB(int csv_id, ulong server_id)
	{
		TypeCsvFB csv_fb = ManagerCsv.getFB(csv_id);
		isOpenNewFB = true;
		switch(csv_fb.type)
		{
			case 1:
			infoFBList.addFBInfo(server_id);
			break;
			case 2:
			infoFBListElit.addFBInfo(server_id);
			break;
			case 3:
			infoFBListChallenge.addFBInfo(server_id);
			break;
			default:
			UtilLog.LogError("fb type is not exist!! type = "  + csv_fb.type);
			break;
		}

	}

	/// <summary>
	/// 根据副本类型获得未通关的副本
	/// </summary>
	public ulong getNoComFB(Config.EFBType type)
	{
		ulong ret = 0;
		switch (type)
		{
			case Config.EFBType.ENormal:
			ret = infoFBList.getNotComFBId();
			break;
			case Config.EFBType.EElit:
			ret = infoFBListElit.getNotComFBId();
			break;
			case Config.EFBType.EChallenge:
			ret = infoFBListChallenge.getNotComFBId();
			break;
			default:
			UtilLog.LogError("fb type is not exist!! type = "  + type);
			break;

		}

		return ret;

	}
	
	/// <summary>
	/// 获取所有开启的副本最大的id
	/// </summary>
	/// <returns>
	/// The max COM F.
	/// </returns>
	/// <param name='type'>
	/// Type.
	/// </param>
	public ulong getMaxComFB(Config.EFBType type)
	{
		ulong ret = 0;
		switch (type)
		{
			case Config.EFBType.ENormal:
			ret = infoFBList.getMaxComFBId();
			break;
			case Config.EFBType.EElit:
			ret = infoFBListElit.getMaxComFBId();
			break;
			case Config.EFBType.EChallenge:
			ret = infoFBListChallenge.getMaxComFBId();
			break;
			default:
			UtilLog.LogError("fb type is not exist!! type = "  + type);
			break;

		}

		return ret;
		
	}
	
	/// <summary>
	/// 根据副本类型，获取当前landid
	/// </summary>
	/// <returns>
	/// The current land I.
	/// </returns>
	/// <param name='type'>
	/// Type.
	/// </param>
	public int getCurLandID(Config.EFBType type = Config.EFBType.ENormal)
	{
		int ret = 0;
		ulong no_com_fb = getMaxComFB(type);
		if(no_com_fb == 0)
		{
			UtilLog.LogError("have no max fb type = " + type);
			// changed by ssy
			// 曾经的做法是认为没有未完成的副本时就是通关了，直接取的csv表中最大的那个landid
			// 后来策划增加了开启副本的各种限制，所以逻辑应该修改，当前的landid不应该更csv表中去，而是从数据模型中取最大的副本，然后判断此副本的landid
			//ret = ManagerCsv.getLandID(0);
			
			// changed end
		}
		else
		{
			InfoFB fb_info = DataMode.getFB(no_com_fb);
			ret  = ManagerCsv.getLandID(fb_info.idCsvFB);
		}

		return ret;
	}
	
	//根据副本类型 找出当前副本类型的大陆
	public int getCurTypeLandID(Config.EFBType type)
	{
		int ret = 0;
		ulong no_com_fb = getMaxComFB(type);
		if(no_com_fb == 0)
		{
			UtilLog.LogError("have no max fb type = " + type);
			// changed by ssy
			// 曾经的做法是认为没有未完成的副本时就是通关了，直接取的csv表中最大的那个landid
			// 后来策划增加了开启副本的各种限制，所以逻辑应该修改，当前的landid不应该更csv表中去，而是从数据模型中取最大的副本，然后判断此副本的landid
			//ret = ManagerCsv.getLandID(0);
			
			// changed end
		}
		else
		{
			InfoFB fb_info = DataMode.getFB(no_com_fb);
			ret  = ManagerCsv.getLandID(fb_info.idCsvFB);
		}

		return ret;
	}
	
	/// <summary>
	/// 两个副本id是否是连续的。
	/// </summary>
	public bool isTwoFBNext(ulong id0, ulong id1)
	{
		bool ret = false;
		if(id0 <= 0 || id1 <= 0)
		{
			UtilLog.LogWarning("you give me wrong ids!! id0 = " + id0 + " id1 = " + id1);
			return ret;
		}
		
		int index0 = 0;
		int index1 = 0;
		
		if(infoFBList.fbs.Contains(id0))
		{
			index0 = infoFBList.fbs.IndexOf(id0);
			index1 = infoFBList.fbs.IndexOf(id1);
			
		}
		else if(infoFBListElit.fbs.Contains(id0))
		{
			index0 = infoFBListElit.fbs.IndexOf(id0);
			index1 = infoFBListElit.fbs.IndexOf(id1);
		}
		else if(infoFBListChallenge.fbs.Contains(id0))
		{
			index0 = infoFBListChallenge.fbs.IndexOf(id0);
			index1 = infoFBListChallenge.fbs.IndexOf(id1);
		}
		
		if(Mathf.Abs(index1 - index0) == 1)
		{
			ret = true;
		}
		
		return ret;

	}
	
	/// <summary>
	/// Clears all F.
	/// </summary>
	public void clearAllFB()
	{
		infoFBList.clearFB();
		infoFBListElit.clearFB();
		infoFBListChallenge.clearFB();
	}
	
	
	// add end
     * 
     **/
}




