select 
	Region,
	RecordDate,
	(TemperatureLowest+TemperatureHighest) /2 as Temperature,
	(convert(int, right(WindPowerStart,1))-1)+(convert(int, right(WindPowerChange,1))+1)/2 as Wind
	 into #tempW
from weather
order by Region,RecordDate

select Region,RecordDate,abs((33-Temperature)*(9+10.9*sqrt(wind) -wind)-300) as WCI
from #tempW
order by Region,RecordDate

drop table #tempW