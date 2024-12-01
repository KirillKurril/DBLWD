namespace DBLWD6.API.Services
{
    public interface IPartnerService
    {
        Task<ResponseData<IEnumerable<Partner>>> GetPartnersCollection(int? page, int? itemsPerPage);
        Task<ResponseData<Partner>> GetPartnerById(int id);
        Task<ResponseData<bool>> AddPartner(Partner partner);
        Task<ResponseData<bool>> UpdatePartner(Partner partner, int prevId);
        Task<ResponseData<bool>> DeletePartner(int Id);
    }
}
