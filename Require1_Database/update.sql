
--update stationRecord set AboardCount=0
--select * from stationRecord
--select * from trainRecord
/*
update stationRecord set AboardCount
=sum(FlowCount) from flowInfo
where stationRecord.TrainNum=flowInfo.TrainNum and
stationRecord.RecordDate=flowInfo.RecordDate and
stationRecord.Station=flowInfo.AboardStation

select a=sum(FlowCount)
from flowInfo
group by TrainNum ,RecordDate,AboardStation
*/
update stationRecord set AboardCount=temp.Amount
from(select Amount=SUM(FlowCount),TrainNum,RecordDate,AboardStation from
flowInfo group by TrainNum ,RecordDate,AboardStation)temp,
stationRecord where stationRecord.TrainNum=temp.TrainNum and
stationRecord.RecordDate=temp.RecordDate and
stationRecord.Station=temp.AboardStation

update stationRecord set DebusCount=temp.Amount
from(select Amount=SUM(FlowCount),TrainNum,RecordDate,DebusStation from
flowInfo group by TrainNum ,RecordDate,DebusStation)temp,
stationRecord where stationRecord.TrainNum=temp.TrainNum and
stationRecord.RecordDate=temp.RecordDate and
stationRecord.Station=temp.DebusStation



;with cte as(
	select 车抵达时间=CONVERT(varchar(4),ArriveTime,120)+'0',
	站点=Station,
	下车人数=sum(DebusCount)
	from stationRecord with(nolock)
	group by CONVERT(varchar(4),ArriveTime,120)+'0',Station
)select * from cte
go
;with cte as(
	select 车离开时间=CONVERT(varchar(4),DepartsTime,108)+'0',
	站点=Station,
	上车人数=sum(AboardCount)
	from stationRecord with(nolock)
	group by CONVERT(varchar(4),DepartsTime,108)+'0',Station
)select * from cte
go