##坑点
1、Q:SherlockApiController继承了ControllerBase，所有的api返回对象使用object。
   A:因为SchuberApiController中去掉了WebApiCompatShim，返回的HtppResponseMessage是全部的信息，信息量有点大

2、Q:启动项中要把底层的用到的Service层的类库都要引用进去。
   A:不引用，扫描不到底层的module.json，还有以前的间接引用不正确。

3、Q:数据库字符串字符集 Charset=utf8
   A:不加，插入数据库中的中文会乱码

4、Q:API路由参数问题，SendProcessByBatchNO/{batchNo}，方法获取不到batchNo
   A:架构升级到3.0.02就没有问题了

5、启动项里增加了打包配置项，将各个模块打包的包中：
 <ItemGroup>
    <DotNetCliToolReference Include="Sherlock.Framework.Modularity.Tools.Vs2017" Version="*" />
  </ItemGroup>
  <Target Name="Modularity" AfterTargets="AfterPublish">
    <Message Text="publishUrl=$(publishUrl)"></Message>
    <Exec Command="dotnet modularity --dest $(publishUrl)"></Exec>
  </Target>
6、目前测试和产线服务器，Framework的版本是4.5，到官网下载和安装4.6.1，应用才能运行


##进度
1、注册api已经完成
2、注销api已经完成
3、接受推送信息api已经完成
4、将数据丢入到redis队列中
5、新的线程处理队列中的数据
6、获取批次号完成
7、通过批次号推送消息完成
8、使用Eys写日志完成


