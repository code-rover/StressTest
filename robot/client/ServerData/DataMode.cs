
using System.Collections;
using System.Collections.Generic;
using robot.client;

/// 服务器发送来的数据模型缓存
public class DataMode
{
    /// 我的主角
    public InfoPlayer myPlayer;

    /// 主角缓存(idServer, value)
    public Dictionary<uint, InfoPlayer> _serverPlayer = new Dictionary<uint, InfoPlayer>();

    /// 卡片缓存(idServer, value)
    public Dictionary<ulong, InfoHero> _serverHero = new Dictionary<ulong, InfoHero>();

    // 副本缓存(idServer, value)
    public Dictionary<ulong, InfoFB> _serverFB = new Dictionary<ulong, InfoFB>();

    /// 副本奖励应该存的地方
    public InfoFBRewardList infoFBRewardList = new InfoFBRewardList();

	//邮件数据缓存
	public Dictionary<ulong, EmailInfo> _emailInfo = new Dictionary<ulong, EmailInfo>();

	//邮件ID列表
	public List<ulong> emailList = new List<ulong>();

	///邮件列表
	//public InfoEmailList _infoEmailList = new InfoEmailList();

    public EmailInfo getEmailInfo( ulong serverEmailId )
    {
        if( !_emailInfo.ContainsKey( serverEmailId ) )
            return null;
        return _emailInfo[ serverEmailId ];
    }

    /// 碎片信息
    public InfoHeroChip infoHeroChip = new InfoHeroChip();

    /// 获得 主角
    public InfoPlayer getPlayer( uint idServer )
    {
        if( idServer == 0 )
            return null;
        if( !_serverPlayer.ContainsKey( idServer ) )
            return null;
        return _serverPlayer[ idServer ];
    }

    /// 获得 卡牌
    public InfoHero getHero( ulong idServer )
    {
        if( idServer == 0 )
            return null;
        if( !_serverHero.ContainsKey( idServer ) )
            return null;
        return _serverHero[ idServer ];
    }

    /// 获得 副本信息
    public InfoFB getFB( ulong idServer )
    {
        if( idServer == 0 )
            return null;
        if( !_serverFB.ContainsKey( idServer ) )
            return null;
        return _serverFB[ idServer ];
    }


    /// 护送 信息
    public InfoEscort infoEscort = new InfoEscort();

    /// 护送 队伍信息
    public  Dictionary<ulong, InfoEscortSafe> _serverEscortSafe = new Dictionary<ulong, InfoEscortSafe>();
    public Dictionary<ulong, InfoEscortSafeTeam> _serverEscortSafeTeam = new Dictionary<ulong, InfoEscortSafeTeam>();

    /// 护送 信息
    public InfoEscortSafe getEscortSafe( ulong sIDServer )
    {
        if( _serverEscortSafe.ContainsKey( sIDServer ) )
            return _serverEscortSafe[ sIDServer ];
        return null;
    }
    /// 护送 队伍信息
    public InfoEscortSafeTeam getEscortSafeTeam( ulong sIDServerTeam )
    {
        if( _serverEscortSafeTeam.ContainsKey( sIDServerTeam ) )
            return _serverEscortSafeTeam[ sIDServerTeam ];
        return null;
    }


}


