using Plumbery.Domain.Interfaces.Infrastructure;
using Plumbery.Infrastructure.Data.Context;
using System.Web;

namespace Plumbery.Infrastructure.Data.Configuration {
    /// <summary>
    /// Class to handle initialisation of Context
    /// </summary>
    public class RepositoryManager : IRepositoryManager {
        public const string ContextHttp = "ContextHttp";

        /// <summary>
        /// Get Context
        /// </summary>
        public ContextBank Context {
            get {
                if (HttpContext.Current.Items[ContextHttp] == null)
                    HttpContext.Current.Items[ContextHttp] = new ContextBank();
                return HttpContext.Current.Items[ContextHttp] as ContextBank;
            }
        }
        /// <summary>
        /// Dispose Context
        /// </summary>
        public void Finalise() {
            if (HttpContext.Current.Items[ContextHttp] != null)
                (HttpContext.Current.Items[ContextHttp] as ContextBank).Dispose();
        }
    }
}
