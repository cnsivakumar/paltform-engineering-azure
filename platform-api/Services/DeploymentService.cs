using PlatformApi.Models;
using PlatformApi.Repositories;

namespace PlatformApi.Services
{
    public class DeploymentService
    {
        private readonly DeploymentRepository _repository;

        public DeploymentService(DeploymentRepository repository)
        {
            _repository = repository;
        }

        public DeploymentRequest CreateDeployment(DeploymentRequest request)
        {
            request.Status = "Pending";
            request.CreatedAt = DateTime.UtcNow;
            _repository.Add(request);
            return request;
        }

        public IEnumerable<DeploymentRequest> GetAllRequests() => _repository.GetAll();
    }
}
