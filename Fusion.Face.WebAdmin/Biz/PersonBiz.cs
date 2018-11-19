using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Biz
{
    public class PersonBiz
    {
        public PersonInfo Find(Guid id)
        {
            using (FusionFaceDb db = new FusionFaceDb())
            {
                return db.People.Where(p => p.ID == id)
                    .Select(p => new PersonInfo()
                    {
                        ID = p.ID,
                        Fullname = p.Fullname,
                        IdentityNumber = p.IdentityNumber,
                        Nationality = p.Nationality,
                        PhoneNumber = p.PhoneNumber,
                        Address = p.Address,
                        Company = p.Company,
                        Dob = p.DOB,
                        Status = p.Status,
                        CreatedAt = p.CreatedAt,
                        ModifiedAt = p.ModifiedAt,
                        Photos = p.Photos.Where(i => !i.IsDeleted).Select(i => new PhotoInfo() {ID = i.ID }).ToList()
                    }
                ).FirstOrDefault();
            }
        }

        public PhotoInfo GetPhoto(Guid id)
        {
            using (FusionFaceDb db = new FusionFaceDb())
            {
                return db.Photos.Where(p => p.ID == id).Select(p => new PhotoInfo() { ID = p.ID, PersonID = p.PersonID }).FirstOrDefault();
            }
        }

        public byte[] GetPhotoData(Guid id)
        {
            using (FusionFaceDb db = new FusionFaceDb())
            {
                return db.Photos.Where(p => p.ID == id).Select(p => p.Data).FirstOrDefault();
            }
        }

        public bool DeletePhoto(Guid id)
        {
            using (FusionFaceDb db = new FusionFaceDb())
            {
                Photo photo = db.Photos.Where(p => p.ID == id).FirstOrDefault();
                photo.IsDeleted = true;
                db.SaveChanges();
                return true;
            }
        }


        public SearchResult Search(SearchInfo searchInfo)
        {
            if (searchInfo.search.value == null)
            {
                searchInfo.search.value = "";
            }

            string keyword = searchInfo.search.value;

            using (FusionFaceDb db = new FusionFaceDb())
            {
                var q = (from u in db.People
                         where (u.Fullname.Contains(keyword)
                         || u.IdentityNumber.Contains(keyword)
                         || u.Nationality.Contains(keyword)
                         || u.Company.Contains(keyword))
                         && !u.IsDeleted
                         select new PersonInfo()
                         {
                             ID = u.ID,
                             Fullname = u.Fullname,
                             IdentityNumber = u.IdentityNumber,
                             Dob = u.DOB,
                             Company = u.Company,
                             Nationality = u.Nationality,
                             PhoneNumber = u.PhoneNumber,
                             Address = u.Address,
                             Status = u.Status,
                             CreatedAt = u.CreatedAt,
                             ModifiedAt = u.ModifiedAt
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
                    q = q.OrderBy(x => x.Fullname);
                }

                List<PersonInfo> persons = q.Skip(searchInfo.start).Take(searchInfo.length).ToList();

                SearchResult result = new SearchResult()
                {
                    draw = searchInfo.draw,
                    recordsTotal = total,
                    recordsFiltered = total,
                    data = persons.ToArray()
                };

                return result;
            }
        }
    }
}