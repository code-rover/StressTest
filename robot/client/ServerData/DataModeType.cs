using System;
using System.Collections;
using System.Collections.Generic;
using net;
using robot.client;
using utils;
using net.unity3d;

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


/// 副本的基础信息
public class InfoFB
{
    public ulong idServer;
    public int idCsvFB;
    public int score;
    /// 是否通关
    public bool isComplete;
    //是否是第一次通关
    public bool isFirstComplete;
    // add by ssy today times
    public int comTimes;
    //允许扫荡次数
    public int allowSweepCnt;
    //重置副本次数
    public int resetFBCnt;

    public InfoFB( SFBInfo info )
    {
        idServer = info.luiIdFB;
        idCsvFB = ( int ) info.uiIdCsvFB;
        score = ( int ) info.cLvKo;

        //判断是否是第一次通关副本  --wen
        if( score > 0 && !isComplete )
        {
            isFirstComplete = true;
        }
        else
        {
            isFirstComplete = false;
        }

        if( score == 0 )
        {
            isComplete = false;
        }
        else
        {
            isComplete = true;
        }
        comTimes = ( int ) info.sKoTodayTimes;
        resetFBCnt = ( int ) info.cResetTimes;
    }

    public InfoFB()
    {
        idServer = 0;
        idCsvFB = 0;
        score = 0;
        isComplete = false;
        isFirstComplete = false;
        comTimes = 0;
    }
}



/// 我的副本信息
public class InfoFBList
{
    public InfoFBList(DataMode dm)
    {
        _dataMode = dm;        
    }
    private DataMode _dataMode;
    private List<ulong> _fbs = new List<ulong>();
    public List<ulong> fbs
    {
        get
        {
            return _fbs;
        }
    }
    /// 获得副本信息
    public InfoFB getFBInfoByCsv( int idCsv )
    {
        InfoFB ret = null;
        foreach( ulong id in _fbs )
        {
            InfoFB info_fb = _dataMode.getFB( id );
            if( info_fb.idCsvFB == idCsv )
            {
                ret = info_fb;
                break;
            }
        }

        return ret;
    }
    /// 设置副本信息
    public void addFBInfo( ulong idServer )
    {
        if( -1 == _fbs.IndexOf( idServer ) )
            _fbs.Add( idServer );
    }
    // add by ssy
    /// <summary>
    /// cache the id
    /// </summary>
    private ulong _noComID = 0;

    /// 获得当前未通关的副本（算法只支持，同时只能存在一个未开启的副本）
    public ulong getNotComFBId()
    {
        ulong ret_error = 0;
        if( _fbs.Count <= 0 )
        {
            //UtilLog.LogError("还没获得任何id，系统开启后，至少得有一个未完成的吧!!");
            // 存在没有开启任何副本的时候。逻辑变了
            return ret_error;
        }

        if( _noComID != 0 &&
           !_dataMode.getFB( _noComID ).isComplete )
        {
            return _noComID;
        }
        else
        {
            // 默认升序 
            _fbs.Sort();

            _noComID = _fbs[ _fbs.Count - 1 ];
            if( _dataMode.getFB( _noComID ).isComplete )
            {
                Logger.Error( "没有未完成的副本呢！！！" );
                return ret_error;

            }
            return _noComID;
        }

    }

    /// <summary>
    /// 获取所有已经开启的副本最大的id
    /// </summary>
    /// <returns>
    /// The max COM FB identifier.
    /// </returns>
    public ulong getMaxComFBId()
    {
        ulong ret = 0;
        if( _fbs.Count <= 0 )
        {
            Logger.Error( "还没获得任何id，系统开启后，至少得有一个未完成的吧!!" );
            return ret;
        }

        if( _noComID != 0 &&
           !_dataMode.getFB( _noComID ).isComplete )
        {
            ret = _noComID;
            return ret;
        }
        else
        {
            // 默认升序 
            _fbs.Sort();

            ret = _fbs[ _fbs.Count - 1 ];

            return ret;
        }

    }

    /// <summary>
    /// Clears the F.
    /// </summary>
    public void clearFB()
    {
        if( fbs.Count > 0 )
        {
            fbs.Clear();
            _noComID = 0;
        }
    }

    // add end
    /// 重置副本今天的攻击次数 重置次数 扫荡次数
    public void resetFBData()
    {
        foreach( ulong idServerFB in _fbs )
        {
            if( null == _dataMode.getFB( idServerFB ) )
                continue;
            _dataMode.getFB( idServerFB ).resetFBCnt = 0;
            _dataMode.getFB( idServerFB ).comTimes = 0;
            _dataMode.getFB( idServerFB ).allowSweepCnt = 0;
        }
    }
}


/// 副本奖励 列表
public class InfoFBRewardList
{
    /// 缓存 object
    private Dictionary<string, List<InfoFBReward>> _rewardObject = new Dictionary<string, List<InfoFBReward>>();
    /// 缓存 钱
    private Dictionary<string, int> _rewardMoneyGame = new Dictionary<string, int>();
    /// 缓存 钻石
    private Dictionary<string, int> _rewardMoney = new Dictionary<string, int>();
    private Dictionary<string, int> _rewardExpPlayer = new Dictionary<string, int>();
    private Dictionary<string, int> _rewardPower = new Dictionary<string, int>();

    /// 缓存 卡牌进入副本前的经验
    private Dictionary<ulong, ulong> _rewardExpBefore = new Dictionary<ulong, ulong>();
    /// 缓存扫荡副本额外奖励
    private Dictionary<string, List<InfoFBReward>> _sweepReward = new Dictionary<string, List<InfoFBReward>>();
    /// 额外扫荡结果
    private List<InfoFBReward> _rewardSweepResultObj = new List<InfoFBReward>();

    private List<InfoFBReward> _rewardSweepObj = new List<InfoFBReward>();
    private int _rewardSweepMoney = 0;
    private bool _isDataOver = false;
    /// 好友点数
    public int friendPoint;
    //扫荡次数
    public int sweepCnt = 0;
    public bool isDataOver
    {
        set
        {
            _isDataOver = value;
        }
        get
        {
            return _isDataOver;
        }
    }

    // add end


    /// 整个副本奖励的经验(玩家的 不是卡牌)
    public ulong exp;
    /// 星级宝箱 钻石(不是游戏币)
    public long money = 0;
    // add by ssy
    public ulong expBegin;
    public int curFbCsvID;
    // add end

    /// 添加钱
    public void clear()
    {
        _rewardExpBefore.Clear();
        _rewardObject.Clear();
        _rewardMoneyGame.Clear();
        _rewardMoney.Clear();
        _rewardPower.Clear();
        _rewardExpPlayer.Clear();
        _isSweepRewardCom = false;
        _rewardSweepObj.Clear();
        _rewardSweepMoney = 0;
        exp = 0;
        money = 0;
        _isDataOver = false;
    }

    /// 清空扫荡结果
    public void clearSweepReward()
    {
        _sweepReward.Clear();
        _rewardSweepResultObj.Clear();
    }
    // add by ssy 
    private bool _isSweepRewardCom = false;
    public bool isSweepRewardCom
    {
        get
        {
            return _isSweepRewardCom;
        }
        set
        {
            _isSweepRewardCom = value;
        }
    }

    /// <summary>
    ///  set sweep fb reward money
    /// </summary>
    /// <param name="money">Money.</param>
    public void addMoneySweep( int money )
    {
        _rewardSweepMoney = money;
    }

    /// <summary>
    /// Adds the reward sweep.
    /// </summary>
    /// <param name="fb_reward">Fb_reward.</param>
    public void addRewardSweep( InfoFBReward fb_reward )
    {
        if( null == fb_reward )
        {
            return;
        }
        else
        {
            _rewardSweepObj.Add( fb_reward );
        }
    }

    /// 扫荡副本的额外结果
    public void addResultRewardSweep( InfoFBReward fb_reward )
    {
        if( null == fb_reward )
        {
            return;
        }
        else
        {
            _rewardSweepResultObj.Add( fb_reward );
        }
    }

    // add end
    /// 获得卡牌(队伍内的卡牌)进入副本之前的经验
    public ulong getExpBefore( ulong serverId )
    {
        if( _rewardExpBefore.ContainsKey( serverId ) )
            return _rewardExpBefore[ serverId ];
        return 0;
    }
    public void setExpBefore( ulong serverId, ulong exp )
    {
        if( !_rewardExpBefore.ContainsKey( serverId ) )
            _rewardExpBefore.Add( serverId, exp );
        else
            _rewardExpBefore[ serverId ] = exp;
    }
    /// 获得金钱
    public int getMoneyGame( int indexBatch, int indexStand )
    {
        if( _rewardMoneyGame.ContainsKey( indexBatch + "/" + indexStand ) )
            return _rewardMoneyGame[ indexBatch + "/" + indexStand ];
        return 0;
    }
    public void setMoneyGame( int indexBatch, int indexStand, int money )
    {
        if( !_rewardMoneyGame.ContainsKey( indexBatch + "/" + indexStand ) )
            _rewardMoneyGame.Add( indexBatch + "/" + indexStand, money );
        else
            _rewardMoneyGame[ indexBatch + "/" + indexStand ] = money;
    }
    /// 获得钻石
    public int getMoney( int indexBatch, int indexStand )
    {
        if( _rewardMoney.ContainsKey( indexBatch + "/" + indexStand ) )
            return _rewardMoney[ indexBatch + "/" + indexStand ];
        return 0;
    }
    public void setMoney( int indexBatch, int indexStand, int money )
    {
        if( !_rewardMoney.ContainsKey( indexBatch + "/" + indexStand ) )
            _rewardMoney.Add( indexBatch + "/" + indexStand, money );
        else
            _rewardMoney[ indexBatch + "/" + indexStand ] = money;
    }

    /// 获得 战队经验
    public int getExpPlayer( int indexBatch, int indexStand )
    {
        if( _rewardExpPlayer.ContainsKey( indexBatch + "/" + indexStand ) )
            return _rewardExpPlayer[ indexBatch + "/" + indexStand ];
        return 0;
    }
    public void setExpPlayer( int indexBatch, int indexStand, int sExp )
    {
        if( !_rewardExpPlayer.ContainsKey( indexBatch + "/" + indexStand ) )
            _rewardExpPlayer.Add( indexBatch + "/" + indexStand, sExp );
        else
            _rewardExpPlayer[ indexBatch + "/" + indexStand ] = sExp;
    }
    public int getAllExpPlayer()
    {
        int result = 0;
        foreach( int nmb in _rewardExpPlayer.Values )
        {
            result += nmb;
        }
        return result;
    }
    /// 获得 体力值
    public int getPower( int indexBatch, int indexStand )
    {
        if( _rewardPower.ContainsKey( indexBatch + "/" + indexStand ) )
            return _rewardPower[ indexBatch + "/" + indexStand ];
        return 0;
    }
    public void setPower( int indexBatch, int indexStand, int sPower )
    {
        if( !_rewardPower.ContainsKey( indexBatch + "/" + indexStand ) )
            _rewardPower.Add( indexBatch + "/" + indexStand, sPower );
        else
            _rewardPower[ indexBatch + "/" + indexStand ] = sPower;
    }
    public int getAllPower()
    {
        int result = 0;
        foreach( int nmb in _rewardPower.Values )
        {
            result += nmb;
        }
        return result;
    }
    /// 获得副本扫荡额外奖励
    public List<InfoFBReward> getSweepReward( int indexBatch, int indexStand )
    {
        if( _sweepReward.ContainsKey( indexBatch + "/" + indexStand ) )
            return _sweepReward[ indexBatch + "/" + indexStand ];
        return null;
    }
    public void setSweepReward( int indexBatch, int indexStand, InfoFBReward reward )
    {
        if( !_sweepReward.ContainsKey( indexBatch + "/" + indexStand ) )
            _sweepReward.Add( indexBatch + "/" + indexStand, new List<InfoFBReward>() );
        _sweepReward[ indexBatch + "/" + indexStand ].Add( reward );
    }
    /// 获得副本扫荡额外所有奖励
    public List<InfoFBReward> getSweepAllReward()
    {
        List<InfoFBReward> list = new List<InfoFBReward>();
        foreach( List<InfoFBReward> reward in _sweepReward.Values )
        {
            foreach( InfoFBReward re in reward )
            {
                if( re != null )
                {
                    list.Add( re );
                }
            }
        }

        // add by ssy
        foreach( InfoFBReward re in _rewardSweepResultObj )
        {
            if( re != null )
            {
                list.Add( re );
            }
        }
        // add end
        return list;
    }


    /// 获得奖励
    public List<InfoFBReward> getReward( int indexBatch, int indexStand )
    {
        if( _rewardObject.ContainsKey( indexBatch + "/" + indexStand ) )
            return _rewardObject[ indexBatch + "/" + indexStand ];
        return null;
    }
    public void setReward( int indexBatch, int indexStand, InfoFBReward reward )
    {
        if( !_rewardObject.ContainsKey( indexBatch + "/" + indexStand ) )
            _rewardObject.Add( indexBatch + "/" + indexStand, new List<InfoFBReward>() );
        _rewardObject[ indexBatch + "/" + indexStand ].Add( reward );
    }
    /// 获得所有奖励
    public List<InfoFBReward> getAllReward()
    {
        List<InfoFBReward> list = new List<InfoFBReward>();
        foreach( List<InfoFBReward> reward in _rewardObject.Values )
        {
            foreach( InfoFBReward re in reward )
            {
                if( re != null )
                {
                    list.Add( re );
                }
            }
        }

        // add by ssy
        foreach( InfoFBReward re in _rewardSweepObj )
        {
            if( re != null )
            {
                list.Add( re );
            }
        }
        // add end
        return list;
    }
    /// 获得所有的钱
    public int getAllMoneyGame()
    {
        int money = 0;
        foreach( int nmb in _rewardMoneyGame.Values )
        {
            money += nmb;
        }
        // add by ssy
        money += _rewardSweepMoney;
        // add end
        return money;
    }
    /// 获得所有的钱
    public int getAllMoney()
    {
        int money = 0;
        foreach( int nmb in _rewardMoney.Values )
        {
            money += nmb;
        }
        return money;
    }
}
/// 副本奖励
public class InfoFBReward
{
    /// 我的csvID
    public int idCsv;
    ///掉落分类1货币2经验3碎片4卡牌5道具 8钻石
    public byte type;
    /// 我的个数
    public int cnt;
}


/// 主角的信息
public class InfoPlayer
{
    public InfoPlayer(DataMode dm)
    {
        this._dataMode = dm; 
        this.infoFBList = new InfoFBList(this._dataMode);
        this.infoFBListElit = new InfoFBList( this._dataMode );
        this.infoFBListChallenge = new InfoFBList( this._dataMode );
    }
    private DataMode _dataMode;
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
	public InfoHeroList infoHeroList = new InfoHeroList();
	/// 魂兽背包信息
	//public InfoBeastList infoBeastList = new InfoBeastList();
	/// 好友
	//public InfoFriend infoFriendList = new InfoFriend();
	/// 道具背包
	public InfoPropList infoPropList = new InfoPropList();	
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


    

	//远征商店
	public InfoShop infoEDShop = new InfoShop();
	//金币商店
	public InfoShop infoMoneyShop = new InfoShop();

    /**
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
	*/
	/// 获得副本信息
    public InfoFBList infoFBList;
    public InfoFBList infoFBListElit;
    public InfoFBList infoFBListChallenge;

	public bool isOpenNewFB = false;
	public bool isOpenNewFBElit = false;
	public bool isOpenNewFBChallenge = false;
	
    /**
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
	public InfoPK infoPK = new InfoPK();
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
	**/
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
			Logger.Error("fb type is not exist!! type = "  + csv_fb.type);
			break;
		}

	}

    /*
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
	**/
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
      
     
}


/// 商店
public class InfoShop
{
    /// 我的 cd
    //public InfoCD infoCD = new InfoCD();
    /// 刷新的 次数
    public int timesReset;
    /// 刷新的 道具
    public List<InfoShopObject> sells = new List<InfoShopObject>();
}
/// 商店商品
public class InfoShopObject
{
    public int idCsvShop;
    public bool isSell;
    public int index;
}


/// 竞技场排位的信息
public class InfoPK
{
    /// 我的我是谁
    public uint idServerPlayer;
    /// 我的名次
    public int rank;
    /// 上一次的排序
    public int rankPre;
    /// 积分
    public int score;
    /// 竞技等级
    public int lv;
    /// 竞技的总次数
    public int combatTimes;
    /// 胜利次数
    public int combatTimesWin;
    /// 还有多少次啊
    public int cnt;
    /// 购买次数
    public int cntBuy;


    /// 团队头像
    public List<int> idTeamCsv = new List<int>();
    /// 团队星级
    public List<int> idTeamStar = new List<int>();
    /// 团队等级
    public List<ulong> idTeamExp = new List<ulong>();

    /// 魂兽
    public int idTeamCsvBeast = 1;


    /// 竞技积分商店
    public InfoShop infoShop = new InfoShop();

    /// 是谁打的你呢
    public string name
    {
        get
        {
            //if( null ==  _dataMode.getPlayer( idServerPlayer ) )
                return "未知的名字";
            //return DataMode.getPlayer( idServerPlayer ).name;
        }
    }
}


/// 卡片 信息
public class InfoHero
{
    public ulong idServer;
    public int idCsv;
    public ulong exp;
    /// 卡牌是几星的
    public int star = 1;


    /// 我会的技能
    //public InfoSkillList infoSkill = new InfoSkillList();
    /// 我的装备
    public InfoPropList infoEquip = new InfoPropList();
    /// 我的进阶石属性增加
    public InfoStone infoStone = new InfoStone();

    /// 计算我本身的进阶石头属性
    
    //TODO
    //...

    /// 属于团队几的
    public bool isInTeam;
    /// 是否是队长
    public bool isTeamLeader;
    /// 我的站立位置算法
    public int standIndex;
    /// 被保护起来的
    public bool isProtected = false;


    private static Dictionary<ulong, int> _fastExpLv = new Dictionary<ulong, int>();
    public static void clearFastExpLv()
    {
        _fastExpLv.Clear();
    }

    /// 获得等级
    public int lv
    {
        get
        {
            if( _fastExpLv.ContainsKey( exp ) )
                return _fastExpLv[ exp ];

            int resultLv = 1;
            ulong expAdd = 0;
            while( true )
            {
                if( resultLv >= ManagerCsv.getAttribute().limitLv )
                    break;
                if( null == ManagerCsv.getHeroLv( resultLv + 1 ) )
                    break;
                expAdd += ManagerCsv.getHeroLv( resultLv ).exp;
                if( exp < expAdd )
                    break;
                resultLv += 1;
            }
            _fastExpLv.Add( exp, resultLv );
            return resultLv;
        }
    }
}

/// 进阶石头的镶嵌
public class InfoStone
{
    /// 石头值
    private ulong _myStone;
    /// 那个地方有没有石头
    public bool hasStone( int sIndex )
    {
        return ( ( ( ulong ) 1 << sIndex ) & _myStone ) != 0;
    }
    /// 设置某个索引镶嵌没镶嵌上
    public void setStone( int sIndex, bool sValue )
    {
        if( !sValue )
        {
            _myStone = ( _myStone & ( ~( ulong ) 1 << sIndex ) );
        }
        if( sValue )
        {
            _myStone = ( _myStone | ( ( ( ulong ) 1 << sIndex ) ) );
        }
    }
    /// 设置基础石
    public void setStone( ulong sMyStone )
    {
        _myStone = sMyStone;
    }
    /// 初始化魔石效果
    public void resetStone()
    {
        _myStone = 0;
    }
}


/// 卡片 列表
public class InfoHeroList
{
	/// 获得角色
    private List<ulong> _heros = new List<ulong>();

    /// 获得所有的角色列表
    public List<InfoHero> getHeros()
    {
        List<InfoHero> result = new List<InfoHero>();
        /// 寻找相同团队的
        //for( int index = 0; index < _heros.Count; index++ )
        //{
        //    result.Add(DataMode.getHero( _heros[ index ] ) );
        //}
        return result;
    }

    public List<ulong> getHeroList()
    {
        return _heros;
    }

    /// 添加数据
    public void addHero( ulong idServerHero )
    {
        if( -1 == _heros.IndexOf( idServerHero ) )
        {
            _heros.Add( idServerHero );
        }
    }
    /// 移除数据
    public void removeHero( ulong idServerHero )
    {
        if( -1 != _heros.IndexOf( idServerHero ) )
            _heros.Remove( idServerHero );
    }

    /// 清除
    public void clear()
    {
        _heros.Clear();
    }

    //TODO ...

}

/// 背包信息
public class InfoPropList
{
    /// 我的道具
    private List<InfoProp> _props = new List<InfoProp>();
    private Dictionary<int, InfoProp> _propsHashCsv = new Dictionary<int, InfoProp>();

    /// 引导专用 add by yxh
    public bool isChange = false;
    private bool _needResetRed = false;
    private Dictionary<int, int> _changeId = new Dictionary<int, int>();
    /// 是否需要显示红点
    public bool needResetRed
    {
        get
        {
            return _needResetRed;
        }
    }
    /// 重置
    public void ResetRed()
    {
        _needResetRed = false;
        _changeId.Clear();
    }
    /// 记录具体发生变化的物品id
    private void AddChangeData( int idCsv )
    {
        _needResetRed = true;
        isChange = true;
        if( !_changeId.ContainsKey( idCsv ) )
        {
            _changeId.Add( idCsv, 0 );
        }
    }
    /// 获取具体发生变化的物品id 这个里面的东西可能在背包中是没有的 
    public List<int> GetChangeData()
    {
        List<int> temp = new List<int>();
        foreach( int id in _changeId.Keys )
        {
            temp.Add( id );
        }
        return temp;
    }


    /// 清除道具
    public void clear()
    {
        _props.Clear();
        _propsHashCsv.Clear();
    }
    /// 添加道具
    public void changeProp( int idCsv, int cnt )
    {
        if( idCsv == 0 )
            return;

        AddChangeData( idCsv );
        InfoProp infoProp = null;
        if( _propsHashCsv.ContainsKey( idCsv ) )
            infoProp = _propsHashCsv[ idCsv ];
        if( cnt <= 0 && infoProp == null )
            return;
        /// 移除不需要的玩意
        if( cnt <= 0 )
        {
            _props.Remove( infoProp );
            _propsHashCsv.Remove( idCsv );
            return;
        }

        if( null == infoProp )
        {
            infoProp = new InfoProp();
            infoProp.idCsv = idCsv;
            _propsHashCsv.Add( idCsv, infoProp );
            _props.Add( infoProp );
        }
        infoProp.cnt = cnt;


    }
    /// 删除一个道具
    public void removeProp( int idCsv, int cnt )
    {
        if( !_propsHashCsv.ContainsKey( idCsv ) )
            return;
        AddChangeData( idCsv );
        _props.Remove( _propsHashCsv[ idCsv ] );
        _propsHashCsv.Remove( idCsv );
    }
    /// 追加道具个数
    public void addProp( int idCsv, int cnt )
    {
        AddChangeData( idCsv );
        if( !_propsHashCsv.ContainsKey( idCsv ) )
        {
            changeProp( idCsv, cnt );
            return;
        }
        changeProp( idCsv, _propsHashCsv[ idCsv ].cnt + cnt );
    }
    /// 直接在某个地方添加一个道具
    public void setProp( int index, int idCsv )
    {
        AddChangeData( idCsv );
        InfoProp infoProp = null;
        if( idCsv != 0 )
        {
            infoProp = new InfoProp();
            infoProp.idCsv = idCsv;
            infoProp.cnt = 1;
        }
        while( _props.Count <= index )
        {
            _props.Add( null );
        }
        _props[ index ] = infoProp;
    }
    /// 获得道具
    public List<InfoProp> getProps()
    {
        List<InfoProp> result = new List<InfoProp>();
        for( int i = 0; i < _props.Count; i++ )
        {
            if( _props[ i ] != null )
            {
                result.Add( _props[ i ] );
            }
        }
        return result;
    }

    public InfoProp getProp( int csvId )
    {
        if( _propsHashCsv.ContainsKey( csvId ) )
        {
            return _propsHashCsv[ csvId ];
        }
        else
        {
            return null;
        }
    }

    //....
}