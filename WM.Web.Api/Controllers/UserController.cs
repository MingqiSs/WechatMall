using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Config;
using WM.Infrastructure.Models;
using WM.Service.App.Dto.WebDto.RP;
using WM.Service.App.Dto.WebDto.RQ;
using WM.Service.App.Interface;
using WM.Web.Api.Configurations;

namespace WM.Web.Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly TokenManagement _tokenManagement;
        public UserController(ILogger<UserController> logger, IUserService userService, IOptions<TokenManagement> tokenManagement)
        {
            _logger = logger;
            _userService = userService;
            _tokenManagement = tokenManagement.Value;
        }
        /// <summary>
        ///登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("M101")]
        [ProducesResponseType(typeof(ResultDto<UserLoginRP>), 200)]
        public IActionResult Login(UserLoginRQ rq)
        {
            var r = LoginInterface(rq);

            return Ok(r);
        }
        /// <summary>
        ///注册
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("M102")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Register(RegisterRQ rq)
        {
            var r = _userService.Register(rq);
            if (r.Res == 1 && r.Dt)
            {
                var loginR = LoginInterface(new UserLoginRQ
                {
                    Name = rq.Name,
                    Password = rq.Pwd,
                });
                return Ok(loginR);
            }
            return Ok(r);
        }
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("M103")]
        [ProducesResponseType(typeof(ResultDto<bool>), 200)]
        public IActionResult WeChatLogin()
        {
            // TODO
            return Ok(new ResultDto<bool>(ResponseCode.sys_param_format_error, "test"));
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("W104")]
        [ProducesResponseType(typeof(UserInfoRP), 200)]
        public IActionResult GetUserInfo()
        {
          var r=  _userService.GetUserInfo(User.GetToken().UID);
            return Ok(r);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("W105")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult ModifyUserInfo(ModifyUserInfoRQ rq)
        {
            var r = _userService.ModifyUserInfo(User.GetToken().UID,rq);
            return Ok(r);
        }
        /// <summary>
        /// 获取收货地址列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("W106")]
        [ProducesResponseType(typeof(List<UserShoppingAddressRP>), 200)]
        public IActionResult GetUserShoppingAddress()
        {
            var r = _userService.GetUserShoppingAddress(User.GetToken().UID);
            return Ok(r);
        }
        /// <summary>
        /// 新增或修改收货地址
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("W107")]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult AddOrUpdateUserShoppingAddress(UserShoppingAddressRQ rq)
        {
            var r = _userService.AddOrUpdateUserShoppingAddress(User.GetToken().UID, rq);
            return Ok(r);
        }

        #region Common
        private ResultDto<UserLoginRP> LoginInterface(UserLoginRQ rq)
        {
            var r = _userService.Login(rq);
            if (r.Res != 1)
            {
                return r;
            }
            var userToken = new UserToken
            {
                IP = "",
                IMEI = "",
                Channel = "",
                Version = "",
                UID = r.Dt.uid,
                Name = r.Dt.Name,
                Email = r.Dt.Email,
                Mobile = r.Dt.Mobile,
                MobileArea = "",
            };
            string token;

            //if (IsAuthenticated(userToken, out token))

            token = userToken.Serialization(_tokenManagement);

            HttpContext.Response.Headers.Add("Authorization", new StringValues(token));

            return r;
        }


        //private bool IsAuthenticated(UserToken userToken, out string token)
        //{
        //    var claims = new[]
        //    {
        //          new Claim(JwtRegisteredClaimNames.Sub, userToken.Email??""),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(),ClaimValueTypes.Integer64),
        //            new Claim(JwtRegisteredClaimNames.UniqueName, userToken.UID.ToString()),
        //            new Claim("name",userToken.Name?? ""),
        //            new Claim("imei", userToken.IMEI ?? ""),
        //            new Claim("version", userToken.Version),
        //            new Claim("email", userToken.Email ?? ""),
        //            new Claim("mobile", userToken.Mobile?? ""),
        //            new Claim("mobilearea", userToken.MobileArea),
        //            new Claim("logintype", ((int)userToken.LoginType).ToString()),
        //            new Claim("platform", ((int)userToken.Platform).ToString()),
        //            new Claim("channel", userToken.Channel??""),
        //            new Claim("ip", userToken.IP??""),
        //            new Claim(ClaimTypes.UserData, "UserData"),
        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration), signingCredentials: credentials);

        //    token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        //    return true;

        //}
        #endregion
    }
}
