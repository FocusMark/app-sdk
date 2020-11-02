using Microsoft.Extensions.DependencyInjection;

namespace Focusmark.SDK
{

    public class FocusMarkBuilder
    {
        public FocusMarkBuilder(IServiceCollection services) => this.ServiceRegistery = services;
        public IServiceCollection ServiceRegistery { get; }
    }
}
