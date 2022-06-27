using System;
using Carts.Carts.Products;

namespace Carts.Carts.AddingProduct;

public class ProductAdded
{
    public ProductAdded(Guid cartId, PricedProductItem productItem)
    {
        CartId = cartId;
        ProductItem = productItem;
    }

    public Guid CartId { get; }

    public PricedProductItem ProductItem { get; }

    public static ProductAdded Create(Guid cartId, PricedProductItem productItem)
    {
        if (cartId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(cartId));

        return new ProductAdded(cartId, productItem);
    }
}
