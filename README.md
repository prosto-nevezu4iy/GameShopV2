# GameShopV2

This is ecommerce project which uses Monolith as Architectural pattern.
It has three bounded contexts, which are built using CQRS pattern.

## Features

- Catalog of products with filter by genre
- Basket functionality (add product, update quantity, remove it)
- Checkout
- Orders


## Roadmap

- Make this application loosely coupled

- Add Azure services

- Event sourcing for orders

- Add some sort of message bus for contexts for them to communicate with each other. 
This should remove dependencies between them.

- Monolith -> Microservices

- NoSQL for reads ?

