using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Security;
using ZyGames.Framework.Data;
using ZyGames.Framework.Game.Runtime;

namespace ZyGames.Moshouxingkong.Bll.Logic
{
    class DirectGameUserRefDBAccessManager
    {
        private static readonly DbBaseProvider _dbBaseProvider;
        private const string ConnectKey = "MoshouxingkongData";

        static DirectGameUserRefDBAccessManager()
        {
            _dbBaseProvider = DbConnectionProvider.CreateDbProvider(ConnectKey);
            if (_dbBaseProvider == null)
            {
                string providerType = ConfigUtils.GetSetting("MoshouxingkongData_ProviderType");
                string connectionFormat = ConfigUtils.GetSetting("MoshouxingkongData_ConnectionString");
                string dataSource = string.Empty;
                string userInfo = string.Empty;
                try
                {
                    dataSource = ConfigUtils.GetSetting("MoshouxingkongData_Server");
                    userInfo = ConfigUtils.GetSetting("MoshouxingkongData_Acount");
                    if (!string.IsNullOrEmpty(userInfo))
                    {
                        userInfo = CryptoHelper.DES_Decrypt(userInfo, GameEnvironment.Setting.ProductDesEnKey);
                    }
                }
                catch (Exception)
                {
                }
                string connectionString = "";
                if (!string.IsNullOrEmpty(dataSource) && !string.IsNullOrEmpty(userInfo))
                {
                    connectionString = string.Format(connectionFormat, dataSource, userInfo);
                }
                _dbBaseProvider = DbConnectionProvider.CreateDbProvider(ConnectKey, providerType, connectionString);
            }
        }

        public static DbBaseProvider Provider
        {
            get { return _dbBaseProvider; }
        }

        public static readonly DirectGameUserRefDBAccessManager Manager = new DirectGameUserRefDBAccessManager();
    }
}
