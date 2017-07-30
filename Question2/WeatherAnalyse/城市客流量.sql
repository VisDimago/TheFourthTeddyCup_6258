use Railway

drop table #temp

select 
	stationInfo.Region as city,
	stationInfo.Station,
	convert(char(10),stationRecord.DepartsTime,111) as datet,
	stationRecord.AboardCount as flow	

into #temp
from stationRecord,stationInfo
where stationInfo.Station=stationRecord.Station and stationInfo.Inpipe= 'ÊÇ'


union all

select 
	stationInfo.Region as city,
	stationInfo.Station,
	convert(char(10),stationRecord.ArriveTime,111) as datet,
	stationRecord.DebusCount as flow	

from stationRecord,stationInfo
where stationInfo.Station=stationRecord.Station and stationInfo.Inpipe= 'ÊÇ'



select city,datet,cc=sum(flow )
from #temp
where flow!=0
group by city,datet
order by city,datet



select * from #temp


