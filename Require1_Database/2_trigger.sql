--插入客流信息表数据时，如果在车站记录表中没有对应日期的车次和站点，则需要对应添加
--如果车站记录表中已经有对应数据，则在两个对应站点上车人数或下车人数做累加
USE Railway
GO

if (object_id('tgr_flowInfo_insert','tr') is not null)
	drop trigger tgr_flowInfo_insert
go
create trigger tgr_flowInfo_insert
on flowInfo 
	instead of insert
as
begin
	declare @_TrainNum varchar(20),@_RecordDate date,@_AboardStation varchar(20),@_DebusStation varchar(20),@_FlowCount int
	--从inserted临时表中获取记录值
	select @_TrainNum = TrainNum,
	@_RecordDate = RecordDate,
	@_AboardStation = AboardStation,
	@_DebusStation = DebusStation,
	@_FlowCount = FlowCount
	from inserted
	
	if @_AboardStation not in (select Station from stationRecord) or 
	@_TrainNum not in (select TrainNum from stationRecord) or
	@_RecordDate not in (select RecordDate from stationRecord)
	begin
		insert into stationRecord values(@_TrainNum,@_RecordDate,@_AboardStation, NULL, NULL,@_FlowCount,default)
	end
	else
	begin
		update stationRecord set AboardCount = AboardCount+@_FlowCount
		where Station=@_AboardStation and TrainNum=@_TrainNum and RecordDate=@_RecordDate;
	end
	
	if @_TrainNum not in (select TrainNum from stationRecord) or 
	@_DebusStation not in (select Station from stationRecord) or
	@_RecordDate not in (select RecordDate from stationRecord)
	begin
		insert into stationRecord values(@_TrainNum,@_RecordDate,@_DebusStation, NULL, NULL,default,@_FlowCount)
	end
	else
	begin
		update stationRecord set DebusCount = DebusCount+@_FlowCount
		where Station=@_DebusStation and TrainNum=@_TrainNum and RecordDate=@_RecordDate;
	end
	
	insert into flowInfo select * from inserted
end
go
--测试数据
--insert into flowInfo values('k1',getdate(),'a','b',5)
--insert into flowInfo values('k1',getdate(),'b','a',13)


--客流信息表删除数据，车站信息表随着改变数据
if (object_id('tgr_flowInfo_delete', 'TR') is not null)
    drop trigger tgr_flowInfo_delete
go
create trigger tgr_flowInfo_delete
on flowInfo
    after delete --删除触发
as
	declare @pre_TrainNum varchar(20),@pre_RecordDate date,
	@pre_AboardStation varchar(20),@pre_DebusStation varchar(20),@pre_FlowCount int
	
	select @pre_TrainNum = TrainNum,
	@pre_RecordDate = RecordDate,
	@pre_AboardStation = AboardStation,
	@pre_DebusStation = DebusStation,
	@pre_FlowCount = FlowCount
	from deleted;

	update stationRecord set AboardCount = AboardCount-@pre_FlowCount
	where Station=@pre_AboardStation and TrainNum=@pre_TrainNum and RecordDate=@pre_RecordDate;

	update stationRecord set DebusCount = DebusCount-@pre_FlowCount
	where Station=@pre_AboardStation and TrainNum=@pre_TrainNum and RecordDate=@pre_RecordDate;
go


--客流信息表更新数据，车站记录表随着改变数据
--update触发器会在更新数据后，将更新前的数据保存在deleted表中，更新后的数据保存在inserted表中
if (object_id('tgr_FlowInfo_update', 'TR') is not null)
    drop trigger tgr_flowInfo_update
go
create trigger tgr_flowInfo_update
on flowInfo
    after update
as
/*
	declare  @pre_TrainNum varchar(20),@pre_RecordDate date,
	@pre_AboardStation varchar(20),@pre_DebusStation varchar(20),@pre_FlowCount int,
	@after_TrainNum varchar(20),@after_RecordDate date,
	@after_AboardStation varchar(20),@after_DebusStation varchar(20),@after_FlowCount int
	
	--更新前的数据
	select 
	@pre_TrainNum = TrainNum,
	@pre_RecordDate = RecordDate,
	@pre_AboardStation = AboardStation,
	@pre_DebusStation = DebusStation,
	@pre_FlowCount = FlowCount
	from deleted;
	
	--更新后的数据
	select @after_FlowCount = FlowCount,
	@after_TrainNum = TrainNum,
	@after_RecordDate = RecordDate,
	@after_AboardStation = AboardStation,
	@after_DebusStation = DebusStation
	from inserted;
*/
	begin
		delete from flowInfo select * from deleted;
		insert into flowInfo select * from inserted;
		/*
		--将被修改的记录在stationRecord表中进行对应删除
		update stationRecord set AboardCount = AboardCount-@pre_FlowCount
		where Station=@pre_AboardStation and TrainNum=@pre_TrainNum and RecordDate=@pre_RecordDate;
		update stationRecord set DebusCount = DebusCount-@pre_FlowCount
		where Station=@pre_DebusStation and TrainNum=@pre_TrainNum and RecordDate=@pre_RecordDate;
		
		--将修改好的数据在stationRecord表中进行对应添加
		if @after_AboardStation not in (select Station from stationRecord) or 
			@after_TrainNum not in (select TrainNum from stationRecord) or
			@after_RecordDate not in (select RecordDate from stationRecord))
		begin
			insert into stationRecord values(@after_TrainNum,@after_RecordDate,@after_AboardStation, NULL, NULL,@after_FlowCount,default)
		end
		else
		begin
			update stationRecord set AboardCount = AboardCount+@after_FlowCount
			where Station=@after_AboardStation and TrainNum=@after_TrainNum and RecordDate=@after_RecordDate;
		end
		
		if @after_TrainNum not in (select TrainNum from stationRecord) or 
			@after_DebusStation not in (select Station from stationRecord) or
			@after_RecordDate not in (select RecordDate from stationRecord)
		begin
			insert into stationRecord values(@after_TrainNum,@after_RecordDate,@after_DebusStation, NULL, NULL,default,@after_FlowCount)
		end
		else
		begin
			update stationRecord set DebusCount = DebusCount+@after_FlowCount
			where Station=@after_DebusStation and TrainNum=@after_TrainNum and RecordDate=@after_RecordDate;
		end
		*/
	end
go



--车站信息表插入数据，判断车次记录表是否存在相应记录，没有则添加记录
CREATE trigger [dbo].[tgr_stationRecord_insert]  
on [dbo].[stationRecord]   
	instead of insert  
as  
begin
	declare @_TrainNum varchar(20),@_RecordDate date,@_Station varchar(20),@_AboardCount int  
	select @_TrainNum = TrainNum,  
	@_RecordDate = RecordDate,  
	@_Station = Station,  
	@_AboardCount = AboardCount  
	from inserted  
   
	if @_Station not in (select Station from stationInfo)  
	begin  
	insert into stationInfo values('管外',@_Station,'否',0)  
	end  
   
	if @_TrainNum not in (select TrainNum from trainRecord) or  
	@_RecordDate not in (select RecordDate from trainRecord)  
	begin  
		insert into trainRecord values(@_TrainNum,@_RecordDate,NULL,NULL,NULL,@_AboardCount,NULL)  
	end
	else
	begin
		update trainRecord set RealCount = RealCount+@_AboardCount  
		where TrainNum=@_TrainNum and RecordDate=@_RecordDate;  
	end

	insert into stationRecord select * from inserted  

end
/*if (object_id('tgr_stationRecord_insert','tr') is not null)
	drop trigger tgr_stationRecord_insert
go
create trigger tgr_stationRecord_insert
on stationRecord 
	for insert
as
begin
	declare @_TrainNum varchar(20),@_RecordDate date,@_Station varchar(20),@_AboardCount int
	select @_TrainNum = TrainNum,
	@_RecordDate = RecordDate,
	@_Station = Station,
	@_AboardCount = AboardCount
	from inserted
	
	if @_Station not in (select Station from stationInfo))
	begin
		insert into stationInfo values('管外',@_Station,'否',0)
	end
	
	if @_TrainNum not in (select TrainNum from trainRecord) or
		@_RecordDate not in (select RecordDate from trainRecord)
	begin
		insert into trainRecord values(@_TrainNum,@_RecordDate,NULL,NULL,NULL,@_AboardCount,NULL)
	end
	else
	begin
		update trainRecord set RealCount = RealCount+@_AboardCount
		where TrainNum=@_TrainNum and RecordDate=@_RecordDate;
	end
end
go*/


--车站信息表删除数据
if (object_id('tgr_stationRecord_delete','tr') is not null)
	drop trigger tgr_stationRecord_delete
go
create trigger tgr_stationRecord_delete
on stationRecord 
	for delete
as
begin
	declare @_TrainNum varchar(20),@_RecordDate date,@_Station varchar(20),@_AboardCount int
	select @_TrainNum = TrainNum,
	@_RecordDate = RecordDate,
	@_AboardCount = AboardCount
	from deleted
	
	update trainRecord set RealCount = RealCount-@_AboardCount
	where TrainNum=@_TrainNum and RecordDate=@_RecordDate;
	
	if(exists (select * from flowInfo where AboardStation = @_Station))
		delete from flowInfo where AboardStation=@_Station;
	
	if(exists (select * from flowInfo where DebusStation = @_Station))
		delete from flowInfo where DebusStation=@_Station;
end
go

/*
--车站信息表更新数据
if (object_id('tgr_stationRecord_update','tr') is not null)
	drop trigger tgr_stationRecord_update
go
create trigger tgr_stationRecord_update
on stationRecord 
	for update
as
begin
	declare @pre_TrainNum varchar(20),@pre_RecordDate date,@pre_Station varchar(20),@pre_AboardCount int,@pre_DebusCount int,
	@after_TrainNum varchar(20),@after_RecordDate date,@after_Station varchar(20),@after_AboardCount int,@after_DebusCount int
	select @pre_TrainNum = TrainNum,
	@pre_RecordDate = RecordDate,
	@pre_Station = Station,
	@pre_AboardCount = AboardCount,
	@pre_DebusCount = DebusCount
	from deleted
	select @after_TrainNum = TrainNum,
	@after_RecordDate = RecordDate,
	@after_Station = Station,
	@after_AboardCount = AboardCount,
	@after_DebusCount = DebusCount
	from inserted
	
	if(@pre_TrainNum!=@after_TrainNum or @pre_RecordDate!=@after_RecordDate or @pre_Station!=@after_Station or
	@pre_AboardCount!=@after_AboardCount or @pre_DebusCount!=@after_DebusCount)
	begin
		raisError('只能修改到站时间和发车时间', 16, 10);
		rollback tran;
	end
end
go


--车次记录表更新数据
if (object_id('tgr_trainRecord_update','tr') is not null)
	drop trigger tgr_trainRecord_update
go
create trigger tgr_trainRecord_update
on trainRecord 
	for update
as
begin
	declare @pre_TrainNum varchar(20),@pre_RecordDate date,@pre_RealCount int,
	@after_TrainNum varchar(20),@after_RecordDate date,@after_RealCount int
	select @pre_TrainNum = TrainNum,
	@pre_RecordDate = RecordDate,
	@pre_RealCount = RealCount
	from deleted
	select @after_TrainNum = TrainNum,
	@after_RecordDate = RecordDate,
	@after_RealCount = RealCount
	from inserted
	
	if(@pre_TrainNum!=@after_TrainNum or @pre_RecordDate!=@after_RecordDate or @pre_RealCount!=@after_RealCount)
	begin
		raisError('不能修改车次、日期和实际人数', 16, 10);
		rollback tran;
	end
end
go
*/
--车次记录表删除数据
if (object_id('tgr_trainRecord_delete','tr') is not null)
	drop trigger tgr_trainRecord_delete
go
create trigger tgr_trainRecord_delete
on trainRecord
	for delete
as
begin
	declare @pre_TrainNum varchar(20),@pre_RecordDate date,@pre_RealCount int
	select @pre_TrainNum = TrainNum,
	@pre_RecordDate = RecordDate,
	@pre_RealCount = RealCount
	from deleted

	delete from stationRecord where TrainNum=@pre_TrainNum and RecordDate=@pre_RecordDate;
end
go


--车站信息表删除数据
if (object_id('tgr_stationInfo_delete','tr') is not null)
	drop trigger tgr_stationInfo_delete
go
create trigger tgr_stationInfo_delete
on stationInfo
	for delete
as
begin
	declare @pre_Region varchar(20),@pre_Station varchar(20)
	select @pre_Region = Region,
	@pre_Station = Station
	from deleted
	
	if(exists (select * from trainRecord where OriginStation = @pre_Station))
		delete from trainRecord where OriginStation=@pre_Station;
	if(exists (select * from trainRecord where TerminalStation = @pre_Station))
		delete from trainRecord where TerminalStation=@pre_Station;
end
go

/*
--车次记录表插入数据
if (object_id('tgr_trainRecord_insert','tr') is not null)
	drop trigger tgr_trainRecord_insert
go
create trigger tgr_trainRecord_insert
on trainRecord 
	instead of insert
as
begin
	declare @_TrainNum varchar(20),@_RecordDate date,@_OriginStation varchar(20),@_TerminalStation varchar(20)
	select @_TrainNum = TrainNum,
	@_RecordDate = RecordDate,
	@_OriginStation = OriginStation,
	@_TerminalStation = TerminalStation
	from inserted
	
	if exists(select * from trainRecord where trainRecord.OriginStation not in (select Station from stationInfo))
	begin
		insert into stationInfo values('管外',@_OriginStation,'否',0)
	end
	if exists(select * from trainRecord where trainRecord.TerminalStation not in (select Station from stationInfo))
	begin
		insert into stationInfo values('管外',@_TerminalStation,'否',0)
	end
	insert into trainRecord select * from inserted
end
go*/

--车次记录表插入数据
CREATE trigger [dbo].[tgr_trainRecord_insert]  
on [dbo].[trainRecord]   
 instead of insert  
as  
begin  
 declare @_TrainNum varchar(20),@_RecordDate date,@_OriginStation varchar(20),@_TerminalStation varchar(20)  
 select @_TrainNum = TrainNum,  
 @_RecordDate = RecordDate,  
 @_OriginStation = OriginStation,  
 @_TerminalStation = TerminalStation  
 from inserted  
   
 if @_OriginStation not in (select Station from stationInfo)  
 begin  
  insert into stationInfo values('管外',@_OriginStation,'否',0)  
 end  
 if @_TerminalStation not in (select Station from stationInfo)  
 begin  
  insert into stationInfo values('管外',@_TerminalStation,'否',0)  
 end  
  
 insert into trainRecord select * from inserted  
  
end



/*
在删除（delete）数据的时候，可以假定数据库将要删除的数据放到一个deleted临时表中，
我们可以向读取普通的表一样，select 字段 from deleted而insert的时候道理一样，
只不过是把要插入的数据放在inserted表中。更新操作可以认为是执行了两个操作，
先把那一行记录delete掉，然后再insert，这样update操作实际上就对deleted表和inserted表的操作，
所以不会有updated表了，有的时候两个表是主外键关系，想删除主表数据的同时把子表相关的数据也删除，
这个时候如果用触发器就没有效果了，因为这个触发器是在你删除表后才触发的，
这个时候直接终止，提示“有主外键关系，不能删除等”，所有这样的删除触发器是没有效果的
*/





/*
select * from sys.triggers
--查询已存在的触发器
select * from sys.triggers;
select * from sys.objects where type = 'TR';
--查看单个触发器
exec sp_helptext '触发器名'
--删除触发器
drop trigger trigger_name
--创建触发器
create trigger trigger_name 
on  {table_name|view_name} 
{After|Instead of} {insert|update|delete}
as 相应T-SQL语句
--修改触发器
alter trigger trigger_name 
on  {table_name|view_name} 
{After|Instead of} {insert|update|delete}
as 相应T-SQL语句
--查看触发器触发事件
select te.* from sys.trigger_events te join sys.triggers t
on t.object_id = te.object_id
where t.parent_class = 0 and t.name = 'tgr_valid_data';
--查看创建触发器语句
exec sp_helptext 'tgr_message';
*/
