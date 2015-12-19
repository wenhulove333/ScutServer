using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ZyGames.Moshouxingkong.Model;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Security;
using ZyGames.Framework.Data;
using ZyGames.Framework.Data.Sql;
using ZyGames.Framework.Game.Runtime;

namespace ZyGames.Moshouxingkong.Bll.Logic
{
    class GameUserRef
    {
        /// <summary>
        /// 用户结构信息
        /// </summary>
        /// 
        private GameUser _gameuser;
        public GameUser Gameuser
        {
            get;
            set;
        }

        int _userid;

        public GameUserRef(GameUser gameuser)
        {
            _gameuser = gameuser;
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        public int GetUserId()
        {
            var command = DirectGameUserRefDBAccessManager.Provider.CreateCommandStruct("GameUserRef", CommandMode.Inquiry);
            command.OrderBy = "USERID ASC";
            command.Columns = "USERID";
            command.Filter = DirectGameUserRefDBAccessManager.Provider.CreateCommandFilter();

            command.Filter.Condition = string.Format("{0} AND {1}",
                    command.Filter.FormatExpression("UserName"),
                    command.Filter.FormatExpression("UserType")
                );
            command.Filter.AddParam("UserName", _gameuser.UserName);
            command.Filter.AddParam("UserType", _gameuser.UserType);

            command.Parser();

            using (var aReader = DirectGameUserRefDBAccessManager.Provider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
            {
                if (aReader.Read())
                {
                    try
                    {
                        _userid = Convert.ToInt32(aReader["userid"]);
                    }
                    catch (Exception ex)
                    {
                        TraceLog.WriteError("GetUserId method error:{0}, sql:{0}", ex, command.Sql);
                    }
                    return _userid;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 向用户索引表中插入用户信息
        /// </summary>
        public int InsertGameUser()
        {
            var command = DirectGameUserRefDBAccessManager.Provider.CreateCommandStruct("GameUserRef", CommandMode.Insert);
            command.ReturnIdentity = true;
            command.AddParameter("UserName", _gameuser.UserName);
            command.AddParameter("UserType", _gameuser.UserType);
            command.AddParameter("UserBirthday", _gameuser.UserBirthday);
            command.Parser();

            try
            {
                using (var aReader = DirectGameUserRefDBAccessManager.Provider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
                {
                    if (aReader.Read())
                    {
                        _userid = Convert.ToInt32(aReader[0]);
                    }
                }
                return _userid;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新用户索引表中用户信息
        /// </summary>
        public int UpdateGameUser()
        {
            var command = DirectGameUserRefDBAccessManager.Provider.CreateCommandStruct("GameUserRef", CommandMode.Modify);
            command.Filter = DirectGameUserRefDBAccessManager.Provider.CreateCommandFilter();

            command.Filter.Condition = string.Format("{0}",
                    command.Filter.FormatExpression("UserId")
                );
            command.Filter.AddParam("UserId", _gameuser.UserId);

            command.AddParameter("UserName", _gameuser.UserName);
            command.AddParameter("UserType", _gameuser.UserType);

            command.Parser();

            using (var aReader = DirectGameUserRefDBAccessManager.Provider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
            {
                if (aReader.Read())
                {
                    try
                    {
                        
                    }
                    catch (Exception ex)
                    {
                        TraceLog.WriteError("GetUserId method error:{0}, sql:{0}", ex, command.Sql);
                    }
                    return _userid;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
