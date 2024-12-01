namespace DBLWD6.API.Services
{
    public interface IPromoCodeService
    {
        Task<ResponseData<IEnumerable<PromoCode>>> GetPromoCodesCollection(int? page, int? itemsPerPage);
        Task<ResponseData<PromoCode>> GetPromoCodeById(int id);
        Task<ResponseData<bool>> AddPromoCode(PromoCode promoCode);
        Task<ResponseData<bool>> UpdatePromoCode(PromoCode promoCode, int prevId);
        Task<ResponseData<bool>> DeletePromoCode(int id);
    }
}
