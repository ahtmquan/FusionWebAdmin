using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Biz
{
    public class ReportBiz
    {
        public SearchResult SearchReportA(SearchInfo searchInfo)
        {
            if (searchInfo.search.value == null)
            {
                searchInfo.search.value = "";
            }

            string keyword = searchInfo.search.value;

            using (FusionFaceDb db = new FusionFaceDb())
            {
                var q = (from u in db.UserMasters
                         where (u.Username.Contains(keyword)
                         || u.Fullname.Contains(keyword))
                         && !u.IsDeleted
                         select new UserInfo()
                         {
                             ID = u.ID,
                             Username = u.Username,
                             Email = u.Email,
                             Status = u.Status,
                             Fullname = u.Fullname
                         });

                int total = q.Count();



                if (searchInfo.order != null && searchInfo.order.Length > 0)
                {
                    string orderBy = "";
                    bool orderDesc = false;

                    orderBy = searchInfo.columns[searchInfo.order[0].column].data;
                    orderDesc = searchInfo.order[0].dir == "desc";

                    q = q.OrderByDynamic(orderBy, orderDesc);
                }
                else
                {
                    q = q.OrderBy(x => x.Username);
                }

                List<UserInfo> users = q.Skip(searchInfo.start).Take(searchInfo.length).ToList();

                SearchResult result = new SearchResult()
                {
                    draw = searchInfo.draw,
                    recordsTotal = total,
                    recordsFiltered = total,
                    data = users.ToArray()
                };

                return result;
            }
        }

        public SearchResult SearchReportTransaction(SearchInfo searchInfo)
        {
            if (searchInfo.search.value == null)
            {
                searchInfo.search.value = "";
            }

            string keyword = searchInfo.search.value;
            bool dateFiltered = false;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;

            if (searchInfo.search.fields != null && searchInfo.search.fields.Length >= 2)
            {
                try
                {
                    int iStartDate = Array.IndexOf(searchInfo.search.fields, "startDate");
                    startDate = DateTime.ParseExact(searchInfo.search.values[iStartDate], "yyyy-MM-dd", null);

                    int iEndDate = Array.IndexOf(searchInfo.search.fields, "endDate");
                    endDate = DateTime.ParseExact(searchInfo.search.values[iEndDate], "yyyy-MM-dd", null);
                    endDate = endDate.AddDays(1);

                    dateFiltered = endDate >= startDate;
                }
                catch { }
            }
            

            using (FusionFaceDb db = new FusionFaceDb())
            {
                var q = (from u in db.TransactionMasters
                         where (u.TranxType.Contains(keyword)
                         || u.ObjectID.Contains(keyword))
                         select new TransactionInfo()
                         {
                             ID = u.ID,
                             TranxType = u.TranxType,
                             ObjectID = u.ObjectID,
                             ObjectName = u.ObjectName,
                             RecordedDate = u.RecordedDate,
                             ClientID = u.ClientID
                         });

                if (dateFiltered)
                {
                    q = q.Where(t => t.RecordedDate >= startDate && t.RecordedDate < endDate);
                }

                int total = q.Count();


                if (searchInfo.order != null && searchInfo.order.Length > 0)
                {
                    string orderBy = "";
                    bool orderDesc = false;

                    orderBy = searchInfo.columns[searchInfo.order[0].column].data;
                    orderDesc = searchInfo.order[0].dir == "desc";

                    q = q.OrderByDynamic(orderBy, orderDesc);
                }
                else
                {
                    q = q.OrderByDescending(x => x.RecordedDate);
                }

                List<TransactionInfo> list = q.Skip(searchInfo.start).Take(searchInfo.length).ToList();

                SearchResult result = new SearchResult()
                {
                    draw = searchInfo.draw,
                    recordsTotal = total,
                    recordsFiltered = total,
                    data = list.ToArray()
                };

                return result;
            }
        }


        public SearchResult SearchReportTransactionSummary(SearchInfo searchInfo)
        {
            if (searchInfo.search.value == null)
            {
                searchInfo.search.value = "";
            }

            string keyword = searchInfo.search.value;
            bool dateFiltered = false;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;

            if (searchInfo.search.fields != null && searchInfo.search.fields.Length >= 2)
            {
                try
                {
                    int iStartDate = Array.IndexOf(searchInfo.search.fields, "startDate");
                    startDate = DateTime.ParseExact(searchInfo.search.values[iStartDate], "yyyy-MM-dd", null);

                    int iEndDate = Array.IndexOf(searchInfo.search.fields, "endDate");
                    endDate = DateTime.ParseExact(searchInfo.search.values[iEndDate], "yyyy-MM-dd", null);
                    endDate = endDate.AddDays(1);

                    dateFiltered = endDate >= startDate;
                }
                catch { }
            }

            using (FusionFaceDb db = new FusionFaceDb())
            {
                var q = (from d in db.DateMasters
                         join u in db.TransactionSummaries on d.DateID equals u.RecordedDate into ux
                         from u in ux.DefaultIfEmpty()
                         where d.DateID >= startDate && d.DateID < endDate
                         select new TransactionSummaryInfo()
                         {
                             TranxType = (u != null) ? u.TranxType : "PREDICT",
                             RecordedDate = d.DateID,
                             ClientID = (u != null) ? u.ClientID : "AA",
                             Total = (u != null) ? u.Total : 0
                         });

                int total = q.Count();

                List<TransactionSummaryInfo> list = q.ToList();

                List<DateTime> dates = list.Select(u => u.RecordedDate).Distinct().ToList();

                foreach (DateTime date in dates)
                {
                    if (list.Where(u => u.RecordedDate == date).Count() < 2)
                    {
                        if (list.Where(u => u.RecordedDate == date).FirstOrDefault().TranxType == "PREDICT")
                        {
                            list.Add(new TransactionSummaryInfo() { TranxType = "REGISTER", RecordedDate = date, Total = 0, ClientID = "AA" });
                        }
                        else {
                            list.Add(new TransactionSummaryInfo() { TranxType = "PREDICT", RecordedDate = date, Total = 0, ClientID = "AA" });
                        }
                    }
                }

                list = list.OrderBy(u => u.RecordedDate).ThenBy(u => u.TranxType).ToList();

                SearchResult result = new SearchResult()
                {
                    draw = searchInfo.draw,
                    recordsTotal = total,
                    recordsFiltered = total,
                    data = list.ToArray()
                };

                return result;
            }
        }
    }
}