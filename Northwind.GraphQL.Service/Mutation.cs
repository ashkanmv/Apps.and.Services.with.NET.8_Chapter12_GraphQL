﻿using Northwind.EntityModels;

namespace Northwind.GraphQL.Service;
public record AddProductInput(
    string ProductName,
    int? SupplierId,
    int? CategoryId,
    string QuantityPerUnit,
    decimal? UnitPrice,
    short? UnitsInStock,
    short? UnitsOnOrder,
    short? ReorderLevel,
    bool Discontinued);
public class AddProductPayload
{
    public AddProductPayload(Product product)
    {
        Product = product;
    }
    public Product Product { get; }
}
public class Mutation
{
    public async Task<AddProductPayload> AddProductAsync(
        AddProductInput input, NorthwindContext db)
    {
        // This could be a good place to use a tool like AutoMapper,
        // but we will do the mapping between two objects manually.
        Product product = new()
        {
            ProductName = input.ProductName,
            SupplierId = input.SupplierId,
            CategoryId = input.CategoryId,
            QuantityPerUnit = input.QuantityPerUnit,
            UnitPrice = input.UnitPrice,
            UnitsInStock = input.UnitsInStock,
            UnitsOnOrder = input.UnitsOnOrder,
            ReorderLevel = input.ReorderLevel,
            Discontinued = input.Discontinued
        };
        db.Products.Add(product);
        int affectedRows = await db.SaveChangesAsync();
        // We could use affectedRows to return an error
        // or some other action if it is 0.
        return new AddProductPayload(product);
    }
}