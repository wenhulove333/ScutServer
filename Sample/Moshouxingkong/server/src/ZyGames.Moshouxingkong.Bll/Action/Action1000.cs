using System;
using System.Collections.Generic;
using ZyGames.Moshouxingkong.Bll.Logic;
using ZyGames.Moshouxingkong.Lang;
using ZyGames.Moshouxingkong.Model;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Cache;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Game.Sns;


namespace ZyGames.Moshouxingkong.Bll.Action
{

    /// <summary>
    /// 1000_用户登录
    /// 如果存在用户ID的话，需要提供用户ID；不存在的话，服务器需要直查数据库的方式查找用户
    /// </summary>
    public class Action1000 : BaseStruct
    {
        private string _username;
        private string _clientversion;
        private string _usertype;
        private string _clientos;
        private string _clientarchive;
        private string _usermagic;
        private string _useridreq;
        private string _useridres;

        public Action1000(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action1000, httpGet)
        {
            
        }

        public override void BuildPacket()
        {
            this.PushIntoStack(_clientarchive);
            this.PushIntoStack(_usermagic);
            this.PushIntoStack(_useridres);
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("username", ref _username)            
                && httpGet.GetString("usertype", ref _usertype))
            {
                httpGet.GetString("clientversion", ref _clientversion);
                httpGet.GetString("clientos", ref _clientos);
                httpGet.GetString("userid", ref _useridreq);
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            GameUser user = null;
            BaseUser usertype = null;
            int userid = 0;

            if ((null != _useridreq)
                &&("" != _useridreq))
            {
                userid = Convert.ToInt32(_useridreq);
            }

            /*获取用户信息*/
            GameUser userinfo = new GameUser(userid);
            userinfo.UserName = _username;
            userinfo.UserType = _usertype;
            userinfo.ClientOS = _clientos;
            userinfo.ClientVersion = _clientversion;

            if (userinfo.UserType != "guest")
            {
                usertype = new ExternalAccountTypeUser(userinfo);
            }
            else
            {
                usertype = new GuestTypeUser(userinfo);
            }

            /*插入新的用户*/
            if (false == usertype.IsExistGameUser()
                && (true == usertype.BUserInfo.IsValid))
            {
                usertype.CreateGameuser();
            }

            if (false == usertype.BUserInfo.IsValid)
            {
                return false;
            }

            user = usertype.GetGameuserInfo();

            /*返回UserID信息、Magic以及存档信息（如果有的话）给客户端*/
            if (null != user)
            {
                _usermagic = user.UserMagic;
                _useridres = user.UserId.ToString();
                _clientarchive = user.ClientArchive;
                return true;
            }

            return false;
        }
    }
}