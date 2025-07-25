﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Base;
using WarehouseManagementService.Interface;

namespace WarehouseManagementService.Implement
{
    public class CategoryServices : ICategoryServices
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryServices()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> DeleteAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    var result = await _unitOfWork.CategoryRepository.RemoveAsync(category);
                    if (result)
                        return new BusinessResult(1, "success");
                    else
                        return new BusinessResult(0, "error");
                }
                return new BusinessResult(0, "no content");
            }
            catch (Exception)
            {

                return new BusinessResult(0, "Category has at least 1 course");
            }
        }

        public async Task<IBusinessResult> GetAll(int page, int size)
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var category = await _unitOfWork.CategoryRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => true,
                    page: page,
                    size: size
                    );
                if (category == null)
                {
                    return new BusinessResult(4, "No currency data");
                }
                else
                {
                    return new BusinessResult(1, "Get currency list success", category);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllCategories()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var category = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (category == null)
                {
                    return new BusinessResult(4, "No currency data");
                }
                else
                {
                    return new BusinessResult(1, "Get currency list success", category);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    return new BusinessResult(1, "Get course category successfully", category);
                }
                else
                {
                    return new BusinessResult(-1, "Get course category fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetByIdAsync(string id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.SingleOrDefaultAsync(
                    selector: x => x,
                    predicate: x => x.Id.ToString() == id
                    );
                if (category != null)
                {
                    return new BusinessResult(1, "Get product successfully", category);
                }
                else
                {
                    return new BusinessResult(-1, "Get product fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }


        public async Task<IBusinessResult> Save(Category category)
        {
            try
            {
                // Lấy CategoryId hiện tại cao nhất
                var latestCategory = _unitOfWork.CategoryRepository
                    .GetAll() // giả sử bạn có hàm GetAll trả về IQueryable<Category>
                    .Where(c => c.CategoryId != null)
                    .OrderByDescending(c => c.CategoryId)
                    .FirstOrDefault();

                int nextNumber = 1;

                if (latestCategory != null && !string.IsNullOrEmpty(latestCategory.CategoryId))
                {
                    var numericPart = Regex.Match(latestCategory.CategoryId, @"\d+").Value;
                    if (int.TryParse(numericPart, out int parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }

                category.CategoryId = $"CA{nextNumber.ToString("D5")}";
                category.CreatedDateTime = DateTime.Now;
                category.UpdatedDateTime = DateTime.Now;
                category.UpdatedBy = 2;
                category.CreatedBy = 2;
                category.Status = 1;

                int result = await _unitOfWork.CategoryRepository.CreateAsync(category);

                if (result > 0)
                {
                    return new BusinessResult(1, "success");
                }
                else
                {
                    return new BusinessResult(2, "fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }


        public async Task<IBusinessResult> Search(string searchTerm, int page, int size)
        {
            try
            {
                var productBrands = await _unitOfWork.CategoryRepository.GetPagingListAsync(
                    selector: x => x,
                    predicate: x => x.Name.Contains(searchTerm),
                    page: page,
                    size: size
                    );

                if (productBrands != null)
                {
                    return new BusinessResult(1, "Create successfully", productBrands);
                }
                else
                {
                    return new BusinessResult(1, "Create fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateAsync(Category category)
        {
            try
            {
                // Lấy category gốc từ DB
                var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(category.Id);
                if (existingCategory == null)
                {
                    return new BusinessResult(2, "Category not found");
                }

                // Chỉ cập nhật những thuộc tính cần thiết
                existingCategory.Name = category.Name;
                existingCategory.UpdatedDateTime = DateTime.Now;

                // Cập nhật
                int result = await _unitOfWork.CategoryRepository.UpdateAsync(existingCategory);
                if (result > 0)
                {
                    return new BusinessResult(1, "success");
                }
                else
                {
                    return new BusinessResult(2, "fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }

    }
}
