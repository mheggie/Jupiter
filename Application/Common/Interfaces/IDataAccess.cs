
namespace Application.Common.Interfaces
{
    public interface IDataAccess
    {
        List<T> LoadData<T>(string sql, string connectionStringName);
        void SaveData(string sql, string connectionStringName);
    }
}