﻿using BasketProject.Contracts.Entities;

namespace BasketProject.Contracts.Abstracts
{
    public interface IBasketService
    {
        Task TransferBasketAsync(string anonymousId, string userId);
        Task<Basket> AddItemToBasket(Guid userId, int productId, decimal price, byte quantity = 1);
        Task SetQuantities(int basketId, Dictionary<string, byte> quantities); // <<Result<Basket>>
        Task DeleteBasketAsync(int basketId);
    }
}
