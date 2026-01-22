using PlatformApi.Models;

namespace PlatformApi.Repositories
{
    public class DeploymentRepository
    {
        private readonly List<DeploymentRequest> _requests = new();

        public IEnumerable<DeploymentRequest> GetAll() => _requests;

        public void Add(DeploymentRequest request)
        {
            _requests.Add(request);
        }

        public DeploymentRequest? GetById(string id)
        {
            return _requests.FirstOrDefault(r => r.RequestId == id);
        }
    }
}
