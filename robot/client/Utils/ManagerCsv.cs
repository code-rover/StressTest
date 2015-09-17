using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace utils
{
    class ManagerCsv
    {
        private static List<Dictionary<string, string>> csv_path_list;

        public static Dictionary<string, UtilCsvReader> _csvTables = new Dictionary<string, UtilCsvReader>();

        public static void init_csv( string infocsv )
        {
            if( !File.Exists( infocsv ) )
            {
                Logger.Error( "infocsv不存在: " + infocsv );
                return;
            }

            FileStream fs = new FileStream( infocsv, FileMode.Open );

            UtilCsvReader infocsvReader = new UtilCsvReader( fs );

            /// 地址表
            csv_path_list = infocsvReader.searchs( "type", "csv" );

            foreach( Dictionary<string, string> item in csv_path_list )
            {
                string csvPath = item[ "value" ];
                if( !File.Exists( csvPath ) )
                {
                    Logger.Error( "文件不存在: " + csvPath );
                    throw new Exception( "error 文件不存在: " + csvPath );
                }

                UtilCsvReader csvReader = new UtilCsvReader(new FileStream(csvPath, FileMode.Open));

                string key = csvPath.Substring( csvPath.LastIndexOf( "/" ) + 1 );
                key = key.Replace( ".csv", "" );

                _csvTables.Add( key, csvReader );
            }

        }

        /// 数据缓存
        protected static Dictionary<string, object> _csvItemMemory = new Dictionary<string, object>();

        /////////////////////////////////////////////////////////////////////

        // 获得 道具表
        public static TypeCsvProp getProp( int idCsvProp )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvProp getProp(int idCsvProp)" + idCsvProp;
            string key = string.Intern( new StringBuilder( "public static TypeCsvProp getProp(int idCsvProp)" ).Append( idCsvProp ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvProp ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvProp result = _csvTables[ "Prop" ].searchAndNew<TypeCsvProp>( "id", idCsvProp );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 获得主角 的数据
        public static TypeCsvHero getHero( int idCsv )
        {
            /// 数据缓存部分
            string key = string.Intern( new StringBuilder( "public static TypeCsvHero getHero(int idCsv) idCsv = " ).Append( idCsv ).ToString() );
            //		string key = string.Intern("public static TypeCsvHero getHero(int idCsv) idCsv = " + idCsv);
            //		string key = "public static TypeCsvHero getHero(int idCsv) idCsv = " + idCsv;
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvHero ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvHero result = _csvTables[ "Hero" ].searchAndNew<TypeCsvHero>( "id", idCsv );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 获得角色等级的*量数据
        public static TypeCsvHeroStar getHeroStar( int idCsvHeroSame )
        {
            //		string key = "public static TypeCsvHeroStar getHeroStar(int idCsvHeroSame) idCsvHeroSame = " + idCsvHeroSame;
            string key = string.Intern( new StringBuilder( "public static TypeCsvHeroStar getHeroStar(int idCsvHeroSame) idCsvHeroSame = " ).Append( idCsvHeroSame ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvHeroStar ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvHeroStar result = _csvTables[ "HeroStar" ].searchAndNew<TypeCsvHeroStar>( "id", idCsvHeroSame );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 检索副本的数据
        public static TypeCsvFB getFB( int idCsv )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvFB getFB(int idCsv) idCsv = " + idCsv;
            string key = string.Intern( new StringBuilder( "public static TypeCsvFB getFB(int idCsv) idCsv = " ).Append( idCsv ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvFB ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvFB result = _csvTables[ "FB" ].searchAndNew<TypeCsvFB>( "id", idCsv );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 变量数据
        public static TypeCsvAttribute getAttribute()
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvAttribute getAttribute()";
            string key = string.Intern( "public static TypeCsvAttribute getAttribute()" );
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvAttribute ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvAttribute result = _csvTables[ "Attribute" ].getAttribue<TypeCsvAttribute>( "name", "val" );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 获得角色等级的*量数据
        public static TypeCsvHeroLv getHeroLv( int lv )
        {
            string key = string.Intern( new StringBuilder( "public static TypeCsvHeroLv getHeroLv(int lv) lv = " ).Append( lv ).ToString() );
            //		string key = "public static TypeCsvHeroLv getHeroLv(int lv) lv = " + lv;
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvHeroLv ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvHeroLv result = _csvTables[ "HeroLv" ].searchAndNew<TypeCsvHeroLv>( "lv", lv );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }


        public static TypeCsvPropEquip getPropEquip( int idCsvProp )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvPropEquip getPropEquip(int idCsvProp) = " + idCsvProp;
            string key = string.Intern( new StringBuilder( "public static TypeCsvPropEquip getPropEquip(int idCsvProp) = " ).Append( idCsvProp ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
            {
                return ( TypeCsvPropEquip ) _csvItemMemory[ key ];
            }
            /// 返回值
            TypeCsvPropEquip result = _csvTables[ "PropEquip" ].searchAndNew<TypeCsvPropEquip>( "id", idCsvProp );
            if( null != result )
                _csvTables[ "Prop" ].searchAndSet<TypeCsvPropEquip>( result, "id", idCsvProp );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 获得角色 身上进化石的属性
        public static TypeCsvHeroUp getHeroUp( int idCsvHero )
        {
            //		string key = "public static TypeCsvHeroUp getHeroUp(int idCsvHero) = " + idCsvHero;
            string key = string.Intern( new StringBuilder( "public static TypeCsvHeroUp getHeroUp(int idCsvHero) = " ).Append( idCsvHero ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvHeroUp ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvHeroUp result = _csvTables[ "HeroUp" ].searchAndNew<TypeCsvHeroUp>( "id", idCsvHero );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 检索军衔数据
        public static TypeCsvNobility getNobility( int lv )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvNobility getNobility(int lv) = " + lv;
            string key = string.Intern( new StringBuilder( "public static TypeCsvNobility getNobility(int lv) = " ).Append( lv ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
            {
                return ( TypeCsvNobility ) _csvItemMemory[ key ];
            }
            /// 返回值
            TypeCsvNobility result = _csvTables[ "Nobility" ].searchAndNew<TypeCsvNobility>( "id", lv );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 获得消耗数据
        public static TypeCsvConsume getConsume( int idCsvConsume )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvConsume getConsume(int idCsvConsume)" + idCsvConsume;
            string key = string.Intern( new StringBuilder( "public static TypeCsvConsume getConsume(int idCsvConsume)" ).Append( idCsvConsume ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
                return ( TypeCsvConsume ) _csvItemMemory[ key ];
            /// 返回值
            TypeCsvConsume result = _csvTables[ "Consume" ].searchAndNew<TypeCsvConsume>( "id", idCsvConsume );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        public static List<TypeCsvEquipCreat> getEquipCreat()
        {
            string keyList = "public static List<TypeCsvEquipCreat> getEquipCreat()";
            if( _csvItemMemory.ContainsKey( keyList ) )
                return ( List<TypeCsvEquipCreat> ) _csvItemMemory[ keyList ];

            List<TypeCsvEquipCreat> temp = new List<TypeCsvEquipCreat>();
            int id = 0;
            while( true )
            {
                id++;
                //			string key = "List<TypeCsvEquipCreat> getEquipCreat()" + id;
                //			string key = string.Intern(new StringBuilder("List<TypeCsvEquipCreat> getEquipCreat()").Append(id).ToString());
                //			if(_csvItemMemory.ContainsKey(key))
                //			{
                //				temp.Add((TypeCsvEquipCreat)_csvItemMemory[key]);
                //				continue;
                //			}

                TypeCsvEquipCreat result = _csvTables[ "EquipCreat" ].searchAndNew<TypeCsvEquipCreat>( "id", id );
                if( result == null )
                {
                    break;
                }
                //			_csvItemMemory.Add(key, result);
                temp.Add( result );
            }
            _csvItemMemory.Add( keyList, temp );
            return temp;
        }

        /// 获得标准技能
        public static TypeCsvHeroSkillBase getHeroSkillBase( int idCsv )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvHeroSkill getHeroSkill(int idCsv) = " + idCsv;
            string key = string.Intern( new StringBuilder( "public static TypeCsvHeroSkill getHeroSkill(int idCsv) = " ).Append( idCsv ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) && null != _csvItemMemory[ key ] )
            {
                return ( TypeCsvHeroSkillBase ) _csvItemMemory[ key ];
            }
            //		key = "public static TypeCsvHeroSkillAttribute getHeroSkillAttribute(int idCsv) = " + idCsv;
            key = string.Intern( new StringBuilder( "public static TypeCsvHeroSkillAttribute getHeroSkillAttribute(int idCsv) = " ).Append( idCsv ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) && null != _csvItemMemory[ key ] )
            {
                return ( TypeCsvHeroSkillBase ) _csvItemMemory[ key ];
            }
            /// 主动若没有，去被动技能中找
            TypeCsvHeroSkillBase result = getHeroSkill( idCsv );
            if( null == result )
                result = getHeroSkillAttribute( idCsv );

            /// 返回
            return result;
        }

        /// 获得主角 技能数据
        public static TypeCsvHeroSkill getHeroSkill( int idCsv )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvHeroSkill getHeroSkill(int idCsv) = " + idCsv;
            string key = string.Intern( new StringBuilder( "public static TypeCsvHeroSkill getHeroSkill(int idCsv) = " ).Append( idCsv ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
            {
                return ( TypeCsvHeroSkill ) _csvItemMemory[ key ];
            }
            /// 返回值
            TypeCsvHeroSkill result = _csvTables[ "HeroSkill" ].searchAndNew<TypeCsvHeroSkill>( "id", idCsv );
            if( null != result )
                _csvTables[ "HeroSkillBase" ].searchAndSet<TypeCsvHeroSkill>( result, "id", idCsv );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }

        /// 获得主角 被动技能数据
        public static TypeCsvHeroSkillAttribute getHeroSkillAttribute( int idCsv )
        {
            /// 数据缓存部分
            //		string key = "public static TypeCsvHeroSkillAttribute getHeroSkillAttribute(int idCsv) = " + idCsv;
            string key = string.Intern( new StringBuilder( "public static TypeCsvHeroSkillAttribute getHeroSkillAttribute(int idCsv) = " ).Append( idCsv ).ToString() );
            if( _csvItemMemory.ContainsKey( key ) )
            {
                return ( TypeCsvHeroSkillAttribute ) _csvItemMemory[ key ];
            }
            /// 返回值
            TypeCsvHeroSkillAttribute result = _csvTables[ "HeroSkillAttribute" ].searchAndNew<TypeCsvHeroSkillAttribute>( "id", idCsv );
            if( null != result )
                _csvTables[ "HeroSkillBase" ].searchAndSet<TypeCsvHeroSkillAttribute>( result, "id", idCsv );
            /// 值存储
            _csvItemMemory.Add( key, result );
            /// 返回
            return result;
        }
    }
}
