using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Moshouxingkong.Model;
using ZyGames.Moshouxingkong.Lang;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Cache;

namespace ZyGames.Moshouxingkong.Bll.Logic
{
    class ExternalAccountTypeUser : BaseUser
    {
        public ExternalAccountTypeUser(GameUser user): base (user)
        {
        }

        public override bool IsExistGameUser()
        {
            var cacheSet = new GameDataCacheSet<GameUser>();
            GameUserRef userref = new GameUserRef(_userinfo);

            /*首先检测是否是游客账号转正式账号*/
            if (0 != _userinfo.UserId)
            {
                var guestusercache = cacheSet.FindKey(_userinfo.UserId.ToString());
                if (null != guestusercache)
                {
                    if (guestusercache.UserType == "guest")
                    {
                        guestusercache.UserType = _userinfo.UserType;
                        guestusercache.UserName = _userinfo.UserName;
                        cacheSet.Add(guestusercache);
                        cacheSet.Update();
                        /*更新索引表*/
                        userref.UpdateGameUser();
                        _buserinfo.IsValid = true;
                        return true;
                    }
                }
            }

            /*直接通过直接访问数据库的方式查询用户*/  
            _buserinfo.UserId = userref.GetUserId();
            if (0 == _buserinfo.UserId)
            {
                _buserinfo.UserId = userref.InsertGameUser();
                if (0 == _buserinfo.UserId)
                {
                    _buserinfo.IsValid = false;
                    return false;
                }
            }
            else
            {
                /*客户端上送的用户信息是错的*/
                if ((0 != _userinfo.UserId)
                    && (_userinfo.UserId != _buserinfo.UserId))
                {
                    _buserinfo.IsValid = false;
                    return false;
                }
            }

            /*尝试刷新缓存,并且检测用户是否给的UserID是否正确*/
            var usercache = new GameDataCacheSet<GameUser>().FindKey(_buserinfo.UserId.ToString());
            if (null != usercache)
            {
                _buserinfo.IsValid = true;
                return true;
            }
            /*插入新的用户*/
            else
            {
                _buserinfo.IsValid = true;
                return false;
            }
        }
    }
}
