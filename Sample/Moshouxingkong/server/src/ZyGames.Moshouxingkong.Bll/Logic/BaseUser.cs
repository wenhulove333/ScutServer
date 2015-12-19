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
    public class BriefUserInfo
    {
        private int _userid;
        private bool _isvalid;

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public int UserId
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }

        /// <summary>
        /// 用户是否合法
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isvalid;
            }
            set
            {
                _isvalid = value;
            }
        }
    }

    public abstract class BaseUser
    {
        protected GameUser _userinfo = null;
        protected BriefUserInfo _buserinfo = null;

        public GameUser UserInfo
        {
            get
            {
                return _userinfo;
            }
            set
            {
                _userinfo = value;
            }
        }

        public BriefUserInfo BUserInfo
        {
            get
            {
                return _buserinfo;
            }
            set
            {
                _buserinfo = value;
            }
        }

        public BaseUser(GameUser user)
        {
            _userinfo = new GameUser(user.UserId);

            /*传入客户端上报过来的用户信息*/
            _userinfo.UserName = user.UserName;
            _userinfo.UserType = user.UserType;
            _userinfo.ClientOS = user.ClientOS;
            _userinfo.ClientVersion = user.ClientVersion;
            _userinfo.UserMagic = RandomGen.Instance.UserMagic;
            _userinfo.UserBirthday = DateTime.Now;

            _buserinfo = new BriefUserInfo();
        }

        /// <summary>
        /// 检测是否存在用户
        /// </summary>
        public abstract bool IsExistGameUser();

        /// <summary>
        /// 创建用户
        /// </summary>
        public GameUser CreateGameuser()
        {
            GameUser newuser = new GameUser(_buserinfo.UserId);
            newuser.UserName = _userinfo.UserName;
            newuser.UserType = _userinfo.UserType;
            newuser.ClientOS = _userinfo.ClientOS;
            newuser.ClientVersion = _userinfo.ClientVersion;
            newuser.UserMagic = _userinfo.UserMagic;
            newuser.UserBirthday = _userinfo.UserBirthday;
            var cacheSet = new GameDataCacheSet<GameUser>();
            cacheSet.Add(newuser);
            cacheSet.Update();

            return _userinfo;
        }

        /// <summary>
        /// 从缓存中获取用户信息
        /// </summary>
        public GameUser GetGameuserInfo()
        {
            var usercache = new GameDataCacheSet<GameUser>().FindKey(_buserinfo.UserId.ToString());

            return usercache;
        }
    }
}
