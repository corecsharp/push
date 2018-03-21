/*
Navicat MySQL Data Transfer

Source Server         : 要发车test
Source Server Version : 50634
Source Host           : rm-bp1429l3e4gt513wx.mysql.rds.aliyuncs.com:3306
Source Database       : push

Target Server Type    : MYSQL
Target Server Version : 50634
File Encoding         : 65001

Date: 2018-03-21 16:26:35
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for infra_dic
-- ----------------------------
DROP TABLE IF EXISTS `infra_dic`;
CREATE TABLE `infra_dic` (
  `id` bigint(19) NOT NULL,
  `key` bigint(20) NOT NULL COMMENT '键',
  `value` varchar(100) DEFAULT NULL COMMENT '值',
  `type` varchar(20) NOT NULL COMMENT '类型。appid,brandid',
  `index` int(11) DEFAULT NULL COMMENT '排序',
  `status` bit(1) NOT NULL COMMENT '状态',
  `memo` varchar(20) DEFAULT NULL COMMENT '说明',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '跟新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='字典信息，B2B的AppID是1001';

-- ----------------------------
-- Records of infra_dic
-- ----------------------------
INSERT INTO `infra_dic` VALUES ('4373964025499648', '1001', 'B2B国资云农', 'AppId', '1001', '', 'AppId对应的App', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499649', '1002', 'B2C国资商城', 'AppId', '1002', '', 'AppId对应的App', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499650', '0', 'default', 'BrandId', '0', '', '默认手机品牌', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499651', '1', 'apple', 'BrandId', '1', '', 'iphone', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499652', '2', 'xiaomi', 'BrandId', '2', '', '小米', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499653', '3', 'samsung', 'BrandId', '3', '', '三星', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499654', '4', 'huawei', 'BrandId', '4', '', '华为', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499655', '5', 'zte', 'BrandId', '5', '', '中兴', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499656', '6', 'nubia', 'BrandId', '6', '', '中兴努比亚', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499657', '7', 'coolpad', 'BrandId', '7', '', '酷派', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499658', '8', 'lenovo', 'BrandId', '8', '', '联想', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499659', '9', 'meizu', 'BrandId', '9', '', '魅族', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499660', '10', 'htc', 'BrandId', '10', '', 'HTC', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499661', '11', 'oppo', 'BrandId', '11', '', 'OPPO', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499662', '12', 'vivo', 'BrandId', '12', '', 'VIVO', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499663', '13', 'motorola', 'BrandId', '13', '', '摩托罗拉', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499664', '14', 'sony', 'BrandId', '14', '', '索尼', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499665', '15', 'lg', 'BrandId', '15', '', 'LG', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499666', '16', 'jinli', 'BrandId', '16', '', '金立', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499667', '17', 'tianyu', 'BrandId', '17', '', '天语', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499668', '18', 'nokia', 'BrandId', '18', '', '诺基亚', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499669', '19', 'meitu', 'BrandId', '19', '', '美图秀秀', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499670', '20', 'google', 'BrandId', '20', '', '谷歌', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499671', '21', 'tcl', 'BrandId', '21', '', 'TCL', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499672', '22', 'chuizi', 'BrandId', '22', '', '锤子手机', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499673', '23', '1+', 'BrandId', '23', '', '一加手机', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499674', '24', 'china mobile', 'BrandId', '24', '', '中国移动', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499675', '25', 'angda', 'BrandId', '25', '', '昂达', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499676', '26', 'banghua', 'BrandId', '26', '', '邦华', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499677', '27', 'bird', 'BrandId', '27', '', '波导', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499678', '28', 'changhong', 'BrandId', '28', '', '长虹', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499679', '29', 'dakele', 'BrandId', '29', '', '大可乐', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499680', '30', 'doov', 'BrandId', '30', '', '朵唯', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499681', '31', 'haier', 'BrandId', '31', '', '海尔', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499682', '32', 'hisense', 'BrandId', '32', '', '海信', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499683', '33', 'konka', 'BrandId', '33', '', '康佳', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499684', '34', 'kubimofang', 'BrandId', '34', '', '酷比魔方', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499685', '35', 'mige', 'BrandId', '35', '', '米歌', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499686', '36', 'ouboxi', 'BrandId', '36', '', '欧博信', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499687', '37', 'ouxi', 'BrandId', '37', '', '欧新', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499688', '38', 'philip', 'BrandId', '38', '', '飞利浦', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499689', '39', 'voto', 'BrandId', '39', '', '维图', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499690', '40', 'xiaolajiao', 'BrandId', '40', '', '小辣椒', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499691', '41', 'xiaxi', 'BrandId', '41', '', '夏新', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499692', '42', 'yitong', 'BrandId', '42', '', '亿通', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499693', '43', 'yuxi', 'BrandId', '43', '', '语信', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499694', '1000', 'B2C国资文旅', 'AppId', '1000', '', 'AppId对应的App', '2017-05-18 16:24:54', '2017-05-18 16:24:54', '0', '0');
INSERT INTO `infra_dic` VALUES ('4373964025499695', '1003', 'B2B国资妙鲜', 'AppId', '1003', '', 'AppId对应的App', '2017-08-21 15:08:00', '2017-08-21 15:08:00', '0', '0');

-- ----------------------------
-- Table structure for push_app_channel
-- ----------------------------
DROP TABLE IF EXISTS `push_app_channel`;
CREATE TABLE `push_app_channel` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `app_id` bigint(20) NOT NULL COMMENT 'app主键id',
  `system_type` int(11) NOT NULL DEFAULT '0' COMMENT '系统类别：0是iOS，1是Android',
  `channel_id` bigint(20) NOT NULL COMMENT '通道主键id',
  `app_key` varchar(100) DEFAULT NULL COMMENT '程序秘钥',
  `app_secret` varchar(100) DEFAULT NULL COMMENT '程序秘钥私钥',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='app走的推送通道，记录了注册过的所有推送通道';

-- ----------------------------
-- Records of push_app_channel
-- ----------------------------
INSERT INTO `push_app_channel` VALUES ('4373954965803008', '1001', '0', '1', '586f2505f29d982f7b001d4f', '6jsosx8jbrypov3nn7hmdt00uq1npgil', '2017-05-23 18:38:14', '2017-05-23 18:38:14', '0', '0');
INSERT INTO `push_app_channel` VALUES ('4373954965803009', '1001', '1', '1', '586f20f4677baa42550018fc', 'zkfgf4d0nfinccsrijrpxxoi0qfrkoaq', '2017-05-23 18:38:14', '2017-05-23 18:38:14', '0', '0');
INSERT INTO `push_app_channel` VALUES ('4373954965803010', '1001', '1', '3', '5781756647883', '29xR5mKhylQXx9Z7DYdqrA==', '2017-05-23 18:38:14', '2017-05-23 18:38:14', '0', '0');
INSERT INTO `push_app_channel` VALUES ('4373954965803011', '1001', '1', '4', '', '', '2017-05-23 18:38:14', '2017-05-23 18:38:14', '0', '0');
INSERT INTO `push_app_channel` VALUES ('4373954965803012', '1003', '0', '1', '59a51949677baa467a000cca', 'tthgyyvheoacglhoefldl7izcwjtntvg', '2017-09-05 10:17:26', '2017-09-05 10:17:26', '0', '0');
INSERT INTO `push_app_channel` VALUES ('4373954965803013', '1003', '1', '1', '59a3ca15734be45d0b00060a', 'yjevz1o8vatqmboe40ahh2iepm7hbax8', '2017-09-05 10:17:26', '2017-09-05 10:17:26', '0', '0');
INSERT INTO `push_app_channel` VALUES ('4373954965803014', '1003', '1', '3', '5581761053514', 'o2T7mQjJFlVdqbfqhA00kw==', '2017-09-05 10:17:26', '2017-09-05 10:17:26', '0', '0');

-- ----------------------------
-- Table structure for push_brand_channel
-- ----------------------------
DROP TABLE IF EXISTS `push_brand_channel`;
CREATE TABLE `push_brand_channel` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `app_id` bigint(20) NOT NULL DEFAULT '0',
  `brand_id` bigint(20) NOT NULL COMMENT '品牌主键id',
  `channel_id` bigint(20) NOT NULL COMMENT '通道主键id',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='手机品牌走的推送通道，入参中给的通道值是无作用的。实际也是根据手机的品牌来决定推送通道的。0代表无法我们库里没有的品牌。';

-- ----------------------------
-- Records of push_brand_channel
-- ----------------------------
INSERT INTO `push_brand_channel` VALUES ('4373954965803012', '1001', '0', '3', '2017-05-27 17:38:47', '2017-06-19 17:38:51', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803013', '1001', '1', '1', '2017-05-27 17:40:08', '2017-05-27 17:40:12', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803014', '1001', '2', '3', '2017-05-27 17:40:35', '2017-05-27 17:40:39', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803015', '1001', '4', '3', '2017-05-27 17:40:56', '2017-05-27 17:41:02', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803016', '1003', '0', '3', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803017', '1003', '1', '1', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803018', '1003', '2', '3', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803019', '1003', '4', '3', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803020', '1005', '0', '3', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803021', '1005', '1', '1', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803022', '1005', '2', '3', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');
INSERT INTO `push_brand_channel` VALUES ('4373954965803023', '1005', '4', '3', '2017-09-30 13:18:52', '2017-09-30 13:18:52', '0', '0');

-- ----------------------------
-- Table structure for push_channel
-- ----------------------------
DROP TABLE IF EXISTS `push_channel`;
CREATE TABLE `push_channel` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `channel_name` varchar(50) NOT NULL COMMENT '通道名称',
  `url` varchar(100) NOT NULL,
  `multi_url` varchar(100) DEFAULT NULL,
  `is_active` bit(1) NOT NULL COMMENT '是否启用',
  `push_num` int(11) DEFAULT NULL,
  `push_time_out` int(11) NOT NULL COMMENT '推送超时值',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='推送通道';

-- ----------------------------
-- Records of push_channel
-- ----------------------------
INSERT INTO `push_channel` VALUES ('1', '友盟推送', 'http://msg.umeng.com/api/send', 'http://msg.umeng.com/api/send', '', '25', '50', '2016-11-24 14:10:28', '2016-12-29 15:12:34', '0', '0');
INSERT INTO `push_channel` VALUES ('2', '苹果推送', 'gateway.push.apple.com', 'gateway.push.apple.com', '', '25', '50', '2016-12-10 14:58:15', '2016-12-29 15:12:34', '0', '0');
INSERT INTO `push_channel` VALUES ('3', '小米推送', 'https://api.xmpush.xiaomi.com/v3/message/regid', 'https://api.xmpush.xiaomi.com/v2/multi_messages/regids', '', '200', '10', '2016-12-08 15:22:35', '2016-12-29 15:12:34', '0', '0');
INSERT INTO `push_channel` VALUES ('4', '华为推送', 'https://api.vmall.com/rest.php', 'https://api.vmall.com/rest.php', '', '25', '50', '2016-12-29 16:04:19', '2016-12-29 16:04:19', '0', '0');

-- ----------------------------
-- Table structure for push_config
-- ----------------------------
DROP TABLE IF EXISTS `push_config`;
CREATE TABLE `push_config` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `config_name` varchar(50) NOT NULL COMMENT '配置名称',
  `config_key` varchar(50) NOT NULL COMMENT '配置键',
  `config_value` varchar(1000) NOT NULL COMMENT '配置值',
  `config_index` int(11) NOT NULL COMMENT '配置排序索引',
  `config_des` varchar(100) NOT NULL COMMENT '配置描述',
  `is_active` bit(1) NOT NULL COMMENT '是否启用',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='推送配置';

-- ----------------------------
-- Records of push_config
-- ----------------------------
INSERT INTO `push_config` VALUES ('1', '推送调试模式', 'ProductionMode', 'false', '1', '推送调试模式', '', '2016-11-24 18:02:28', '2017-01-09 18:19:29', '0', '0');
INSERT INTO `push_config` VALUES ('2', '一次接收推送人上限', 'TokenMaxNum', '500', '2', '一次接收推送人上限', '', '2016-12-09 13:34:29', '2016-12-10 17:31:29', '0', '0');
INSERT INTO `push_config` VALUES ('3', '推送开关', 'IsRealPushMsg', 'true', '2', '推送开关', '', '2016-12-09 13:34:29', '2016-12-15 16:08:43', '0', '0');
INSERT INTO `push_config` VALUES ('4', '信号量的数量', 'SemaphoreCount', '4', '4', '信号量的数量', '', '2016-12-20 17:03:19', '2016-12-20 17:03:19', '0', '0');
INSERT INTO `push_config` VALUES ('5', '监控时间段(单位：分钟)', 'MonitorTime', '5', '5', '监控时间段(单位：分钟)', '', '2017-01-06 09:59:53', '2017-01-06 09:59:53', '0', '0');

-- ----------------------------
-- Table structure for push_message
-- ----------------------------
DROP TABLE IF EXISTS `push_message`;
CREATE TABLE `push_message` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `user_id` bigint(20) NOT NULL COMMENT '消息接收人用户id',
  `app_id` bigint(20) NOT NULL COMMENT 'app主键id',
  `title` varchar(100) NOT NULL COMMENT '消息标题',
  `msg` varchar(1000) NOT NULL COMMENT '消息内容',
  `attach_info` varchar(1000) DEFAULT NULL COMMENT '消息图片',
  `state` int(11) NOT NULL DEFAULT '0' COMMENT '阅读状态：0未读，1已读，99消息被清空',
  `message_type` int(11) NOT NULL COMMENT '消息类别：用户自定义',
  `update_at` datetime NOT NULL COMMENT '记录更新时间',
  `update_id` bigint(20) NOT NULL COMMENT '记录更新人',
  `create_at` datetime NOT NULL COMMENT '记录创建时间',
  `create_id` bigint(20) NOT NULL COMMENT '记录创建人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='推送消息，消息中心';

-- ----------------------------
-- Records of push_message
-- ----------------------------
INSERT INTO `push_message` VALUES ('6113679049388032', '4408958819725312', '1001', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '0', '1', '2018-03-20 17:48:58', '0', '2018-03-20 17:48:58', '0');
INSERT INTO `push_message` VALUES ('6113679049388033', '4408958819725312', '1001', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '0', '1', '2018-03-20 17:48:59', '0', '2018-03-20 17:48:59', '0');
INSERT INTO `push_message` VALUES ('6113691531636736', '4408958819725312', '1001', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '0', '1', '2018-03-20 17:51:44', '0', '2018-03-20 17:51:44', '0');
INSERT INTO `push_message` VALUES ('6113790114557952', '4408958819725312', '1001', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '0', '1', '2018-03-20 18:16:12', '0', '2018-03-20 18:16:12', '0');

-- ----------------------------
-- Table structure for push_process_history
-- ----------------------------
DROP TABLE IF EXISTS `push_process_history`;
CREATE TABLE `push_process_history` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `serial_no` varchar(36) NOT NULL,
  `token` varchar(20) NOT NULL,
  `app_id` bigint(20) NOT NULL COMMENT 'app主键id',
  `title` varchar(100) NOT NULL COMMENT '标题',
  `msg` varchar(1000) NOT NULL COMMENT '消息',
  `attach_info` varchar(1000) DEFAULT NULL COMMENT '附加信息',
  `priority_level` int(11) NOT NULL COMMENT '优先级',
  `start_time` datetime NOT NULL COMMENT '开始时间',
  `end_time` datetime NOT NULL COMMENT '结束时间',
  `send_time` datetime NOT NULL COMMENT '发送时间',
  `send_status` int(11) NOT NULL DEFAULT '0' COMMENT '发送状态：0是失败，1是成功',
  `return_sign` varchar(128) DEFAULT NULL COMMENT '返回消息',
  `error_type` int(11) DEFAULT NULL COMMENT '1:尝试次数过多，2：超时，3：账号登出，4：配置信息错误，5：发送到推送平台失败',
  `remark` varchar(200) DEFAULT NULL COMMENT '备注',
  `delay_times` int(11) NOT NULL COMMENT '延迟时间',
  `batch_no` varchar(36) DEFAULT NULL COMMENT '批量号',
  `brand_id` bigint(20) NOT NULL COMMENT '品牌id',
  `channel_id` bigint(20) NOT NULL COMMENT '通道id',
  `device_token` varchar(100) NOT NULL COMMENT '设备令牌',
  `request_time` int(11) NOT NULL COMMENT '请求时间',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='推送消息历史发送记录。记录，无论成功或失败。';

-- ----------------------------
-- Records of push_process_history
-- ----------------------------
INSERT INTO `push_process_history` VALUES ('6113766827782144', '532b1cd7-a836-408c-a40b-33203c7aae1a', '4408958819725312', '1001', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '2', '2018-03-20 17:49:19', '2018-03-21 17:49:19', '2018-03-20 18:10:26', '0', null, '5', 'Code:2054', '0', 'ec8bf91e-601b-4ba8-a74e-83c6931f44e3', '1', '1', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '364', '2018-03-20 18:10:26', '2018-03-20 18:10:26', '0', '0');
INSERT INTO `push_process_history` VALUES ('6113766827782145', 'fcbaf36d-b770-4c91-86ca-99c48277dc46', '4408958819725312', '1001', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '2', '2018-03-20 17:49:19', '2018-03-21 17:49:19', '2018-03-20 18:10:26', '0', null, '5', 'Code:2054', '0', 'ec8bf91e-601b-4ba8-a74e-83c6931f44e3', '1', '1', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '364', '2018-03-20 18:10:26', '2018-03-20 18:10:26', '0', '0');
INSERT INTO `push_process_history` VALUES ('6113766827782146', 'ef036547-5ea8-41b7-91b8-ed2e1881166f', '4408958819725312', '1001', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '2', '2018-03-20 17:51:44', '2018-03-21 17:51:44', '2018-03-20 18:10:26', '0', null, '5', 'Code:2054', '0', 'ec8bf91e-601b-4ba8-a74e-83c6931f44e3', '1', '1', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '364', '2018-03-20 18:10:26', '2018-03-20 18:10:26', '0', '0');

-- ----------------------------
-- Table structure for push_send_process
-- ----------------------------
DROP TABLE IF EXISTS `push_send_process`;
CREATE TABLE `push_send_process` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `serial_no` varchar(36) NOT NULL,
  `token` varchar(20) NOT NULL,
  `app_id` int(11) NOT NULL COMMENT 'app主键id',
  `token_brand_id` bigint(20) DEFAULT NULL,
  `title` varchar(100) NOT NULL COMMENT '标题',
  `msg` varchar(1000) NOT NULL COMMENT '信息',
  `attach_info` varchar(1000) DEFAULT NULL COMMENT '附加信息',
  `priority_level` int(11) NOT NULL COMMENT '优先级',
  `start_time` datetime NOT NULL COMMENT '开始时间',
  `end_time` datetime NOT NULL COMMENT '结束时间',
  `is_used` bit(1) NOT NULL COMMENT '是否使用',
  `expire_time` datetime NOT NULL COMMENT '失效时间',
  `send_time` datetime NOT NULL COMMENT '发送时间',
  `delay_times` int(11) NOT NULL COMMENT '延迟时间',
  `batch_no` varchar(36) DEFAULT NULL COMMENT '批量号',
  `batch_expire_time` datetime DEFAULT NULL COMMENT '批量失效时间',
  `brand_id` int(11) NOT NULL COMMENT '品牌id',
  `channel_id` bigint(20) NOT NULL COMMENT '通道id',
  `device_token` varchar(100) NOT NULL COMMENT '设备号',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='待推送的消息，如果一个token对应多个手机，则一条消息拆成多条去发，如果发送失败，回写待发送消息，记录哪个手机设备发送失败。';

-- ----------------------------
-- Records of push_send_process
-- ----------------------------
INSERT INTO `push_send_process` VALUES ('6113681800851456', '532b1cd7-a836-408c-a40b-33203c7aae1a', '4408958819725312', '1001', '5022162167885830', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '2', '2018-03-20 17:49:19', '2018-03-21 17:49:19', '\0', '2018-03-20 17:49:19', '2018-03-20 18:10:26', '1', '3c417243-5c90-4fc6-8be2-1161d37fb5a8', '2018-03-20 18:19:20', '1', '1', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '2018-03-20 17:49:19', '2018-03-20 17:49:19', '0', '0');
INSERT INTO `push_send_process` VALUES ('6113681800851457', 'fcbaf36d-b770-4c91-86ca-99c48277dc46', '4408958819725312', '1001', '5022162167885830', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '2', '2018-03-20 17:49:19', '2018-03-21 17:49:19', '\0', '2018-03-20 17:49:19', '2018-03-20 18:10:26', '1', '3c417243-5c90-4fc6-8be2-1161d37fb5a8', '2018-03-20 18:19:20', '1', '1', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '2018-03-20 17:49:19', '2018-03-20 17:49:19', '0', '0');
INSERT INTO `push_send_process` VALUES ('6113691531636737', 'ef036547-5ea8-41b7-91b8-ed2e1881166f', '4408958819725312', '1001', '5022162167885830', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '2', '2018-03-20 17:51:44', '2018-03-21 17:51:44', '\0', '2018-03-20 17:51:44', '2018-03-20 18:10:26', '1', '3c417243-5c90-4fc6-8be2-1161d37fb5a8', '2018-03-20 18:19:20', '1', '1', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '2018-03-20 17:51:44', '2018-03-20 17:51:44', '0', '0');
INSERT INTO `push_send_process` VALUES ('6113790114557953', '6d2dc4af-79c4-4a48-83ea-dbfa684d61de', '4408958819725312', '1001', '5022162167885830', '冯伟发来消息', '想我了吗？我是冯伟', '{\"key1\":\"val1\",\"key2\":\"val2\"}', '2', '2018-03-20 18:16:12', '2018-03-21 18:16:12', '\0', '2018-03-20 18:16:12', '2018-03-20 18:16:12', '0', '3c417243-5c90-4fc6-8be2-1161d37fb5a8', '2018-03-20 18:19:20', '1', '1', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '2018-03-20 18:16:12', '2018-03-20 18:16:12', '0', '0');

-- ----------------------------
-- Table structure for push_token_brand
-- ----------------------------
DROP TABLE IF EXISTS `push_token_brand`;
CREATE TABLE `push_token_brand` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `token` varchar(128) NOT NULL COMMENT '令牌',
  `app_id` int(11) NOT NULL COMMENT 'app主键id',
  `device_id` varchar(50) NOT NULL COMMENT '设备id',
  `brand_id` int(11) NOT NULL COMMENT '品牌id',
  `system_type` int(11) NOT NULL COMMENT '系统类别',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='手机设备注册信息表，只有此表中的手机才可收到推送';

-- ----------------------------
-- Records of push_token_brand
-- ----------------------------
INSERT INTO `push_token_brand` VALUES ('5018703981379595', '4918362878996480', '1003', 'FEDEFFCC-33FD-4DB1-B1C3-1CD658F933D7', '1', '0', '2017-09-12 21:28:38', '2017-09-12 21:28:38', '0', '0');
INSERT INTO `push_token_brand` VALUES ('5018704182706186', '5011240207740928', '1003', '1808fa699c57b4ca', '2', '1', '2017-09-12 21:28:40', '2017-09-12 21:28:40', '0', '0');
INSERT INTO `push_token_brand` VALUES ('5018704249815050', '4918362878996480', '1003', 'ae5a3709a9ff5dec', '11', '1', '2017-09-12 21:28:42', '2017-09-12 21:28:42', '0', '0');
INSERT INTO `push_token_brand` VALUES ('5022162167885830', '4408958819725312', '1001', 'D718107D-EBE9-4943-99DD-C8DBC630418D', '1', '0', '2017-09-13 11:47:28', '2017-09-13 11:47:28', '0', '0');
INSERT INTO `push_token_brand` VALUES ('5022195185446918', '4333160700014592', '1001', 'de5df8ffd24c1505', '2', '1', '2017-09-13 11:55:40', '2017-09-13 11:55:40', '0', '0');
INSERT INTO `push_token_brand` VALUES ('5022228739878921', '4727896651821056', '1001', '15a8e1d5ec113228', '12', '1', '2017-09-13 12:04:01', '2017-09-13 12:04:01', '0', '0');
INSERT INTO `push_token_brand` VALUES ('5068910874267648', '5010470868402176', '1003', '745959b280c96808', '9', '1', '2017-09-21 13:17:39', '2017-09-21 13:17:39', '0', '0');

-- ----------------------------
-- Table structure for push_token_brand_detail
-- ----------------------------
DROP TABLE IF EXISTS `push_token_brand_detail`;
CREATE TABLE `push_token_brand_detail` (
  `id` bigint(20) NOT NULL COMMENT '主键id',
  `token_brand_id` bigint(20) NOT NULL,
  `device_token` varchar(100) NOT NULL COMMENT '各个推送平台的devicetoken',
  `channel_id` bigint(20) NOT NULL COMMENT '通道id',
  `create_at` datetime NOT NULL COMMENT '创建时间',
  `update_at` datetime NOT NULL COMMENT '更新时间',
  `create_id` bigint(20) NOT NULL COMMENT '创建人',
  `update_id` bigint(20) NOT NULL COMMENT '更新人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='手机设备注册明细，包括几个推送平台的token';

-- ----------------------------
-- Records of push_token_brand_detail
-- ----------------------------
INSERT INTO `push_token_brand_detail` VALUES ('5018703981379598', '5018703981379595', '05d367aca6da0f2da5ea0da0c9bc6e5890bb9a90cbd9d60202ab306987a45264', '1', '2017-09-12 21:28:38', '2017-09-12 21:28:38', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5018704182706189', '5018704182706186', 'FVQuqLiUT4h4wy5/A4w0gK74W0cPyGkOvOWCRjYbUi4=', '3', '2017-09-12 21:28:40', '2017-09-12 21:28:40', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5018704249815053', '5018704249815050', 'yWkHqS2o+k+HSQsep+BCgXCCptYY9yNShuttzOIbp34=', '3', '2017-09-12 21:28:42', '2017-09-12 21:28:42', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5018704249815056', '5018704249815050', 'AibgpSyCjsjfFZcu2y7iS4wwD8-547kaJmHNLoIJVGWy', '1', '2017-09-12 21:28:42', '2017-09-12 21:28:42', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5022162167885833', '5022162167885830', '2f6237d492619b3ad0f9f9e75494cfd64da3a44fb4323bd4207f50639ea9edd3', '1', '2017-09-13 11:47:28', '2017-09-13 11:47:28', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5022195185446921', '5022195185446918', 'Ah4-3SAWIRnK-jHyKDjmSkS6lIokm7NuSPTFx-70SQ2R', '1', '2017-09-13 11:55:40', '2017-09-13 11:55:40', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5022195185446924', '5022195185446918', '4iUq7u1/ZwElN64p8/npZYT/9p6ZT5DfO6lhpLRKzZM=', '3', '2017-09-13 11:55:40', '2017-09-13 11:55:40', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5022228739878924', '5022228739878921', 'ndMKeUmEsEJxFmBA2Tw+CwFEjSOFkPmESQd7jLANq8A=', '3', '2017-09-13 12:04:01', '2017-09-13 12:04:01', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5068910874267649', '5068910874267648', 'AqR0rZTNeZSSD9veCiqVA5S_SFbxaQeIBUNoVE1B82JW', '1', '2017-09-21 13:17:39', '2017-09-21 13:17:39', '0', '0');
INSERT INTO `push_token_brand_detail` VALUES ('5068910874267650', '5068910874267648', 'oVrXAZkhSzd5lN8iBzMlilSqQviw77hztDBK8YQutXQ=', '3', '2017-09-21 13:17:39', '2017-09-21 13:17:39', '0', '0');
