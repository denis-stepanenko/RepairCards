using Dapper;
using RepairCardsDapperData.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsDapperData.Data
{
    public class UnlockedPeriodRepo : RepoBase
    {
        public UnlockedPeriod Get(int id) => conn.Query<UnlockedPeriod, Card, UnlockedPeriod>(
@"select * from CRUnlockedPeriods p
left join CRCards c on c.Id = p.CardId
where p.Id = @Id",
(p, c) => { p.Card = c; return p; },
new { Id = id }).First();

        public IEnumerable<UnlockedPeriod> GetAll() => conn.Query<UnlockedPeriod, Card, UnlockedPeriod>(
@"select * from CRUnlockedPeriods p
left join CRCards c on c.Id = p.CardId",
(p, c) => { p.Card = c; return p; });

        public int Add(UnlockedPeriod item) => conn.ExecuteScalar<int>(
@"insert into CRUnlockedPeriods
(CardId, Year, Month, CreatorName)
values
(@CardId, @Year, @Month, @CreatorName);
select scope_identity();", item);

        public void Update(UnlockedPeriod item) => conn.Execute(
@"update CRUnlockedPeriods
set
CardId = @CardId,
Year = @Year,
Month = @Month,
CreatorName = @CreatorName
where Id = @Id", item);

        public void Remove(UnlockedPeriod item) => conn.Execute(
@"delete from CRUnlockedPeriods where Id = @Id", item);

        public bool IsExistsPeriod(UnlockedPeriod item) => conn.ExecuteScalar<bool>(
@"select case when count(*) > 0 then 1 else 0 end from CRUnlockedPeriods 
where 
CardId = @CardId
and Year = @Year
and Month = @Month
and Id <> @Id", item);

        public bool IsUnlockedPeriod(int year, int month, int cardId) => conn.ExecuteScalar<bool>(
@"select case when count(*) > 0 then 1 else 0 end from CRUnlockedPeriods 
where 
CardId = @CardId
and Year = @Year
and Month = @Month",
new { Year = year, Month = month, CardId = cardId });
        
    }
}
