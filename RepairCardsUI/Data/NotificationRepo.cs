using Dapper;
using RepairCardsDapperData.Models;
using System;
using System.Collections.Generic;

namespace RepairCardsDapperData.Data
{
    public class NotificationRepo : RepoBase
    {
        public IEnumerable<Notification> Get(string UserName) => conn.Query<Notification>(
@"select * from CRNotifications 
where id not in  
(select IdMessage from CRStatusReadNotification where UserName like '" + UserName + "') order by Id");
        
        public void WriteStatusReadNotification(string UserName, int IdMessage) => conn.Execute(
@"insert into CRStatusReadNotification values ('" + UserName + "'," + IdMessage + "," + 1 + ",'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')", null);

    }
}
