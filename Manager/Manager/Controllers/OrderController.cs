using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.Web;
namespace Manager.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(MyDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult Index()
        {
            ViewBag.Order = (from order in _dbContext.Orders
                     join od in _dbContext.OrderDetails on order.OrderId equals od.OrderId
                     group od by new { order.OrderCode,order.Payment,order.Delivery, order.OrderId,order.Status,order.CreatedDate,order.UserName } into order
                     select new Order
                     {
                         OrderId = order.Key.OrderId,
                         Status = order.Key.Status,
                         OrderCode = order.Key.OrderCode,
                         UserName = order.Key.UserName,
                         Payment = order.Key.Payment,
                         Delivery = order.Key.Delivery,
                         CreatedDate = order.Key.CreatedDate,
                         TotalPrice = order.Sum(x=>x.Quantity*x.Price)
                     }).OrderByDescending(x=>x.CreatedDate).ToList();
            
            return View();
        }
        public IActionResult OrderDetail(Guid orderId)
        {
            var result = _dbContext.OrderDetails.Where(x=>x.OrderId==orderId).ToList();
            return View(result);
        }
        public IActionResult Export()
        {
            var stream = new MemoryStream();
            var filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\Manager\ExcelTemplate\RequestQuoteImportExcelTemplate.xlsx"));
            FileInfo existingFile = new FileInfo(filePath);           
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                // đổ dữ liệu vào sheet               
                stream = new MemoryStream(package.GetAsByteArray());
            }
            stream.Position = 0;
            var fileName = $"RequestQuoteImportExcelTemplate_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;                
                using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = GetLastUsedRow(worksheet);
                    int colCount = worksheet.Dimension.End.Column;
                    var order = new Order();
                    var userName = User.Identity.Name;
                    if (rowCount >= 1)
                    {
                        order.OrderId = Guid.NewGuid();
                        order.OrderCode = "DonHang-" + userName + '-' + Helper.RandomString(4);
                        order.Status = OrderStatus.InProgress;
                        order.Payment = "Chưa xử lý";
                        order.Delivery = "Chưa được vận chuyển";
                        order.UserName = userName;
                        order.CreatedDate = DateTime.Now;
                        _dbContext.Orders.Add(order);

                        for (int row = 1; row <= rowCount; row++)
                        {
                            var orderDetail = new OrderDetail();
                            orderDetail.OrderDetailId = Guid.NewGuid();
                            orderDetail.OrderId = order.OrderId;
                            for (int col = 1; col <= colCount; col++)
                            {
                                var cols = (worksheet.Cells[1, col].Value).ToString();
                                var value = worksheet.Cells[row + 1, col].Value == null ? "" : worksheet.Cells[row + 1, col].Value.ToString();
                                var number = value == "" ? "0" : value;
                                if (cols == "Đường link*")
                                {
                                    orderDetail.LinkImage = value;
                                }
                                else if (cols == "Số lượng*")
                                {
                                    orderDetail.Quantity = int.Parse(number);
                                }
                                else if (cols == "Mô tả*")
                                {
                                    orderDetail.Description = value;
                                }
                                else if (cols == "Giá web")
                                {
                                    orderDetail.Price = double.Parse(number);
                                }
                                else if (cols == "Size/loại")
                                {
                                    orderDetail.Size = value;
                                }
                                else if (cols == "Màu sắc")
                                {
                                    orderDetail.Color = value;
                                }
                                else if (cols == "Nước/Tuyến")
                                {
                                    orderDetail.Country = value;
                                }
                                else if (cols == "Mã khuyến mại")
                                {
                                    orderDetail.PromotionalCode = value;
                                }
                            }
                            _dbContext.OrderDetails.Add(orderDetail);
                        }
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        RedirectToAction("Index");
                    }
                   
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
        public int GetLastUsedRow(ExcelWorksheet sheet)
        {
            if (sheet.Dimension == null) { return 0; } // In case of a blank sheet
            var row = sheet.Dimension.End.Row;
            while (row >= 1)
            {
                var range = sheet.Cells[row, 1, row, sheet.Dimension.End.Column];
                if (range.Any(c => !string.IsNullOrEmpty(c.Text)))
                {
                    break;
                }
                row--;
            }
            return row - 1;
        }

    }

}

