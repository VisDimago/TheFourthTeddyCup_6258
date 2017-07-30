function flowCount = flowChange(flowCount, station, stopStation, peopleNum)

for i = 1:size(stopStation, 1)
	if strcmp(stopStation{i,3}, station)
		flowCount = flowCount+peopleNum(i);
	elseif strcmp(stopStation{i,4}, station)
		flowCount = flowCount-peopleNum(i);
	end
end