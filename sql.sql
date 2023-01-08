-- Таблицы

--drop table CRCardOperations
--drop table CRExecutors
--drop table CRCardDetails
--drop table CRCards
--drop table CRRepairTypes
--drop table CRReferenceOperations


create table CRRepairTypes
(
	RepairTypeId int identity primary key,
	[Name] varchar(8000)
)

create table CRCards
(
	CardId int identity primary key,
	ParentCardId int,
	Number varchar(50),
	ActNumber varchar(50),
	RepairTypeId int foreign key references dbo.CRRepairTypes(RepairTypeId),
	Department int,
	[Order] varchar(12),
	Stage varchar(8000),
	FactoryNumber varchar(8000),
	Direction varchar(8000),
	Cipher varchar(8000),
	ClientOrder varchar(8000),

	ProductId int,
	ProductCode varchar(8000),
	ProductName varchar(8000),
	
	InvoiceNumber varchar(8000),
	[Date] date,
	[Source] varchar(8000),
	ReasonForRepair varchar(8000),

	[RowVersion] timestamp
)

create table CRCardDetails
(
	CardId int primary key references dbo.CRCards(CardId) on delete cascade,
	ExternalDefects text,
	InternalDefects text,
	Malfunctions text,
	[RowVersion] timestamp
)

create table CRExecutors
(
	ExecutorId int identity primary key,
	[Name] varchar(8000),
	Department int,
	[RowVersion] timestamp
)

create table CRCardOperations
(
	CardOperationId int identity primary key,
	CardId int foreign key references dbo.CRCardDetails(CardId) on delete cascade,
	ExecutorId int foreign key references dbo.CRExecutors(ExecutorId),
	Code varchar(8000),
	[Name] varchar(8000),
	[Count] int,
	Labor decimal(18, 3),
	[Type] int,
	[Date] date,
	LaborAll decimal(18, 3),
	UnitName varchar(8000),
	GroupName varchar(8000),
	Department int,
	[RowVersion] timestamp
)

create table CRReferenceOperations
(
	ReferenceOperationId int identity,
	Code varchar(8000),
	[Name] varchar(8000),
	Labor decimal(18, 3),
	GroupName varchar(8000),
	[RowVersion] timestamp
)


insert into CRExecutors
([Name], Department)
select distinct [Name], Department from
(
	select upper(substring(rtrim(ltrim(Name)), 1, 1)) + substring(rtrim(ltrim(Name)), 2, len(rtrim(ltrim(Name))) - 1) Name, convert(int, Dept) Department from t_brigade
	where IsExists = 1 and rtrim(ltrim(Name)) not in ('', '.', '*')
) r


insert into CRRepairTypes ([Name]) values ('Капитальный')
insert into CRRepairTypes ([Name]) values ('Гарантийный')
insert into CRRepairTypes ([Name]) values ('Негарантийный')

--select * from CRExecutors
--select * from CRReferenceOperations

create table CRRoles
(
	RoleId int identity primary key,
	[Name] varchar(100)
)

alter table CRUsers
add RoleId int foreign key references dbo.CRRoles(RoleId)

insert into CRRoles ([Name]) values ('Только просмотр')
insert into CRRoles ([Name]) values ('Администратор')
insert into CRRoles ([Name]) values ('Начальник ТБ')
insert into CRRoles ([Name]) values ('Работник ООИОТ')
insert into CRRoles ([Name]) values ('Мастер')
insert into CRRoles ([Name]) values ('Работник ОТК')
insert into CRRoles ([Name]) values ('Работник ПРБ')

-- Триггеры
CREATE TRIGGER [dbo].[CRCardsDeleteTrigger] ON [dbo].[CRCards] 
   FOR DELETE
AS 
BEGIN

	declare @CardId int = (SELECT CardId FROM DELETED);
	IF (SELECT COUNT(*) FROM CRCards WHERE ParentCardId = @CardId) > 0
	BEGIN
		RAISERROR('Невозможно удалить карту ремонта, потому что в нее входят другие карты ремонта.', 17, 1);
		ROLLBACK TRANSACTION
	END

END


CREATE TRIGGER [dbo].[CRCardsInsertTrigger] ON [dbo].[CRCards] 
   FOR INSERT
AS 
BEGIN

	declare @ParentCardId int = (SELECT ParentCardId FROM INSERTED);
	IF (SELECT ParentCardId FROM CRCards WHERE CardId = @ParentCardId) IS NOT NULL
	BEGIN
		RAISERROR('Невозможно использовать выбранную карту ремонта в качестве головной, потому что она входит в другую карту ремонта.', 17, 1);
		ROLLBACK TRANSACTION
	END

END


CREATE TRIGGER [dbo].[CRCardsUpdateTrigger] ON [dbo].[CRCards] 
   FOR UPDATE
AS 
BEGIN

	IF (SELECT CardId FROM INSERTED) = (SELECT ParentCardId FROM INSERTED) 
	BEGIN
		RAISERROR('Невозможно использовать выбранную карту ремонта в качестве головной, потому что карта ремонта не может быть головной по отношению к самой себе.', 17, 1);
		ROLLBACK TRANSACTION
	END

	declare @ParentCardId int = (SELECT ParentCardId FROM INSERTED);
	IF (SELECT ParentCardId FROM CRCards WHERE CardId = @ParentCardId) IS NOT NULL
	BEGIN
		RAISERROR('Невозможно использовать выбранную карту ремонта в качестве головной, потому что она входит в другую карту ремонта.', 17, 1);
		ROLLBACK TRANSACTION
	END

END



--create table CRUsers
--(
--	UserId int identity,
--	[Name] varchar(200),
--	[Password] varbinary(200),
--	Department int,
--	[Role] int
--)

--insert into CRUsers
--([Name], [Password], [Department])
--select [Name], [Password], [Department] from t_normacex_users
--where [name] not like '%мастер%'


---- Пользователи
select *, convert(varchar(100), DecryptByPassPhrase('m14780', [Password])) from CRUsers
where [Name] like 'литви%'

insert into CRUsers
([Name], [Password], [Department], RoleId)
values
('Евдомащенко Ольга Викторовна', EncryptByPassPhrase('m14780', '89lrt3'), 17, 7)

select * from CRRoles

--insert into CRRoles
--([Name]) values ('Работник ПЭО')

select * from CRUsers where [Name] like 'чек%'

update CRUsers
set [RoleId] = 3
where Id = 105

--alter table CRCards
--add constraint ucNumber unique (Number)

--alter table CROperations
--add constraint ucCode unique (Code)

create table CRCardAgreements
(
	CardId int primary key references dbo.CRCards(CardId) on delete cascade,
	TechnicalBureau bit,
	[Master] bit,
	TechnicalControlDepartment bit,
	LaborDepartment bit,
	PlanningAndDistributionDepartment bit,
	[RowVersion] timestamp
)

--drop table CRCardAgreements
select * from CRCardAgreements

select * from CRCArds

--insert into CRCardAgreements
--(CardId, TechnicalBureau, [Master], TechnicalControlDepartment, LaborDepartment, PlanningAndDistributionDepartment)
--values
--(116, 0, 0, 0, 0, 0)

--update CRCardAgreements
--set LaborDepartment = 1
--where CardId = 121

/*
ОТК, ООИОТ и другие отделы не привязаны к определенному цеху
*/

alter table CRCardAgreements add TechnicalBureauUsername varchar(200)
alter table CRCardAgreements add MasterUsername varchar(200)
alter table CRCardAgreements add TechnicalControlDepartmentUsername varchar(200)
alter table CRCardAgreements add LaborDepartmentUsername varchar(200)
alter table CRCardAgreements add PlanningAndDistributionDepartmentUsername varchar(200)

alter table CRCardAgreements add TechnicalBureauUserId int
alter table CRCardAgreements add MasterUserId int
alter table CRCardAgreements add TechnicalControlDepartmentUserId int
alter table CRCardAgreements add LaborDepartmentUserId int
alter table CRCardAgreements add PlanningAndDistributionDepartmentUserId int


create table CRCardAdditionalProducts
(
	CardAdditionalProductId int identity primary key,
	CardId int foreign key references dbo.CRCardDetails(CardId) on delete cascade,
	Code varchar(100),
	Name varchar(200),
	Count int
)

create table CRCardBigOperations
(
	CardBigOperationId int identity primary key,
	CardAdditionalProductId int foreign key references dbo.CRCardAdditionalProducts(CardAdditionalProductId) on delete cascade,
	Code varchar(50),
	Name varchar(500),
	Labor decimal(18, 4),
	Type int,
	Date date,
	Department int
)

create table CRUnlockedPeriods
(
	Id int identity(1,1),
	CardId int,
	Year int,
	Month int,
	CreatorName varchar(600)
)

alter table CRCardUnlockedPeriods
add unique (CardId, Year, Month)

create table CRExportRequests
(
	Id int identity(1,1),
	
	CardNumber varchar(max),

	CustomerName varchar(max),
	ExecutorName varchar(max),

	Date DateTime,
	CloseDate DateTime,
	Department int
)