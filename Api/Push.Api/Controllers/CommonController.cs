using Platform.Core.Filter;
using Push.Api.DTOs;
using Push.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sherlock.Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Push.Service.ConfigService.Service;
using Push.Service.ConfigService.DomainModel;

namespace Push.Api.Controllers
{
    /// <summary>
    /// 公共方法写在这儿
    /// </summary>
    [Route("api/Push")]
    public class CommonController : SherlockApiController
    {
        private Lazy<IInfraDicService> _infraDicServiceLazy = null;


        public CommonController()
        {
            _infraDicServiceLazy = new Lazy<IInfraDicService>(() => WorkContext.Resolve<IInfraDicService>());
        }

        /// <summary>
        /// 手机品牌
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet, Route("Brand")]
        public async Task<object> BrandAsync()
        {
            List<InfraDicDomainModel> list = await _infraDicServiceLazy.Value.GetDicListByTypeAsync("BrandId");
            if (list?.Count == 0)
            {
                return this.Error(ErrCode.DataIsnotExist);
            }
            var ret = from item in list
                      select new
                      {
                          Brand = item.Value,
                          Description = item.Memo
                      };
            return this.Success(ret);
        }
    }
}
