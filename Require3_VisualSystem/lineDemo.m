function colorDemo(choice, tempdata, tempStaRe, temptextdata)

x = [4.0    20.0   32.0    4.0     30.0    3.0    170    337     387      204    204    545    543   810    810];
y = [400   400    377     354    241     88     37     180      389      296    626   626    264   531    138];
stationName={'ZD111-01', 'ZD111-02', 'ZD111-03', 'ZD311', 'ZD326', 'ZD192', 'ZD022', 'ZD250','ZD062', ...
             'ZD120', 'ZD121', 'ZD143', 'ZD370', 'ZD190-02', 'ZD190-01'};
scatter(x,y,80,'g','filled');
hold on
plot(x,y,'h-','Color', [0, 0, 0],'linewidth',1,'MarkerSize',3);
h=line(NaN,NaN,'marker','>','linesty','-','erasemode','none','linewidth',5,'color','r');  %»æ»­ÏßÌõ
text(x(1)-50, y(1)+10, stationName(1),'Color', [0, 0, 0]);
axis off
for i = 2:length(stationName)
    text(x(i)+10, y(i)+10, stationName(i), 'Color', [0, 0, 0]);
end

index = zeros(1, size(tempStaRe, 1));
count = zeros(1, size(tempStaRe, 1));
for L = 1:size(tempStaRe, 1)
	TrainNum = tempStaRe{L, 1};
	RecordDate = tempStaRe{L, 2};
	for j = 1:size(temptextdata, 1)
		if isequal(temptextdata{j,2}, TrainNum) && isequal(temptextdata{j,1}, RecordDate)
			count(L) = count(L)+1;
			if count(L) == 1
				index(L) = j;
			end
		end
	end
end

OriginStation = tempStaRe{choice, 3};
TerminalStation = tempStaRe{choice, 4};
for i = 1: length(stationName)
	if strcmp(stationName(i), OriginStation)==1
		OriginIndex = i;
    elseif strcmp(stationName(i), TerminalStation)==1
		TerminalIndex = i;
	end
end

% route: stationName(OriginIndex) --> stationName(TerminalIndex)
if OriginIndex>TerminalIndex
	seq = true;
	flowCount = 0;
	for i = OriginIndex:-1:TerminalIndex
	    set(h,'xdata',x(i),'ydata',y(i));
	    pause(0.8);
	    if i>(TerminalIndex)
	        plot([x(i) x(i-1)], [y(i), y(i-1)], 'Color', [0.2, 0.8, 0.5], 'linewidth',3+flowCount*0.005);
	    end

	    c = 1;
	    for j = index(choice):index(choice)+count(choice)-1
	    	for n = 1:4;
	    		temp{c,n} = temptextdata{j,n};
	    	end
	    	c = c+1;
	    end
	    flowCount = flowChange(flowCount, stationName(i), temp, tempdata(index(choice):index(choice)+count(choice)-1));
    end
else
	seq = false;
	flowCount = 0;
	for i = OriginIndex:TerminalIndex
	    set(h,'xdata',x(i),'ydata',y(i));
	    pause(0.8);
	    if i<(TerminalIndex)
	        plot([x(i) x(i+1)], [y(i), y(i+1)], 'Color', [0.2, 0.8, 0.5], 'linewidth',3+flowCount*0.005);
	    end

	    c = 1;
	    for j = index(choice):index(choice)+count(choice)-1
	    	for n = 1:4;
	    		temp{c,n} = temptextdata{j,n};
	    	end
	    	c = c+1;
	    end
	    flowCount = flowChange(flowCount, stationName(i), temp, tempdata(index(choice):index(choice)+count(choice)-1));
    end
end