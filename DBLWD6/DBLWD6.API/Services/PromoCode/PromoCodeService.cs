using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public PromoCodeService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<PromoCode>>> GetPromoCodesCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = page.Value * itemsPerPage.Value;
            int endIndex = (page.Value + 1) * itemsPerPage.Value;
            IEnumerable<PromoCode> promoCodes;
            Expression<Func<PromoCode, bool>> predicate = p => p.Id >= startIndex && p.Id < endIndex;

            try
            {
                promoCodes = await _dbService.PromoCodeTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<PromoCode>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<PromoCode>>(promoCodes);
        }

        public async Task<ResponseData<PromoCode>> GetPromoCodeById(int id)
        {
            PromoCode promoCode;
            try
            {
                promoCode = await _dbService.PromoCodeTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<PromoCode>(false, ex.Message);
            }

            return new ResponseData<PromoCode>(promoCode);
        }

        public async Task<ResponseData<bool>> AddPromoCode(PromoCode promoCode)
        {
            try
            {
                await _dbService.PromoCodeTable.Add(promoCode);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdatePromoCode(PromoCode promoCode, int prevId)
        {
            try
            {
                await _dbService.PromoCodeTable.Update(promoCode, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeletePromoCode(int id)
        {
            try
            {
                await _dbService.PromoCodeTable.Delete(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
