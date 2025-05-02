using FluentValidation.Results;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces
{
    public interface IService<T>
    {
        public Task<ValidationResult> CreateAsync(T obj);

        public Task<List<T>> GetAllAsync();

        public Task<T> GetAsync(int id);

        public bool Exists(int id);

        public Task<bool> DeleteAsync(int id);

        public Task<ValidationResult> UpdateAsync(T obj);

    }
}
