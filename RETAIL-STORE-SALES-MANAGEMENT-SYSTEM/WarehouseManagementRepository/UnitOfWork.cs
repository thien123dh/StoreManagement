using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementRepository.Implement;

namespace WarehouseManagementRepository
{
    public class UnitOfWork
    {
        public virtual ProductRepository ProductRepository => new ProductRepository();
        public virtual UserRepository UserRepository => new UserRepository();
        public virtual CategoryRepository CategoryRepository => new CategoryRepository();
        public virtual ImportRequestRepository ImportRequestRepository => new ImportRequestRepository();
        public virtual ImportRequestDetailRepository ImportRequestDetailRepository => new ImportRequestDetailRepository();
        public virtual ReceiptRepository ReceiptRepository => new ReceiptRepository();
        public virtual ReceiptDetailRepository ReceiptDetailRepository => new ReceiptDetailRepository();
        public virtual CustomerRepository CustomerRepository => new CustomerRepository();



    }
}
