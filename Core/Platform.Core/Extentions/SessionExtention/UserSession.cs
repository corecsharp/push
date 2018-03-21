using Sherlock.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core.Extentions
{
    public class UserSession : IUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 用户企业Id
        /// </summary>
        public long EnterpriseId { get; set; }

        /// <summary>
        /// 用户企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 用户账户号码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户类型，0普通用户，1地勤用户
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 推荐码
        /// </summary>
        public string RecommendCode { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 页面展示的用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }


        public string NormalizedUserName { get; set; }

        /// <summary>
        /// sessionId
        /// </summary>
        public string SecurityStamp
        {
            get;

            set;
        }

        public string PasswordHash
        {
            get
            {
                return null;
            }

            set
            {
                ;
            }
        }

        public string Email
        {
            get
            {
                return null;
            }

            set
            {
                ;
            }
        }



        public string Language
        {
            get
            {
                return null;
            }

            set
            {
                ;
            }
        }

        public string TimeZone
        {
            get
            {
                return null;
            }

            set
            {
                ;
            }
        }

        public bool EmailConfirmed
        {
            get
            {
                return default(bool);
            }

            set
            {
                ;
            }
        }

        public bool PhoneNumberConfirmed
        {
            get
            {
                return default(bool);
            }

            set
            {
                ;
            }
        }

        public DateTime? LockoutEnd
        {
            get
            {
                return default(DateTime?);
            }

            set
            {
                ;
            }
        }

        public bool LockoutEnabled
        {
            get
            {
                return default(bool);
            }

            set
            {
                ;
            }
        }

        public int AccessFailedCount
        {
            get
            {
                return default(int);
            }

            set
            {
                ;
            }
        }


    }
}
