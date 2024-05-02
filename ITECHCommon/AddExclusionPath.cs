using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PipeControl.Common
{
    public static class AddExclusionPath
    {
        public static void AddPath(string path)
        {
            //RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            try
            {
                Collection<PSObject> re;
                PowerShell ps = PowerShell.Create();
                ps.Runspace = runspace;
                ps.AddCommand("Add-MpPreference").AddParameter("ExclusionPath", path);
                re = ps.Invoke();
                ps.Dispose();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            finally
            {
                runspace.Close();
                runspace.Dispose();
            }
        }
    }
}
