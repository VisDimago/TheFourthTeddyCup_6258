use RailWay
select * from region
select * from stationInfo
select * from trainRecord
select * from stationRecord
select * from weather
select * from flowInfo
--truncate table trainRecord
--delete from trainRecord
SELECT  Region AS 地区,SUM(Mileage) AS 里程
FROM stationInfo
GROUP BY Region
ORDER BY SUM(Mileage) DESC

--IF EXISTS(SELECT * FROM sysobjects WHERE name='test')
--DROP TABLE test
--GO
--CREATE TABLE test
--(
--	Region varchar(20) NOT NULL,
--	Mileage int
--)
--GO
--insert into test(Region,Mileage) 
--select Region, SUM(Mileage) FROM stationInfo
--GROUP BY Region
--ORDER BY SUM(Mileage) DESC
IF EXISTS(SELECT * FROM sysobjects WHERE name='test')
DROP TABLE test
GO
create table test
(
	RecordDate date NOT NULL,		--日期
	Station varchar(20) NOT NULL,	--当前站点
--	ArriveTime datetime,			--到站时间
--	DepartsTime  datetime,			--发车时间
	AboardCount int,				--上车人数
	DebusCount int					--下车人数
)
insert into test(RecordDate,Station,AboardCount,DebusCount) 
select RecordDate AS 日期,Station AS 站点,
SUM(AboardCount) AS 上车人数,SUM(DebusCount) AS 下车人数 FROM stationRecord
GROUP BY RecordDate,Station
ORDER BY RecordDate,Station DESC
select * from test where Station='ZD159' and RecordDate='2015-01-01'
--delete from test

select * from stationRecord where Station='下车人数合计'

;with cte as(
	select 到达时间=convert(char(15),ArriveTime,108)+'0',
	出发时间=convert(char(15),DepartsTime,108)+'0',
	上车人数=sum(AboardCount),
	下车人数=sum(DebusCount)
	from stationRecord with(nolock)
	group by convert(char(15),ArriveTime,108)+'0',convert(char(15),DepartsTime,108)+'0'
)
--select *,AboardCount+DebusCount as 客流量 from cte
select * from cte
go