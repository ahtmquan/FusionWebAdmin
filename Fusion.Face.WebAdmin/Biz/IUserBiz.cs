using Fusion.Face.WebAdmin.Models;

namespace Fusion.Face.WebAdmin.Biz
{
    public interface IUserBiz : IBaseBiz
    {
        SearchResult Search(SearchInfo searchInfo);
    }
}