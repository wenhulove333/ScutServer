using System;
using ZyGames.Moshouxingkong.Bll;
using ZyGames.Moshouxingkong.Lang;
using ZyGames.Moshouxingkong.Model;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace ZyGames.Moshouxingkong.Bll.Action
{
   
    /// <summary>
    /// 1001_上报用户存档信息
    /// </summary>
    public class Action1001 : BaseStruct
    {
        private string _username;
        private string _usertype;
        private string _clientarchive;
        private string _useridreq;
        

        public Action1001(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1001, httpGet)
        {
            
        }

        public override void BuildPacket()
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("username", ref _username)            
                && httpGet.GetString("usertype", ref _usertype)
                && httpGet.GetString("userid", ref _useridreq) 
                && httpGet.GetString("clientarchive", ref _clientarchive))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var cacheSet = new GameDataCacheSet<GameUser>();

            GameUser user = cacheSet.FindKey(_useridreq);
            if (null != user)
            {
                if ((user.UserName != _username)
                    || (user.UserType != _usertype))
                {
                    return false;
                }

                /*更新客户端上报的存档信息*/
                user.ClientArchive = _clientarchive;

                cacheSet.Add(user);
                cacheSet.Update();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
