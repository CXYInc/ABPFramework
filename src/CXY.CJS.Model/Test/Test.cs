using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    public class Test : IEntity<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsTransient()
        {
            return false;
        }
    }
}
