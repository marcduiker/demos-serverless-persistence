using System.Threading.Tasks;

namespace ServerlessPersistence
{
    public interface IHighScoreOperations
    {
        void Add(int points);
        void Reset();
    }
}
