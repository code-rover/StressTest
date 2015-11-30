using System;
using System.Collections.Generic;
using robot.client.common;
using System.Text;
using utils;

namespace net.unity3d
{
    public class Transceiver
    {
        public Transceiver( AgentNet _agent )
        {
            this.agent = _agent;
        }

        public AgentNet agent;

        /// 改名字
        public void sendChangeName( string name, FunctionListenerEvent listener )
        {
            Logger.Info( "C2RM_ROLE_RENAME >> " + "改名字 ->" + name );

            C2RM_ROLE_RENAME sender = new C2RM_ROLE_RENAME();
            sender.setName( name );

            //C2RM_NAME_RAND sender = new C2RM_NAME_RAND();


            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        // 改名字回应
        public void recvChangeName( ArgsEvent args )
        {
            RM2C_ROLE_RENAME recv = args.getData<RM2C_ROLE_RENAME>();
            if( recv.iResult == 1 )
            {
                Logger.Info( "aid: " + this.agent._accountId + "改名成功：" + recv.getName() );
            }
            else if( recv.iResult == ( int ) EM_CLIENT_ERRORCODE.EE_M2C_ROLE_RENAME_ERROR )
            {
                Logger.Error( "aid: " + this.agent._accountId + "改名失败: " + recv.iResult + " 这个人已经起过名字了" );
            }
            else
            {
                Logger.Error( "aid: " + this.agent._accountId + "改名失败: " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///向服务器发送邮件信息请求
        public void sendWebEmail( FunctionListenerEvent listener )
        {
            this.agent.dataMode._emailInfo.Clear();
            Logger.Info( this.agent._account + " SEND:C2RM_WEB_EMAIL >> " + "发送邮件信息请求 >> " );

            C2RM_WEB_EMAIL sender = new C2RM_WEB_EMAIL();
            sender.uiListen = Dispatcher.addListener( listener, null );

            this.agent.send( sender );

        }

        /// 创建角色
        public void sendCreatRole( int roleCsvId, FunctionListenerEvent sListener )
        {
            C2RM_CREAT_ROLE sender = new C2RM_CREAT_ROLE();
            sender.uiPetCsvId = ( uint ) roleCsvId;
            Logger.Info( this.agent._account + " 创建角色 id: " + roleCsvId );
            this.agent.send( sender );
        }

        //response web email
        ///接受所有邮件信息
        public void recvWebEmail( ArgsEvent args )
        {
            RM2C_WEB_EMAIL recv = args.getData<RM2C_WEB_EMAIL>();

            //Logger.Info( "接收邮件回应 << " + recv.Message );

            for( int i = 0; i < recv.sWebEmail.Length; i++ )
            {
                if( recv.sWebEmail[ i ].uiIdServer == 237275 )
                {
                    Logger.Error( "---237275" );
                }

                if( null == this.agent.dataMode.getEmailInfo( recv.sWebEmail[ i ].uiIdServer ) )
                {
                    this.agent.dataMode._emailInfo.Add( recv.sWebEmail[ i ].uiIdServer, new EmailInfo() );
                }

                EmailInfo reward = this.agent.dataMode.getEmailInfo( recv.sWebEmail[ i ].uiIdServer );
                reward.serverId = recv.sWebEmail[ i ].uiIdServer;
                reward.isDes = recv.sWebEmail[ i ].isDes;
                reward.isLoc = recv.sWebEmail[ i ].isLoc;
                reward.isSpe = recv.sWebEmail[ i ].isSpe;
                reward.isOpen = recv.sWebEmail[ i ].isOpen;
                reward.csvId = ( int ) recv.sWebEmail[ i ].uiRoleId;
                reward.limiltLv = ( int ) recv.sWebEmail[ i ].usLimitLv;
                reward.outTime = ( int ) recv.sWebEmail[ i ].uiOutTime;
                reward.sendTime = ( int ) recv.sWebEmail[ i ].uiSendTime;
                reward.csvMailId = ( int ) recv.sWebEmail[ i ].uiIdWebEmail;
                reward.title = recv.sWebEmail[ i ].getTitle();
                reward.nameSend = recv.sWebEmail[ i ].getSendName();

                reward.Clear();
                for( int m = 0; m < recv.sWebEmail[ i ].vctSWebGoodBase.Length; m++ )
                {
                    if( recv.sWebEmail[ i ].vctSWebGoodBase[ m ].cTypeGoods != 0 )
                    {
                        RewardItems item = new RewardItems();
                        item.prop = new InfoProp();
                        item.prop.idCsv = ( int ) recv.sWebEmail[ i ].vctSWebGoodBase[ m ].uiIdCsvGoods;
                        item.prop.cnt = ( int ) recv.sWebEmail[ i ].vctSWebGoodBase[ m ].m_liCnt;
                        item.emailType = recv.sWebEmail[ i ].vctSWebGoodBase[ m ].cTypeGoods;

                        reward.addReward( item );
                    }
                }

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        // 打开邮件 - wen
        public void sendOpenEmail( ulong emailId, FunctionListenerEvent sListener )
        {
            C2RM_WEB_EMAIL_OPEN sender = new C2RM_WEB_EMAIL_OPEN();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.uiWebEmailId = emailId;
            this.agent.send( sender );
        }

        /// 打开邮件回调（获取物品）
        public void recvOpenEmail( ArgsEvent args )
        {
            RM2C_WEB_EMAIL_OPEN recv = args.getData<RM2C_WEB_EMAIL_OPEN>();


            if( recv.iResult == 1 )
            {
                EmailInfo ei = this.agent.dataMode.getEmailInfo( recv.uiWebEmailId );
                if( ei != null )
                {
                    ei.isOpen = 1;
                }

                for( int i = 0; i < recv.vctSPiece.Length; i++ )
                {
                    if( recv.vctSPiece[ i ].luiIdPiece != 0 )
                        this.agent.dataMode.infoHeroChip.setHeroChip( ( int ) recv.vctSPiece[ i ].uiCsvId, recv.vctSPiece[ i ].iCnt );
                }

                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.vctSEquip.Length; i++ )
                {
                    if( recv.vctSEquip[ i ].uiIdCsvEquipment > 0 )
                    {
                        //csvprop = ManagerCsv.getProp( ( int ) recv.vctSEquip[ i ].uiIdCsvEquipment );
                        //...
                    }
                }
                Logger.Info( this.agent._account + " 打开邮件成功!" );
            }
            else
            {
                Logger.Error( this.agent._account + " 打开邮件失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );

        }

        //除了物品奖励的其他奖励的发放
        public void recvSignReward( ArgsEvent args )
        {
            RM2C_REWARD_MONEY recv = args.getData<RM2C_REWARD_MONEY>();

            Logger.Info( "RM2C_REWARD_MONEY" );

            this.agent.dataMode.myPlayer.money_game += ( long ) recv.sRewardMoney.uiSMoney;
            this.agent.dataMode.myPlayer.money += ( long ) recv.sRewardMoney.uiQMoney;
            this.agent.dataMode.myPlayer.infoPK.score += ( int ) recv.sRewardMoney.uiScoreFight;
            //this.agent.dataMode.myPlayer.infoPoint.moneyTower += ( int ) recv.sRewardMoney.uiTiowerMoney;
            //this.agent.dataMode.myPlayer.infoPoint.badge += ( int ) recv.sRewardMoney.uiBadge;
            this.agent.dataMode.myPlayer.power += recv.sRewardMoney.uiPower;
            this.agent.dataMode.myPlayer.friendShip += ( int ) recv.sRewardMoney.uiFriendShip;
            this.agent.dataMode.myPlayer.honer += ( long ) recv.sRewardMoney.uiPrestige;
            this.agent.dataMode.myPlayer.exp += ( ulong ) recv.sRewardMoney.uiExp;
            //this.agent.dataMode.myPlayer.infoPoint.moneyTBC += ( long ) recv.sRewardMoney.uiMot;
        }

        ///获取副本信息
        //request
        public void sendFBUpdate( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_FB" );
            C2RM_FB sender = new C2RM_FB();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        //response 
        public void recvFBInfo( ArgsEvent args )
        {
            RM2C_FB recv = args.getData<RM2C_FB>();
            if( recv.iResult != 1 )
            {
                Logger.Error( this.agent._account + " RM2C_FB_RESET error: " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 竞技 商店 更新
        public void sendPKShopUpdate( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_GET_PK_SHOP >> 竞技 商店 更新" );
            C2RM_GET_PK_SHOP sender = new C2RM_GET_PK_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            agent.send( sender );
        }

        /// 竞技 商店 刷新 0系统定时自动刷新，1竞技积分刷新
        public void sendPKShopReset( byte sType, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 竞技商店刷新" );
            C2RM_REFRESH_PK_SHOP sender = new C2RM_REFRESH_PK_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.cType = sType;
            this.agent.send( sender );
        }

        /// 竞技 商店 购买
        public void sendPKShopBuy( int sIndex, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 竞技商店购买 " + sIndex );
            C2RM_PK_SHOP_BUY sender = new C2RM_PK_SHOP_BUY();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.iLoc = sIndex;
            this.agent.send( sender );
        }

        /// 竞技 商店 更新
        public void recvPKShopUpdate( ArgsEvent args )
        {
            RM2C_GET_PK_SHOP recv = args.getData<RM2C_GET_PK_SHOP>();


            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "PK商店列表成功回应" );
                this.agent.dataMode.myPlayer.infoPK.infoShop.sells.Clear();
                //this.agent.dataMode.myPlayer.infoPK.infoShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                this.agent.dataMode.myPlayer.infoPK.infoShop.timesReset = recv.iCntRefresh;

                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                    this.agent.dataMode.myPlayer.infoPK.infoShop.sells.Add( shopObj );
                }
            }
            else
            {
                Logger.Error( this.agent._account + " PK商店列表返回失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 竞技 商店 刷新 0系统定时自动刷新，1竞技积分刷新
        public void recvPKShopReset( ArgsEvent args )
        {
            RM2C_REFRESH_PK_SHOP recv = args.getData<RM2C_REFRESH_PK_SHOP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "PK商店刷新成功回应" );
                this.agent.dataMode.myPlayer.infoPK.score += recv.iCostScoreFight;

                this.agent.dataMode.myPlayer.infoPK.infoShop.sells.Clear();
                //this.agent.dataMode.myPlayer.infoPK.infoShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                this.agent.dataMode.myPlayer.infoPK.infoShop.timesReset = recv.iCntRefresh;

                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                    this.agent.dataMode.myPlayer.infoPK.infoShop.sells.Add( shopObj );
                }
            }
            else
            {
                Logger.Error( this.agent._account + " PK商店刷新返回失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 竞技 商店 购买
        public void recvPKShopBuy( ArgsEvent args )
        {
            RM2C_PK_SHOP_BUY recv = args.getData<RM2C_PK_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + "竞技商店购买成功返回 " + recv.iLoc );
            }
            else
            {
                //Logger.Error( this.agent._account + "竞技商店购买失败 " + recv.iResult );    
            }


            //TODO ...

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        /// 爵位商店 刷新 0系统定时自动刷新，1爵位币刷新
        public void sendNobilityShopReset( byte sType, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 爵位商店刷新" );
            C2RM_REFRESH_NOBILITY_SHOP sender = new C2RM_REFRESH_NOBILITY_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.cType = sType;
            this.agent.send( sender );
        }

        /// 爵位商店购买
        public void sendNobilityShopBuy( int sIndex, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 爵位商店购买 " + sIndex );
            C2RM_NOBILITY_SHOP_BUY sender = new C2RM_NOBILITY_SHOP_BUY();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.iLoc = sIndex;
            this.agent.send( sender );
        }

        /// 爵位商店 刷新 0系统定时自动刷新，1钻石刷新
        public void recvNobilityShopReset( ArgsEvent args )
        {
            RM2C_REFRESH_NOBILITY_SHOP recv = args.getData<RM2C_REFRESH_NOBILITY_SHOP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "爵位商店刷新成功回应" );


                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                }
            }
            else
            {
                Logger.Error( this.agent._account + " 爵位商店刷新返回失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 爵位商店购买回应
        public void recvNobilityShopBuy( ArgsEvent args )
        {
            RM2C_NOBILITY_SHOP_BUY recv = args.getData<RM2C_NOBILITY_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + "爵位商店购买成功返回 " + recv.iLoc );
            }
            else
            {
                Logger.Error( this.agent._account + "爵位商店购买失败 " + recv.iResult );
            }


            //TODO ...

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }



        ///获取海加尔山商店信息
        public void sendGetEDShop( FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " 远征商店信息获取" );
            C2RM_GET_MOUNTAIN_SHOP sender = new C2RM_GET_MOUNTAIN_SHOP();
            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        ///刷新海加尔山商店
        public void sendRefreshEDShop( int type, FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " 刷新远征商店" );
            C2RM_REFRESH_MOUNTAIN_SHOP sender = new C2RM_REFRESH_MOUNTAIN_SHOP();
            sender.cType = ( byte ) type;
            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        ///海加尔山商店购买
        public void sendBuyEDShop( int index, FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " 购买远征商店 " + index );
            C2RM_MOUNTAIN_SHOP_BUY sender = new C2RM_MOUNTAIN_SHOP_BUY();
            sender.iLoc = index;
            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        //获取海加尔山商店回复
        public void recvReplyEDShop( ArgsEvent args )
        {
            RM2C_GET_MOUNTAIN_SHOP recv = args.getData<RM2C_GET_MOUNTAIN_SHOP>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 远征商店获取成功" );
                this.agent.dataMode.myPlayer.infoEDShop.timesReset = recv.iCntRefresh;
                //this.agent.dataMode.myPlayer.infoEDShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                int count = recv.m_vctShopGoodsp.Length;
                this.agent.dataMode.myPlayer.infoEDShop.sells.Clear();
                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                    {
                        obj.isSell = false;
                    }
                    else
                    {
                        obj.isSell = true;
                    }
                    this.agent.dataMode.myPlayer.infoEDShop.sells.Add( obj );
                }

            }
            else
            {
                Logger.Error( this.agent._account + " 远征商店获取失败 " + recv.iResult );

            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///刷新海加尔山商店
        public void recvUpdateEDShop( ArgsEvent args )
        {
            RM2C_REFRESH_MOUNTAIN_SHOP recv = args.getData<RM2C_REFRESH_MOUNTAIN_SHOP>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 刷新远征商店成功" );
                this.agent.dataMode.myPlayer.infoEDShop.timesReset = recv.iCntRefresh;
                //this.agent.dataMode.myPlayer.infoEDShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;

                //这里面需要确认是否+=
                //this.agent.dataMode.myPlayer.infoPoint.moneyTBC += recv.iCost;

                int count = recv.m_vctShopGoodsp.Length;
                this.agent.dataMode.myPlayer.infoEDShop.sells.Clear();
                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                    {
                        obj.isSell = false;
                    }
                    else
                    {
                        obj.isSell = true;
                    }
                    this.agent.dataMode.myPlayer.infoEDShop.sells.Add( obj );
                }

            }
            else
            {
                Logger.Error( this.agent._account + " 刷新远征商店失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///海加尔山商店购买回复
        public void recvBuyEDShop( ArgsEvent args )
        {
            RM2C_MOUNTAIN_SHOP_BUY recv = args.getData<RM2C_MOUNTAIN_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 远征商店刷新成功" );
            }
            else
            {
                Logger.Error( this.agent._account + " 远征商店刷新失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///获取背包宠物信息
        public void sendHeroUpdateBag( FunctionListenerEvent sListener )
        {
            C2RM_PET_INFO_BAG sender = new C2RM_PET_INFO_BAG();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        ///发送背包宠物信息
        public void recvHeroBag( ArgsEvent args )
        {
            RM2C_PET_INFO_BAG recv = args.getData<RM2C_PET_INFO_BAG>();
            /// 返回数据1
            if( recv.iResult == 1 )
            {
                InfoPlayer player = this.agent.dataMode.getPlayer( recv.uiMasterId );
                if( recv.cIsBegin == 1 )
                    player.infoHeroList.clear();

                for( int index = 0; index < recv.vctSPetInfo.Length; index++ )
                {
                    if( recv.vctSPetInfo[ index ].uiIdPet == 0 )
                        continue;

                    /// 创建卡片
                    if( null == this.agent.dataMode.getHero( recv.vctSPetInfo[ index ].uiIdPet ) )
                        this.agent.dataMode._serverHero.Add( recv.vctSPetInfo[ index ].uiIdPet, new InfoHero() );

                    player.infoHeroList.addHero( recv.vctSPetInfo[ index ].uiIdPet );
                    /// 设计角色基本信息
                    InfoHero hero = this.agent.dataMode.getHero( recv.vctSPetInfo[ index ].uiIdPet );

                    hero.exp = recv.vctSPetInfo[ index ].luiExp;
                    hero.idCsv = ( int ) recv.vctSPetInfo[ index ].uiIdCsvPet;
                    hero.idServer = recv.vctSPetInfo[ index ].uiIdPet;
                    //				hero.addNumber = recv.vctSPetInfo[index].sAddNum;
                    hero.isProtected = recv.vctSPetInfo[ index ].cIsProtect == 0 ? false : true;
                    hero.star = ( int ) recv.vctSPetInfo[ index ].uiStar;
                    hero.infoStone.setStone( ( ulong ) recv.vctSPetInfo[ index ].uiStone );

                    TypeCsvHero csvHero = ManagerCsv.getHero( hero.idCsv );
                    /// 设置技能信息
                    hero.infoSkill.clear();
                    if( csvHero == null )
                    {
                        Logger.Error( "hero not exist in csv hero id = " + hero.idCsv );
                    }
                    if( null != csvHero.idSkill )
                    {
                        for( int i = 0; i < csvHero.idSkill.Length; i++ )
                        {
                            InfoSkill infoSkill = new InfoSkill();
                            uint sid = recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ];
                            if( sid == 0 )
                            {
                                infoSkill.idCsv = ( int ) float.Parse( csvHero.idSkill[ i ] );
                            }
                            else
                            {
                                infoSkill.idCsv = ( int ) sid;
                                infoSkill.isActive = true;
                            }
                            hero.infoSkill.skillSort.Add( infoSkill.idCsv );
                            hero.infoSkill.setInfoSkill( infoSkill );
                        }
                    }
                    /// 设置释放的技能
                    hero.infoSkill.setSkillRelease( ( int ) recv.vctSPetInfo[ index ].uiIdCsvHandSkill );

                    hero.infoEquip.clear();
                    for( int i = 0; i < recv.vctSPetInfo[ index ].vctLuiIdEquip.Length; i++ )
                    {
                        hero.infoEquip.setProp( i, ( int ) recv.vctSPetInfo[ index ].vctLuiIdEquip[ i ] );
                    }
                }
            }
            else
            {
                Logger.Error( this.agent._account + " RM2C_PET_INFO_BAG error " + recv.iResult );
            }

            if( recv.cIsOver == 1 )  //本命令完成
            {
                Dispatcher.dispatchListener( recv.uiListen, recv );
            }

        }

        /// 升级星级
        public void sendPetStarUp( ulong serverId, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND : C2RM_PET_STAR_UP >> " + serverId );

            C2RM_PET_STAR_UP sender = new C2RM_PET_STAR_UP();
            sender.uiPetId = serverId;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );

        }

        /// 升星
        public void recvPetStarUp( ArgsEvent args )
        {
            RM2C_PET_STAR_UP recv = args.getData<RM2C_PET_STAR_UP>();

            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " RM2C_PET_STAR_UP" );
            }
            else
            {
                Logger.Error( this.agent._account + " RM2C_PET_STAR_UP >> iResult = " + recv.iResult );
            }

            //TODO ...

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 碎片 信息更新
        public void sendHeroChipUpdate( FunctionListenerEvent sListener )
        {
            C2RM_PIECE sender = new C2RM_PIECE();
            Logger.Info( this.agent._account + " SEND:C2RM_PIECE >> " + "碎片 信息更新" );
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 碎片 信息更新
        public void recvHeroChipUpdate( ArgsEvent args )
        {
            RM2C_PIECE recv = args.getData<RM2C_PIECE>();

            Logger.Info( this.agent._account + "  RECV:RM2C_PIECE >> " + "碎片 信息更新" );

            this.agent.dataMode.infoHeroChip.Clear();

            /// 更新我的背包信息
            for( int i = 0; i < recv.sPiece.Length; i++ )
            {
                this.agent.dataMode.infoHeroChip.setHeroChip( ( int ) recv.sPiece[ i ].uiCsvId, recv.sPiece[ i ].iCnt );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 合成卡牌
        public void sendPetChipToPet( int sameId, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + "卡牌合成 sameId: " + sameId );
            C2RM_PET_PIECE_TO_PET sender = new C2RM_PET_PIECE_TO_PET();
            sender.uiCsvId = ( uint ) sameId;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 合成卡牌
        public void recvPetChipToPet( ArgsEvent args )
        {
            RM2C_PET_PIECE_TO_PET recv = args.getData<RM2C_PET_PIECE_TO_PET>();


            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + "卡牌合成成功 " + recv.sPetInfo.uiIdCsvPet );
                this.agent.dataMode.myPlayer.money += ( long ) recv.sMoney.iQMoney;
                this.agent.dataMode.myPlayer.money_game += ( long ) recv.sMoney.iSMoney;

                /// 创建出来卡牌信息
                if( !this.agent.dataMode._serverHero.ContainsKey( recv.sPetInfo.uiIdPet ) )
                    this.agent.dataMode._serverHero.Add( recv.sPetInfo.uiIdPet, new InfoHero() );

                InfoHero hero = this.agent.dataMode.getHero( recv.sPetInfo.uiIdPet );
                hero.exp = recv.sPetInfo.luiExp;
                hero.idServer = recv.sPetInfo.uiIdPet;
                hero.idCsv = ( int ) recv.sPetInfo.uiIdCsvPet;
                hero.star = ( int ) recv.sPetInfo.uiStar;
                hero.infoStone.setStone( ( ulong ) recv.sPetInfo.uiStone );

                this.agent.dataMode.infoHeroChip.setHeroChip( ( int ) recv.sPiece.uiCsvId, recv.sPiece.iCnt );

                TypeCsvHero csvHero = ManagerCsv.getHero( hero.idCsv );
                /// 设置技能信息
                /*
                hero.infoSkill.clear();
                if( csvHero == null )
                    Logger.Error( "hero not exist in csv hero id = " + hero.idCsv );

                if( null != csvHero.idSkill )
                {
                    for( int i = 0; i < csvHero.idSkill.Length; i++ )
                    {
                        InfoSkill infoSkill = new InfoSkill();
                        if( recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ] == 0 )
                        {
                            infoSkill.idCsv = ( int ) float.Parse( csvHero.idSkill[ i ] );
                        }
                        if( recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ] != 0 )
                        {
                            infoSkill.idCsv = ( int ) recv.vctSPetInfo[ index ].vctUiIdCsvSkill[ i ];
                            infoSkill.isActive = true;
                        }
                        hero.infoSkill.skillSort.Add( infoSkill.idCsv );
                        hero.infoSkill.setInfoSkill( infoSkill );
                    }
                }
                /// 设置释放的技能
                hero.infoSkill.setSkillRelease( ( int ) recv.vctSPetInfo[ index ].uiIdCsvHandSkill );
                 */
                hero.infoEquip.clear();
                for( int i = 0; i < recv.sPetInfo.vctLuiIdEquip.Length; i++ )
                {
                    hero.infoEquip.setProp( i, ( int ) recv.sPetInfo.vctLuiIdEquip[ i ] );
                }

                /// 保存卡牌
                this.agent.dataMode.myPlayer.infoHeroList.addHero( hero.idServer );
            }
            else
            {
                Logger.Error( this.agent._account + "卡牌合成失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///获取装备
        public void sendEquipUpdate( FunctionListenerEvent sListener )
        {
            C2RM_EQUIPMENT sender = new C2RM_EQUIPMENT();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        ///Response 装备信息
        public void recvHeroEquip( ArgsEvent args )
        {

            RM2C_EQUIPMENT recv = args.getData<RM2C_EQUIPMENT>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 获取装备信息成功" );
                InfoPlayer player = this.agent.dataMode.getPlayer( recv.uiMasterId );
                if( recv.cIsBegin != 0 )
                {
                    player.infoPropList.clear();
                    //player.infoPropBeastList.clear();
                }

                //bool b = myPlayer.idServer == player.idServer ? true : false;
                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.vctSEquipment.Length; i++ )
                {
                    if( recv.vctSEquipment[ i ].uiIdCsvEquipment <= 0 )
                    {
                        Logger.Error( this.agent._account + " 无意义的物品" );
                        continue;
                    }
                    csvprop = ManagerCsv.getProp( ( int ) recv.vctSEquipment[ i ].uiIdCsvEquipment );
                    if( csvprop == null )
                        continue;

                    if( csvprop.isPropBeast() )
                    {
                        //player.infoPropBeastList.changeProp( ( int ) recv.vctSEquipment[ i ].uiIdCsvEquipment, recv.vctSEquipment[ i ].iCnt );
                    }
                    else
                    {
                        player.infoPropList.changeProp( ( int ) recv.vctSEquipment[ i ].uiIdCsvEquipment, recv.vctSEquipment[ i ].iCnt );
                    }
                }
            }
            else
            {
                Logger.Error( this.agent._account + " 获取装备信息失败 " + recv.iResult );
            }
            if( recv.cIsOver == 1 )
            {
                Dispatcher.dispatchListener( recv.uiListen, recv );
            }

        }

        /// 升级卡牌 吃药
        public void sendPetLvUp( ulong serverId, int csvPropId, int propCnt, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND : C2RM_PET_LV_UP >> " + serverId );
            C2RM_PET_LV_UP sender = new C2RM_PET_LV_UP();
            sender.uiPetId = serverId;
            sender.uiPropCsvId = ( uint ) csvPropId;
            sender.uiCnt = ( uint ) propCnt;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        ///卡牌升级
        public void recvPetLvUp_New( ArgsEvent args )
        {
            RM2C_PET_LV_UP recv = args.getData<RM2C_PET_LV_UP>();
            if( recv.iResult == 1 )
            {
                if( null == this.agent.dataMode.getHero( recv.sPetEat.uiPetId ) )
                    this.agent.dataMode._serverHero.Add( recv.sPetEat.uiPetId, new InfoHero() );

                this.agent.dataMode.myPlayer.infoHeroList.addHero( recv.sPetEat.uiPetId );
                /// 设计角色基本信息
                InfoHero hero = this.agent.dataMode.getHero( recv.sPetEat.uiPetId );
                hero.exp = recv.sPetEat.uiExp;
                hero.idCsv = ( int ) recv.sPetEat.uiCsvId;
                hero.idServer = recv.sPetEat.uiPetId;

                if( recv.sProp.uiIdCsvEquipment > 0 )
                {
                    TypeCsvProp csvprop = ManagerCsv.getProp( ( int ) recv.sProp.uiIdCsvEquipment );
                    if( csvprop.isPropBeast() )
                    {
                        //this.agent.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.sProp.uiIdCsvEquipment, recv.sProp.iCnt );
                    }
                    else
                    {
                        this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.sProp.uiIdCsvEquipment, recv.sProp.iCnt );
                    }
                }

                TypeCsvHeroLv csvherolv = ManagerCsv.getHeroLv( this.agent.dataMode.myPlayer.lv );

                Logger.Info( this.agent._account + " 卡牌升级成功 " );
            }
            else
            {
                Logger.Error( this.agent._account + " 卡牌升级失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 增加体力
        public void sendPowerAdd( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + "SEND:C2RM_POWER_ADD >> 体力 增加" );
            C2RM_POWER_ADD sender = new C2RM_POWER_ADD();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 体力购买
        public void sendPwoerBuy( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 体力购买" );
            C2RM_POWER_BUY sender = new C2RM_POWER_BUY();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 购买体力回调
        public void recvPowerBuy( ArgsEvent args )
        {
            RM2C_POWER_BUY recv = args.getData<RM2C_POWER_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 体力购买成功 " );
                this.agent.dataMode.myPlayer.power = recv.sLeadPowerInfo.usPower;
                this.agent.dataMode.myPlayer.powerBuyCnt = recv.sLeadPowerInfo.sPowerBuyCnt;
            }
            else
            {
                Logger.Error( this.agent._account + " 体力购买失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 角色 装备穿上
        public void sendHeroEquipChange( ulong idServerHero, int[] idCsvEquip, FunctionListenerEvent sListener )
        {
            C2RM_EQUIP_PET_NOTIFY sender = new C2RM_EQUIP_PET_NOTIFY();

            string csvEq = "";
            for( int i = 0; i < idCsvEquip.Length; i++ )
            {
                csvEq += idCsvEquip[ i ] + " ";
            }

            Logger.Info( this.agent._account + " 装备变更  " + idServerHero + " - " + csvEq );

            uint[] idCsvEquipUnit = new uint[ idCsvEquip.Length ];
            for( int i = 0; i < idCsvEquip.Length; i++ )
            {
                idCsvEquipUnit[ i ] = ( uint ) idCsvEquip[ i ];
            }
            sender.sPetEquip.uiPetId = idServerHero;
            sender.sPetEquip.uiIdEquip = idCsvEquipUnit;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 角色 装备穿上
        public void recvHeroEquipChange( ArgsEvent args )
        {
            RM2C_EQUIP_PET_NOTIFY recv = args.getData<RM2C_EQUIP_PET_NOTIFY>();

            if( recv.iResult == 1 )
            {
                /// 装备信息剖析
                InfoHero infoHero = this.agent.dataMode.getHero( recv.sPetEquip.uiPetId );
                infoHero.infoEquip.clear();
                for( int i = 0; i < recv.sPetEquip.uiIdEquip.Length; i++ )
                {
                    infoHero.infoEquip.setProp( i, ( int ) recv.sPetEquip.uiIdEquip[ i ] );
                }
                /// 更新我的背包信息
                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.sEquip.Length; i++ )
                {
                    if( recv.sEquip[ i ].uiIdCsvEquipment <= 0 )
                    {
                        Logger.Error( "无意义的物品" );
                        continue;
                    }
                    csvprop = ManagerCsv.getProp( ( int ) recv.sEquip[ i ].uiIdCsvEquipment );
                    if( csvprop.isPropBeast() )
                    {
                        //this.agent.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.sEquip[ i ].uiIdCsvEquipment, recv.sEquip[ i ].iCnt );
                    }
                    else
                    {
                        this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.sEquip[ i ].uiIdCsvEquipment, recv.sEquip[ i ].iCnt );
                    }
                }
                Logger.Info( this.agent._account + " 装备变更消息回应成功 " + recv.sPetEquip.uiPetId );
            }
            else
            {
                Logger.Error( this.agent._account + " 装备变更消息回应失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        public void recvPowerAdd( ArgsEvent args )
        {
            RM2C_POWER_ADD recv = args.getData<RM2C_POWER_ADD>();
            Logger.Info( "RECV:RM2C_POWER_ADD >> " + recv.iResult + "体力 增加回复" );

            if( recv.iResult == 1 )
            {
                /// 下一次获得体力的时间戳子
                //this.agent.dataMode.myPlayer.powerCD.timeTeamp = ( double ) recv.sLeadPowerInfo.uiPowerLessTime + ManagerCsv.getAttribute().powerAddTime + 1f;
                this.agent.dataMode.myPlayer.power = recv.sLeadPowerInfo.usPower;
            }
            else
            {
                Logger.Error( this.agent._account + " 体力增加失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 镶嵌
        public void sendStoneInLay( ulong petServerId, int index, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND : C2RM_STONE_INLAY >>  petServerId: " + petServerId + " index: " + index );
            C2RM_STONE_INLAY sender = new C2RM_STONE_INLAY();
            sender.luiIdPet = petServerId;
            sender.usLoc = ( ushort ) index;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 进阶
        public void sendPetStoneUp( ulong petServerId, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND : C2RM_PET_STONE_UP >> petServerId:" + petServerId );
            C2RM_PET_STONE_UP sender = new C2RM_PET_STONE_UP();
            sender.luiIdPet = petServerId;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 进阶回应
        public void recvPetStoneUp( ArgsEvent args )
        {
            RM2C_PET_STONE_UP recv = args.getData<RM2C_PET_STONE_UP>();

            if( recv.iResult == 1 )
            {
                InfoHero hero = this.agent.dataMode.getHero( recv.luiIdPet );
                hero.idCsv = ( int ) recv.uiIdCsvPet;
                hero.infoStone.resetStone();
                this.agent.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.agent.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;

                Logger.Info( this.agent._account + " 进阶回应成功  luiIdPet:" + recv.luiIdPet );
            }
            else
            {
                Logger.Error( this.agent._account + " 进阶回应失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 镶嵌回应
        public void recvPetStoneInLay( ArgsEvent args )
        {
            RM2C_STONE_INLAY recv = args.getData<RM2C_STONE_INLAY>();

            if( recv.iResult == 1 )
            {
                InfoHero hero = this.agent.dataMode.getHero( recv.luiIdPet );
                hero.infoStone.setStone( recv.uiStoneInfo );

                this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.SCostStone.uiId, recv.SCostStone.iCnt );
                this.agent.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.agent.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;

                Logger.Info( this.agent._account + " 进阶石镶嵌成功" );
            }
            else
            {
                Logger.Error( this.agent._account + " 进阶石镶嵌失败 " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 装备强化
        public void sendEquipUp( int location, ulong petId, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 装备强化 " + petId + " " + location );

            C2RM_EQUIP_UP sender = new C2RM_EQUIP_UP();
            sender.iLoc = location;
            sender.luiIdPet = petId;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }
        /// 装备强化
        public void recvEquipUp( ArgsEvent args )
        {
            RM2C_EQUIP_UP recv = args.getData<RM2C_EQUIP_UP>();

            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 装备强化回应成功  " + recv.uiIdCsvEqu );
                TypeCsvProp csvprop = null;
                foreach( SCostInfo info in recv.vctCostInfo )
                {
                    if( info.cType == 0 )
                    {
                        if( info.uiId <= 0 )
                        {
                            Logger.Error( "无意义的物品" );
                            continue;
                        }
                        csvprop = ManagerCsv.getProp( ( int ) info.uiId );
                        if( csvprop.isPropBeast() )
                        {
                            //this.agent.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                        else
                        {
                            this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                    }
                }

                InfoHero hero = this.agent.dataMode.getHero( recv.luiIdPet );
                hero.infoEquip.setProp( recv.iLoc, ( int ) recv.uiIdCsvEqu );

                this.agent.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.agent.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;
            }
            else
            {
                Logger.Error( this.agent._account + " 装备强化回应失败  " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 一键强化
        public void sendEquipUpAll( int location, ulong petId, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 一键强化装备 " + petId + " " + location );

            C2RM_EQUIP_UP_ONE_KEY sender = new C2RM_EQUIP_UP_ONE_KEY();
            sender.iLoc = location;
            sender.luiIdPet = petId;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 一键强化
        public void recvEquipUpAll( ArgsEvent args )
        {
            RM2C_EQUIP_UP_ONE_KEY recv = args.getData<RM2C_EQUIP_UP_ONE_KEY>();

            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 装备一键强化回应成功  " + recv.uiIdCsvEqu );
                TypeCsvProp csvprop = null;
                foreach( SCostInfo info in recv.vctCostInfo )
                {
                    if( info.cType == 0 )
                    {
                        if( info.uiId <= 0 )
                        {
                            Logger.Info( "无意义的物品" );
                            continue;
                        }
                        csvprop = ManagerCsv.getProp( ( int ) info.uiId );
                        if( csvprop.isPropBeast() )
                        {
                            //this.agent.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                        else
                        {
                            this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) info.uiId, info.iCnt );
                        }
                    }
                }

                InfoHero hero = this.agent.dataMode.getHero( recv.luiIdPet );
                hero.infoEquip.setProp( recv.iLoc, ( int ) recv.uiIdCsvEqu );

                this.agent.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.agent.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;
            }
            else
            {
                Logger.Error( this.agent._account + " 装备一键强化回应失败  " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 装备合成
        public void sendEquipCreat( uint csvProp, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 合成装备 csvProp: " + csvProp );
            C2RM_EQUIP_COM sender = new C2RM_EQUIP_COM();
            sender.uiIdGoods = csvProp;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 合成装备
        public void recvEquipCreat( ArgsEvent args )
        {
            RM2C_EQUIP_COM recv = args.getData<RM2C_EQUIP_COM>();

            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 装备合成回应成功 " + recv.SEqu.luiIdEquipment );
                TypeCsvProp csvprop = null;
                for( int i = 0; i < recv.vctCostInfo.Length; i++ )
                {
                    if( recv.vctCostInfo[ i ].cType == 0 )
                    {
                        if( recv.vctCostInfo[ i ].uiId <= 0 )
                        {
                            Logger.Error( "无意义的物品" );
                            continue;
                        }
                        csvprop = ManagerCsv.getProp( ( int ) recv.vctCostInfo[ i ].uiId );
                        if( csvprop.isPropBeast() )
                        {
                            //this.agent.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.vctCostInfo[ i ].uiId, recv.vctCostInfo[ i ].iCnt );
                        }
                        else
                        {
                            this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.vctCostInfo[ i ].uiId, recv.vctCostInfo[ i ].iCnt );
                        }
                    }
                }
                if( recv.SEqu.uiIdCsvEquipment > 0 )
                {
                    csvprop = ManagerCsv.getProp( ( int ) recv.SEqu.uiIdCsvEquipment );
                    if( csvprop.isPropBeast() )
                    {
                        //this.agent.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.SEqu.uiIdCsvEquipment, recv.SEqu.iCnt );
                    }
                    else
                    {
                        this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.SEqu.uiIdCsvEquipment, recv.SEqu.iCnt );
                    }
                }

                this.agent.dataMode.myPlayer.money += ( long ) recv.SCostMoney.iQMoney;
                this.agent.dataMode.myPlayer.money_game += ( long ) recv.SCostMoney.iSMoney;
            }
            else
            {
                Logger.Error( this.agent._account + " 装备合成回应失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //请求升级卡牌技能
        public void sendPetSkillUp( UInt64 _uiPetId, UInt32 _uiCsvSkillId, List<int> _consumeList, FunctionListenerEvent _sListener )
        {
            Logger.Info( this.agent._account + " 请求升级卡牌技能  PetId:" + _uiPetId );

            C2RM_SKILL_UP sender = new C2RM_SKILL_UP();
            sender.iCnt = 1;
            sender.uiListen = Dispatcher.addListener( _sListener, null );
            sender.uiPetId = _uiPetId;
            sender.uiCsvSkillId = _uiCsvSkillId;

            this.agent.send( sender );
        }

        ///学习技能
        public void sendSkillUpNew( ulong serverId, int skillId, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 请求学习技能  serverId:" + serverId + " skillId:" + skillId );
            C2RM_SKILL_UP_NEW sender = new C2RM_SKILL_UP_NEW();
            sender.uiPetId = serverId;
            sender.uiCsvSkillId = ( uint ) skillId;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 卡牌技能升级回应
        public void recvSkillUp_New( ArgsEvent args )
        {
            RM2C_SKILL_UP_NEW recv = args.getData<RM2C_SKILL_UP_NEW>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 技能升级成功  PetId: " + recv.sKillUp.uiPetId + "   " + recv.sKillUp.uiOldSkillId + " -> " + recv.sKillUp.uiNewSkillId );
                this.agent.dataMode.myPlayer.skillPoint = ( ( this.agent.dataMode.myPlayer.skillPoint - 1 <= 0 ) ? 0 : this.agent.dataMode.myPlayer.skillPoint -= 1 );

                this.agent.dataMode.myPlayer.money_game += ( long ) recv.sMoney.iSMoney;
                InfoHero _hero = this.agent.dataMode.getHero( recv.sKillUp.uiPetId );
                _hero.infoSkill.setSkillRelease( ( int ) recv.sKillUp.uiHandSkillId );

                _hero.infoSkill.setSkillChange( ( int ) recv.sKillUp.uiOldSkillId, ( int ) recv.sKillUp.uiNewSkillId );
            }
            else
            {
                Logger.Error( this.agent._account + " 技能升级回应失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        public void recvTickByOther( ArgsEvent args )
        {
            RM2C_TICK_BY_OTHER recv = args.getData<RM2C_TICK_BY_OTHER>();

            Logger.Error( this.agent._account + " 被踢  reson: " + recv.iReason );
        }


        /// 爵位 商店
        public void sendGetNobilityShop( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_GET_NOBILITY_SHOP >> 爵位 商店获取" );
            C2RM_GET_NOBILITY_SHOP sender = new C2RM_GET_NOBILITY_SHOP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 爵位商店回应
        public void recvGetNobilityShop( ArgsEvent args )
        {
            RM2C_GET_NOBILITY_SHOP recv = args.getData<RM2C_GET_NOBILITY_SHOP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "爵位商店列表成功回应" );
                ///this.agent.dataMode.myPlayer.infoPK.infoShop.sells.Clear();
                ///this.agent.dataMode.myPlayer.infoPK.infoShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
                ///this.agent.dataMode.myPlayer.infoPK.infoShop.timesReset = recv.iCntRefresh;

                for( int index = 0; index < recv.m_vctShopGoodsp.Length; index++ )
                {
                    InfoShopObject shopObj = new InfoShopObject();
                    shopObj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ index ].uiIdCsvGoods;
                    shopObj.isSell = ( recv.m_vctShopGoodsp[ index ].cIsBuy == 1 );
                    shopObj.index = index;
                    ///this.agent.dataMode.myPlayer.infoPK.infoShop.sells.Add( shopObj );
                }
            }
            else
            {
                Logger.Error( this.agent._account + " 爵位商店列表返回失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///魂侠抽UI
        public void sendLuckySoulList( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_LUCKY_SOUL_LIST >> 魂侠抽UI" );
            C2RM_LUCKY_SOUL_LIST sender = new C2RM_LUCKY_SOUL_LIST();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        ///魂侠抽UI回应
        public void recvLuckySoulList( ArgsEvent args )
        {
            RM2C_LUCKY_SOUL_LIST recv = args.getData<RM2C_LUCKY_SOUL_LIST>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "魂侠UI成功回应" );

            }
            else
            {
                Logger.Error( this.agent._account + " 魂侠UI返回失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }



        ///魂侠抽
        public void sendLuckySoul( FunctionListenerEvent sListener )
        {
            //Logger.Info( "SEND:C2RM_LUCKY_SOUL >> 魂侠抽" );
            C2RM_LUCKY_SOUL sender = new C2RM_LUCKY_SOUL();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.m_bIsTimes_10 = 1;  //
            this.agent.send( sender );
        }

        ///魂侠抽回应
        public void recvLuckySoul( ArgsEvent args )
        {
            RM2C_LUCKY_SOUL recv = args.getData<RM2C_LUCKY_SOUL>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "魂侠成功回应" );


            }
            else
            {
                Logger.Error( this.agent._account + " 魂侠返回失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //使用道具增加技能点
        void sendPropAddSP( FunctionListenerEvent sListener )
        {
            C2RM_USE_PROP_ADD_SP sender = new C2RM_USE_PROP_ADD_SP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.propId = 10060;
            this.agent.send( sender );
        }

        ///使用道具增加技能点回应
        public void recvPropAddSP( ArgsEvent args )
        {
            RM2C_USE_PROP_ADD_SP recv = args.getData<RM2C_USE_PROP_ADD_SP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "使用道具增加技能点成功回应" );

            }
            else
            {
                Logger.Error( this.agent._account + " 使用道具增加技能点返回失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        //使用vip体验卡
        void sendTempVip( FunctionListenerEvent sListener )
        {
            C2RM_USE_TEMP_VIP sender = new C2RM_USE_TEMP_VIP();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            sender.propId = 10061;
            this.agent.send( sender );
        }

        ///使用vip体验卡回应
        public void recvTempVip( ArgsEvent args )
        {
            RM2C_USE_TEMP_VIP recv = args.getData<RM2C_USE_TEMP_VIP>();

            if( 1 == recv.iResult )
            {
                Logger.Info( this.agent._account + "使用vip体验卡成功回应" );

            }
            else
            {
                Logger.Error( this.agent._account + " 使用vip体验卡返回失败 " + recv.iResult );

            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 购买副本次数 request
        public void sendBuyFBCnt( int idCsvFb, FunctionListenerEvent sListener )
        {
            C2RM_FB_RESET sender = new C2RM_FB_RESET();
            sender.uiFbCsvId = ( uint ) idCsvFb;
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        //response 购买副本次数 
        public void recvBuyFBCnt( ArgsEvent args )
        {
            RM2C_FB_RESET recv = args.getData<RM2C_FB_RESET>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 购买副本成功 " + recv.sFb.uiIdCsvFB );
                this.agent.dataMode.myPlayer.money_game += recv.sMoney.iSMoney;
                this.agent.dataMode.myPlayer.money += recv.sMoney.iQMoney;

                InfoFB _fb = this.agent.dataMode.getFB( recv.sFb.luiIdFB );
                _fb.score = ( int ) recv.sFb.cLvKo;
                _fb.comTimes = ( int ) recv.sFb.sKoTodayTimes;
                _fb.resetFBCnt = ( int ) recv.sFb.cResetTimes;
                _fb.idCsvFB = ( int ) recv.sFb.uiIdCsvFB;
            }
            else
            {
                Logger.Error( this.agent._account + " 购买副本失败 " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        //C2RM_GET_SMONEY_SHOP
        public void sendGetSMoneyShop( FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " 发送金币商店信息获取命令" );
            C2RM_GET_SMONEY_SHOP sender = new C2RM_GET_SMONEY_SHOP();
            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        ///获取金币商店回复
        public void recvReplyMoneyShop( ArgsEvent args )
        {
            RM2C_GET_SMONEY_SHOP recv = args.getData<RM2C_GET_SMONEY_SHOP>();
            //this.agent.dataMode.myPlayer.infoMoneyShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;

            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 金币商店信息获取成功" );

                this.agent.dataMode.myPlayer.infoMoneyShop.timesReset = recv.iCntRefresh;

                int count = recv.m_vctShopGoodsp.Length;
                this.agent.dataMode.myPlayer.infoMoneyShop.sells.Clear();

                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                        obj.isSell = false;
                    else
                        obj.isSell = true;
                    this.agent.dataMode.myPlayer.infoMoneyShop.sells.Add( obj );
                }
            }
            else
            {
                Logger.Error( this.agent._account + " 金币商店信息获取失败  " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //刷新金币商店
        public void sendRefreshMoneyShop( int type, FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_REFRESH_SMONEY_SHOP >> " + "刷新金币商店 >> " );
            C2RM_REFRESH_SMONEY_SHOP sender = new C2RM_REFRESH_SMONEY_SHOP();
            sender.cType = ( byte ) type;
            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        ///金币商店购买
        public void sendBuyMoneyShop( int index, FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_SMONEY_SHOP_BUY >> " + "购买金币商店 >> " + index );
            C2RM_SMONEY_SHOP_BUY sender = new C2RM_SMONEY_SHOP_BUY();
            sender.iLoc = index;
            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        ///刷新金币商店回复
        public void recvRefreshMoneyShop( ArgsEvent args )
        {
            RM2C_REFRESH_SMONEY_SHOP recv = args.getData<RM2C_REFRESH_SMONEY_SHOP>();
            //this.agent.dataMode.myPlayer.infoMoneyShop.infoCD.timeTeamp = ( double ) recv.uiRefreshTime;
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 金币商店刷新成功" );

                this.agent.dataMode.myPlayer.infoMoneyShop.timesReset = recv.iCntRefresh;

                this.agent.dataMode.myPlayer.money += recv.iCost;

                int count = recv.m_vctShopGoodsp.Length;
                this.agent.dataMode.myPlayer.infoMoneyShop.sells.Clear();

                for( int i = 0; i < count; i++ )
                {
                    InfoShopObject obj = new InfoShopObject();
                    obj.index = i;
                    obj.idCsvShop = ( int ) recv.m_vctShopGoodsp[ i ].uiIdCsvGoods;
                    if( recv.m_vctShopGoodsp[ i ].cIsBuy == 0 )
                        obj.isSell = false;
                    else
                        obj.isSell = true;
                    this.agent.dataMode.myPlayer.infoMoneyShop.sells.Add( obj );
                }
            }
            else
            {
                Logger.Error( this.agent._account + " 金币商店刷新失败  " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///购买金币商店物品回复
        public void recvBuyMoneyShop( ArgsEvent args )
        {
            RM2C_SMONEY_SHOP_BUY recv = args.getData<RM2C_SMONEY_SHOP_BUY>();
            if( recv.iResult == 1 )
            {
                Logger.Info( this.agent._account + " 金币商店物品购买成功" );

                this.agent.dataMode.myPlayer.infoMoneyShop.sells[ recv.iLoc ].isSell = true;
                this.agent.dataMode.myPlayer.money_game += recv.iCost;

                if( recv.SPiece.uiCsvId > 0 )
                    this.agent.dataMode.infoHeroChip.setHeroChip( ( int ) recv.SPiece.uiCsvId, recv.SPiece.iCnt );

                //if( recv.SEquip.uiIdCsvEquipment > 0 )
                //{
                //    TypeCsvProp csvprop = ManagerCsv.getProp( ( int ) recv.SEquip.uiIdCsvEquipment );
                //    if( csvprop.isPropBeast() )
                //    {
                //        //this.agent.dataMode.myPlayer.infoPropBeastList.changeProp( ( int ) recv.SEquip.uiIdCsvEquipment, recv.SEquip.iCnt );
                //    }
                //    else
                //    {
                //        this.agent.dataMode.myPlayer.infoPropList.changeProp( ( int ) recv.SEquip.uiIdCsvEquipment, recv.SEquip.iCnt );
                //    }
                //}
                //TODO
            }
            else
            {
                Logger.Error( this.agent._account + " 金币商店物品购买失败  " + recv.iResult );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //副本扫荡
        public void sendFBSweep( uint fb_id, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " 副本扫荡  " + fb_id );

            C2RM_FB_SWEEP sender = new C2RM_FB_SWEEP();
            sender.uiFbId = fb_id;
            sender.uiListen = Dispatcher.addListener( sListener, null );

            this.agent.send( sender );

            //sendPingTwo();  ? why
        }

        //扫荡副本返回
        public void recvSweep( ArgsEvent args )
        {
            RM2C_FB_SWEEP recv = args.getData<RM2C_FB_SWEEP>();

            if( recv.iResult == 1 )
            {
                // updata to temp datamode
                this.agent.dataMode.infoFBRewardList.addMoneySweep( ( int ) recv.uiSMoney );
                this.agent.dataMode.infoFBRewardList.exp += ( ulong ) recv.uiExp;
                this.agent.dataMode.infoFBRewardList.expBegin = ( ulong ) recv.uiPreExp;
                this.agent.dataMode.infoFBRewardList.isDataOver = true;
                this.agent.dataMode.infoFBRewardList.curFbCsvID = ( int ) recv.uiFbCsvId;

                Logger.Info( this.agent._account + " 扫荡返回成功  SMoney: " + recv.uiSMoney + " Exp: " + recv.uiExp + " csvId: " + recv.uiFbCsvId );
            }
            else
            {
                Logger.Error( this.agent._account + " 副本扫荡错误  " + recv.iResult );
            }
            // dispatch
            Dispatcher.dispatchListener( recv.uiListen, ( object ) recv );
        }


        ///发送聊天信息
        public void sendChat( ChatInfo info, FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_CHAT >> " + "发送聊天信息 >> " );

            C2RM_CHAT sender = new C2RM_CHAT();
            sender.uiListen = Dispatcher.addListener( listener, null );
            sender.sChatItem.cType = info.Type;
            sender.sChatItem.setName( info.cName );
            sender.sChatItem.setContent( info.Content );
            sender.sChatItem.uiGangServerId = ( uint ) info.GangServerId;
            sender.sChatItem.uiSrcRoleId = ( uint ) info.RoleId;
            //sender.sChatItem.uiDstRoleId =(uint) info.IdMaster ;
            sender.sChatItem.uiTalkTime = ( uint ) info.TalkTime;
            sender.sChatItem.sProtectItem.uiProtectId = info.sProtectItem.ProtectId;
            sender.sChatItem.sProtectItem.cProtectType = ( byte ) info.sProtectItem.ProtectType;
            sender.sChatItem.sProtectItem.cProectType2 = ( byte ) info.sProtectItem.ProectType2;
            sender.sChatItem.usTeamLv = ( ushort ) info.teamLv;
            sender.sChatItem.uiTeamLeaderCsvID = ( uint ) info.teamLeaderCsvId;
            sender.sChatItem.ulChatID = ( ulong ) info.chatId;
            sender.sChatItem.uiDstRoleId = info.DstRoleId;
            sender.sChatItem.sProtectItem.uiProtectSessionId = info.sProtectItem.ProtectSessionId;
            sender.sChatItem.uiGangServerId = info.unionID;
            if( sender.sChatItem.cType == 3 )
                sender.sChatItem.setNameDst( info.cNameDst );

            if( sender.sChatItem.cType >= 1 && sender.sChatItem.cType <= 3 )
            {
                //if( DataMode.CanSpeak() )
                {
                    this.agent.send( sender );
                }
            }
            else
            {
                this.agent.send( sender );
            }
        }

        ///接收聊天信息
        public void recvChat( ArgsEvent args )
        {
            RM2C_CHAT recv = args.getData<RM2C_CHAT>();
            Logger.Info( this.agent._account + " RM2C_CHAT >> " + recv.iResult + " dst: " + recv.sChatItem.uiDstRoleId );
            if( recv.iResult == 1 )
            {
                if( recv.sChatItem.uiSrcRoleId == this.agent.dataMode.myPlayer.idServer )
                {
                    //this.agent.dataMode.ChatTimes++;
                }
                ChatInfo chatInfo = new ChatInfo();
                chatInfo.Type = recv.sChatItem.cType;
                //		chatInfo.cName = System.Text.Encoding.ASCII.GetString(recv.sChatItem.cName);
                chatInfo.cName = recv.sChatItem.GetName();
                //		chatInfo.Content =System.Text.Encoding.ASCII.GetString( recv.sChatItem.cContent);
                chatInfo.Content = recv.sChatItem.GetContent();
                chatInfo.GangServerId = ( int ) recv.sChatItem.uiGangServerId;
                chatInfo.RoleId = recv.sChatItem.uiSrcRoleId;
                chatInfo.IdMaster = ( int ) recv.sChatItem.uiDstRoleId;
                chatInfo.TalkTime = ( int ) recv.sChatItem.uiTalkTime;
                chatInfo.sProtectItem.ProtectId = recv.sChatItem.sProtectItem.uiProtectId;
                chatInfo.sProtectItem.ProtectType = ( int ) recv.sChatItem.sProtectItem.cProtectType;
                chatInfo.sProtectItem.ProectType2 = ( int ) recv.sChatItem.sProtectItem.cProectType2;
                chatInfo.chatId = ( ulong ) recv.sChatItem.ulChatID;
                chatInfo.teamLv = ( int ) recv.sChatItem.usTeamLv;
                chatInfo.teamLeaderCsvId = ( int ) recv.sChatItem.uiTeamLeaderCsvID;
                chatInfo.DstRoleId = recv.sChatItem.uiDstRoleId;
                chatInfo.unionID = recv.sChatItem.uiGangServerId;
                chatInfo.cNameDst = recv.sChatItem.GetNameDst();
                chatInfo.sProtectItem.ProtectSessionId = ( ulong ) recv.sChatItem.sProtectItem.uiProtectSessionId;
                this.agent.dataMode.myPlayer.money += ( long ) recv.iQMoney;
                if( chatInfo.Type == 1 )
                {
                    //DataMode.setWorldChatInfo( chatInfo );
                    //( ( ChatWindow ) WindowsMngr.getInstance().getWindow( WindowsID.CHAT ) ).insertChat( chatInfo );
                }
                else if( chatInfo.Type == 2 )
                {
                    //DataMode.setUnionChatInfo( chatInfo );
                    //( ( ChatWindow ) WindowsMngr.getInstance().getWindow( WindowsID.CHAT ) ).insertChat( chatInfo );
                }
                else if( chatInfo.Type == 3 )
                {
                    //DataMode.setPrivateChatInfo( chatInfo );
                    //( ( ChatWindow ) WindowsMngr.getInstance().getWindow( WindowsID.CHAT ) ).insertChat( chatInfo );
                }
                //else if( ( chatInfo.Type == 5 || chatInfo.Type == 6 || chatInfo.Type == 4 ) && DataMode.hasSystem( InfoSystem.CARDTEAM ) )
                //{
                //DataMode.setTradeChatInfo( chatInfo );
                //( ( ChatWindow ) WindowsMngr.getInstance().getWindow( WindowsID.CHAT ) ).insertChat( chatInfo );
                //}

            }
            //Dispatcher.dispatchListener( recv.uiListen, recv );

            if( recv.sChatItem.uiSrcRoleId == this.agent.dataMode.myPlayer.idServer )  //如果消息是自己发送的 
            {
                Dispatcher.dispatchListener( recv.uiListen, recv );
            }
        }

        /// 接收最近聊天信息
        public void recvChatRencent( ArgsEvent args )
        {
            RM2C_CHAT_RECENT_RESPONSE recv = args.getData<RM2C_CHAT_RECENT_RESPONSE>();
            Logger.Info( this.agent._account + " RM2C_CHAT_RECENT_RESPONSE >> " + recv.iResult );
            //存储最近的聊天信息
            if( recv.iResult == 1 )
            {
                int count = recv.sChatItem.Length;
                for( int i = 0; i < count; i++ )
                {
                    if( recv.sChatItem[ i ].ulChatID == 0 )
                        continue;
                    ChatInfo chatInfo = new ChatInfo();
                    chatInfo.Type = recv.sChatItem[ i ].cType;
                    chatInfo.cName = recv.sChatItem[ i ].GetName();
                    chatInfo.cNameDst = recv.sChatItem[ i ].GetNameDst();
                    chatInfo.Content = recv.sChatItem[ i ].GetContent();
                    chatInfo.GangServerId = ( int ) recv.sChatItem[ i ].uiGangServerId;
                    chatInfo.RoleId = recv.sChatItem[ i ].uiSrcRoleId;
                    chatInfo.IdMaster = ( int ) recv.sChatItem[ i ].uiDstRoleId;
                    chatInfo.DstRoleId = recv.sChatItem[ i ].uiDstRoleId;
                    chatInfo.TalkTime = ( int ) recv.sChatItem[ i ].uiTalkTime;
                    chatInfo.sProtectItem.ProtectId = recv.sChatItem[ i ].sProtectItem.uiProtectId;
                    chatInfo.sProtectItem.ProtectType = ( int ) recv.sChatItem[ i ].sProtectItem.cProtectType;
                    chatInfo.sProtectItem.ProectType2 = ( int ) recv.sChatItem[ i ].sProtectItem.cProectType2;
                    chatInfo.chatId = ( ulong ) recv.sChatItem[ i ].ulChatID;
                    chatInfo.teamLv = ( int ) recv.sChatItem[ i ].usTeamLv;
                    chatInfo.teamLeaderCsvId = ( int ) recv.sChatItem[ i ].uiTeamLeaderCsvID;
                    chatInfo.sProtectItem.ProtectSessionId = ( ulong ) recv.sChatItem[ i ].sProtectItem.uiProtectSessionId;
                    if( chatInfo.Type == 1 )
                    {
                        //this.dataMode.setWorldChatInfo( chatInfo );
                    }
                    else if( chatInfo.Type == 2 )
                    {
                        //DataMode.setUnionChatInfo( chatInfo );
                        //( ( ChatWindow ) WindowsMngr.getInstance().getWindow( WindowsID.CHAT ) ).insertChat( chatInfo );
                    }
                    else if( chatInfo.Type == 3 )
                    {
                        //DataMode.setPrivateChatInfo( chatInfo );
                        //( ( ChatWindow ) WindowsMngr.getInstance().getWindow( WindowsID.CHAT ) ).insertChat( chatInfo );
                    }
                    //else if( ( chatInfo.Type == 5 || chatInfo.Type == 6 || chatInfo.Type == 4 ) && DataMode.hasSystem( InfoSystem.CARDTEAM ) )
                    //{
                    //DataMode.setTradeChatInfo( chatInfo );
                    //}

                }
                //this.agent.dataMode.ChatTimes = ( int ) recv.uiChatCnt;
            }
            //Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// pk 请战
        public void sendPKCombat( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_MATE_PK  >> " + "pk 请战" );
            C2RM_MATE_PK sender = new C2RM_MATE_PK();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// pk 请求竞技场匹配回复（此协议包含对手战斗信息）
        public void recvPKCombat( ArgsEvent args )
        {
            RM2C_MATE_PK recv = args.getData<RM2C_MATE_PK>();
            Logger.Info( "RECV:RM2C_MATE_PK >> " + recv.iResult );
            if( recv.iResult == 1 )
            {
                this.agent.dataMode.myPlayer.enemyId = recv.uiIdEnemy;

                ///// 更新自己信息
                this.agent.dataMode.myPlayer.infoPK.cnt = ( int ) recv.usCntPk;
                ///// 更新别人信息
                ////if( null == this.agent.dataMode.getPlayer( recv.uiIdEnemy ) )
                ////    _serverPlayer.Add( recv.uiIdEnemy, new InfoPlayer() );
                //InfoPlayer player = DataMode.getPlayer( recv.uiIdEnemy );
                ///// 设置索引
                //player.idServer = recv.uiIdEnemy;
                //player.name = recv.getEnemyName();
                ////recv.usLv = ( ushort ) Mathf.Min( recv.usLv, 60 );
                //player.exp = 0;
                //for( int i = 1; i <= recv.usLv; i++ )
                //{
                //    player.exp += ManagerCsv.getHeroLv( i ).expPlayer;
                //}
                //player.exp = player.exp - ( ulong ) ( player.exp == 0 ? 0 : 1 );
                ///// 设置我的pk队伍
                ////player.infoHeroList.idTeamSelectPK = recv.STeamInfo.uiIdTeam;
                //player.infoNobility.lv = ( int ) recv.cNobility;
                ///// 设置角色信息
                //for( int index = 0; index < recv.vctPetInfo.Length; index++ )
                //{
                //    if( recv.vctPetInfo[ index ].uiIdPet == 0 )
                //        continue;
                //    /// 创建卡片
                //    if( null == this.agent.dataMode.getHero( recv.vctPetInfo[ index ].uiIdPet ) )
                //        this.agent.dataMode._serverHero.Add( recv.vctPetInfo[ index ].uiIdPet, new InfoHero() );

                //    player.infoHeroList.addHero( recv.vctPetInfo[ index ].uiIdPet );
                //    /// 设计角色基本信息
                //    InfoHero hero = this.agent.dataMode.getHero( recv.vctPetInfo[ index ].uiIdPet );
                //    hero.exp = recv.vctPetInfo[ index ].luiExp;
                //    hero.idCsv = ( int ) recv.vctPetInfo[ index ].uiIdCsvPet;
                //    hero.idServer = recv.vctPetInfo[ index ].uiIdPet;
                //    //				hero.addNumber = recv.vctPetInfo[index].sAddNum;
                //    hero.star = ( int ) recv.vctPetInfo[ index ].uiStar;
                //    hero.infoStone.setStone( ( ulong ) recv.vctPetInfo[ index ].uiStone );
                //    TypeCsvHero csvHero = ManagerCsv.getHero( hero.idCsv );
                //    /// 设置技能信息
                //    hero.infoSkill.clear();
                //    for( int i = 0; i < csvHero.idSkill.Length; i++ )
                //    {
                //        InfoSkill infoSkill = new InfoSkill();
                //        if( recv.vctPetInfo[ index ].vctUiIdCsvSkill[ i ] == 0 )
                //        {
                //            //infoSkill.idCsv = ( csvHero.idSkill[ i ] );
                //        }
                //        if( recv.vctPetInfo[ index ].vctUiIdCsvSkill[ i ] != 0 )
                //        {
                //            infoSkill.idCsv = ( int ) recv.vctPetInfo[ index ].vctUiIdCsvSkill[ i ];
                //            infoSkill.isActive = true;
                //        }
                //        hero.infoSkill.skillSort.Add( infoSkill.idCsv );
                //        hero.infoSkill.setInfoSkill( infoSkill );
                //    }
                //    /// 设置释放的技能
                //    hero.infoSkill.setSkillRelease( ( int ) recv.vctPetInfo[ index ].uiIdCsvHandSkill );
                //    hero.infoEquip.clear();
                //    for( int i = 0; i < recv.vctPetInfo[ index ].vctLuiIdEquip.Length; i++ )
                //    {
                //        hero.infoEquip.setProp( i, ( int ) recv.vctPetInfo[ index ].vctLuiIdEquip[ i ] );
                //    }
                //}

                //player.infoPK.rank = ( int ) recv.uiRankEnemy;
                //if( !player.infoHeroList.teamInfo.ContainsKey( recv.STeamInfo.uiIdTeam ) )
                //    player.infoHeroList.teamInfo.Add( recv.STeamInfo.uiIdTeam, new ulong[ 5 ] { 0, 0, 0, 0, 0 } );

                ///// 清除团队信息
                //player.infoHeroList.clearTeam( ( int ) recv.STeamInfo.uiIdTeam );
                ///// 我的队长
                //InfoHero hero2 = DataMode.getHero( recv.STeamInfo.uiIdLead );
                //hero2.isInTeam = true;
                //player.infoHeroList.teamInfo[ recv.STeamInfo.uiIdTeam ][ 0 ] = hero2.idServer;
                //int indexTeam = 1;
                ///// 录入团队信息
                //for( int i = 0; i < recv.STeamInfo.vctUiIdPet.Length; i++ )
                //{
                //    hero2 = DataMode.getHero( recv.STeamInfo.vctUiIdPet[ i ] );
                //    if( null == hero2 )
                //        continue;
                //    player.infoHeroList.teamInfo[ recv.STeamInfo.uiIdTeam ][ indexTeam ] = hero2.idServer;
                //    indexTeam++;
                //}
                ///// 我的魂兽 不为空
                //if( recv.STeamInfo.luiIdBeast > 0 )
                //{
                //    InfoBeast infoBeast = new InfoBeast();
                //    if( !_serverBeast.ContainsKey( recv.STeamInfo.luiIdBeast ) )
                //        _serverBeast.Add( recv.STeamInfo.luiIdBeast, new InfoBeast() );
                //    _serverBeast[ recv.STeamInfo.luiIdBeast ].idServer = recv.STeamInfo.luiIdBeast;
                //    _serverBeast[ recv.STeamInfo.luiIdBeast ].exp = recv.SBeastInro.luiExp;
                //    _serverBeast[ recv.STeamInfo.luiIdBeast ].idCsv = ( int ) recv.SBeastInro.uiIdCsvPet;
                //    _serverBeast[ recv.STeamInfo.luiIdBeast ].infoEquip.clear();
                //    for( int index = 0; index < recv.SBeastInro.vctLuiIdEquip.Length; index++ )
                //    {
                //        _serverBeast[ recv.STeamInfo.luiIdBeast ].infoEquip.setProp( index, ( int ) recv.SBeastInro.vctLuiIdEquip[ index ] );
                //    }
                //}
                //player.infoHeroList.setTeamBeast( recv.STeamInfo.uiIdTeam, recv.STeamInfo.luiIdBeast );
                ///// 开始我的pk之旅吧
                ////			ManagerSencePK.start(player.idServer);
                 
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        ///获取队伍信息
        public void sendHeroUpdateTeam( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND>>C2RM_TEAM_INFO" );
            C2RM_TEAM_INFO sender = new C2RM_TEAM_INFO();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        ///获取队伍信息
        public void recvHeroTeam( ArgsEvent args )
        {
            RM2C_TEAM_INFO recv = args.getData<RM2C_TEAM_INFO>();
            Logger.Info( this.agent._account + " RECV:RM2C_TEAM_INFO " + recv.iResult );
            if( recv.iResult == 1 )
            {
                /// 获得主角
                InfoPlayer player = this.agent.dataMode.getPlayer( recv.uiMasterId );
                /// 清除队伍信息
                player.infoHeroList.teamList.Clear();

                for( int index = 0; index < recv.vctSTeamInfo.Length; index++ )
                {
                    /// 新建队伍
                    if( !player.infoHeroList.teamInfo.ContainsKey( recv.vctSTeamInfo[ index ].uiIdTeam ) )
                        player.infoHeroList.teamInfo.Add( recv.vctSTeamInfo[ index ].uiIdTeam, new ulong[ 5 ] { 0, 0, 0, 0, 0 } );


                    /// 队伍 pk
                    if( recv.vctSTeamInfo[ index ].cLoc == 1 )
                        player.infoHeroList.idTeamSelectPK = recv.vctSTeamInfo[ index ].uiIdTeam;

                    /// 队伍 普通
                    if( recv.vctSTeamInfo[ index ].cLoc == 2 )
                        player.infoHeroList.idTeamSelect = recv.vctSTeamInfo[ index ].uiIdTeam;

                    /*
                    /// 队伍 爬塔
                    if( recv.vctSTeamInfo[ index ].cLoc == 7 )
                        player.infoHeroList.idTeamSelectTower = recv.vctSTeamInfo[ index ].uiIdTeam;
                    /// 队伍 远征
                    if( recv.vctSTeamInfo[ index ].cLoc == 12 )
                        player.infoHeroList.idTeamTBC = recv.vctSTeamInfo[ index ].uiIdTeam;
                    /// 队伍 海山 奖励关卡进攻
                    if( recv.vctSTeamInfo[ index ].cLoc == 14 )
                        player.infoHeroList.idTeamTBCReward = recv.vctSTeamInfo[ index ].uiIdTeam;
                    /// 队伍 护送 掠夺
                    if( recv.vctSTeamInfo[ index ].cLoc == 20 )
                        player.infoHeroList.idTeamEscortRob = recv.vctSTeamInfo[ index ].uiIdTeam;
                    */

                    /// 队伍插入禁区 WILL TESTING
                    while( player.infoHeroList.teamList.Count <= recv.vctSTeamInfo[ index ].cLoc )
                    {
                        player.infoHeroList.teamList.Add( 0 );
                    }
                    player.infoHeroList.teamList[ ( int ) recv.vctSTeamInfo[ index ].cLoc ] = recv.vctSTeamInfo[ index ].uiIdTeam;

                    ///// 清除团队信息
                    //player.infoHeroList.clearTeam( ( int ) recv.vctSTeamInfo[ index ].uiIdTeam );

                    ///// 我的队长
                    InfoHero hero = this.agent.dataMode.getHero( recv.vctSTeamInfo[ index ].uiIdLead );
                    if( null == hero )
                        continue;
                    player.infoHeroList.teamInfo[ recv.vctSTeamInfo[ index ].uiIdTeam ][ 0 ] = hero.idServer;
                    hero.isInTeam = true;
                    int indexTeam = 1;
                    ///// 录入团队信息
                    for( int i = 0; i < recv.vctSTeamInfo[ index ].vctUiIdPet.Length; i++ )
                    {
                        hero = this.agent.dataMode.getHero( recv.vctSTeamInfo[ index ].vctUiIdPet[ i ] );
                        if( null == hero )
                            continue;
                        player.infoHeroList.teamInfo[ recv.vctSTeamInfo[ index ].uiIdTeam ][ indexTeam ] = hero.idServer;
                        hero.isInTeam = true;
                        indexTeam++;
                    }
                    //player.infoHeroList.setTeamBeast( recv.vctSTeamInfo[ index ].uiIdTeam, recv.vctSTeamInfo[ index ].luiIdBeast );
                }
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //发送消息更新队伍信息
        public void sendNewTeamInfo( uint _teamId, ulong[] _memberId, FunctionListenerEvent _sListener )
        {
            C2RM_TEAM_SET sender = new C2RM_TEAM_SET();
            sender.uiListen = Dispatcher.addListener( _sListener, null );

            sender.sTeam.uiIdTeam = _teamId;
            sender.sTeam.uiIdLead = _memberId[ 0 ];

            sender.sTeam.uiIdPet = new ulong[ 4 ];
            for( ulong i = 1; i < 5; i++ )
            {
                sender.sTeam.uiIdPet[ i - 1 ] = _memberId[ i ];
            }

            this.agent.send( sender );
        }

        /// 角色 设定阵形
        public void recvHeroTeamChange( ArgsEvent args )
        {
            RM2C_TEAM_SET recv = args.getData<RM2C_TEAM_SET>();
            Logger.Info( this.agent._account + "  RECV:RM2C_TEAM_SET >> " + recv.iResult + "角色 阵法改变" );

            if( 1 == recv.iResult )
            {
                if( !this.agent.dataMode.myPlayer.infoHeroList.teamInfo.ContainsKey( recv.sTeam.uiIdTeam ) )
                    this.agent.dataMode.myPlayer.infoHeroList.teamInfo.Add( recv.sTeam.uiIdTeam, new ulong[ 5 ] { 0, 0, 0, 0, 0 } );

                /// 清除团队信息
                //this.agent.dataMode.myPlayer.infoHeroList.clearTeam( ( int ) recv.sTeam.uiIdTeam );
                /// 我的队长
                InfoHero hero = this.agent.dataMode.getHero( recv.sTeam.uiIdLead );

                if( null != hero )
                {
                    this.agent.dataMode.myPlayer.infoHeroList.teamInfo[ recv.sTeam.uiIdTeam ][ 0 ] = hero.idServer;
                    hero.isInTeam = true;
                    hero.isTeamLeader = true;
                }
                int indexTeam = 1;
                /// 录入团队信息
                for( int i = 0; i < recv.sTeam.uiIdPet.Length; i++ )
                {
                    hero = this.agent.dataMode.getHero( recv.sTeam.uiIdPet[ i ] );
                    if( null == hero )
                        continue;
                    hero.isInTeam = true;
                    this.agent.dataMode.myPlayer.infoHeroList.teamInfo[ recv.sTeam.uiIdTeam ][ indexTeam ] = hero.idServer;
                    indexTeam++;
                }
                //this.agent.dataMode.myPlayer.infoHeroList.setTeamBeast( recv.sTeam.uiIdTeam, recv.sTeam.luiIdBeast );
                //UtilListener.dispatch( "HERO_TEAM_SETTING_EVENT" );
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        public void sendFBSetFlag( int land_id )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_FB_SET_FLAG  land_id:" + land_id );

            C2RM_FB_SET_FLAG sender = new C2RM_FB_SET_FLAG();
            sender.iFbFlag = land_id;
            this.agent.send( sender );
        }

        /// 进入副本
        public void sendFBIn( int idCsvFB, FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_GOTO_FB " + "进入副本" + idCsvFB );

            C2RM_GOTO_FB sender = new C2RM_GOTO_FB();
            //List<InfoHero> infoHeroArr = this.agent.dataMode.myPlayer.infoHeroList.getTeam();
            List<ulong> infoHeroArr = this.agent.dataMode.myPlayer.infoHeroList.getHeroList();
            if( infoHeroArr.Count == 0 )
            {
                Logger.Error( this.agent._account + " heroList is empty" );
                return;
            }

            ulong[] standInfo = new ulong[ 9 ];

            standInfo[ 0 ] = infoHeroArr[ 0 ];
            //foreach( InfoHero infoHero in infoHeroArr )
            //{
            //    standInfo[ infoHero.standIndex ] = infoHero.idServer;
            //}
            sender.vctIdServerPet = standInfo;
            sender.uiCsvFBid = ( uint ) idCsvFB;

            sender.uiMasterid = 0; //this.agent.dataMode.myPlayer.idServer;
            sender.uiFriendPetid = 0;

            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 副本 进入
        public void recvFBIn( ArgsEvent args )
        {
            RM2C_GOTO_FB recv = args.getData<RM2C_GOTO_FB>();
            Logger.Info( this.agent._account + " RECV:RM2C_GOTO_FB >> " + recv.iResult + "/" + recv.uiIdCsvFB );

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        public void sendFBCombatReward( FunctionListenerEvent sListener )
        {
            Logger.Info(this.agent._account + " SEND:C2RM_FB_PK_BEGIN >> " + "领取一次奖励" );
            C2RM_FB_PK_BEGIN sender = new C2RM_FB_PK_BEGIN();

            sender.uiListen = Dispatcher.addListener( sListener ,null);

            this.agent.send( sender );
        }

        /// 副本 战斗奖励返回
        public void recvFBCombatReward( ArgsEvent args )
        {
            RM2C_FB_PK_BEGIN recv = args.getData<RM2C_FB_PK_BEGIN>();
            Logger.Info(this.agent._account + " RECV:RM2C_FB_PK_BEGIN >> " + recv.iResult );

            /// 抛出侦听
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 离开副本
        public void sendFBOut( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_LEAVE_FB " + "退出副本" );
            C2RM_LEAVE_FB sender = new C2RM_LEAVE_FB();
            sender.uiListen = Dispatcher.addListener( sListener, null );
            this.agent.send( sender );
        }

        /// 副本 离开
        public void recvFBOut( ArgsEvent args )
        {
            RM2C_LEAVE_FB recv = args.getData<RM2C_LEAVE_FB>();
            Logger.Info(this.agent._account + " RECV:RM2C_LEAVE_FB " + recv.iResult + "/" + recv.usIdTown );
            
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 副本 战斗数据开始
        public void sendFBCombatStart()
        {
            Logger.Info(this.agent._account + " SEND:C2RM_CHECK_FB_PK_BEGIN >> " + "战斗数据 Begin" );
            C2RM_CHECK_FB_PK_BEGIN sender = new C2RM_CHECK_FB_PK_BEGIN();
            sender.uiBeginTime = ( uint ) 0;
            this.agent.send( sender );
        }

        /// 副本 战斗数据结束
        public void sendFBCombatEnd( bool isWin, FunctionListenerEvent listener )
        {
            Logger.Info(this.agent._account + " SEND:C2RM_CHECK_FB_PK_OVER >> " + "战斗数据 End >> " + isWin );
            C2RM_CHECK_FB_PK_OVER sender = new C2RM_CHECK_FB_PK_OVER();

            if( isWin )
            {
                //int cnt = 0;
                //for( int index = 0; index < 9; index++ )
                //{
                //    if( index >= AICombat._item.Length )
                //        continue;
                //    if( null == AICombat._item[ index ] )
                //        continue;
                //    if( AICombat._item[ index ].isDie() )
                //        continue;
                //    cnt++;
                //}
                //int cntTotal = DataMode.myPlayer.infoHeroList.getTeam().Count;
                //if( null != ManagerSenceFB.infoHeroFriend )
                //    cntTotal += 1;
                //sender.iCntDead = cntTotal - cnt;
                sender.iCntDead = 0;
            }
            sender.cIsWin = ( byte ) ( isWin ? 1 : 0 );
            sender.fStopTime = ( float ) 0;
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.agent.send( sender );
        }

        /// 副本 验证回复
        public void recvFBCombatCheck( ArgsEvent args )
        {
            RM2C_CHECK_FB_PK recv = args.getData<RM2C_CHECK_FB_PK>();
            Logger.Info(this.agent._account + " RECV:RM2C_CHECK_FB_PK    " + recv.iResult);
            /// 如果成功
            if( recv.iResult != 1 )
            {
                Logger.Error(this.agent._account + " 战斗逻辑服务器判断出错" );
            }

            /// 抛出侦听
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 世界boss 战斗数据开始
        public void sendPKCombatStart()
        {
            Logger.Info(this.agent._account + " SEND:C2RM_CHECK_PK_BEGIN >> " + "pk 战斗数据 Begin" );
            C2RM_CHECK_PK_BEGIN sender = new C2RM_CHECK_PK_BEGIN();
            sender.uiBeginTime = ( uint ) 0;

            List<ulong> infoHeroArr = this.agent.dataMode.myPlayer.infoHeroList.getHeroList();
            
            if( infoHeroArr.Count == 0 )
            {
                Logger.Error( this.agent._account + " heroList is empty" );
                return;
            }

            //ulong[] standInfo = new ulong[ 9 ];

            //standInfo[ 1 ] = 270183000000005; //infoHeroArr[0];

            sender.vctIdServerPetSelf = new ulong[9];

            sender.vctIdServerPetEnemy = new ulong[9];

            this.agent.send( sender );
        }

        ///// 世界boss 战斗数据
        //public static void sendPKCombatData( List<TypeCombatData> combatDataArr, int sIndexData )
        //{
        //    if( null == combatDataArr )
        //        return;
        //    if( UtilLog.isBulidLog )
        //        UtilLog.Log( "SEND:C2RM_CHECK_PK_MID >> " + "pk 战斗数据 Data" + sIndexData + "/" + combatDataArr.Count );
        //    List<SFightingRound> dataArr = new List<SFightingRound>();
        //    C2RM_CHECK_PK_MID sender = new C2RM_CHECK_PK_MID();
        //    for( int index = 0; index < combatDataArr.Count; index++ )
        //    {
        //        /// 从索引开始向下
        //        SFightingRound data = new SFightingRound();
        //        data.m_beAckedState = combatDataArr[ index ].state;
        //        data.m_cAckPetLoc = ( byte ) combatDataArr[ index ].standIndexAttack;
        //        data.m_cHarmType = combatDataArr[ index ].atkHPType;
        //        data.m_cPetLoc = ( byte ) combatDataArr[ index ].standIndexBeaten;
        //        data.m_fTime = ( float ) combatDataArr[ index ].timeTeamp;
        //        data.m_iHarm = combatDataArr[ index ].atkHP;
        //        data.m_uiIdCsvSkill = ( uint ) combatDataArr[ index ].idCsv;
        //        data.m_usBatchSkill = ( ushort ) combatDataArr[ index ].indexRelBatch;
        //        data.m_usNumFight = ( ushort ) combatDataArr[ index ].indexRel;
        //        dataArr.Add( data );
        //    }
        //    sender.vctSReward = dataArr.ToArray();
        //    sender.iCnt = dataArr.Count;
        //    sender.cNum = ( byte ) sIndexData;
        //    AgentNet.getInstance().send( sender );
        //}

        /// 世界boss 战斗数据结束
        public void sendPKCombatEnd( bool isWin, FunctionListenerEvent listener )
        {
            Logger.Info(this.agent._account + " SEND:C2RM_CHECK_PK_OVER");
            C2RM_CHECK_PK_OVER sender = new C2RM_CHECK_PK_OVER();
            sender.cIsWin = ( byte ) ( isWin ? 1 : 0 );
            sender.fStopTime = ( float ) 0;
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.agent.send( sender );
        }

        /// pk 战斗验证回复
        public void recvPKCombatResult( ArgsEvent args )
        {
            RM2C_CHECK_PK recv = args.getData<RM2C_CHECK_PK>();
            Logger.Info(this.agent._account + " RECV:RM2C_CHECK_PK  " + recv.iResult);
            if( recv.iResult != 1 )
            {
                Logger.Error( this.agent._account + " RM2C_CHECK_PK Failed " + recv.iResult );
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 护送 搜索商队
        public void sendEscortFindRob( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_ESCORT_FIND_GROUP" );
            C2RM_ESCORT_FIND_GROUP sender = new C2RM_ESCORT_FIND_GROUP();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.agent.send( sender );
        }

        /// 护送 搜索商队
        public void recvEscortFindRob( ArgsEvent args )
        {
            RM2C_ESCORT_FIND_GROUP recv = args.getData<RM2C_ESCORT_FIND_GROUP>();
            Logger.Info(this.agent._account + " RECV:RM2C_ESCORT_FIND_GROUP   " + recv.iResult );
            
            if( recv.iResult == 1 )
            {

                /// 清空残血 残怒气数据
                //DataMode._serverEscortRobBeast.Clear();
                //DataMode._serverEscortRobHero.Clear();
                ///// 领取面包
                //DataMode.infoEscort.bread = ( int ) recv.EscortInfo.uiCntBread;
                //DataMode.infoEscort.breadTimeStamp = ( double ) recv.EscortInfo.uiBreadLessTime;
                //DataMode.infoEscort.breadBuyCnt = ( int ) recv.EscortInfo.uiCntBreadBuy;
                //DataMode.infoEscort.searchCount = ( int ) recv.EscortInfo.uiCntSearch;
                this.agent.dataMode.infoEscort.idServerEscortRob = recv.EscortInfo.uiIdEGF;
                this.agent.dataMode.infoEscort.idServerEscortSearch = recv.EscortInfo.uiIdEGS;

                /// 创建 商队
                if( !this.agent.dataMode._serverEscortSafe.ContainsKey( recv.EscortGroup.SInfoBase.luiIdEG ) )
                    this.agent.dataMode._serverEscortSafe.Add( recv.EscortGroup.SInfoBase.luiIdEG, new InfoEscortSafe() );

                InfoEscortSafe infoSafe = this.agent.dataMode._serverEscortSafe[ recv.EscortGroup.SInfoBase.luiIdEG ];
                infoSafe.idServer = recv.EscortGroup.SInfoBase.luiIdEG;
                infoSafe.idSession = recv.EscortGroup.SInfoBase.uiSessionId;
                infoSafe.timeStamp = ( double ) recv.EscortGroup.SInfoBase.uiBeginTime;
                infoSafe.type = recv.EscortGroup.SInfoBase.cType;
                infoSafe.typeSafe = ( int ) recv.EscortGroup.SInfoBase.uiEscortGroupType;
                /// 搜索时候扣钱
                

                infoSafe.idServerSafeTeams.Clear();
                for( int j = 0; j < recv.EscortGroup.vctTeamShow.Length; j++ )
                {
                    infoSafe.setSafeTeam( j, recv.EscortGroup.vctTeamShow[ j ].luiIdETid );

                    /// 创建队伍
                    if( recv.EscortGroup.vctTeamShow[ j ].luiIdETid <= 0 )
                        continue;

                    if( !this.agent.dataMode._serverEscortSafeTeam.ContainsKey( recv.EscortGroup.vctTeamShow[ j ].luiIdETid ) )
                        this.agent.dataMode._serverEscortSafeTeam.Add( recv.EscortGroup.vctTeamShow[ j ].luiIdETid, new InfoEscortSafeTeam() );
                    
                    InfoEscortSafeTeam infoSafeTeam = this.agent.dataMode._serverEscortSafeTeam[ recv.EscortGroup.vctTeamShow[ j ].luiIdETid ];
                    infoSafeTeam.idSession = recv.EscortGroup.vctTeamShow[ j ].uiSessionId;
                    infoSafeTeam.idServerTeam = recv.EscortGroup.vctTeamShow[ j ].luiIdETid;
                    infoSafeTeam.timeStamp = ( double ) recv.EscortGroup.vctTeamShow[ j ].uiBeginTime;
                    infoSafeTeam.lv = ( int ) recv.EscortGroup.vctTeamShow[ j ].usLv;
                    infoSafeTeam.fighting = recv.EscortGroup.vctTeamShow[ j ].uiFightValue;
                    infoSafeTeam.type = recv.EscortGroup.vctTeamShow[ j ].cType;
                    /// 服务器id
                    infoSafeTeam.idLoginServer = recv.EscortGroup.uiIdServer;
                    /// 数据类型
                    List<ulong> teamData = new List<ulong>();
                    teamData.Add( recv.EscortGroup.vctTeamShow[ j ].luiIdLeadServer );
                    infoSafeTeam.idServerTeamHeroList = teamData;


                    /// 创建 队长信息 信息
                    if( recv.EscortGroup.vctTeamShow[ j ].luiIdLeadServer <= 0 )
                        continue;
                    /// 跨服信息
                    ulong idServerHero = recv.EscortGroup.vctTeamShow[ j ].luiIdLeadServer;
                    //if( DataMode.idLoginServer != infoSafeTeam.idLoginServer )
                    //    idServerHero = DataMode.dummyEscortHero( idServerHero );

                    if( !this.agent.dataMode._serverHero.ContainsKey( idServerHero ) )
                        this.agent.dataMode._serverHero.Add( idServerHero, new InfoHero() );
                    InfoHero infoHero = this.agent.dataMode._serverHero[ idServerHero ];
                    infoHero.idServer = recv.EscortGroup.vctTeamShow[ j ].luiIdLeadServer;
                    infoHero.idCsv = ( int ) recv.EscortGroup.vctTeamShow[ j ].uiIdLead;
                }
            }
            
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 护送 当前正在进攻的 商队信息
        public void sendEscortDataRobInfo( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + "  SEND:C2RM_ESCORT_BEAT_GROUP" );
            
            C2RM_ESCORT_BEAT_GROUP sender = new C2RM_ESCORT_BEAT_GROUP();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.agent.send( sender );
        }

        /// 护送 获取正在进攻队伍的信息
        public void sendEscortDataRobTeam( FunctionListenerEvent sListener )
        {
            Logger.Info( this.agent._account + "  SEND:C2RM_ESCORT_BEAT_TEAM" );
            
            C2RM_ESCORT_BEAT_TEAM sender = new C2RM_ESCORT_BEAT_TEAM();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.agent.send( sender );
        }


        /// 护送 当前正在进攻的 商队信息
        public void recvEscortDataRobInfo( ArgsEvent args )
        {
            RM2C_ESCORT_BEAT_GROUP recv = args.getData<RM2C_ESCORT_BEAT_GROUP>();
            Logger.Info(this.agent._account + " RECV:RM2C_ESCORT_BEAT_GROUP 正在进攻的商队信息 " + recv.iResult );
            
            if( recv.iResult == 1 )
            {
                /// 创建 商队
                if( !this.agent.dataMode._serverEscortSafe.ContainsKey( recv.EscortInfo.SInfoBase.luiIdEG ) )
                    this.agent.dataMode._serverEscortSafe.Add( recv.EscortInfo.SInfoBase.luiIdEG, new InfoEscortSafe() );

                InfoEscortSafe infoSafe = this.agent.dataMode._serverEscortSafe[ recv.EscortInfo.SInfoBase.luiIdEG ];
                infoSafe.idServer = recv.EscortInfo.SInfoBase.luiIdEG;
                infoSafe.idSession = recv.EscortInfo.SInfoBase.uiSessionId;
                infoSafe.timeStamp = ( double ) recv.EscortInfo.SInfoBase.uiBeginTime;
                //DataMode.infoEscort.timeStampRob = ( double ) recv.EscortInfo.uiFightTime;
                //DataMode.infoEscort.timeStampSearch = ( double ) recv.EscortInfo.uiFindTime;

                infoSafe.type = recv.EscortInfo.SInfoBase.cType;
                infoSafe.typeSafe = ( int ) recv.EscortInfo.SInfoBase.uiEscortGroupType;
                infoSafe.idServerSafeTeams.Clear();


                for( int j = 0; j < recv.EscortInfo.vctTeamShow.Length; j++ )
                {
                    /// 创建队伍
                    infoSafe.setSafeTeam( j, recv.EscortInfo.vctTeamShow[ j ].luiIdETid );
                    if( recv.EscortInfo.vctTeamShow[ j ].luiIdETid <= 0 )
                        continue;
                    if( !this.agent.dataMode._serverEscortSafeTeam.ContainsKey( recv.EscortInfo.vctTeamShow[ j ].luiIdETid ) )
                        this.agent.dataMode._serverEscortSafeTeam.Add( recv.EscortInfo.vctTeamShow[ j ].luiIdETid, new InfoEscortSafeTeam() );

                    InfoEscortSafeTeam infoSafeTeam = this.agent.dataMode._serverEscortSafeTeam[ recv.EscortInfo.vctTeamShow[ j ].luiIdETid ];
                    infoSafeTeam.idSession = recv.EscortInfo.vctTeamShow[ j ].uiSessionId;
                    infoSafeTeam.idServerTeam = recv.EscortInfo.vctTeamShow[ j ].luiIdETid;
                    infoSafeTeam.timeStamp = ( double ) recv.EscortInfo.vctTeamShow[ j ].uiBeginTime;
                    infoSafeTeam.lv = ( int ) recv.EscortInfo.vctTeamShow[ j ].usLv;
                    infoSafeTeam.fighting = recv.EscortInfo.vctTeamShow[ j ].uiFightValue;
                    infoSafeTeam.type = recv.EscortInfo.vctTeamShow[ j ].cType;
                    /// 服务器id
                    infoSafeTeam.idLoginServer = recv.EscortInfo.uiIdServer;
                    /// 数据类型
                    List<ulong> teamData = new List<ulong>();
                    teamData.Add( recv.EscortInfo.vctTeamShow[ j ].luiIdLeadServer );
                    infoSafeTeam.idServerTeamHeroList = teamData;


                    /// 创建 队长信息 信息
                    if( recv.EscortInfo.vctTeamShow[ j ].luiIdLeadServer <= 0 )
                        continue;

                    /// 跨服信息
                    ulong idServerHero = recv.EscortInfo.vctTeamShow[ j ].luiIdLeadServer;
                    //if( DataMode.idLoginServer != infoSafeTeam.idLoginServer )
                    //    idServerHero = DataMode.dummyEscortHero( idServerHero );

                    if( !this.agent.dataMode._serverHero.ContainsKey( idServerHero ) )
                        this.agent.dataMode._serverHero.Add( idServerHero, new InfoHero() );
                    InfoHero infoHero = this.agent.dataMode._serverHero[ idServerHero ];
                    infoHero.idServer = recv.EscortInfo.vctTeamShow[ j ].luiIdLeadServer;
                    infoHero.idCsv = ( int ) recv.EscortInfo.vctTeamShow[ j ].uiIdLead;
                }
            }
            
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 护送 获取正在进攻队伍的信息
        public void recvEscortDataRobTeam( ArgsEvent args )
        {
            RM2C_ESCORT_BEAT_TEAM recv = args.getData<RM2C_ESCORT_BEAT_TEAM>();
            Logger.Info(this.agent._account + " RECV:RM2C_ESCORT_BEAT_TEAM 正在进攻的商队信息 " + recv.iResult );
            
            if( recv.iResult == 1 )
            {
                for( int i = 0; i < recv.vctInfo.Length; i++ )
                {
                    if( !this.agent.dataMode._serverEscortSafeTeam.ContainsKey( recv.vctInfo[ i ].luiIdET ) )
                        this.agent.dataMode._serverEscortSafeTeam.Add( recv.vctInfo[ i ].luiIdET, new InfoEscortSafeTeam() );

                    InfoEscortSafeTeam infoSafeTeam = this.agent.dataMode._serverEscortSafeTeam[ recv.vctInfo[ i ].luiIdET ];
                    /// 无sessionID
                    infoSafeTeam.idServerTeam = recv.vctInfo[ i ].luiIdET;
                    infoSafeTeam.idServerPlayer = recv.vctInfo[ i ].uiIdMaster;
                    infoSafeTeam.idLoginServer = recv.vctInfo[ i ].uiIdServer;
                    infoSafeTeam.name = recv.vctInfo[ i ].GetName();
                    infoSafeTeam.idServerBeast = recv.vctInfo[ i ].SBeast.uiIdBeast;

                    /// 赋值队伍
                    List<ulong> teamData = new List<ulong>();
                    List<SEscortTeamPet> VctSPet = new List<SEscortTeamPet>();
                    teamData.Add( recv.vctInfo[ i ].sLead.uiIdPet );
                    VctSPet.Add( recv.vctInfo[ i ].sLead );
                    for( int j = 0; j < recv.vctInfo[ i ].VctSPet.Length; j++ )
                    {
                        teamData.Add( recv.vctInfo[ i ].VctSPet[ j ].uiIdPet );
                        VctSPet.Add( recv.vctInfo[ i ].VctSPet[ j ] );
                    }
                    infoSafeTeam.idServerTeamHeroList = teamData;


                    /// 填充 队伍 信息
                    {
                        for( int j = 0; j < VctSPet.Count; j++ )
                        {
                            if( VctSPet[ j ].uiIdPet <= 0 )
                                continue;
                            /// 跨服考虑
                            ulong idServerHero = VctSPet[ j ].uiIdPet;
                            //if( DataMode.idLoginServer != infoSafeTeam.idLoginServer )
                            //    idServerHero = DataMode.dummyEscortHero( idServerHero );
                            if( !this.agent.dataMode._serverHero.ContainsKey( idServerHero ) )
                                this.agent.dataMode._serverHero.Add( idServerHero, new InfoHero() );

                            InfoHero infoHero = this.agent.dataMode._serverHero[ idServerHero ];
                            infoHero.idCsv = ( int ) VctSPet[ j ].uiIdCsvPet;
                            infoHero.idServer = VctSPet[ j ].uiIdPet;
                            infoHero.exp = VctSPet[ j ].luiExp;

                            TypeCsvHero csvHero = ManagerCsv.getHero( infoHero.idCsv );
                            /// 设置装备
                            infoHero.infoEquip.clear();
                            for( int k = 0; k < VctSPet[ j ].vctluiIdEquip.Length; k++ )
                            {
                                infoHero.infoEquip.setProp( k, ( int ) VctSPet[ j ].vctluiIdEquip[ k ] );
                            }
                            /// 设置技能信息
                            infoHero.infoSkill.clear();
                            if( null != csvHero.idSkill )
                            {
                                for( int k = 0; k < csvHero.idSkill.Length; k++ )
                                {
                                    InfoSkill infoSkill = new InfoSkill();
                                    if( VctSPet[ j ].sMotSkill[ k ].uiIdSkill == 0 )
                                    {
                                        infoSkill.idCsv = ( int ) double.Parse( csvHero.idSkill[ k ].ToString() );//GMath.toInt( csvHero.idSkill[ k ] );
                                    }
                                    if( VctSPet[ j ].sMotSkill[ k ].uiIdSkill != 0 )
                                    {
                                        infoSkill.idCsv = ( int ) VctSPet[ j ].sMotSkill[ k ].uiIdSkill;
                                        infoSkill.isActive = true;
                                    }
                                    infoHero.infoSkill.skillSort.Add( infoSkill.idCsv );
                                    infoHero.infoSkill.setInfoSkill( infoSkill );
                                }
                            }
                            /// 设置手动技能
                            infoHero.infoSkill.setSkillRelease( ( int ) VctSPet[ j ].uiIdCsvSkillHand );

                            
                        }
                    }

                    /// 获取这个商队 魂兽 信息
                    //if( recv.vctInfo[ i ].SBeast.uiIdBeast > 0 )
                    //{
                    //    /// 跨服考虑
                    //    ulong idServerBeast = recv.vctInfo[ i ].SBeast.uiIdBeast;

                    //    //if( DataMode.idLoginServer != infoSafeTeam.idLoginServer )
                    //    //    idServerBeast = DataMode.dummyEscortBeast( idServerBeast );

                    //    //if( !_serverBeast.ContainsKey( idServerBeast ) )
                    //    //    _serverBeast.Add( idServerBeast, new InfoBeast() );

                    //    //InfoBeast infoBeast = _serverBeast[ idServerBeast ];
                    //    //infoBeast.idServer = recv.vctInfo[ i ].SBeast.uiIdBeast;
                    //    //infoBeast.idCsv = ( int ) recv.vctInfo[ i ].SBeast.uiIdCsvBeast;
                    //    //infoBeast.exp = recv.vctInfo[ i ].SBeast.luiExp;
                    //    //infoBeast.infoEquip.clear();
                    //    //for( int j = 0; j < recv.vctInfo[ i ].SBeast.vctluiIdEquip.Length; j++ )
                    //    //{
                    //    //    infoBeast.infoEquip.setProp( j, ( int ) recv.vctInfo[ i ].SBeast.vctluiIdEquip[ j ] );
                    //    //}
                    //    ///// 设置抢矿属性 怒气
                    //    //if( !_serverEscortRobBeast.ContainsKey( idServerBeast ) )
                    //    //    _serverEscortRobBeast.Add( idServerBeast, new InfoEscortRobBeast() );
                    //    //_serverEscortRobBeast[ idServerBeast ].angerMul = recv.vctInfo[ i ].SBeast.fAnger;
                    //}
                }
            }
             
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        /// 护送 获取一个商队的队伍信息
        public void sendEscortDataTeam( ulong sIDServerEscortTeam, FunctionListenerEvent sListener )
        {
            Logger.Info(this.agent._account + " SEND:C2RM_ESCORT_GET_TEAM 获取一个商队的队伍信息" + sIDServerEscortTeam );
            C2RM_ESCORT_GET_TEAM sender = new C2RM_ESCORT_GET_TEAM();
            sender.STeam = new SEscortSearchBase();
            sender.STeam.luiId = sIDServerEscortTeam;
            //sender.STeam.uiSessionId = DataMode.getEscortSafeTeam( sIDServerEscortTeam ).idSession;
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.agent.send( sender );
        }

        /// 护送 获取一个商队的队伍信息 (队伍详细信息 包括装备 不包括血量)
        public void recvEscortDataTeam( ArgsEvent args )
        {
            RM2C_ESCORT_GET_TEAM recv = args.getData<RM2C_ESCORT_GET_TEAM>();
            Logger.Info(this.agent._account + " RECV:RM2C_ESCORT_GET_TEAM 获取一个商队的队伍信息 " + recv.iResult );
            
            if( recv.iResult == 1 )
            {
                if( !this.agent.dataMode._serverEscortSafeTeam.ContainsKey( recv.EscortInfo.luiIdETid ) )
                    this.agent.dataMode._serverEscortSafeTeam.Add( recv.EscortInfo.luiIdETid, new InfoEscortSafeTeam() );
                InfoEscortSafeTeam infoSafeTeam = this.agent.dataMode._serverEscortSafeTeam[ recv.EscortInfo.luiIdETid ];
                infoSafeTeam.idSession = recv.EscortInfo.uiSessionId;
                infoSafeTeam.idServerTeam = recv.EscortInfo.luiIdETid;
                infoSafeTeam.idServerPlayer = recv.EscortInfo.uiIdMaster;
                infoSafeTeam.idServerBeast = recv.EscortInfo.Beast.uiIdPet;
                infoSafeTeam.idLoginServer = recv.EscortInfo.uiIdServer;
                infoSafeTeam.timeStamp = ( double ) recv.EscortInfo.uiBeginTime;
                infoSafeTeam.fighting = recv.EscortInfo.uiFightValue;
                infoSafeTeam.name = recv.EscortInfo.GetName();
                List<ulong> teamData = new List<ulong>();
                teamData.Add( recv.EscortInfo.Team.uiIdLead );
                //GUtil.addRange<ulong>( teamData, recv.EscortInfo.Team.uiIdPet );
                infoSafeTeam.idServerTeamHeroList = teamData;

                /// 填充 队伍 信息
                {
                    for( int i = 0; i < recv.EscortInfo.vctPetInfo.Length; i++ )
                    {
                        if( recv.EscortInfo.vctPetInfo[ i ].uiIdPet <= 0 )
                            continue;
                        /// 跨服考虑
                        ulong idServerHero = recv.EscortInfo.vctPetInfo[ i ].uiIdPet;
                        //if( DataMode.idLoginServer != infoSafeTeam.idLoginServer )
                        //    idServerHero = DataMode.dummyEscortHero( idServerHero );
                        if( !this.agent.dataMode._serverHero.ContainsKey( idServerHero ) )
                            this.agent.dataMode._serverHero.Add( idServerHero, new InfoHero() );
                        InfoHero infoHero = this.agent.dataMode._serverHero[ idServerHero ];
                        infoHero.idServer = recv.EscortInfo.vctPetInfo[ i ].uiIdPet;
                        /// 不是自己的重写 防止覆盖
                        if( infoSafeTeam.idServerPlayer != this.agent.dataMode.myPlayer.idServer )
                        {
                            infoHero.idCsv = ( int ) recv.EscortInfo.vctPetInfo[ i ].uiIdCsvPet;
                            infoHero.exp = recv.EscortInfo.vctPetInfo[ i ].luiExp;

                            TypeCsvHero csvHero = ManagerCsv.getHero( infoHero.idCsv );
                            /// 设置装备
                            infoHero.infoEquip.clear();
                            for( int j = 0; j < recv.EscortInfo.vctPetInfo[ i ].vctLuiIdEquip.Length; j++ )
                            {
                                infoHero.infoEquip.setProp( j, ( int ) recv.EscortInfo.vctPetInfo[ i ].vctLuiIdEquip[ j ] );
                            }
                            /// 设置技能信息
                            //infoHero.infoSkill.clear();
                            //if( null != csvHero.idSkill )
                            //{
                            //    for( int j = 0; j < csvHero.idSkill.Length; j++ )
                            //    {
                            //        InfoSkill infoSkill = new InfoSkill();
                            //        if( recv.EscortInfo.vctPetInfo[ i ].vctUiIdCsvSkill[ j ] == 0 )
                            //        {
                            //            infoSkill.idCsv = GMath.toInt( csvHero.idSkill[ j ] );
                            //        }
                            //        if( recv.EscortInfo.vctPetInfo[ i ].vctUiIdCsvSkill[ j ] != 0 )
                            //        {
                            //            infoSkill.idCsv = ( int ) recv.EscortInfo.vctPetInfo[ i ].vctUiIdCsvSkill[ j ];
                            //            infoSkill.isActive = true;
                            //        }
                            //        infoHero.infoSkill.skillSort.Add( infoSkill.idCsv );
                            //        infoHero.infoSkill.setInfoSkill( infoSkill );
                            //    }
                            //}
                            ///// 设置手动技能
                            //infoHero.infoSkill.setSkillRelease( ( int ) recv.EscortInfo.vctPetInfo[ i ].uiIdCsvHandSkill );
                        }
                    }
                }

                /// 获取这个商队 魂兽 信息
                //if( recv.EscortInfo.Beast.uiIdPet > 0 )
                //{
                //    /// 跨服考虑
                //    ulong idServerBeast = recv.EscortInfo.Beast.uiIdPet;

                //    if( DataMode.idLoginServer != infoSafeTeam.idLoginServer )
                //        idServerBeast = DataMode.dummyEscortBeast( idServerBeast );
                //    if( !_serverBeast.ContainsKey( idServerBeast ) )
                //        _serverBeast.Add( idServerBeast, new InfoBeast() );
                //    InfoBeast infoBeast = _serverBeast[ idServerBeast ];
                //    infoBeast.idServer = recv.EscortInfo.Beast.uiIdPet;
                //    infoBeast.idCsv = ( int ) recv.EscortInfo.Beast.uiIdCsvPet;
                //    /// 不是自己的重写 防止覆盖
                //    if( infoSafeTeam.idServerPlayer != DataMode.myPlayer.idServer )
                //    {
                //        infoBeast.exp = recv.EscortInfo.Beast.luiExp;
                //        infoBeast.infoEquip.clear();
                //        for( int i = 0; i < recv.EscortInfo.Beast.vctLuiIdEquip.Length; i++ )
                //        {
                //            infoBeast.infoEquip.setProp( i, ( int ) recv.EscortInfo.Beast.vctLuiIdEquip[ i ] );
                //        }
                //    }
                //}
            }

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 护送 进入掠夺
        public void sendEscortRobIn( ulong sIDServerSafeTeam, FunctionListenerEvent sListener )
        {
            Logger.Info(this.agent._account + " SEND:C2RM_ESCORT_GO_TO 护送 进入掠夺  id: " + sIDServerSafeTeam);
            C2RM_ESCORT_GO_TO sender = new C2RM_ESCORT_GO_TO();
            sender.luiIdTeam = sIDServerSafeTeam;
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.agent.send( sender );
        }

        /// 护送 离开掠夺
        public void sendEscortRobOut( FunctionListenerEvent sListener )
        {
            Logger.Info(this.agent._account + " SEND:C2RM_ESCORT_LEAVE 离开 掠夺" );
            C2RM_ESCORT_LEAVE sender = new C2RM_ESCORT_LEAVE();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            this.agent.send( sender );
        }

        /// 护送 进入掠夺
        public void recvEscortRobIn( ArgsEvent args )
        {
            RM2C_ESCORT_GO_TO recv = args.getData<RM2C_ESCORT_GO_TO>();
            Logger.Info( this.agent._account + " RECV:RM2C_ESCORT_GO_TO 进入 掠夺 " + recv.iResult );
            if( recv.iResult == 1 )
            {
                /*
                DataMode.infoEscort.bread = ( int ) recv.SInfo.uiCntBread;
                DataMode.infoEscort.breadTimeStamp = ( double ) recv.SInfo.uiBreadLessTime;
                DataMode.infoEscort.breadBuyCnt = ( int ) recv.SInfo.uiCntBreadBuy;
                DataMode.infoEscort.searchCount = ( int ) recv.SInfo.uiCntSearch;
                DataMode.infoEscort.idServerEscortRob = recv.SInfo.uiIdEGF;
                DataMode.infoEscort.idServerEscortSearch = recv.SInfo.uiIdEGS;
                */
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 护送 离开掠夺
        public void recvEscortRobOut( ArgsEvent args )
        {
            RM2C_ESCORT_LEAVE recv = args.getData<RM2C_ESCORT_LEAVE>();
            Logger.Info( this.agent._account + " RECV:RM2C_ESCORT_LEAVE 离开 掠夺 " + recv.iResult );
            if( recv.iResult == 1 )
            {
                /*
                /// WILL DONE 退出掠夺战斗
                WindowsMngr.getInstance().closeWindow( WindowsID.CARAVAN_RESULT );
                bool isOpenFbCaravanMap = false;
                foreach( ulong idServerEscort in DataMode.infoEscort.idServerEscortSafe )
                {
                    if( null == DataMode.getEscortSafe( idServerEscort ) )
                        continue;
                    if( !DataMode.getEscortSafe( idServerEscort ).isEmpty )
                        continue;
                    WindowsMngr.getInstance().openFbCaravanMap( idServerEscort );
                    isOpenFbCaravanMap = true;
                    break;
                }
                /// 如果没有新矿 打开老矿搜索界面
                if( !isOpenFbCaravanMap )
                    WindowsMngr.getInstance().openFbCaravanMap( ManagerSenceEscortRob.infoSafe.idServer );
                 */
            }
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// 护送 战斗数据开始
        public void sendEscortCombatStart()
        {
            Logger.Info(agent._account + " SEND:C2RM_ESCORT_PK_BEGIN >> " + "护送 战斗数据 Begin" );
            C2RM_ESCORT_PK_BEGIN sender = new C2RM_ESCORT_PK_BEGIN();
            
            sender.uiBeginTime = ( uint )GUtil.getEpoch(); //AICombat.timeStartWaitTeamp;
            sender.luiIdBeast = 0;
            sender.uiFriendId = 0;
            sender.vctPet = new ulong[ 9 ];
            sender.vctTeamPet = new ulong[ 9 ];
 
            /*
            /// 站位 主角
            List<InfoHero> infoHeroArr = DataMode.myPlayer.infoHeroList.getTeam( DataMode.myPlayer.infoHeroList.idTeamEscortRob );

            if( null != ManagerSenceEscortRob.infoFriendHero )
            {
                infoHeroArr.Add( ManagerSenceEscortRob.infoFriendHero );
                sender.uiFriendId = ManagerSenceEscortRob.infoFriend.idServer;
            }
            DataMode.myPlayer.infoHeroList.mathStandIndexArr( infoHeroArr );
            ulong[] standInfo = new ulong[ 9 ];
            foreach( InfoHero infoHero in infoHeroArr )
            {
                standInfo[ infoHero.standIndex ] = infoHero.idServer;
            }
            sender.vctPet = standInfo;
            /// 增加魂兽信息
            if( null != DataMode.myPlayer.infoHeroList.getTeamBeast( DataMode.myPlayer.infoHeroList.idTeamEscortRob ) )
                sender.luiIdBeast = DataMode.myPlayer.infoHeroList.getTeamBeast( DataMode.myPlayer.infoHeroList.idTeamEscortRob ).idServer;
            /// 站位 怪物
            infoHeroArr = ManagerSenceEscortRob.infoSafeTeam.getTeam();
            DataMode.myPlayer.infoHeroList.mathStandIndexArr( infoHeroArr );
            standInfo = new ulong[ 9 ];
            foreach( InfoHero infoHero in infoHeroArr )
            {
                standInfo[ infoHero.standIndex ] = infoHero.idServer;
            }
            sender.vctTeamPet = standInfo;
             */
            /// 发送 协议
            this.agent.send( sender );
        }


        /// 护送 战斗数据结束
        public void sendEscortCombatEnd( bool isWin, /*AICombat[]*/ int aiCombat, FunctionListenerEvent listener )
        {
            Logger.Info( agent._account + " SEND:C2RM_ESCORT_PK_OVER >> " + "护送 战斗数据 End >> " + isWin );
            
            C2RM_ESCORT_PK_OVER sender = new C2RM_ESCORT_PK_OVER();
            sender.uiListen = Dispatcher.addListener( listener, null );

            sender.fStopTime = 0; //GUtil.getEpoch();
            sender.luiIdBeast = 0;
            //sender.vctHpPet = new float[](0,0,0,0);
            //sender.vctPetSkill = new SEscortPetSkill[ 18 ];
            sender.cIsWin = 1;
            sender.fBeastAnger = 0;
            sender.fBeastAngerEnemy = 0;

            /*
            /// 把血量传送给服务器
            for( int index = 0; index < aiCombat.Length; index++ )
            {

                if( null == aiCombat[ index ] || aiCombat[ index ].isDie() )
                    continue;
                /// 设置血量(超时间)
                if( AICombat.timeOverTeamp < Data.gameTime )
                    sender.vctHpPet[ index ] = 0;
                else
                    sender.vctHpPet[ index ] = aiCombat[ index ].getCombatData().hp / aiCombat[ index ].getCombatData().hpTotal;
                /// 暂存数据模型中
                if( index < 9 && null != DataMode.getEscortRobHero( aiCombat[ index ].getCombatData().myServerHero.idServer ) )
                    DataMode.getEscortRobHero( aiCombat[ index ].getCombatData().myServerHero.idServer ).hpMul = sender.vctHpPet[ index ];

                /// 设置宠物id
                sender.vctPetSkill[ index ].luiIdPet = aiCombat[ index ].getCombatData().myServerHero.idServer;
                /// 设置技能cd
                List<int> skillSort = aiCombat[ index ].getCombatData().myServerHero.infoSkill.skillSort;
                int i = 0;
                /// 主动技能
                if( aiCombat[ index ].getCombatData().mySkillRelease.Count > 0 )
                {
                    i = skillSort.IndexOf( aiCombat[ index ].getCombatData().mySkillRelease[ 0 ].csvSkill.id );
                    if( -1 != i && i < sender.vctPetSkill[ index ].m_vctfProSkill.Length )
                    {
                        sender.vctPetSkill[ index ].m_vctfProSkill[ i ] = ( float ) ( aiCombat[ index ].getCombatData().mySkillRelease[ 0 ].timeStampUnlock - Data.gameTime ) / aiCombat[ index ].getCombatData().mySkillRelease[ 0 ].csvSkill.cd;
                        sender.vctPetSkill[ index ].m_vctfProSkill[ i ] = Mathf.Max( 0f, sender.vctPetSkill[ index ].m_vctfProSkill[ i ] );
                        sender.vctPetSkill[ index ].m_vctfProSkill[ i ] = 1f - sender.vctPetSkill[ index ].m_vctfProSkill[ i ];
                    }
                }
                /// 被动技能
                for( int j = 0; j < aiCombat[ index ].getCombatData().mySkillAuto.Count; j++ )
                {
                    i = skillSort.IndexOf( aiCombat[ index ].getCombatData().mySkillAuto[ j ].csvSkill.id );
                    if( -1 != i && i < sender.vctPetSkill[ index ].m_vctfProSkill.Length )
                    {
                        sender.vctPetSkill[ index ].m_vctfProSkill[ i ] = ( float ) ( aiCombat[ index ].getCombatData().mySkillAuto[ j ].timeStampUnlock - Data.gameTime ) / aiCombat[ index ].getCombatData().mySkillAuto[ j ].csvSkill.cd;
                        sender.vctPetSkill[ index ].m_vctfProSkill[ i ] = Mathf.Max( 0f, sender.vctPetSkill[ index ].m_vctfProSkill[ i ] );
                        sender.vctPetSkill[ index ].m_vctfProSkill[ i ] = 1f - sender.vctPetSkill[ index ].m_vctfProSkill[ i ];
                    }

                }
            }

            sender.cIsWin = ( byte ) ( isWin ? 1 : 0 );
            sender.fStopTime = ( float ) Data.gameTime;
            sender.uiListen = DataMode.addListener( listener );
            if( null != ManagerCombatBeast.beastSlef )
            {
                sender.luiIdBeast = ManagerCombatBeast.beastSlef.infoBeast.idServer;
                sender.fBeastAnger = Mathf.Max( ManagerCombatBeast.beastSlef.anger / ManagerCombatBeast.beastSlef.dataAttCount.angerTotal, 0f );
            }
            if( null != ManagerCombatBeast.beastEnemy )
            {
                sender.fBeastAngerEnemy = Mathf.Max( ManagerCombatBeast.beastEnemy.anger / ManagerCombatBeast.beastEnemy.dataAttCount.angerTotal, 0f );
            }
             */
            /// 战斗协议发送
            this.agent.send( sender );
        }

        /// 护送 结果返回
        public void recvEscortRobResult( ArgsEvent args )
        {
            RM2C_ESCORT_PK_OVER recv = args.getData<RM2C_ESCORT_PK_OVER>();
            Logger.Info(this.agent._account + " RECV:RM2C_ESCORT_PK_OVER 战斗结果返回 " + recv.iResult );
            
            if( recv.iResult == 1 )
            {
                /// 新矿信息
                for( int i = 0; i < recv.vctInfo.Length; i++ )
                {
                    //dataMode.infoEscort.idServerEscortSafe.Add( recv.vctInfo[ i ].SInfoBase.luiIdEG );
                    /// 创建 商队
                    if( !this.agent.dataMode._serverEscortSafe.ContainsKey( recv.vctInfo[ i ].SInfoBase.luiIdEG ) )
                        this.agent.dataMode._serverEscortSafe.Add( recv.vctInfo[ i ].SInfoBase.luiIdEG, new InfoEscortSafe() );

                    InfoEscortSafe infoSafe = this.agent.dataMode._serverEscortSafe[ recv.vctInfo[ i ].SInfoBase.luiIdEG ];
                    infoSafe.idServerSafeTeams.Clear();
                    infoSafe.idServer = recv.vctInfo[ i ].SInfoBase.luiIdEG;
                    infoSafe.idSession = recv.vctInfo[ i ].SInfoBase.uiSessionId;
                    infoSafe.timeStamp = ( double ) recv.vctInfo[ i ].SInfoBase.uiBeginTime;
                    infoSafe.type = recv.vctInfo[ i ].SInfoBase.cType;
                    infoSafe.typeSafe = ( int ) recv.vctInfo[ i ].SInfoBase.uiEscortGroupType;
                }
                /// 攻打人的信息
                {
                    /// 攻打的人的数据
                    if( !this.agent.dataMode._serverEscortSafe.ContainsKey( recv.AckEscortGroup.SInfoBase.luiIdEG ) )
                        this.agent.dataMode._serverEscortSafe.Add( recv.AckEscortGroup.SInfoBase.luiIdEG, new InfoEscortSafe() );

                    InfoEscortSafe infoSafe = this.agent.dataMode._serverEscortSafe[ recv.AckEscortGroup.SInfoBase.luiIdEG ];
                    infoSafe.idServer = recv.AckEscortGroup.SInfoBase.luiIdEG;
                    infoSafe.idSession = recv.AckEscortGroup.SInfoBase.uiSessionId;
                    infoSafe.timeStamp = ( double ) recv.AckEscortGroup.SInfoBase.uiBeginTime;
                    infoSafe.type = recv.AckEscortGroup.SInfoBase.cType;
                    infoSafe.typeSafe = ( int ) recv.AckEscortGroup.SInfoBase.uiEscortGroupType;

                    /// 证明掠夺了新商队
                    //if( recv.vctInfo.Length > 0 )
                    //{
                    //    /// 把奖励写进数据模型
                    //    DataMode.myPlayer.money += recv.Money.iQMoney;
                    //    DataMode.myPlayer.money_game += recv.Money.iSMoney;
                    //    DataMode.myPlayer.infoPropList.addProp( ( int ) recv.Equ.uiIdCsvEquipment, recv.Equ.iCnt );
                    //}
                    /// 收获的奖励
                    //infoSafe.infoRewardRob.clear();
                    //if( recv.Money.iSMoney > 0 )
                    //    infoSafe.infoRewardRob.addMoneyGame( ( int ) recv.Money.iSMoney );
                    //if( recv.Money.iQMoney > 0 )
                    //    infoSafe.infoRewardRob.addMoney( ( int ) recv.Money.iQMoney );
                    //if( recv.Equ.iCnt > 0 )
                    //    infoSafe.infoRewardRob.addProp( ( int ) recv.Equ.uiIdCsvEquipment, recv.Equ.iCnt );

                    infoSafe.idServerSafeTeams.Clear();
                    /// 攻打矿坑的信息
                    for( int j = 0; j < recv.AckEscortGroup.vctTeamShow.Length; j++ )
                    {
                        /// 设置队伍
                        infoSafe.setSafeTeam( j, recv.AckEscortGroup.vctTeamShow[ j ].luiIdETid );
                        /// 创建队伍
                        if( recv.AckEscortGroup.vctTeamShow[ j ].luiIdETid <= 0 )
                            continue;
                        if( !this.agent.dataMode._serverEscortSafeTeam.ContainsKey( recv.AckEscortGroup.vctTeamShow[ j ].luiIdETid ) )
                            this.agent.dataMode._serverEscortSafeTeam.Add( recv.AckEscortGroup.vctTeamShow[ j ].luiIdETid, new InfoEscortSafeTeam() );

                        InfoEscortSafeTeam infoSafeTeam = this.agent.dataMode._serverEscortSafeTeam[ recv.AckEscortGroup.vctTeamShow[ j ].luiIdETid ];
                        infoSafeTeam.idSession = recv.AckEscortGroup.vctTeamShow[ j ].uiSessionId;
                        infoSafeTeam.idServerTeam = recv.AckEscortGroup.vctTeamShow[ j ].luiIdETid;
                        infoSafeTeam.timeStamp = ( double ) recv.AckEscortGroup.vctTeamShow[ j ].uiBeginTime;
                        infoSafeTeam.lv = ( int ) recv.AckEscortGroup.vctTeamShow[ j ].usLv;
                        infoSafeTeam.fighting = recv.AckEscortGroup.vctTeamShow[ j ].uiFightValue;
                        /// 服务器id
                        infoSafeTeam.idLoginServer = recv.AckEscortGroup.uiIdServer;
                        /// 数据类型
                        List<ulong> teamData = new List<ulong>();
                        teamData.Add( recv.AckEscortGroup.vctTeamShow[ j ].luiIdLeadServer );
                        infoSafeTeam.idServerTeamHeroList = teamData;


                        /// 创建 队长信息 信息
                        if( recv.AckEscortGroup.vctTeamShow[ j ].luiIdLeadServer <= 0 )
                            continue;
                        /// 跨服信息
                        ulong idServerHero = recv.AckEscortGroup.vctTeamShow[ j ].luiIdLeadServer;
                        //if( DataMode.idLoginServer != infoSafeTeam.idLoginServer )
                         //   idServerHero = DataMode.dummyEscortHero( idServerHero );

                        if( !this.agent.dataMode._serverHero.ContainsKey( idServerHero ) )
                            this.agent.dataMode._serverHero.Add( idServerHero, new InfoHero() );
                        InfoHero infoHero = this.agent.dataMode._serverHero[ idServerHero ];
                        infoHero.idServer = recv.AckEscortGroup.vctTeamShow[ j ].luiIdLeadServer;
                        infoHero.idCsv = ( int ) recv.AckEscortGroup.vctTeamShow[ j ].uiIdLead;
                    }
                }
            }
             
            /// 胜利失败返回
            //WindowsMngr.getInstance().openWindow( WindowsID.CARAVAN_RESULT, ( int ) recv.uiType );

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        /// WILL DONE 护送 改变己方商队队伍阵法
        public void sendEscortTeamChange( ulong sIDServerEscort, int sIndex, ulong sIDServerTeam, ulong sIDServerLeader, ulong[] sIDServerHero, ulong sIDServerBeast, FunctionListenerEvent sListener )
        {
            
            Logger.Info(this.agent._account + " SEND:C2RM_ESCORT_SET_SELF_TEAM 改变己方商队队伍阵法" );
            C2RM_ESCORT_SET_SELF_TEAM sender = new C2RM_ESCORT_SET_SELF_TEAM();
            sender.uiListen = Dispatcher.addListener( sListener, null);
            sender.TeamUpdate.uiIdLead = sIDServerLeader;
            sender.TeamUpdate.uiIdPet = sIDServerHero;
            sender.TeamUpdate.uiLoc = ( uint ) sIndex;
            sender.TeamUpdate.SGroup.luiId = sIDServerEscort;
            if( null != this.agent.dataMode.getEscortSafe( sIDServerEscort ) )
                sender.TeamUpdate.SGroup.uiSessionId = this.agent.dataMode.getEscortSafe( sIDServerEscort ).idSession;

            sender.TeamUpdate.luiIdBeast = sIDServerBeast;

            sender.TeamUpdate.STeam.luiId = sIDServerTeam;
            if( null != this.agent.dataMode.getEscortSafeTeam( sIDServerTeam ) )
                sender.TeamUpdate.STeam.uiSessionId = this.agent.dataMode.getEscortSafeTeam( sIDServerTeam ).idSession;

            this.agent.send( sender );
             
        }

        /// 护送 放弃护送
        public void sendEscortGiveUp( ulong sIDServerSafe, FunctionListenerEvent listener )
        {
            Logger.Info(this.agent._account + " SEND:C2RM_ESCORT_GIVE_UP >> " + "护送 放弃" );

            sIDServerSafe = this.agent.dataMode.infoEscort.idServerEscortSearch;
            C2RM_ESCORT_GIVE_UP sender = new C2RM_ESCORT_GIVE_UP();
            sender.SGroup.luiId = sIDServerSafe;
            InfoEscortSafe ies = this.agent.dataMode.getEscortSafe( sIDServerSafe );
            sender.SGroup.uiSessionId = ies.idSession;
            if( null != ies.getMySafeTeam( this.agent ) )
            {
                sender.STeam.luiId = this.agent.dataMode.getEscortSafe( sIDServerSafe ).getMySafeTeam( this.agent ).idServerTeam;
                sender.STeam.uiSessionId = this.agent.dataMode.getEscortSafe( sIDServerSafe ).getMySafeTeam( this.agent ).idSession;
            }
            sender.uiListen = Dispatcher.addListener( listener, null);
            this.agent.send( sender );
        }

        /// 护送 放弃
        public void recvEscortGiveUp( ArgsEvent args )
        {
            RM2C_ESCORT_GIVE_UP recv = args.getData<RM2C_ESCORT_GIVE_UP>();
            Logger.Info(this.agent._account + " RECV:RM2C_ESCORT_GIVE_UP 护送 放弃 " + recv.iResult );
            
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }


        //创建公会
        public void sendCreateUnion(string _name, int _icon, int _iconbg, int _limitLv, int _type,FunctionListenerEvent listener )
        {
            Logger.Info( this.agent._account + " SEND:C2RM_CREATE_GAME_UNION >> 创建公会" );
            C2RM_CREATE_GAME_UNION sender = new C2RM_CREATE_GAME_UNION();
            sender.setName( _name );
            sender.m_uiGUPic = ( uint ) _icon;
            sender.m_ucJoinType = ( byte ) _type;
            sender.m_usJoinLevel = ( ushort ) _limitLv;
            sender.uiListen = Dispatcher.addListener( listener, null );
            this.agent.send( sender );
        }

        //解散公会
        public void sendDisbandUnion(int _unionID, FunctionListenerEvent listener)
        {
            Logger.Info( this.agent._account + " SEND:C2RM_DISBAND_GAME_UNION >> 解散公会  " + _unionID);
            C2RM_DISBAND_GAME_UNION sender = new C2RM_DISBAND_GAME_UNION();
            sender.uiListen = Dispatcher.addListener( listener, null);
            sender.m_uiDisbandGUID = ( uint ) _unionID;
            this.agent.send( sender );
        }

        //创建公会回调
        public void recvCreateUnion( ArgsEvent args )
        {
            RM2C_CREATE_GAME_UNION recv = args.getData<RM2C_CREATE_GAME_UNION>();
            Logger.Info(this.agent._account + " RECV:RM2C_CREATE_GAME_UNION >> " + recv.iResult);

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //解散公会
        public void recvDisbandUnion( ArgsEvent args )
        {
            RM2C_DISBAND_GAME_UNION recv = args.getData<RM2C_DISBAND_GAME_UNION>();
            Logger.Info( this.agent._account + "  RECV:RM2C_DISBAND_GAME_UNION >> " + recv.m_iResult );
            
            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //请求加入公会(某一个)
        public void recvRequestJoinUnion( ArgsEvent args )
        {
            RM2C_REQUEST_JOIN_GAME_UNION recv = args.getData<RM2C_REQUEST_JOIN_GAME_UNION>();
            Logger.Info( this.agent._account + " RECV:RM2C_REQUEST_JOIN_GAME_UNION >> " + recv.m_iResult );

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

        //请求返回 申请公会列表
        public void recvGetUnionList( ArgsEvent args )
        {
            RM2C_GAME_UNION_LIST recv = args.getData<RM2C_GAME_UNION_LIST>();
            Logger.Info( this.agent._account + " RECV:RM2C_GAME_UNION_LIST >>  " );
            ////清空申请列表
            //DataMode.applayUnionList.Clear();

            //for( int i = 0; i < recv.m_SGUList.Length; i++ )
            //{
            //    unionInfo _info = new unionInfo();
            //    if( i < ( int ) recv.m_ucRequestCount )
            //        _info.isApplay = true;
            //    else
            //        _info.isApplay = false;
            //    _info.uID = ( int ) recv.m_SGUList[ i ].m_uiID;
            //    _info.uName = recv.m_SGUList[ i ].getName();
            //    _info.uType = ( int ) recv.m_SGUList[ i ].m_ucType;
            //    _info.uLimitJoinLevel = ( int ) recv.m_SGUList[ i ].m_uiJoinLevel;
            //    _info.uIcon = ( int ) recv.m_SGUList[ i ].m_uiPic;
            //    _info.uIconBG = ( int ) recv.m_SGUList[ i ].m_uiPicFrame;
            //    _info.uAnnouncement = recv.m_SGUList[ i ].getAnnouncement();
            //    _info.uCurMemberCnt = ( int ) recv.m_SGUList[ i ].m_ucCurMemberCount;
            //    _info.uMaxMemberCnt = ( int ) recv.m_SGUList[ i ].m_ucTotalMemberCount;

            //    DataMode.applayUnionList.Add( _info );
            //}

            Dispatcher.dispatchListener( recv.uiListen, recv );
        }

    }
}
