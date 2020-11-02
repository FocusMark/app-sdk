using Microsoft.Extensions.DependencyInjection;

namespace FocusMark.SDK
{

    public class FocusMarkBuilder
    {
        public FocusMarkBuilder(IServiceCollection services) => this.ServiceRegistery = services;
        public IServiceCollection ServiceRegistery { get; }
    }
}
