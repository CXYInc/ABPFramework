using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    public class Test : IEntity<string>
    {
        public string Id { get; set; }

        public bool IsTransient()
        {
            return true;
        }
    }
}
