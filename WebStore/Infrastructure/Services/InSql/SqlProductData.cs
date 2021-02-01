﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entites;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(b => b.Products);

        public IEnumerable<Section> GetSections() => _db.Sections.Include(s => s.Products);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products;

            if (Filter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.BrandId is { } brand_id)
                query = query.Where(product => product.BrandId == brand_id);

            return query;
        }

        public Section GetSection(int sectionId)
        {
            var section = _db.Sections.Where(s => s.Id == sectionId).FirstOrDefault();

            if (section is null)
                return new Section();

            return section;
        }

        public Brand GetBrand(int brandId)
        {
            var brand = _db.Brands.Where(s => s.Id == brandId)                
                .FirstOrDefault();

           // brand.Products = GetProducts(new ProductFilter { BrandId = brand.Id, SectionId = null }).ToList();
            if (brand is null)
                return new Brand();

            return brand;
        }

        public int AddBrand(Brand brand)
        {
            
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.Add(brand);
             
                _db.SaveChanges();             

                _db.Database.CommitTransaction();

                return brand.Id;
            }
            
        }

        public int UpdateBrand(Brand brand)
        {
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.Update(brand);

                _db.SaveChanges();

                _db.Database.CommitTransaction();

                return brand.Id;
            }
        }

        public void DeleteBrand(int Id)
        {
            using (_db.Database.BeginTransaction())
            {
                var brand = _db.Brands.Where(s => s.Id == Id)
                .FirstOrDefault();

                if (brand != null)
                {

                    _db.Brands.Remove(brand);

                    _db.SaveChanges();

                    _db.Database.CommitTransaction();
                }

            }
        }
    }

}