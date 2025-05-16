using MediatR;
using Portfolio.Api.Domain.Data;
using Portfolio.Api.Domain.Entities;
using Portfolio.Api.Domain.Mediator;
using Portfolio.Api.Domain.Mediator.Messages;
using Portfolio.Api.Domain.Mediator.Notifications;
using static Portfolio.Api.Domain.Entities.Project;

namespace Portfolio.Api.Domain.Projects.Commands
{
    public class RegisterProjectCommand : Command
    {
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string GitHubURL { get; set; }
        public ICollection<string> Images { get; set; } = new List<string>();
    }
    public class RegisterProjectCommandHandler : CommandHandler
    {
        private readonly IUnitOfWork _uow;

        public RegisterProjectCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications) : base(mediator, notifications)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(RegisterProjectCommand command, CancellationToken cancellationToken)
        {
            try { 
                var container = "projects-images";
                var images =(
                    from item in command.Images
                    select new ProjectImages()
                    {
                        ImagePath = $"{container}/{Guid.NewGuid()}",
                    }
                ).ToList();
                var project = ProjectFactory.Create(command.Name, command.Subtitle, command.Description, command.GitHubURL, images);
            
                await _uow.ProjectRepository.CreateAsync(project, cancellationToken);
                await _uow.SaveAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _notifications.Handle(new DomainNotification("Error", "Falha ao registrar o Projeto."), cancellationToken);
                return false;
            }
            return true;
        }
    }
}
