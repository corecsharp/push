using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Push.Service.MessageCenterService.DomainModel;
using Push.Service.MessageCenterService.Repository;
using AutoMapper;
using Sherlock.Framework.Services;
using Sherlock;
using Push.Service.MessageCenterService.DBModel;
using Platform.Core.Helper;
using Sherlock.Framework.Events;
using Sherlock.Framework.Environment;
using Sherlock.Framework.Data;
using Push.Service.MessageCenterService.Enum;
using System.Linq;

namespace Push.Service.MessageCenterService.Service
{
    public class PushMessageService : IPushMessageService
    {

        private readonly IPushMessageRepository _pushMessageRepository;
        private readonly IMapper _mapper;
        private readonly IIdGenerationService _idGenerationService;

        public PushMessageService(IPushMessageRepository pushMessageRepository, IMapper mapper, IIdGenerationService idGenerationService)
        {
            _pushMessageRepository = pushMessageRepository;
            _mapper = mapper;
            _idGenerationService = idGenerationService;

        }
        /// <summary>
        /// 通过事件的方式，将推送消息写入到消息中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>

        [EventSubscription("AddMessageToMessageCenter")]
        public async Task AddPushMessageAsync(object sender, object args)
        {
            using (var scope = SherlockEngine.Current.CreateScope())
            {
                PushMessageDomainModel pushMessageDomainModel = (PushMessageDomainModel)args;
                Guard.ArgumentNotNull(pushMessageDomainModel, nameof(pushMessageDomainModel));
                PushMessage entity = _mapper.Map<PushMessage>(pushMessageDomainModel);
                entity.Id = _idGenerationService.GenerateId();
                entity.CreateAt = entity.UpdateAt = DateTimeHelper.GetNow();
                await _pushMessageRepository.InsertAsync(entity);
            }
        }

        /// <summary>
        /// 批量设置消息阅读状态为已读。
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int> BatchSetReadAsync(long[] ids, long userId, int appId)
        {
            Guard.ArgumentNotNullOrEmptyArray(ids, nameof(ids));
            var res = await _pushMessageRepository.BatchSetReadAsync(ids, userId, appId);
            return res;
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public async Task<int> ClearPushMessageAsync(long userId, int appId)
        {
            Guard.ArgumentCondition(userId > 0, $"{nameof(userId)}参数不合法");
            Guard.ArgumentCondition(appId > 0, $"{nameof(appId)}参数不合法");
            SingleQueryFilter filter = new SingleQueryFilter();
            filter.AddNotEqual(nameof(PushMessage.State), (int)ReadState.Clear);
            filter.AddEqual(nameof(PushMessage.UserId), userId);
            filter.AddEqual(nameof(PushMessage.AppId), appId);
            int res = await _pushMessageRepository.UpdateAsync(filter, new List<KeyValuePair<string, object>>(){
                new KeyValuePair<string, object>(nameof(PushMessage.State), (int)ReadState.Clear),
                new KeyValuePair<string, object>(nameof(PushMessage.UpdateId), userId),
                new KeyValuePair<string, object>(nameof(PushMessage.UpdateAt), DateTimeHelper.GetNow())
            });
            return res;
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PushMessageDomainModel> GetPushMessageAsync(long id, long userId, int appId)
        {
            Guard.ArgumentCondition(id > 0, $"{nameof(id)}参数不合法");
            Guard.ArgumentCondition(userId > 0, $"{nameof(userId)}参数不合法");
            Guard.ArgumentCondition(appId > 0, $"{nameof(appId)}参数不合法");
            SingleQueryFilter filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushMessage.Id), id);
            filter.AddEqual(nameof(PushMessage.UserId), userId);
            filter.AddEqual(nameof(PushMessage.AppId), appId);
            filter.AddNotEqual(nameof(PushMessage.State), (int)ReadState.Clear);
            PushMessage entity = await _pushMessageRepository.QueryFirstOrDefaultAsync(filter);
            PushMessageDomainModel model = _mapper.Map<PushMessageDomainModel>(entity);
            return model;
        }
        /// <summary>
        /// 通过消息类型获取消息列表（包括已读和未读）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>

        public async Task<IList<PushMessageDomainModel>> GetPushMessageListByMessageTypeAsync(long userId, int appId, int messageType)
        {
            Guard.ArgumentCondition(userId > 0, $"{nameof(userId)}参数不合法");
            Guard.ArgumentCondition(appId > 0, $"{nameof(appId)}参数不合法");
            SingleQueryFilter filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushMessage.UserId), userId);
            filter.AddEqual(nameof(PushMessage.AppId), appId);
            filter.AddEqual(nameof(PushMessage.MessageType), messageType);
            filter.AddNotEqual(nameof(PushMessage.State), (int)ReadState.Clear);
            var list = await _pushMessageRepository.QueryAsync(filter);
            var res = _mapper.Map<IList<PushMessageDomainModel>>(list);
            return res;
        }

        /// <summary>
        /// 获取消息中心，多少个消息类型，就有多少条
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<IList<PushMessageTypeDomainModel>> GetPushMessageListGroupByMessageTypeAsync(long userId, int appId)
        {

            Guard.ArgumentCondition(userId > 0, $"{nameof(userId)}参数不合法");
            Guard.ArgumentCondition(appId > 0, $"{nameof(appId)}参数不合法");
            SingleQueryFilter filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushMessage.UserId), userId);
            filter.AddEqual(nameof(PushMessage.AppId), appId);
            filter.AddNotEqual(nameof(PushMessage.State), (int)ReadState.Clear);
            var list = await _pushMessageRepository.QueryAsync(filter);
            if (list == null) return null;
            IEnumerable<int> messageTypeList = list.Select(t => t.MessageType).Distinct().ToList();
            if (messageTypeList == null) return null;
            IList<PushMessageTypeDomainModel> typeList = new List<PushMessageTypeDomainModel>();
            foreach (var item in messageTypeList)
            {
                var pushMessage = list.Where(t => t.MessageType == item).OrderByDescending(t => t.CreateAt).FirstOrDefault();
                PushMessageTypeDomainModel model = new PushMessageTypeDomainModel { Message = pushMessage.Msg, MessageType = item };
                typeList.Add(model);
            }
            return typeList.OrderBy(t => t.MessageType).ToList();
        }

        /// <summary>
        /// 根据主键读取消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> ReadPushMessageAsync(long id, long userId, int appId)
        {
            Guard.ArgumentCondition(id > 0, $"{nameof(id)}参数不合法");
            Guard.ArgumentCondition(userId > 0, $"{nameof(userId)}参数不合法");
            Guard.ArgumentCondition(appId > 0, $"{nameof(appId)}参数不合法");
            SingleQueryFilter filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushMessage.Id), id);
            filter.AddEqual(nameof(PushMessage.UserId), userId);
            filter.AddEqual(nameof(PushMessage.AppId), appId);
            filter.AddEqual(nameof(PushMessage.State), (int)ReadState.UnRead);
            PushMessage entity = await _pushMessageRepository.QueryFirstOrDefaultAsync(filter);
            if (entity == null)
            {
                return 0;
            }
            entity.UpdateId = userId;
            entity.UpdateAt = DateTimeHelper.GetNow();
            entity.State = (int)ReadState.Read;
            return await _pushMessageRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 未读消息条数（可以给IOS那边打上消息个数）
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>

        public async Task<int> UnReadCountAsync(long userId, int appId)
        {
            Guard.ArgumentCondition(userId > 0, $"{nameof(userId)}参数不合法");
            Guard.ArgumentCondition(appId > 0, $"{nameof(appId)}参数不合法");
            SingleQueryFilter filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushMessage.UserId), userId);
            filter.AddEqual(nameof(PushMessage.AppId), appId);
            filter.AddEqual(nameof(PushMessage.State), (int)ReadState.UnRead);
            int count = await _pushMessageRepository.CountAsync(filter);
            return count;
        }
    }
}
