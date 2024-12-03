using System.Linq.Expressions;
using DBLWD6.API.Models;
using DBLWD6.Domain.Entities;

namespace DBLWD6.API.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public VacancyService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Vacancy>>> GetVacanciesCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = page.Value * itemsPerPage.Value;
            int endIndex = (page.Value + 1) * itemsPerPage.Value;
            IEnumerable<Vacancy> vacancies;
            Expression<Func<Vacancy, bool>> predicate = v => v.Id >= startIndex && v.Id < endIndex;

            try
            {
                vacancies = await _dbService.VacancyTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Vacancy>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Vacancy>>(vacancies);
        }

        public async Task<ResponseData<Vacancy>> GetVacancyById(int id)
        {
            Vacancy vacancy;
            try
            {
                vacancy = await _dbService.VacancyTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Vacancy>(false, ex.Message);
            }

            return new ResponseData<Vacancy>(vacancy);
        }

        public async Task<ResponseData<bool>> AddVacancy(Vacancy vacancy)
        {
            try
            {
                vacancy.CreatedAt = DateTime.UtcNow;
                await _dbService.VacancyTable.Add(vacancy);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateVacancy(Vacancy vacancy, int prevId)
        {
            try
            {
                await _dbService.VacancyTable.Update(vacancy, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteVacancy(int id)
        {
            try
            {
                await _dbService.VacancyTable.Delete(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
