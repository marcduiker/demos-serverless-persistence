using System.Threading.Tasks;

namespace ServerlessPersistence
{
    public interface IPlayerScore
    {
        void Add(int points);
        void Reset();
    }
}
