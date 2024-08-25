async function getAllCategory() {
    let url = "https://localhost:7277/api/Categories/api/categories/getall";
    let request = await fetch(url);
    let data = await request.json();
    let cards = document.getElementById("container");
  
  
    cards.innerHTML = '';
  
    data.forEach((category) => {
        cards.innerHTML += 
        `<div class="card" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title">${category.categoryName}</h5>
                <button type="button" class="btn btn-primary" data-category-id="${category.categoryId}">View Products</button>
            </div>
        </div>`;
    });
  

    document.querySelectorAll('.btn-primary').forEach(button => {
        button.addEventListener('click', async (event) => {
            let categoryId = event.target.getAttribute('data-category-id');
            await fetchAndDisplayProducts(categoryId);
        });
    });
}

async function fetchAndDisplayProducts(productId) {

    let url = `https://localhost:7277/api/Products/category/${productId}`;
    let request = await fetch(url);
    let data = await request.json();
    let productsContainer = document.getElementById("products-container");
  
 
    productsContainer.innerHTML = '';
 
    data.forEach((product) => {
        productsContainer.innerHTML += 
        `<div class="product-card" style="width: 18rem;">
            <div class="product-body">
                <h5 class="product-title">${product.productName}</h5>
                <p class="product-description">${product.description}</p>
                <p class="product-price">$${product.price}</p>
                <button type="button" class="btn btn-secondary" data-product-id="${product.productId}">View Details</button>
            </div>
        </div>`;
    });
}
  
document.querySelectorAll('.btn.btn-secondary').forEach(button => {
    button.addEventListener('click', async (event) => {
        let productId = event.target.getAttribute('data-product-id');
        await fetchAndDisplayProductDetails(productId);  
    });
});
    async function fetchAndDisplayProductDetails(productId) {

        let url = `https://localhost:7277/api/Products/category/${productId}`;
        let request = await fetch(url);
        let data = await request.json();
        let productsContainer = document.getElementById("products-container");
      
     productDetailsContainer.innerHTML = '';

     productDetailsContainer.innerHTML += 
        `<div class="product-details" style="width: 18rem;">
            <div class="product-body">
                <h5 class="product-title">${product.productName}</h5>
                <p class="product-description">${product.description}</p>
                <p class="product-price">$${product.price}</p>
            </div>
        </div>`;
}

getAllCategory();
