using Microsoft.AspNetCore.Mvc;

namespace TasarYeriProject.PresantationLayer.ViewComponents.DashboardChart
{
    public class DashboardChartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
