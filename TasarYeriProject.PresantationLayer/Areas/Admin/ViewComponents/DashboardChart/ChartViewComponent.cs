using Microsoft.AspNetCore.Mvc;

namespace TasarYeriProject.PresantationLayer.Areas.Admin.ViewComponents.DashboardChart
{
    public class ChartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
