using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Net;

namespace Threads
{
    public class ThreadsService : IService
    {
        #region Community
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
                        DefaultColumnID = 0,
                        CountMembers = comm.CountMembers
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

            long memberID = 0;

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
                        DefaultColumnID = comm.DefaultColumnID ?? 0,
                        CountMembers = comm.CountMembers
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

            long memberID = 0;
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
                        DefaultColumnID = comm.DefaultColumnID ?? 0,
                        CountMembers = comm.CountMembers
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

            long memberID = 0;
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
                        DefaultColumnID = comm.DefaultColumnID ?? 0,
                        CountMembers = comm.CountMembers
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
        #endregion

        #region Country

        public wsResponse<Country_ReadDict_Resp> Country_ReadDict()
        {
            var results = new wsResponse<Country_ReadDict_Resp>();
            var resp = new Country_ReadDict_Resp();
            var dc = new DataThreadsDataContext();

            try
            {
                foreach (Country_ReadDictResult country in dc.Country_ReadDict())
                {
                    resp.Add(new wsCountry()
                    {
                        Code = country.Code,
                        Name = country.Name

                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = "Error in Country_ReadDict";
            }

            return results;
        }

        public wsResponse<Country_ReadDict_Resp> GetCountry_ReadDict()
        {
            var results = new wsResponse<Country_ReadDict_Resp>();
            var resp = new Country_ReadDict_Resp();
            var dc = new DataThreadsDataContext();

            try
            {
                foreach (Country_ReadDictResult country in dc.Country_ReadDict())
                {
                    resp.Add(new wsCountry()
                    {
                        Code = country.Code,
                        Name = country.Name

                    });
                }
                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 0;
                results.ErrText = "Error in GetCountry_ReadDict";
            }

            return results;
        }
        #endregion

        #region Entry
        //----------------------------Entry
        public wsResponse<Entry_ReadByCommunityID_Resp> Entry_ReadByCommunityID(wsRequest<Entry_ReadByCommunityID_Req> req)
        {
            var results = new wsResponse<Entry_ReadByCommunityID_Resp>();
            var resp = new Entry_ReadByCommunityID_Resp();
            var dc = new DataThreadsDataContext();
            long communityID = 0;
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
                        Community_ID = entry.Community_ID ?? 0,
                        ColumnCommunity_ID = entry.ColumnCommunity_ID ?? 0,
                        Community_Name = entry.Community_Name,
                        Entry_ID = entry.Entry_ID,
                        ColumnCommunity_Name = entry.ColumnCommunity_Name,
                        Entry_Text = entry.Entry_Text,
                        Entry_CreateDate = entry.Entry_CreateDate,
                        Entry_CreateDateEst = entry.Entry_CreateDateEst,
                        CreatorID = entry.CreatorID ?? 0,
                        CreatorID_Fullname = entry.CreatorID_Fullname
                    });
                }

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in Entry_ReadByCommunityID {0}", communityID);
            }
            return results;
        }

        public wsResponse<Entry_Save_Resp> Entry_Save(wsRequest<Entry_Save_Req> req)
        {
            var results = new wsResponse<Entry_Save_Resp>();
            var resp = new Entry_Save_Resp();
            var dc = new DataThreadsDataContext();

            long communityID = 0;
            long columnID = 0;
            long creatorID = 0;
            String entryText = "";

            if (req.Params != null)
            {
                communityID = req.Params.CommunityID;
                columnID = req.Params.ColumnID;
                creatorID = req.Params.CreatorID;
                communityID = req.Params.CommunityID;
                entryText = req.Params.EntryText;
            }
            else
            {
                communityID = 0;
                columnID = 0;
                creatorID = 0;
                entryText = "";
            }

            try
            {
                foreach (Entry_SaveResult entry in dc.Entry_Save(communityID, columnID, creatorID, entryText))
                {
                    resp.ID = entry.ID;
                }

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in Entry_Save {0}", communityID);
            }
            return results;
        }
        #endregion

        #region News
        //----------------------------News
        public wsResponse<News_ReadByMemberID_Resp> News_ReadByMemberID(wsRequest<News_ReadByMemberID_Req> req)
        {
            var results = new wsResponse<News_ReadByMemberID_Resp>();
            var resp = new News_ReadByMemberID_Resp();
            var dc = new DataThreadsDataContext();
            long memberID = 0;
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
                        ColumnCommunity_ID = news.ColumnCommunity_ID,
                        ColumnCommunity_Name = news.ColumnCommunity_Name,
                        Entry_Text = news.Entry_Text,
                        Entry_CreateDate = news.Entry_CreateDate,
                        Entry_CreateDateEst = news.Entry_CreateDateEst,
                        CreatorID = news.CreatorID ?? 0,
                        CreatorID_Fullname = news.CreatorID_Fullname
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
        #endregion

        #region Session

        public wsResponse<SessionReq_Save_Resp> SessionReq_Save(wsRequest<SessionReq_Save_Req> req)
        {
            var results = new wsResponse<SessionReq_Save_Resp>();
            var resp = new SessionReq_Save_Resp();
            var dc = new DataThreadsDataContext();
            string DID = "";
            string Phone = "";

            Random generator = new Random();
            string code = generator.Next(0, 1000).ToString("D4");

            if (req.Params != null)
            {
                DID = req.Params.SessionReq.DID;
                Phone = req.Params.SessionReq.Phone;
            }

            try
            {
              /*  string message = String.Format("Comm+code+confirm:+{0}", code);
                string http = String.Format("{0}sms.ru/sms/send?api_id={1}&to={2}&text={3}", "http://", "8B4D21F6-33D2-DBD4-8425-34631CD434BE", Phone, message);
                var request = (HttpWebRequest)WebRequest.Create(http);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();*/
            }
            catch
            {
                results.ErrCode = 2;
                results.ErrText = String.Format("Error in Send sms {0}", Phone);
            }
            try
            {
                foreach (SessionReq_SaveResult res in dc.SessionReq_Save(DID, Phone))
                {
                    resp.ID = res.ID ?? 0;
                    resp.Code = code;
                }

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in SessionReq_Save {0}", Phone);
            }

            return results;
        }

        public wsResponse<Session_Save_Resp> Session_Save(wsRequest<Session_Save_Req> req)
        {
            var results = new wsResponse<Session_Save_Resp>();
            var resp = new Session_Save_Resp();
            var dc = new DataThreadsDataContext();
            long sessionReq_ID = 0;
            string dID = "";
            string Phone = "";

            if (req.Params != null)
            {
                sessionReq_ID = req.Params.SessionReq_ID;
                dID = req.DID;
            }

            try
            {
                foreach (Session_SaveResult res in dc.Session_Save(sessionReq_ID, dID))
                {
                    resp.SessionID = res.SessionID;
                    resp.MemberID = res.MemberID ?? 0;
                }

                results.Data = resp;
            }
            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in Session_Save {0}", Phone);
            }

            return results;
        }

        #endregion

        #region Member
        public wsResponse<Member_ReadInstance_Resp> Member_ReadInstance(wsRequest<Member_ReadInstance_Req> req)
        {
            var results = new wsResponse<Member_ReadInstance_Resp>();
            var resp = new Member_ReadInstance_Resp();
            var dc = new DataThreadsDataContext();
            long memberID = 0;
            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
            }

            try
            {

                foreach (Member_ReadInstanceResult mem in dc.Member_ReadInstance(memberID))
                {
                    resp.ID = mem.ID;
                    resp.Name = mem.Name;
                    resp.Surname = mem.Surname;
                    resp.UserName = mem.UserName;
                    resp.About = mem.About;
                    resp.Phone = mem.Phone;

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

        public wsResponse<Member_Save_Resp> Member_Save(wsRequest<Member_Save_Req> req)
        {
            var results = new wsResponse<Member_Save_Resp>();
            var resp = new Member_Save_Resp();
            var dc = new DataThreadsDataContext();

            long? mID = 0;
            string mName = "";
            string mSurname = "";
            string mUserName = "";
            string mAbout = "";
            string mPhone = "";

            if (req.Params != null)
            {
                mID = req.Params.Member.ID;
                mName = req.Params.Member.Name;
                mSurname = req.Params.Member.Surname;
                mUserName = req.Params.Member.UserName;
                mAbout = req.Params.Member.About;
                mPhone = req.Params.Member.Phone;

            }

            try
            {
                foreach (Member_SaveResult mem in dc.Member_Save(ref mID, mName, mSurname, mUserName, mAbout, mPhone))
                {
                    resp.ID = mem.ID;
                    resp.Name = mem.Name;
                    resp.Surname = mem.Name;
                    resp.UserName = mem.UserName;
                    resp.About = mem.About;
                    resp.Phone = mem.Phone;
                }

                results.Data = resp;
            }


            catch
            {
                results.ErrCode = 1;
                results.ErrText = String.Format("Error in Member_Save {0}", mID);
            }

            return results;
        }

        #endregion
    }
}
