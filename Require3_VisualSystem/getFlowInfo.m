
tempdata=data(2679:2778);

% for j =1:1
% 	for i=1:4
% 		temptextdata{j,i}=textdata{j,i};
% 	end
% end
M = 2680;
for j =M:M+99
	for i=1:4
		temptextdata{mod(j,M-1),i}=textdata{j,i};
	end
end

tempStaRe{1,1}='D19';
tempStaRe{1,2}='2015-10-01';
tempStaRe{1,3}='ZD190-01';
tempStaRe{1,4}='ZD111-03';
tempStaRe{2,1}='D18';
tempStaRe{2,2}='2015-10-01';
tempStaRe{2,3}='ZD111-01';
tempStaRe{2,4}='ZD190-01';
tempStaRe{3,1}='D49';
tempStaRe{3,2}='2015-10-01';
tempStaRe{3,3}='ZD111-02';
tempStaRe{3,4}='ZD190-02';
tempStaRe{4,1}='D50';
tempStaRe{4,2}='2015-10-01';
tempStaRe{4,3}='ZD190-02';
tempStaRe{4,4}='ZD111-02';

savePath = strcat('F:\数据挖掘\可视化系统\testdata.mat');
if exist ( savePath, 'file' ) == 0
	save( savePath, 'tempdata','temptextdata','tempStaRe' );
else
	save( savePath, 'tempdata','temptextdata','tempStaRe', '-append' );
end

