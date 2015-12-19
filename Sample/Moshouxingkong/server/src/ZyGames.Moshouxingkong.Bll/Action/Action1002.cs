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
    /// 1002_获取用户存档信息
    /// </summary>
    public class Action1002 : BaseStruct
    {
        private string _username;
        private string _usertype;
        private string _clientarchive;
        private string _useridreq;
        

        public Action1002(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1002, httpGet)
        {
            
        }

        public override void BuildPacket()
        {
            this.PushIntoStack(_clientarchive);

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("username", ref _username)
                && httpGet.GetString("userid", ref _useridreq) 
                && httpGet.GetString("usertype", ref _usertype))
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

                _clientarchive = user.ClientArchive;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
