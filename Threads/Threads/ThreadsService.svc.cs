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
    public class ThreadsService : IService
    {
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public wsResponse<Entry_ReadByCommunityID_Resp> Entry_ReadByCommunityID(wsRequest<Entry_ReadByCommunityID_Req> req)
        {
            var results = new wsResponse<Entry_ReadByCommunityID_Resp>();
            var resp = new Entry_ReadByCommunityID_Resp();
            var dc = new DataThreadsDataContext();
            var communityID = 0;
            if (req.Params != null)
            {
                communityID = req.Params.CommunityID;
            } else
            {
                communityID = 0;
            }

            try
            {
                foreach (Entry_ReadByCommunityIDResult entry in dc.Entry_ReadByCommunityID(communityID))
                {
                    resp.Add(new wsEntry()
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

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in Entry_ReadByCommunityID{0}", communityID);
            }
            return results;
        }

        public wsResponse<News_ReadByPersonID_Resp> News_ReadByPersonID(wsRequest<News_ReadByPersonID_Req> req)
        {
            var results = new wsResponse<News_ReadByPersonID_Resp>();
            var resp = new News_ReadByPersonID_Resp();
            var dc = new DataThreadsDataContext();
            var personID = 0;
            if (req.Params != null)
            {
                personID = req.Params.PersonID;
            } else
            {
                personID = 0;
            }

            try
            {
                foreach (News_ReadByPersonIDResult news in dc.News_ReadByPersonID(personID))
                {
                    resp.Add(new wsNews()
                    {
                        CommunityID = news.CommunityID,
                        CommunityID_Name = news.CommunityID_Name,
                        EntryID = news.EntryID,
                        ColumnCommunityID_Name = news.ColumnCommunityID_Name,
                        EntryID_EntryText = news.EntryID_EntryText,
                        EntryID_CreateDate = news.EntryID_CreateDate ?? System.DateTime.Now
                    });
                }

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in News_ReadByPersonID {0}", personID);
            }

            return results;
        }

        public wsResponse<Community_ReadDict_Resp> Community_ReadDict(wsRequest<Community_ReadDict_Req> req)
        {
            var results = new wsResponse<Community_ReadDict_Resp>();
            var resp = new Community_ReadDict_Resp();

            DataThreadsDataContext dc = new DataThreadsDataContext();

            try
            {
                foreach (Community_ReadDictResult comm in dc.Community_ReadDict())
                {
                    resp.Add(new wsCommunity()
                    {
                        ID = comm.ID,
                        Name = comm.Name,
                        Description = comm.Decription,
                        OwnerID = comm.OwnerID ?? 0,
                        CreateDate = comm.CreateDate ?? System.DateTime.Now
                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = "Error in Community_ReadDict";
            }

            return results;
        }


        public wsResponse<Community_ReadDict_Resp> GetCommunity_ReadDict()
        {
            var results = new wsResponse<Community_ReadDict_Resp>();
            var resp = new Community_ReadDict_Resp();

            DataThreadsDataContext dc = new DataThreadsDataContext();

            try
            {
                foreach (Community_ReadDictResult comm in dc.Community_ReadDict())
                {
                    resp.Add(new wsCommunity()
                    {
                        ID = comm.ID,
                        Name = comm.Name,
                        Description = comm.Decription,
                        OwnerID = comm.OwnerID ?? 0,
                        CreateDate = comm.CreateDate ?? System.DateTime.Now
                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = "Error in Community_ReadDict";
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
