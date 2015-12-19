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
    class GuestTypeUser : BaseUser
    {
        public GuestTypeUser(GameUser user) : base (user)
        {

        }

        public override bool IsExistGameUser()
        {
            /*直接通过直接访问数据库的方式查询用户*/
            GameUserRef userref = new GameUserRef(_userinfo);
            _buserinfo.UserId = userref.GetUserId();
            if (0 == _buserinfo.UserId)
            {
                _buserinfo.UserId = userref.InsertGameUser();
                if (0 == _buserinfo.UserId)
                {
                    _buserinfo.IsValid = false;
                    return false;
                }

                _userinfo.UserName = _buserinfo.UserId.ToString();

                /*更新用户索引表*/
                GameUser updateuser = new GameUser(_buserinfo.UserId);
                updateuser.UserName = _buserinfo.UserId.ToString();
                updateuser.UserType = _userinfo.UserType;
                GameUserRef userupdateref = new GameUserRef(updateuser);
                userupdateref.UpdateGameUser();
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
