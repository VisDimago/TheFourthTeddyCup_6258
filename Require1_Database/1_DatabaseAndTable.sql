--创建数据库RailWay
Use master
IF EXISTS(SELECT * FROM sysdatabases WHERE name='Railway')
DROP DATABASE Railway
CREATE DATABASE Railway
ON PRIMARY                 --主文件组
(
	NAME='Railway_data',
	FILENAME='F:\SQL\project\E_Market_data.mdf',
	SIZE=10MB,
	MAXSIZE=500MB,
	FILEGROWTH=10% 
)--第一个文件组结束

Use Railway
--附件3
--地区表region
IF EXISTS(SELECT * FROM sysobjects WHERE name='region')
DROP TABLE region
GO
CREATE TABLE region
(
	Region varchar(20) NOT NULL
)
GO
ALTER TABLE region
ADD CONSTRAINT PK_Region PRIMARY KEY (Region)
GO

--附件4
--创建车站所属地区气象状况表weather
IF EXISTS(SELECT * FROM sysobjects WHERE name='weather')
DROP TABLE weather
GO
CREATE TABLE weather
(
	Region varchar(20) NOT NULL,    --地区
	RecordDate date NOT NULL,		--日期
	WeatherStart varchar(10),		--起始天气状况
	WeatherChange varchar(10),		--转换后天气状况
	TemperatureLowest int,			--最低气温
	TemperatureHighest int,			--最高气温
	WindDirectStart varchar(20),	--起始风向
	WindPowerStart varchar(20),		--起始风力
	WindDirectChange varchar(20),	--转换后风向
	WindPowerChange varchar(20)		--转换后风力
)
GO
ALTER TABLE weather WITH NOCHECK ADD 
CONSTRAINT [PK_weather] PRIMARY KEY  NONCLUSTERED 
(
  [Region],
  [RecordDate]
)
ALTER TABLE weather
	ADD CONSTRAINT FK_Region1 FOREIGN KEY(Region) REFERENCES region(Region)	--添加外键约束
GO
-- --删除表内所有数据
-- truncate table weather
-- SELECT * FROM dbo.weather

--附件3
--创建城市区段车站信息表stationInfo
IF EXISTS(SELECT * FROM sysobjects WHERE name='stationInfo')
DROP TABLE stationInfo
GO
CREATE TABLE stationInfo
(
	Region varchar(20) NOT NULL,
	Station varchar(20) NOT NULL,
	InPipe varchar(3) NOT NULL,
	Mileage int
)
GO
ALTER TABLE stationInfo	--给station添加主外键约束
ADD CONSTRAINT PK_Station PRIMARY KEY (Station),
	CONSTRAINT CK_InPipe CHECK(InPipe='是' OR InPipe='否'),
	CONSTRAINT FK_Region2 FOREIGN KEY(Region) REFERENCES  Region(Region)
	GO
-- --取消主键
-- ALTER TABLE stationInfo
-- DROP CONSTRAINT PK_Station
-- GO
-- --修改数据类型
-- alter table stationInfo alter column Station varchar(20) not null
-- SELECT * FROM stationInfo

--附件1
--创建车次信息表trainRecord
IF EXISTS(SELECT * FROM sysobjects WHERE name='trainRecord')
DROP TABLE trainRecord
GO
CREATE TABLE trainRecord
(
	TrainNum varchar(20) NOT NULL,		--车次
	RecordDate date NOT NULL,			--始发日期
	OriginStation varchar(20),			--始发车站
	TerminalStation varchar(20),		--抵达车站
	MaxCount int,						--日均定员
	RealCount int NOT NULL,				--真实总人数
	LoadFactors decimal(4,1)			--客座率
)
GO
ALTER TABLE trainRecord WITH NOCHECK ADD --设置联合主键
CONSTRAINT [PK_trainRecord] PRIMARY KEY  NONCLUSTERED 
(
  [TrainNum],
  [RecordDate]
)
GO
ALTER TABLE trainRecord
ADD CONSTRAINT FK_OriginStation FOREIGN KEY(OriginStation) REFERENCES  stationInfo(Station),
	CONSTRAINT FK_TerminalStation FOREIGN KEY(TerminalStation) REFERENCES  stationInfo(Station),
	CONSTRAINT DF_RecordCount DEFAULT(0) FOR RealCount
GO

--附件1
--创建车站记录表
IF EXISTS(SELECT * FROM sysobjects WHERE name='stationRecord')
DROP TABLE stationRecord
GO
CREATE TABLE stationRecord
(
	TrainNum varchar(20) NOT NULL,	--车次
	RecordDate date NOT NULL,		--日期
	Station varchar(20) NOT NULL,	--当前站点
	ArriveTime datetime,			--到站时间
	DepartsTime  datetime,			--发车时间
	AboardCount int,				--上车人数
	DebusCount int					--下车人数
)
GO
ALTER TABLE stationRecord WITH NOCHECK ADD 
CONSTRAINT [PK_stationRecord] PRIMARY KEY  NONCLUSTERED 
(
  [TrainNum],
  [RecordDate],
  [Station]
)
ALTER TABLE stationRecord
ADD CONSTRAINT FK_RecordDate_TrainNum FOREIGN KEY(TrainNum,RecordDate) REFERENCES  trainRecord(TrainNum,RecordDate),
	CONSTRAINT FK_Station FOREIGN KEY(Station) REFERENCES  stationInfo(Station),
	CONSTRAINT DF_AboardCount DEFAULT(0) FOR AboardCount,
	CONSTRAINT DF_DebusCount DEFAULT(0) FOR DebusCount
GO
	
--附件1
--创建客流信息表
IF EXISTS(SELECT * FROM sysobjects WHERE name='flowInfo')
DROP TABLE flowInfo
GO
CREATE TABLE flowInfo
(
	TrainNum varchar(20) NOT NULL,				--车次
	RecordDate date NOT NULL,					--日期
	AboardStation varchar(20) NOT NULL,			--上车站
	DebusStation varchar(20) NOT NULL,			--下车站
	FlowCount int								--总人数
)
GO
ALTER TABLE flowInfo WITH NOCHECK ADD 
CONSTRAINT [PK_flowInfo] PRIMARY KEY  NONCLUSTERED 
(
  [TrainNum],
  [RecordDate],
  [AboardStation],
  [DebusStation]
)
ALTER TABLE flowInfo
ADD CONSTRAINT FK_RecordDate_TrainNum1 FOREIGN KEY(TrainNum,RecordDate) REFERENCES  trainRecord(TrainNum,RecordDate),
	CONSTRAINT FK_AboardStation FOREIGN KEY(AboardStation) REFERENCES  stationInfo(Station),
	CONSTRAINT FK_DebusStation FOREIGN KEY(DebusStation) REFERENCES  stationInfo(Station),
	CONSTRAINT DF_FlowCount DEFAULT(0) FOR FlowCount
GO

--数据关系表创建结束