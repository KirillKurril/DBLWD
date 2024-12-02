using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class ProductService : IProductService
    {
        DbService _dbService;
        IConfiguration _configuration;
        public ProductService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }
        public async Task<ResponseData<IEnumerable<Product>>> GetProductsCollection(int? page, int? itemsPerPage, int? categoryId, bool? includeCategory = false, bool? includeManufacturers = false, bool? includeSuppliers = false, bool? includePickupPoints = false)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<Product> products;
            Expression<Func<Product, bool>> predicate;

            if (categoryId == null)
                predicate = p => p.Id > startIndex && p.Id <= endIndex;
            else
                predicate = p => p.Id > startIndex && p.Id <= endIndex && p.CategoryId == categoryId;

            try
            {
                products = await _dbService.ProductTable.GetWithConditions(predicate);

                includeCategory = includeCategory ?? false;
                includeManufacturers = includeManufacturers ?? false;
                includeSuppliers = includeSuppliers ?? false;
                includePickupPoints = includePickupPoints ?? false;

                foreach (var product in products)
                {
                    if (includeCategory.Value)
                    {
                        product.Category = await _dbService.CategoryTable.GetById(product.CategoryId);
                    }
                    if (includeManufacturers.Value || includeSuppliers.Value)
                    {
                        Expression<Func<Supply, bool>> supplyPredicate = s => s.ProductId == product.Id;
                        var supplies = await _dbService.SupplyTable.GetWithConditions(supplyPredicate);

                        if (includeManufacturers.Value)
                        {
                            var manufacturers = new List<Manufacturer>();
                            foreach (var supply in supplies)
                            {
                                var manufacturer = await _dbService.ManufacturerTable.GetById(supply.ManufacturerId);
                                if (manufacturer != null && !manufacturers.Any(m => m.Id == manufacturer.Id))
                                {
                                    manufacturers.Add(manufacturer);
                                }
                            }
                            product.Manufacturers = manufacturers;
                        }

                        if (includeSuppliers.Value)
                        {
                            var suppliers = new List<Supplier>();
                            foreach (var supply in supplies)
                            {
                                var supplier = await _dbService.SupplierTable.GetById(supply.SupplierId);
                                if (supplier != null && !suppliers.Any(s => s.Id == supplier.Id))
                                {
                                    suppliers.Add(supplier);
                                }
                            }
                            product.Suppliers = suppliers;
                        }
                    }
                    if (includePickupPoints.Value)
                    {
                        Expression<Func<ProductPickupPoint, bool>> ppPredicate = pp => pp.ProductId == product.Id;
                        var productPickupPoints = await _dbService.ProductPickupPointTable.GetWithConditions(ppPredicate);
                        
                        var pickupPoints = new List<PickupPoint>();
                        foreach (var pp in productPickupPoints)
                        {
                            var pickupPoint = await _dbService.PickupPointTable.GetById(pp.PickupPointId);
                            if (pickupPoint != null)
                            {
                                pickupPoints.Add(pickupPoint);
                            }
                        }
                        product.PickupPoints = pickupPoints;
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Product>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Product>>(products);
        }
        public async Task<ResponseData<Product>> GetProductById(int id, bool? includeCategory = false, bool? includeManufacturers = false, bool? includeSuppliers = false, bool? includePickupPoints = false)
        {
            Product product;
            try
            {
                product = await _dbService.ProductTable.GetById(id);

                includeCategory = includeCategory ?? false;
                includeManufacturers = includeManufacturers ?? false;
                includeSuppliers = includeSuppliers ?? false;
                includePickupPoints = includePickupPoints ?? false;

                if (includeCategory.Value)
                {
                    product.Category = await _dbService.CategoryTable.GetById(product.CategoryId);
                }
                if (includeManufacturers.Value || includeSuppliers.Value)
                {
                    // Get supplies for this product
                    Expression<Func<Supply, bool>> supplyPredicate = s => s.ProductId == product.Id;
                    var supplies = await _dbService.SupplyTable.GetWithConditions(supplyPredicate);

                    if (includeManufacturers.Value)
                    {
                        var manufacturers = new List<Manufacturer>();
                        foreach (var supply in supplies)
                        {
                            var manufacturer = await _dbService.ManufacturerTable.GetById(supply.ManufacturerId);
                            if (manufacturer != null && !manufacturers.Any(m => m.Id == manufacturer.Id))
                            {
                                manufacturers.Add(manufacturer);
                            }
                        }
                        product.Manufacturers = manufacturers;
                    }

                    if (includeSuppliers.Value)
                    {
                        var suppliers = new List<Supplier>();
                        foreach (var supply in supplies)
                        {
                            var supplier = await _dbService.SupplierTable.GetById(supply.SupplierId);
                            if (supplier != null && !suppliers.Any(s => s.Id == supplier.Id))
                            {
                                suppliers.Add(supplier);
                            }
                        }
                        product.Suppliers = suppliers;
                    }
                }
                if (includePickupPoints.Value)
                {
                    // Get pickup points through the ProductPickupPoint junction table
                    Expression<Func<ProductPickupPoint, bool>> ppPredicate = pp => pp.ProductId == product.Id;
                    var productPickupPoints = await _dbService.ProductPickupPointTable.GetWithConditions(ppPredicate);
                    
                    var pickupPoints = new List<PickupPoint>();
                    foreach (var pp in productPickupPoints)
                    {
                        var pickupPoint = await _dbService.PickupPointTable.GetById(pp.PickupPointId);
                        if (pickupPoint != null)
                        {
                            pickupPoints.Add(pickupPoint);
                        }
                    }
                    product.PickupPoints = pickupPoints;
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<Product>(false, ex.Message);
            }

            return new ResponseData<Product>(product);
        }
        public async Task<ResponseData<bool>> AddProduct(Product product)
        {
            try
            {
                await _dbService.ProductTable.Add(product);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
        public async Task<ResponseData<bool>> UpdateProduct(Product product, int prevId)
        {
            try
            {
                await _dbService.ProductTable.Update(product, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
        public async Task<ResponseData<bool>> DeleteProduct(int Id)
        {
            try
            {
                await _dbService.ProductTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }

}
