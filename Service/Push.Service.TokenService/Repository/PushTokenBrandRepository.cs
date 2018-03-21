using Dapper;
using Push.Service.TokenService.DBModel;
using Push.Service.TokenService.DomainModel;
using Microsoft.Extensions.Logging;
using Sherlock.Framework;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.TokenService.Repository
{
    /// <summary>
    /// 手机设备注册
    /// </summary>
    public class PushTokenBrandRepository : DapperRepository<PushTokenBrand>, IPushTokenBrandRepository
    {
        public PushTokenBrandRepository(DapperContext dapperContext, ILoggerFactory loggerFactory = null) : base(dapperContext, loggerFactory)
        {

        }

        /// <summary>
        /// 通过消息的Token和AppId获取设备通道信息（ChannelId, URL, SystemType, AppKey, AppSecret）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<List<DeviceChannelDomainModel>> GetDeviceChannelListAsync(string token, long appId)
        {
            var sql = @"SELECT T.id,PZBD.device_token,T.system_type,T.brand_id,PC.channel_name,PZBD.channel_id,PC.url,PAC.app_key,PAC.app_secret 
                    FROM push_token_brand_detail PZBD
                    INNER JOIN(
                    SELECT PRB.id, PRB.token, PRB.app_id, PRB.brand_id, PRB.system_type, IFNULL(PBC.channel_id, PBCT.channel_id) AS channel_id
                    FROM push_token_brand PRB
                    LEFT JOIN push_brand_channel PBC ON PBC.brand_id = PRB.brand_id AND PBC.app_id=PRB.app_id
                    INNER JOIN push_brand_channel PBCT ON PBCT.brand_id = 0 AND PRB.app_id=PBCT.app_id
                    WHERE PRB.token = @Token AND PRB.app_id = @AppId
                    ) T ON PZBD.token_brand_id = T.id AND PZBD.channel_id = T.channel_id
                    INNER JOIN push_channel PC ON PC.id = PZBD.channel_id AND PC.is_active = 1
                    INNER JOIN push_app_channel PAC ON PAC.app_id = T.app_id AND PAC.system_type = T.system_type AND PAC.channel_id = PC.id";

            var param = new Dictionary<string, object>()
            {
                {"Token",token },
                { "AppId" ,appId}
            };
            var res = await Context.GetConnection().QueryAsync<DeviceChannelDomainModel>(sql, param);
            return res.ToList();
        }

        /// <summary>
        /// 通过消息的Token和AppId单个获取设备通道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeviceChannelDomainModel> GetDeviceChannelListByTokenBrandIdAsync(long id)
        {
            var sql = "SELECT T.id,PZBD.device_token,T.system_type,T.brand_id,PC.channel_name,PZBD.channel_id,PC.url,PAC.app_key,PAC.app_secret "
                    + "FROM push_token_brand_detail PZBD "
                    + "INNER JOIN ( "
                    + "SELECT PRB.id,PRB.token,PRB.app_id,PRB.brand_id,PRB.system_type,IFNULL(PBC.channel_id, 1) as channel_id "
                    + "FROM push_token_brand PRB "
                    + "LEFT JOIN push_brand_channel PBC ON PBC.brand_id=PRB.brand_id "
                    + "WHERE PRB.id=@Id "
                    + ") T ON PZBD.token_brand_id=T.id AND PZBD.channel_id=T.channel_id "
                    + "INNER JOIN push_channel PC ON PC.id=PZBD.channel_id AND PC.is_active=1 "
                    + "INNER JOIN push_app_channel PAC ON PAC.app_id=T.app_id AND PAC.system_type=T.system_type AND PAC.channel_id=PC.id ";

            var param = new Dictionary<string, object>()
            {
                {"Id",id }
            };
            var res = await Context.GetConnection().QueryAsync<DeviceChannelDomainModel>(sql, param);
            return res.FirstOrDefault();
        }

        /// <summary>
        /// 尝试清空在该设备上注册的以前的用户
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public async Task<int> TryClearDeviceOldUser(string deviceId, string userId)
        {
            var sql = @"DELETE FROM push_token_brand_detail 
                        WHERE token_brand_id in (
                                                SELECT id FROM push_token_brand 
                                                WHERE device_id=@deviceid AND token <> @token
                                                );

                        DELETE FROM push_token_brand 
                        WHERE device_id = @Deviceid and token <> @Token;";

            var param = new Dictionary<string, object>()
            {
                {"Deviceid",deviceId },
                {"Token",userId }
            };

            return await Context.GetConnection().ExecuteAsync(sql, param);
        }
    }
}
