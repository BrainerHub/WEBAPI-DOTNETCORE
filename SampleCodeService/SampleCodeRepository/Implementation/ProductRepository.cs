using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PagedList;
using SampleCodeModel.DbEntities;
using SampleCodeModel.CustomModels;
using SampleCodeService.SampleCodeRepository.Interface;
using SampleCodeModel.ResponseModel;
using SampleCodeModel.RequestModel;
using SampleCodeModel.Helper;

namespace SampleCodeService.SampleCodeRepository.Implementation
{
    public class ProductRepository : IProductRepository
    {

        #region Initialization
        SampleContex _sampleContex;

        public ProductRepository(SampleContex sampleContex)
        {
            _sampleContex = sampleContex;
        }
        #endregion

        #region List
        public async Task<List<Product>> List(SearchRequestModel model)
        {
            List<Product> products = new List<Product>();

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                products = _sampleContex.Products.Where(s => s.Name.Contains(model.SearchText)
                               || s.Description.Contains(model.SearchText) || s.Price.Equals(model.SearchText) || s.Quantity.Equals(model.SearchText)).Include(x => x.ProductImages).ToPagedList(model.Page, model.PageSize).ToList();
            }
            else
            {
                products = _sampleContex.Products.Include(x => x.ProductImages).ToPagedList(model.Page, model.PageSize).ToList();
            }

            return products;
        }
        #endregion

        #region Create Product
        public async Task<ProductResponseModel> CreateProduct(ProductRequestModel model, List<string> fileName)
        {
            var product = await _sampleContex.Products.SingleOrDefaultAsync(x => x.Name == model.name);
            if (product != null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "Product Already Exists.");
            }
            product = new Product();
            product.Name = model.name;
            product.Description = model.description;
            product.Price = model.price;
            product.Quantity = model.quantity;
            product.StatusId = 1;

            if (fileName != null && fileName.Count > 0)
            {
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in fileName)
                {
                    ProductImage images = new ProductImage();

                    images.ImageUrl = item;
                    images.StatusId = 1;
                    productImages.Add(images);
                }
                product.ProductImages = productImages;
            }

            await _sampleContex.Products.AddAsync(product);
            await _sampleContex.SaveChangesAsync();

            return new ProductResponseModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                quantity = product.Quantity,
                price = product.Price,
            };
        }
        #endregion

        #region Update Product
        public async Task<ProductResponseModel> UpdateProduct(ProductUpdateRequestModel model, int productId, List<string> fileName)
        {
            var product = await _sampleContex.Products.SingleOrDefaultAsync(x => x.Name == model.name && x.ProductId != productId);
            if (product != null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "Product Already Exists.");
            }

            
            product = await _sampleContex.Products.SingleOrDefaultAsync(x => x.ProductId == productId);
            product.Name = model.name;
            product.Description = model.description;
            product.Price = model.price;
            product.Quantity = model.quantity;
            product.StatusId = 1;

            if (fileName != null && fileName.Count > 0)
            {
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in fileName)
                {
                    ProductImage images = new ProductImage();
                    images.ProductsId = productId;
                    images.ImageUrl = item;
                    images.StatusId = 1;
                    productImages.Add(images);
                }

                await _sampleContex.ProductImages.AddRangeAsync(productImages);
            }
            _sampleContex.Products.Update(product);
            if (model.DeleteImageId != null && model.DeleteImageId.Count > 0)
            {
                List<ProductImage> productImages = _sampleContex.ProductImages.Where(x => model.DeleteImageId.Contains(x.ProductImageId)).ToList();
                if (productImages != null && productImages.Count > 0)
                {
                    _sampleContex.ProductImages.RemoveRange(productImages);
                }
            }
            await _sampleContex.SaveChangesAsync();

            return new ProductResponseModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                quantity = product.Quantity,
                price = product.Price,
            };
        }
        #endregion

        #region Delete
        public async Task Delete(int productId)
        {
            var product = await _sampleContex.Products.SingleOrDefaultAsync(x => x.ProductId != productId);

            if (product == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "Product not found.");
            }
            _sampleContex.Products.Remove(product);
            await _sampleContex.SaveChangesAsync();
        }
        #endregion
    }
}
