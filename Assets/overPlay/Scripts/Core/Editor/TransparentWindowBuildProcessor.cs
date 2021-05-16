using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace TW
{
  class TransparentWindowBuildProcessor : IPreprocessBuildWithReport
  {
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report) => TransparentWindow.DisableFlipModelSwapchain();
  } 
}