using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace VL.GameZero.Service.Utilities
{
    public class SQLiteHelper
    {
        static string _DbName;

        public static string DbName
        {
            get
            {
                if (string.IsNullOrEmpty(_DbName))
                {
                    var dllLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    _DbName = Path.Combine(dllLocation.Substring(0, dllLocation.LastIndexOf("\\")), "VL.GameZero.sqlite");
                }
                return _DbName;
            }
            set { _DbName = value; }
        }

        public static SQLiteConnection Connect()
        {
            return new SQLiteConnection(GetConnectingString());
        }

        public static string GetConnectingString()
        {
            //var logger = new TextLogger("PmLogger.txt", @"D:\");
            //logger.Error("输出到了:" + DbName);
            return $"DataSource={DbName};Version=3;";
        }

        #region PrepareTables
        public static void PrepareTables()
        {
            foreach (var key in TableCreateSQLs.Keys)
            {
                if (!IsTableExist(key))
                {
                    DropTables(key);
                    CreateTables(key);
                }
            }
        }
        static Dictionary<string, string> TableCreateSQLs = new Dictionary<string, string>()
        {
            { "TAccount",$@"create table TAccount (
   UId                  varchar(36)          ,
   AccountName          varchar(20)          not null,
   Password             varchar(128)         not null,
   CreatedOn            datetime             not null,
   constraint PK_TACCOUNT primary key (UId)
);
            "},
            { "TPlayer",$@"create table TPlayer (
   UId                  varchar(36)          ,
   SlotIndex            numeric(2)           not null,
   PlayerName           varchar(20)          not null,
   CreatedOn            datetime             not null,
   constraint PK_TPLAYER primary key (UId)
);
            "},
        };

        static bool IsTableExist(string table)
        {
            bool exits = false;
            using (var connection = Connect())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"select 1 from sqlite_master where name='{table}'";
                var result = command.ExecuteScalar();//注意返回的数值为long,不可强制转换为int
                exits = result != null && (long)result == 1;
                connection.Close();
            }
            return exits;
        }
        static void CreateTables(string key)
        {
            CreateTable(TableCreateSQLs.First(c => c.Key == key).Value);
        }
        static void CreateTable(string createSQL)
        {
            using (var connection = Connect())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = createSQL;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        static void DropTables(string key)
        {
            using (var connection = Connect())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "drop table " + key;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}