using System.Configuration;

namespace ZyGames.Moshouxingkong.Model
{
    public class DbConfig
    {
        public const string Config = "MoshouxingkongConfig";
        public const string Data = "MoshouxingkongData";
        public const string Log = "MoshouxingkongLog";
        public const int GlobalPeriodTime = 0;
        public const int PeriodTime = 0;
        public const string PersonalName = "UserId";

        public static string ConfigConnectString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[Config].ConnectionString;
            }
        }

        public static string DataConnectString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[Data].ConnectionString;
            }
        }

        public static string LogConnectString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[Log].ConnectionString;
            }
        }

    }
}
