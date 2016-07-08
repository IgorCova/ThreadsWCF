using System;
using System.IO;
using System.Net;

namespace Threads
{
    public class ThreadsService : IService
    {
        #region Bookmark
        public wsResponse<Bookmark_Save_Resp> Bookmark_Save(wsRequest<Bookmark_Save_Req> req)
        {
            var funcName = "Bookmark_Save";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<Bookmark_Save_Resp>();
            var resp = new Bookmark_Save_Resp();
            var dc = new DataThreadsDataContext();

            long memberID = 0;
            long entryID = 0;

            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
                entryID = req.Params.EntryID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (Bookmark_SaveResult bmk in dc.Bookmark_Save(entryID, memberID))
                {
                    resp.IsPin = bmk.IsPin ?? false;
                };

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("memberID: {0}, entryID: {1}", memberID, entryID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }

        public wsResponse<Bookmark_ReadByMemberID_Resp> Bookmark_ReadByMemberID(wsRequest<Bookmark_ReadByMemberID_Req> req)
        {
            var funcName = "Bookmark_ReadByMemberID";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<Bookmark_ReadByMemberID_Resp>();
            var resp = new Bookmark_ReadByMemberID_Resp();
            var dc = new DataThreadsDataContext();
            long memberID = 0;
            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (Bookmark_ReadByMemberIDResult bmk in dc.Bookmark_ReadByMemberID(memberID))
                {
                    resp.Add(new wsEntry()
                    {
                        Community_ID = bmk.Community_ID,
                        Community_Name = bmk.Community_Name,
                        Entry_ID = bmk.Entry_ID,
                        ColumnCommunity_ID = bmk.ColumnCommunity_ID,
                        ColumnCommunity_Name = bmk.ColumnCommunity_Name,
                        Entry_Text = bmk.Entry_Text,
                        Entry_CreateDate = bmk.Entry_CreateDate,
                        Entry_CreateDateEst = bmk.Entry_CreateDateEst,
                        CreatorID = bmk.CreatorID ?? 0,
                        CreatorID_Fullname = bmk.CreatorID_Fullname,
                        IsPin = true
                    });
                }

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("memberID: {0}", memberID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region ColumnCommunity
        public wsResponse<ColumnCommunity_ReadDict_Resp> ColumnCommunity_ReadDict(wsRequest<ColumnCommunity_ReadDict_Req> req)
        {
            var funcName = "ColumnCommunity_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<ColumnCommunity_ReadDict_Resp>();
            var resp = new ColumnCommunity_ReadDict_Resp();
            var dc = new DataThreadsDataContext();

            long communityID = 0;

            if (req.Params != null)
            {
                communityID = req.Params.CommunityID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (ColumnCommunity_ReadDictResult clmn in dc.ColumnCommunity_ReadDict(communityID))
                {
                    resp.Add(new wsColumnCommunity()
                    {
                        Name = clmn.Name,
                        ID = clmn.ID
                    });

                    results.Data = resp;
                }
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("communityID: {0}", communityID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region Community
        public wsResponse<Community_ReadDict_Resp> Community_ReadDict(wsRequest<Community_ReadDict_Req> req)
        {
            var funcName = "Community_ReadDict";
            var errCode = 0;
            var errorText = "";

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
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
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
                        Tagline = comm.Tagline,
                        Link = comm.Link,
                        OwnerID = comm.OwnerID ?? 0,
                        IsMember = comm.IsMember ?? false,
                        CreateDate = comm.CreateDate ?? DateTime.Now,
                        DefaultColumnID = comm.DefaultColumnID ?? 0,
                        CountMembers = comm.CountMembers
                    });
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("memberID: {0}", memberID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }

        public wsResponse<Community_ReadDict_Resp> Community_ReadMyDict(wsRequest<Community_ReadDict_Req> req)
        {
            var funcName = "Community_ReadMyDict";
            var errCode = 0;
            var errorText = "";

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
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
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
                        Tagline = comm.Tagline,
                        Link = comm.Link,
                        OwnerID = comm.OwnerID ?? 0,
                        IsMember = comm.IsMember ?? false,
                        CreateDate = comm.CreateDate ?? DateTime.Now,
                        DefaultColumnID = comm.DefaultColumnID ?? 0,
                        CountMembers = comm.CountMembers
                    });
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("memberID: {0}", memberID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }

        public wsResponse<Community_ReadDict_Resp> Community_ReadSuggestDict(wsRequest<Community_ReadDict_Req> req)
        {
            var funcName = "Community_ReadSuggestDict";
            var errCode = 0;
            var errorText = "";

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
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
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
                        Tagline = comm.Tagline,
                        Link = comm.Link,
                        OwnerID = comm.OwnerID ?? 0,
                        IsMember = comm.IsMember ?? false,
                        CreateDate = comm.CreateDate ?? DateTime.Now,
                        DefaultColumnID = comm.DefaultColumnID ?? 0,
                        CountMembers = comm.CountMembers
                    });
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("memberID: {0}", memberID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }

        public wsResponse<Community_ReadInstance_Resp> Community_ReadInstance(wsRequest<Community_ReadInstance_Req> req)
        {
            var funcName = "Community_ReadInstance";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<Community_ReadInstance_Resp>();
            var resp = new Community_ReadInstance_Resp();
            var dc = new DataThreadsDataContext();

            long memberID = 0;
            long iD = 0;

            if (req.Params != null)
            {
                memberID = req.Params.MemberID;
                iD = req.Params.ID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (Community_ReadInstanceResult comm in dc.Community_ReadInstance(iD, memberID))
                {
                    resp.ID = comm.ID;
                    resp.Name = comm.Name;
                    resp.Link = comm.Link;
                    resp.Description = comm.Decription;
                    resp.Tagline = comm.Tagline;
                    resp.OwnerID = comm.OwnerID ?? 0;
                    resp.IsMember = comm.IsMember ?? false;
                    resp.CreateDate = comm.CreateDate ?? DateTime.Now;
                    resp.DefaultColumnID = comm.DefaultColumnID ?? 0;
                    resp.CountMembers = comm.CountMembers;
                };

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("memberID: {0}, iD: {1}", memberID, iD);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }

        public wsResponse<Community_Save_Resp> Community_Save(wsRequest<Community_Save_Req> req)
        {
            var funcName = "Community_Save";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<Community_Save_Resp>();
            var resp = new Community_Save_Resp();
            var dc = new DataThreadsDataContext();

            long iD = 0;
            string name = "";
            string link = "";
            string description = "";
            string tagline = "";
            long ownerId = 0;

            if (req.Params != null)
            {
                iD = req.Params.Community.ID;
                name = req.Params.Community.Name;
                link = req.Params.Community.Link;
                description = req.Params.Community.Description;
                tagline = req.Params.Community.Tagline;
                ownerId = req.Params.Community.OwnerID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (Community_SaveResult comm in dc.Community_Save(iD, name, link, description, tagline, ownerId))
                {
                    resp.ID = comm.ID;
                }

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("iD: {0}, name: {1}, link: {2}, descripion: {3}, tagline: {4}, ownerId: {5}", iD, name, link, description, tagline, ownerId);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region Country

        public wsResponse<Country_ReadDict_Resp> Country_ReadDict()
        {
            var funcName = "Country_ReadDict";
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
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("{0}\nerror: {1}", funcName, e.Message);
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
            long columnID = 0;
            long memberID = 1;

            if (req.Params != null)
            {
                communityID = req.Params.CommunityID;
                columnID = req.Params.ColumnID ?? 0;
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "Entry_ReadByCommunityID: No Params";

                return results;
            }

            try
            {
                foreach (Entry_ReadByCommunityIDResult entry in dc.Entry_ReadByCommunityID(communityID, columnID, memberID))
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
                        CreatorID_Fullname = entry.CreatorID_Fullname,
                        IsPin = entry.IsPin
                    });
                }

                results.Data = resp;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("Entry_ReadByCommunityID {0}, error: {1}", communityID, e.Message);
            }
            return results;
        }

        public wsResponse<Entry_Save_Resp> Entry_Save(wsRequest<Entry_Save_Req> req)
        {
            var funcName = "Entry_Save";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<Entry_Save_Resp>();
            var resp = new Entry_Save_Resp();
            var dc = new DataThreadsDataContext();

            long communityID = 0;
            long columnID = 0;
            long creatorID = 0;
            string entryText = "";

            if (req.Params != null)
            {
                communityID = req.Params.CommunityID;
                columnID = req.Params.ColumnID;
                creatorID = req.Params.CreatorID;
                entryText = req.Params.EntryText;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
            }

            try
            {
                foreach (Entry_SaveResult entry in dc.Entry_Save(communityID, columnID, creatorID, entryText))
                {
                    resp.ID = entry.ID;
                }

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("communityID: {0}, columnID: {1}, creatorID: {2}, entryText: {3}", communityID, columnID, creatorID, entryText);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
        #endregion

        #region Image
        public wsResponse<LogoSave_Resp> LogoSave(wsRequest<LogoSave_Req> req)
        {
            var results = new wsResponse<LogoSave_Resp>();
            var resp = new LogoSave_Resp();
            var filename = "";

            if (req.Params != null)
            {
                filename = string.Format("{0}.png", 1);
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "LogoSave: No Params";

                return results;
            }

            try
            {
                Tools.ObjectFileSaveToLocalHDD(filename, req.Params.logoData);
                resp.isOk = true;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("LogoSave {0}, error: {1}", 1, e.Message);
                resp.isOk = false;
            }

            results.Data = resp;

            return results;
        }
        #endregion

        #region News
        //----------------------------News
        public wsResponse<News_ReadByMemberID_Resp> News_ReadByMemberID(wsRequest<News_ReadByMemberID_Req> req)
        {
            var funcName = "News_ReadByMemberID";
            var errCode = 0;
            var errorText = "";

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
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = errCode;
                return results;
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
                        CreatorID_Fullname = news.CreatorID_Fullname,
                        IsPin = news.IsPin
                    });
                }

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("memberID: {0}", memberID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
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
                DID = req.DID;
                Phone = req.Params.Phone;
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "SessionReq_Save: No Params";
                return results;
            }
            
            try
            {
                  string message = string.Format("Comm+code+confirm:+{0}", code);
                  string http = string.Format("{0}sms.ru/sms/send?api_id={1}&to={2}&text={3}", "http://", "8B4D21F6-33D2-DBD4-8425-34631CD434BE", Phone, message);
                  var request = (HttpWebRequest)WebRequest.Create(http);
                  var response = (HttpWebResponse)request.GetResponse();
                  var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e)
            {
                results.ErrCode = 40;
                results.ErrText = string.Format("Send sms {0}, error: {1}", Phone, e.Message);
            }

            try
            {
                foreach (SessionReq_SaveResult res in dc.SessionReq_Save(DID, Phone))
                {
                    resp.ID = res.ID ?? 0;
                    resp.Code = code;
                    resp.MemberID = res.MemberID;
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("SessionReq_Save {0}, error: {1}", Phone, e.Message);
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
            else
            {
                results.ErrCode = 200;
                results.ErrText = "Session_Save: No Params";
                return results;
            }

            try
            {
                foreach (Session_SaveResult res in dc.Session_Save(sessionReq_ID, dID))
                {
                    resp.SessionID = res.SessionID;
                    resp.MemberID = res.MemberID ?? 0;
                    resp.IsNewMember = res.IsNewMember ?? true;
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("Session_Save {0}: {1}", Phone, e.Message);
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
            else
            {
                results.ErrCode = 200;
                results.ErrText = "Member_ReadInstance: No Params";

                return results;
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
                    resp.IsMale = mem.IsMale ?? true;
                    resp.BirthdayDate = mem.BirthdayDate;
                }

                results.Data = resp;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("Member_ReadInstance {0}, error: {1}", memberID, e.Message);
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
            bool mIsMale = true;
            string mBirthdayDate = "";

            if (req.Params != null)
            {
                mID = req.Params.Member.ID;
                mName = req.Params.Member.Name;
                mSurname = req.Params.Member.Surname;
                mUserName = req.Params.Member.UserName;
                mAbout = req.Params.Member.About;
                mPhone = req.Params.Member.Phone;
                mIsMale = req.Params.Member.IsMale;
                mBirthdayDate = req.Params.Member.BirthdayDate;
            }
            else
            {
                results.ErrCode = 200;
                results.ErrText = "Member_Save: No Params";
                return results;
            }

            try
            {
                foreach (Member_SaveResult mem in dc.Member_Save(ref mID, mName, mSurname, mUserName, mAbout, mPhone, mIsMale, mBirthdayDate))
                {
                    resp.ID = mem.ID;
                    resp.Name = mem.Name;
                    resp.Surname = mem.Name;
                    resp.UserName = mem.UserName;
                    resp.About = mem.About;
                    resp.Phone = mem.Phone;
                    resp.IsMale = mem.IsMale ?? true;
                    resp.BirthdayDate = mem.BirthdayDate;
                }
                results.Data = resp;
            }
            catch (Exception e)
            {
                results.ErrCode = 101;
                results.ErrText = string.Format("Member_Save {0}, error: {1}", mID, e.Message);
            }
            return results;
        }

        #endregion
    }
}
