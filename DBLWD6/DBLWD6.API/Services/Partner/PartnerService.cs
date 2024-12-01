using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public PartnerService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Partner>>> GetPartnersCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = page.Value * itemsPerPage.Value;
            int endIndex = (page.Value + 1) * itemsPerPage.Value;
            IEnumerable<Partner> partners;
            Expression<Func<Partner, bool>> predicate = p => p.Id >= startIndex && p.Id < endIndex;

            try
            {
                partners = await _dbService.PartnerTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Partner>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Partner>>(partners);
        }

        public async Task<ResponseData<Partner>> GetPartnerById(int id)
        {
            Partner partner;
            try
            {
                partner = await _dbService.PartnerTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Partner>(false, ex.Message);
            }

            return new ResponseData<Partner>(partner);
        }

        public async Task<ResponseData<bool>> AddPartner(Partner partner)
        {
            try
            {
                await _dbService.PartnerTable.Add(partner);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdatePartner(Partner partner, int prevId)
        {
            try
            {
                await _dbService.PartnerTable.Update(partner, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeletePartner(int Id)
        {
            try
            {
                await _dbService.PartnerTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
