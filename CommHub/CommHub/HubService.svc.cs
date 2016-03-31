using CommHub.wsClasses;
using System;

namespace CommHub
{
    public class HubService : IService
    {
        public wsResponse<SubjectComm_ReadDict_Resp> SubjectComm_ReadDict(wsRequest<SubjectComm_ReadDict_Req> req)
        {
            var funcName = "SubjectComm_ReadDict";
            var errCode = 0;
            var errorText = "";

            var results = new wsResponse<SubjectComm_ReadDict_Resp>();
            var resp = new SubjectComm_ReadDict_Resp();
            var dc = new DataHubDataContext();
            long ownerHubID = 0;
            
            if (req.Params != null)
            {
                ownerHubID = req.Params.ownerHubID;
            }
            else
            {
                errCode = 200;
                errorText = Tools.GetErrorTextByCode(errCode);
                Tools.ErrorLog_Save(req, "", funcName, errorText);

                results.ErrText = string.Format("{0}\n{1}", funcName, errorText);
                results.ErrCode = -1;
                return results;
            }

            try
            {
                foreach (SubjectComm_ReadDictResult sbj in dc.SubjectComm_ReadDict(ownerHubID))
                {
                    resp.Add(new wsSubjectComm()
                    {
                        id = sbj.id,
                        name = sbj.name
                    });
                };

                results.Data = resp;
            }
            catch (Exception e)
            {
                errCode = 101;
                string param = string.Format("ownerHubID: {0}", ownerHubID);
                Tools.ErrorLog_Save(req, param, funcName, e.Message);

                results.ErrCode = errCode;
                results.ErrText = string.Format("{0}\n{1}\n{2}", funcName, Tools.GetErrorTextByCode(errCode), e.Message);
            }

            return results;
        }
    }
}
