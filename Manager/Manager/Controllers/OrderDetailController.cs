using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Manager.Controllers
{
    public class OrderDetailController : BaseController
    {
        public OrderDetailController(MyDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult OrderDetail(Guid id)
        {
            var result = _dbContext.OrderDetails.Where(x => x.OrderId == id).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var result = _dbContext.OrderDetails.Where(x=>x.OrderDetailId == id).First();
            return View(result);
        }
        [HttpPost]
        public IActionResult Update(OrderDetail detail)
        {
            var orderDetail = _dbContext.OrderDetails.Where(x=>x.OrderDetailId==detail.OrderDetailId).First();
            orderDetail.Quantity = detail.Quantity;
            orderDetail.Price = detail.Price;
            orderDetail.Description = detail.Description;
            orderDetail.Size = detail.Size;
            orderDetail.Color = detail.Color;
            orderDetail.Country = detail.Country;
            orderDetail.LinkImage = detail.LinkImage;
            orderDetail.PromotionalCode = detail.PromotionalCode;
            _dbContext.OrderDetails.Update(orderDetail);
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Order");
        }
        public IActionResult Export(Guid id)
        {

            ////Format Ctrl+A -> Home -> Format -> Column(with, height)

            var stream = new MemoryStream();
            var result = (from od in _dbContext.OrderDetails where od.OrderId == id select od).ToList();
            var filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\Manager\ExcelTemplate\RequestQuoteImportExcelTemplate.xlsx"));
            FileInfo existingFile = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                // đổ dữ liệu vào sheet
                int rowId = 2;
                int stt = 1;
                foreach (var row in result)
                {
                      
                    sheet.Cells[rowId, 1].Value = row.LinkImage;
                    sheet.Cells[rowId, 2].Value = row.Quantity;
                    sheet.Cells[rowId, 3].Value = row.Description;
                    sheet.Cells[rowId, 4].Value = row.Price;
                    sheet.Cells[rowId, 5].Value = row.Size;
                    sheet.Cells[rowId, 6].Value = row.Color;
                    sheet.Cells[rowId, 7].Value = row.Country;
                    sheet.Cells[rowId, 8].Value = row.PromotionalCode;
                    stt++;
                    rowId++;
                }
                stream = new MemoryStream(package.GetAsByteArray());
            }
            stream.Position = 0;
            var fileName = $"RequestQuoteImportExcelTemplate_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}
