using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto;

namespace WarehouseManagementController.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public DashboardModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { set; get; } = 1;

        [BindProperty]
        public Paginate<Receipt> Receipts { set; get; } = default!;

        [BindProperty]
        public Paginate<Customer> Customer { set; get; } = default!;

        [BindProperty]
        public DtoDashboardData DashboardData { set; get; } = new DtoDashboardData();

        private async Task GetBestSellerProductAsync()
        {
            var currentDateTime = DateTime.UtcNow.AddHours(7);
            DateTime firstDay = new DateTime(currentDateTime.Year, currentDateTime.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            var receiptsInMonth = await _unitOfWork.ReceiptRepository
                                    .Search(r => firstDay.Date <= r.CreatedDateTime.Value.AddHours(7).Date && r.CreatedDateTime.Value.AddHours(7).Date <= lastDay.Date)
                                    .Include(r => r.ReceiptDetails)
                                        .ThenInclude(d => d.Product)
                                    .ToListAsync();

            var checkDate = firstDay;
            var totalQuantityInMonthList = new List<int>();
            while (checkDate.Date <= lastDay.Date)
            {
                var receiptsInDay = receiptsInMonth.Where(r => r.CreatedDateTime.Value.AddHours(7).Date == checkDate.Date);

                var totalSelledInDay = receiptsInDay?.SelectMany(r => r.ReceiptDetails)?.Sum(d => d.Quantity);

                totalQuantityInMonthList.Add(totalSelledInDay ?? 0);

                checkDate = checkDate.AddDays(1);
            }
            DashboardData.TotalSelledByMonth = totalQuantityInMonthList;

            var receiptDetails = receiptsInMonth.SelectMany(Product => Product.ReceiptDetails).ToList();

            var bestSellerFindingList = new List<BestSellerProductDto>();

            foreach (var detail in receiptDetails)
            {
                var findProductById = bestSellerFindingList.FirstOrDefault(b => b.ProductId == detail.ProductId);

                if (findProductById != null)
                {
                    findProductById.TotalQuantity += detail.Quantity ?? 0;
                    findProductById.TotalPrice += detail.Price * detail.Quantity ?? 0;
                } else
                {
                    findProductById = new BestSellerProductDto();
                    findProductById.ProductId = detail.ProductId ?? 0;
                    findProductById.ProductName = detail.ProductName ?? "";
                    findProductById.TotalQuantity = detail.Quantity ?? 0;
                    findProductById.TotalPrice = detail.Price * detail.Quantity ?? 0;
                    bestSellerFindingList.Add(findProductById);
                }
            }

            var bestSeller = bestSellerFindingList.OrderByDescending(b => b.TotalQuantity).FirstOrDefault();
            DashboardData.BestSellerProduct = bestSeller;
        }

        private async Task GetTotalRevenueInMonthAsync()
        {
            var currentDateTime = DateTime.UtcNow.AddHours(7);
            DateTime firstDay = new DateTime(currentDateTime.Year, currentDateTime.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            var receiptsInMonth = await _unitOfWork.ReceiptRepository
                                    .Search(r => firstDay.Date <= r.CreatedDateTime.Value.AddHours(7).Date && r.CreatedDateTime.Value.AddHours(7).Date <= lastDay.Date)
                                    .Include(r => r.ReceiptDetails)
                                    .ToListAsync();
            var imports = await _unitOfWork.ImportRequestRepository
                                    .Search(r => firstDay.Date <= r.CreatedDateTime.Value.AddHours(7).Date && r.CreatedDateTime.Value.AddHours(7).Date <= lastDay.Date)
                                    .Include(r => r.ImportRequestDetails)
                                    .ToListAsync();

            var totalRevenue = receiptsInMonth?.SelectMany(r => r.ReceiptDetails)?.Sum(d => d.Quantity * d.Price) ?? 0;
            var totalImportPrice = imports?.SelectMany(i => i.ImportRequestDetails)?.Sum(d => d.Quantity * d.ImportPrice) ?? 0;
            
            DashboardData.TotalRevenue = totalRevenue;
            DashboardData.TotalImportPrice = totalImportPrice;
        }

        private List<int> GetNumberOfProductsFromDateRange(DateTime startDate, DateTime endDate, List<Receipt> receipts)
        {
            var currentDate = startDate;
            List<int> result = new List<int>();
            while (currentDate <= endDate)
            {
                var receiptsInDate = receipts.Where(r => r.CreatedDateTime.Value.Date == currentDate.Date);

                var productsSelled = receiptsInDate.SelectMany(r => r.ReceiptDetails);

                var numberOfTotalProductsSelled = productsSelled?.Sum(p => p.Quantity) ?? 0;

                result.Add(numberOfTotalProductsSelled);

                currentDate = currentDate.AddDays(1);
            }

            return result;
        }

        private async Task GetDataColumnCharts()
        {
            //vietnam datetime
            DateTime today = DateTime.UtcNow.AddHours(7);

            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = today.AddDays(-diff);

            DateTime endOfWeek = startOfWeek.AddDays(6);

            var receiptsInWeeks = await _unitOfWork.ReceiptRepository
                .Search(r => startOfWeek.Date <= r.CreatedDateTime.Value.AddHours(7).Date && r.CreatedDateTime.Value.AddHours(7).Date <= endOfWeek.Date)
                .Include(r => r.ReceiptDetails)
                .ToListAsync();

            var dataSelledProductsInWeeks = GetNumberOfProductsFromDateRange(startOfWeek, endOfWeek, receiptsInWeeks);

            DashboardData = new DtoDashboardData
            {
                TotalSelledData = dataSelledProductsInWeeks,
                TotalImportedData = new List<int>()
            };
        }

        private async Task SearchReceiptsAsync()
        {
            Receipts = await _unitOfWork.ReceiptRepository.GetPagingListAsync<Receipt>(
                selector: p => p,
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                include: i => i.Include(p => p.ReceiptDetails).ThenInclude(d => d.Product)
                .Include(p => p.CreatedByNavigation),
                page: PageIndex,
                size: 20
            );
        }

        private async Task SearchCustomersAsync()
        {
            Customer = await _unitOfWork.CustomerRepository.GetPagingListAsync<Customer>(
                selector: p => p,
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                page: PageIndex,
                size: 20
            );
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await SearchReceiptsAsync().ConfigureAwait(false);
            await SearchCustomersAsync().ConfigureAwait(false);
            await GetDataColumnCharts().ConfigureAwait(false);
            await GetTotalRevenueInMonthAsync().ConfigureAwait(false);
            await GetBestSellerProductAsync().ConfigureAwait(false);
            return Page();
        }
    }
}
