using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace Threads
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class ThreadsService : IService
    {
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }


        public List<wsEntry> EntryReadByCommunityID(string strCommunityID)
        {
            DataThreadsDataContext dc = new DataThreadsDataContext();
            List<wsEntry> results = new List<wsEntry>();
            long communityID = long.Parse(strCommunityID);

            foreach (Entry_ReadByCommunityIDResult entry in dc.Entry_ReadByCommunityID(communityID))
            {
                results.Add(new wsEntry()
                {
                    ID = entry.ID,
                    CommunityID = entry.CommunityID,
                    CommunityID_Name = entry.CommunityID_Name,
                    ColumnID = entry.ColumnID ?? 0,
                    ColumnID_Name = entry.ColumnID_Name,
                    CreatorID = entry.CreatorID ?? 1,
                    CreatorID_FullName = entry.CreatorID_FullName,
                    EntryText = entry.EntryText,
                    CreateDate = entry.CreateDate ?? System.DateTime.Now
                });

            }

            return results;
        }

        public List<wsCommunity> GetAllCommunities()
        {
            DataThreadsDataContext dc = new DataThreadsDataContext();
            List<wsCommunity> results = new List<wsCommunity>();

            foreach (Community comm in dc.Community)
            {
                results.Add(new wsCommunity()
                {
                    ID = comm.ID,
                    Name = comm.Name,
                    LogoLink = comm.LogoLink,
                    Link = comm.Link,
                    Description = comm.Decription,
                    OwnerID = comm.OwnerID ?? 0,
                    CreateDate = comm.CreateDate ?? System.DateTime.Now
                });
            }

            return results;
        }


        public List<wsCommunity> CommunityReadDict()
        {
            List<wsCommunity> results = new List<wsCommunity>();
            DataThreadsDataContext dc = new DataThreadsDataContext();

            foreach (Community_ReadDictResult comm in dc.Community_ReadDict())
            {
                results.Add(new wsCommunity()
                {
                    ID = comm.ID,
                    Name = comm.Name,
                    LogoLink = comm.LogoLink,
                    Link = comm.Link,
                    Description = comm.Decription,
                    OwnerID = comm.OwnerID ?? 0,
                    CreateDate = comm.CreateDate ?? System.DateTime.Now
                });
            }

            return results;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
