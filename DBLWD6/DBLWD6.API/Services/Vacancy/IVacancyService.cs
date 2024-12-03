using DBLWD6.API.Models;
using DBLWD6.Domain.Entities;

namespace DBLWD6.API.Services
{
    public interface IVacancyService
    {
        Task<ResponseData<IEnumerable<Vacancy>>> GetVacanciesCollection(int? page, int? itemsPerPage);
        Task<ResponseData<Vacancy>> GetVacancyById(int id);
        Task<ResponseData<bool>> AddVacancy(Vacancy vacancy);
        Task<ResponseData<bool>> UpdateVacancy(Vacancy vacancy, int prevId);
        Task<ResponseData<bool>> DeleteVacancy(int id);
    }
}
