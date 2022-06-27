using System;

namespace Carts.Carts.InitializingCart;

public class CartInitialized
{
    public CartInitialized(Guid cartId, Guid clientId, CartStatus cartStatus)
    {
        CartId = cartId;
        ClientId = clientId;
        CartStatus = cartStatus;
    }

    public Guid CartId { get; }

    public Guid ClientId { get; }

    public CartStatus CartStatus { get; }

    public static CartInitialized Create(Guid cartId, Guid clientId, CartStatus cartStatus)
    {
        if (cartId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(cartId));
        if (clientId == Guid.Empty)
            throw new ArgumentOutOfRangeException(nameof(clientId));
        if (cartStatus == default)
            throw new ArgumentOutOfRangeException(nameof(cartStatus));

        return new CartInitialized(cartId, clientId, cartStatus);
    }
}
