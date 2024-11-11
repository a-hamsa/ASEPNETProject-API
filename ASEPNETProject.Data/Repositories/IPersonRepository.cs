using ASEPNETProject.Data.Models;

namespace ASEPNETProject.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> CreatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<Person> GetPeopleAsync(int id);
        Task UpdatePersonAsync(Person person);
    }
}