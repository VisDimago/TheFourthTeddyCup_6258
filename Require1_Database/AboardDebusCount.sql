select * from test

;with cte as(
	select 车抵达时间=CONVERT(varchar(4),ArriveTime,108)+'0',
	站点=Station,
	下车人数=sum(DebusCount)
	from stationRecord with(nolock)
	where Station in (select Station from stationInfo where InPipe = '是')and (TrainNum like 'D%')
	group by CONVERT(varchar(4),ArriveTime,108)+'0',Station
)select * from cte
where 车抵达时间 is not NULL 
order by 站点,车抵达时间
go

;with cte as(
	select 车离开时间=CONVERT(varchar(3),DepartsTime,108)+'00',
	站点=Station,
	上车人数=sum(AboardCount)
	from stationRecord with(nolock)
	where Station in (select Station from stationInfo where InPipe = '是') and (TrainNum like 'D%')
	group by CONVERT(varchar(3),DepartsTime,108)+'00',Station
)select * from cte
where 车离开时间 is not NULL 
order by 站点,车离开时间
go

select * from stationRecord
where TrainNum like 'D%'