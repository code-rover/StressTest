﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace utils
{
    class ManagerCsv
    {
        /**
        private static ManagerCsv _this;
        public static ManagerCsv getInstance() {
            if(_this == null) {
                _this = new ManagerCsv();
            }
            return _this;
        }
        **/

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

    }
}
