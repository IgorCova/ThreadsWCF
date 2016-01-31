using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace Threads
{
    public class ThreadsService : IService
    {
        //----------------------------Community
        public wsResponse<Community_ReadDict_Resp> GetCommunity_ReadDict()
        {
            var results = new wsResponse<Community_ReadDict_Resp>();
            var resp = new Community_ReadDict_Resp();
            var dc = new DataThreadsDataContext();

            try
            {
                foreach (Community_ReadDictResult comm in dc.Community_ReadDict(1))
                {
                    resp.Add(new wsCommunity()
                    {
                        ID = comm.ID,
                        Name = comm.Name,
                        Description = comm.Decription,
                        OwnerID = comm.OwnerID ?? 0,
                        IsMember = comm.IsMember ?? false,
                        CreateDate = comm.CreateDate ?? System.DateTime.Now,
                        DefaultColumnID = 0
                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = "Error in Community_ReadDict memberID: 1";
            }

            return results;
        }

        public wsResponse<Community_ReadDict_Resp> Community_ReadDict(wsRequest<Community_ReadDict_Req> req)
        {
            var results = new wsResponse<Community_ReadDict_Resp>();
            var resp = new Community_ReadDict_Resp();
            var dc = new DataThreadsDataContext();

            var memberID = 0;
            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
            }
            else
            {
                memberID = 0;
            }

            try
            {
                foreach (Community_ReadDictResult comm in dc.Community_ReadDict(memberID))
                {
                    resp.Add(new wsCommunity()
                    {
                        ID = comm.ID,
                        Name = comm.Name,
                        Description = comm.Decription,
                        OwnerID = comm.OwnerID ?? 0,
                        IsMember = comm.IsMember ?? false,
                        CreateDate = comm.CreateDate ?? System.DateTime.Now,
                        DefaultColumnID = 0
                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = String.Format("Error in Community_ReadDict memberID: {0}", memberID);
            }

            return results;
        }

        public wsResponse<Community_ReadDict_Resp> Community_ReadMyDict(wsRequest<Community_ReadDict_Req> req)
        {
            var results = new wsResponse<Community_ReadDict_Resp>();
            var resp = new Community_ReadDict_Resp();
            var dc = new DataThreadsDataContext();

            var memberID = 0;
            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
            }
            else
            {
                memberID = 0;
            }

            try
            {
                foreach (Community_ReadMyDictResult comm in dc.Community_ReadMyDict(memberID))
                {
                    resp.Add(new wsCommunity()
                    {
                        ID = comm.ID,
                        Name = comm.Name,
                        Description = comm.Decription,
                        OwnerID = comm.OwnerID ?? 0,
                        IsMember = comm.IsMember ?? false,
                        CreateDate = comm.CreateDate ?? System.DateTime.Now,
                        DefaultColumnID = comm.DefaultColumnID ?? 0
                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = String.Format("Error in Community_ReadMyDict {0}", memberID);
            }

            return results;
        }

        public wsResponse<Community_ReadDict_Resp> Community_ReadSuggestDict(wsRequest<Community_ReadDict_Req> req)
        {
            var results = new wsResponse<Community_ReadDict_Resp>();
            var resp = new Community_ReadDict_Resp();
            var dc = new DataThreadsDataContext();

            var memberID = 0;
            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
            }
            else
            {
                memberID = 0;
            }

            try
            {
                foreach (Community_ReadSuggestDictResult comm in dc.Community_ReadSuggestDict(memberID))
                {
                    resp.Add(new wsCommunity()
                    {
                        ID = comm.ID,
                        Name = comm.Name,
                        Description = comm.Decription,
                        OwnerID = comm.OwnerID ?? 0,
                        IsMember = comm.IsMember ?? false,
                        CreateDate = comm.CreateDate ?? System.DateTime.Now,
                        DefaultColumnID = comm.DefaultColumnID ?? 0
                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = String.Format("Error in Community_ReadSuggestDict {0}", memberID);
            }

            return results;
        }


        //----------------------------Entry
        public wsResponse<Entry_ReadByCommunityID_Resp> Entry_ReadByCommunityID(wsRequest<Entry_ReadByCommunityID_Req> req)
        {
            var results = new wsResponse<Entry_ReadByCommunityID_Resp>();
            var resp = new Entry_ReadByCommunityID_Resp();
            var dc = new DataThreadsDataContext();
            var communityID = 0;
            if (req.Params != null)
            {
                communityID = req.Params.CommunityID;
            }
            else
            {
                communityID = 0;
            }

            try
            {
                foreach (Entry_ReadByCommunityIDResult entry in dc.Entry_ReadByCommunityID(communityID))
                {
                    resp.Add(new wsEntry()
                    {
                        Community_ID = entry.Community_ID,
                        Community_Name = entry.Community_Name,
                        Entry_ID = entry.Entry_ID,
                        ColumnCommunity_Name = entry.ColumnCommunity_Name,
                        Entry_Text = entry.Entry_Text,
                        Entry_CreateDate = entry.Entry_CreateDate ?? System.DateTime.Now
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


        //----------------------------News
        public wsResponse<News_ReadByMemberID_Resp> News_ReadByMemberID(wsRequest<News_ReadByMemberID_Req> req)
        {
            var results = new wsResponse<News_ReadByMemberID_Resp>();
            var resp = new News_ReadByMemberID_Resp();
            var dc = new DataThreadsDataContext();
            var memberID = 0;
            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
            }
            else
            {
                memberID = 0;
            }

            try
            {
                foreach (News_ReadByMemberIDResult news in dc.News_ReadByMemberID(memberID))
                {
                    resp.Add(new wsEntry()
                    {
                        Community_ID = news.Community_ID,
                        Community_Name = news.Community_Name,
                        Entry_ID = news.Entry_ID,
                        ColumnCommunity_Name = news.ColumnCommunity_Name,
                        Entry_Text = news.Entry_Text,
                        Entry_CreateDate = news.Entry_CreateDate ?? System.DateTime.Now
                    });
                }

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in News_ReadByPersonID {0}", memberID);
            }

            return results;
        }


        //----------------------------Member
        public wsResponse<Member_ReadInstance_Resp> Member_ReadInstance(wsRequest<Member_ReadInstance_Req> req)
        {
            var results = new wsResponse<Member_ReadInstance_Resp>();
            var resp = new Member_ReadInstance_Resp();
            var dc = new DataThreadsDataContext();

            var memberID = 0;
            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
            }
            else
            {
                memberID = 0;
            }

            try
            {

                foreach (Member_ReadInstanceResult mem in dc.Member_ReadInstance(memberID))
                {
                    resp.ID = mem.ID;
                    resp.Name = mem.Name;
                    resp.UserName = mem.UserName;
                    resp.FullName = mem.FullName;
                    resp.About = mem.About;
                    resp.JoinedDate = mem.JoinedDate ?? System.DateTime.Now;
                }

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in Member_ReadInstance {0}", memberID);
            }

            return results;
        }
    }
}
