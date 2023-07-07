using Shared.Core.Models;
using System.Diagnostics;

namespace Shared.Core.Interfaces
{
    public interface ISystemProcessService
    {
        SystemProcessOutput Execute(ProcessStartInfo processStartInfo);
    }
}