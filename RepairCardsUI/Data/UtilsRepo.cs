using Dapper;
using RepairCardsDapperData.Models;
using System;
using System.Threading.Tasks;

namespace RepairCardsDapperData.Data
{
    public class UtilsRepo : RepoBase
    {
		public DateTime GetServerDate() => conn.ExecuteScalar<DateTime>("select getdate()");

		public void ExportToPDM(Card card) => conn.Execute(
@"Declare @table table (id int identity,idWhat int,[Type] int,DecNumWhat varchar(max),[Name] varchar(max),[Count] int, TableWhat int) 
if @ProductId != 0 and @Id != 0 and (select count(id) from Connect where IdIn = @ProductId)<2 
BEGIN 
	insert into @table 
	select t2.id 'idWhat',t2.Type,t2.DecNum,t2.Name,cast(t1.[Count] as decimal(18,3)),'2' 'TableWhat' from CRCardOwnProducts t1 
	left join ref_Dse t2 on t1.Code = t2.DecNum where t1.CardId = @Id and (isnull(t1.HasChangedComposition, 0) = 0 and t1.ParentId is null) and t2.id is not null 
	union all 
	select t2.id 'idWhat',t2.Type,t2.DecNum,t2.Name,cast(t1.[Count] as decimal(18,3)),'1' 'TableWhat' from CRCardOwnProducts t1 
	left join ref_Purchase t2 on t1.Code = t2.DecNum where t1.CardId = @Id and (isnull(t1.HasChangedComposition, 0) = 0 and t1.ParentId is null) and t2.id is not null 
	union all 
	select t2.id 'idWhat',t2.Type,t2.DecNum,t2.Name,cast(t1.[Count] as decimal(18,3)),'1' 'TableWhat' from CRCardPurchasedProducts t1 
	left join ref_Purchase t2 on t1.Code = t2.DecNum where t1.CardId = @Id and t2.id is not null 
	
	insert into [Connect] 
	select @ProductId,0,idWhat,[Type],[Count],null,TableWhat,null,'К/Р '+@Number,2,0,null,null from @table 

	declare @LeadingZeroDepartment varchar(max) = isnull(replicate('0', 3 - len(@Department)), '') + cast(@Department as varchar)

	if (select count(*) from ref_routing where What = @ProductId and TableWhat = 2) = 0
	begin
		insert into ref_routing (What, TableWhat, Dept1, [DateTime])
		values (@ProductId, 2, @LeadingZeroDepartment, getdate())
	end
END",
card);


		public bool IsExistingOrderInPDM(string order) => conn.ExecuteScalar<bool>(
@"Declare @IdDseOrder int = (select top 1 Dse from Orders where Code_+Isp+Ku like @Order)
select case when @IdDseOrder > 0 then 1 else 0 end",
new { Order = order });


        public bool IsPlannedOrder(string order) => conn.ExecuteScalar<bool>(
@"Declare @IdArticle int = (select top 1 ID from Article where DecNum like '%ДЕФИ.'+@Order+'.'+cast(YEAR(CURRENT_TIMESTAMP) as varchar(max))+'%')
select case when @IdArticle > 0 then 1 else 0 end",
new { Order = order });


        public void DeleteOrderInPDM(Card card) => conn.Execute(
@"Declare @IdDseOrder int = (select top 1 Dse from Orders where Code_+Isp+Ku like @Order and Deleted <> 1)
Declare @Control int = (select Id from ref_Dse where DecNum like @ProductCode and Id = @IdDseOrder)
If (@Control>0)
Begin
Delete from Connect where IdIn = @IdDseOrder
End
Else
Begin
	RAISERROR('Удаление не может быть выполнено. Обратитесь к разработчикам', 17, 1);
End
",
card);

		public void ActualizeOperations(Card card) => conn.Execute(
@"update CRCardOperations
set 
Labor = (select Labor from CROperations ro where ro.Code = CRCardOperations.Code),
LaborAll = (select Labor from CROperations ro where ro.Code = CRCardOperations.Code) * [Count]
where Id in
(
	select o.Id from CRCards c
	join CRCardOperations o on o.CardId = c.Id
	where c.Id = @Id
)", card);

    }
}
