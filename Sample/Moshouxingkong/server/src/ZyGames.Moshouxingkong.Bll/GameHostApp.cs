using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using ZyGames.Moshouxingkong.Model;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Data;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.Sockets;
using ZyGames.Framework.Script;

namespace ZyGames.Moshouxingkong.Bll
{
    public class GameHostApp : GameSocketHost
    {
        protected override BaseUser GetUser(int userId)
        {
            return new GameDataCacheSet<GameUser>().FindKey(userId.ToNotNullString());
        }

        protected override void OnConnectCompleted(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine("Connect Completed.");
        }

        protected override void OnReceivedBefore(ConnectionEventArgs e)
        {
            //Console.WriteLine("Url:{0}", Encoding.ASCII.GetString(e.Data));
        }

        protected override void OnRequested(ActionGetter actionGetter, BaseGameResponse response)
        {
            try
            {
                var actionId = actionGetter.GetActionId();
                var uid = actionGetter.GetUserId();
                Console.WriteLine("Action{0} from {1}", actionId, uid);
                ActionFactory.Request(actionGetter, response, GetUser);

            }
            catch (Exception ex)
            {
                TraceLog.WriteError("{0}", ex);
            }
        }

        protected override void OnStartAffer()
        {
            LoadUnlineUser();
        }

        protected override void OnServiceStop()
        {
            GameEnvironment.Stop();
        }

        private void LoadUnlineUser()
        {
            TraceLog.ReleaseWrite("正在加载玩家数据...");
            List<string> userList = new List<string>();
            try
            {
                int loadUnlineDay = ConfigUtils.GetSetting("LoadUnlineDay", "1").ToInt();
                int maxCount = ConfigUtils.GetSetting("MaxLoadCount", "100").ToInt();
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
                var command = dbProvider.CreateCommandStruct("GameUser", CommandMode.Inquiry);
                command.Columns = dbProvider.FormatQueryColumn(",", new string[] { "UserID" });
                command.ToIndex = maxCount;
                command.OrderBy = "UserID desc";
                command.Parser();
                command.Filter = dbProvider.CreateCommandFilter();
                command.Filter.Condition = dbProvider.FormatFilterParam("UserBirthday", ">");
                var param = dbProvider.CreateParameter("UserBirthday", DateTime.Now.Date.AddDays(-loadUnlineDay));
                command.Filter.AddParam(param);
                command.Parser();

                using (IDataReader reader = dbProvider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
                {
                    while (reader.Read())
                    {
                        userList.Add(reader["UserID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError("LoadUnlineUser:{0}", ex);
            }
            var cacheSet = new GameDataCacheSet<GameUser>();
 
            foreach (string userId in userList)
            {
                cacheSet.FindKey(userId);
            }
            TraceLog.ReleaseWrite("正在加载玩家结束");
        }
    }
}
