namespace ProjetoHexagonal.Commons.Persistence
{
    public interface ICommand : IDisposable
    {
        int Timeout { get; set; }

        ICommand AddParameter(string name, object value);

        void ExecuteNonQuery(int minimumNumberOfRowsAffected = 1);

        IReader ExecuteReader();

        TScalar ExecuteScalar<TScalar>();
    }
}
